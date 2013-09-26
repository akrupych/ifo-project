using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using IFOProject.Experimental;
using System.IO;

namespace IFOProject.Forms
{
    /// <summary>
    /// Displays phase difference between current and loaded packages
    /// </summary>
    public partial class PhaseDifferenceForm : Form
    {
        /// <summary>
        /// Array with phase difference
        /// </summary>
        private double[] PhaseDifference { get; set; }

        /// <summary>
        /// Absolute Y-coordinates
        /// </summary>
        private double[] Y { get; set; }

        /// <summary>
        /// First coefficient for y=Ax+B
        /// </summary>
        private double A { get; set; }

        /// <summary>
        /// Second coefficient for y=Ax+B
        /// </summary>
        private double B { get; set; }

        /// <summary>
        /// Saved package name
        /// </summary>
        public string PackageName1 { get; set; }

        /// <summary>
        /// Current package name
        /// </summary>
        public string PackageName2 { get; set; }

        /// <summary>
        /// Form is filled by itself
        /// </summary>
        public PhaseDifferenceForm(string fileName)
        {
            InitializeComponent();
            PackageName1 = Path.GetFileNameWithoutExtension(fileName);
            PackageName2 = Program.Package.Name;
            CalculatePhaseDifference(fileName);
            DoMagic();
        }

        private void DoMagic()
        {
            CalculateY();
            CalculateLinearRegression();
            SetupTopBar();
            SetupGraph();
        }

        /// <summary>
        /// Creates array of phase difference between current and saved packages
        /// </summary>
        private void CalculatePhaseDifference(string fileName)
        {
            double[] first = Package.LoadResults(fileName);
            double[] second = Program.Package.AveragePhase;
            PhaseDifference = new double[Math.Min(first.Length, second.Length)];
            for (int i = 0; i < PhaseDifference.Length; i++)
                PhaseDifference[i] = second[i] - first[i];
        }

        /// <summary>
        /// Creates array of absolute Y-coordinates for selected rows
        /// </summary>
        private void CalculateY()
        {
            RowCalculations[] rows = Program.Package.CurrentPattern.Calculations;
            int middle = (rows.First().Index + rows.Last().Index) / 2;
            Y = new double[PhaseDifference.Length];
            double verticalResolution = double.Parse(textBoxVerticalResolution.Text);
            for (int i = 0; i < Y.Length; i++)
                Y[i] = (rows[i].Index - middle) / verticalResolution;
        }

        /// <summary>
        /// Approximates PhaseDifference values with a line
        /// </summary>
        private void CalculateLinearRegression()
        {
            double[] y = PhaseDifference;
            double[] x = Y;
            int length = y.Length;
            double x_avg = x.Average();
            double y_avg = y.Average();
            double num = 0, den = 0;
            for (int i = 0; i < length; i++)
            {
                num += (x[i] - x_avg) * (y[i] - y_avg);
                den += (x[i] - x_avg) * (x[i] - x_avg);
            }
            A = num / den;
            B = y_avg - A * x_avg;
        }

        /// <summary>
        /// Returns line value assuming A and B are ready
        /// </summary>
        /// <param name="x">Function argument value</param>
        private double LinearFunction(double x)
        {
            return A * x + B;
        }

        /// <summary>
        /// Sets proximity TextBoxes value
        /// </summary>
        private void SetupTopBar()
        {
            double[] y = PhaseDifference;
            double[] x = Y;
            double y_avg = y.Average();
            int length = y.Length;
            double rss = 0, total = 0;
            for (int i = 0; i < length; i++)
            {
                rss += Math.Pow(y[i] - LinearFunction(x[i]), 2);
                total += Math.Pow(y[i] - y_avg, 2);
            }
            double cod = 1 - rss / total;
            textBoxRSS.Text = string.Format("{0:F3}", rss);
            textBoxCoD.Text = string.Format("{0:F3}", cod);
            textBoxSlope.Text = string.Format("{0:F3}", A);
            textBoxStandardError.Text = string.Format("{0:F3}", CalculateStandardError());
        }

        /// <summary>
        /// Map data to graph
        /// </summary>
        private void SetupGraph()
        {
            // initial setup
            GraphPane graph = resultsPlot.GraphPane;
            graph.CurveList.Clear();
            graph.Title.Text = string.Format("Phase difference ({0} - {1})",
                PackageName2, PackageName1);
            graph.YAxis.Title.Text = "Phase difference increment, deg";
            graph.XAxis.Title.Text = "Y, mm";
            // fill with data
            LineItem line = graph.AddCurve("Phase difference",
                Y, PhaseDifference, Color.Red, SymbolType.Circle);
            line.Line.IsVisible = false;
            line.Symbol.Size = 5;
            line.Symbol.Fill = new Fill(Color.Red);
            // show approximation
            double[] x = Y;
            double[] fit = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
                fit[i] = LinearFunction(x[i]);
            line = graph.AddCurve("Best fit", x, fit, Color.Blue, SymbolType.None);
            line.Line.Width = 2;
            line.Line.IsAntiAlias = true;
            // update
            resultsPlot.AxisChange();
            resultsPlot.Refresh();
        }

        /// <summary>
        /// Show POCs form
        /// </summary>
        private void buttonCalculatePOCsClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            new POCsForm(CalculateLoadingMassDifference(), A,
                CalculateStandardError()).Show();
        }

        private double CalculateStandardError()
        {
            // sqrt [ Σ(yi - ŷi)2 / (n - 2) ] / sqrt [ Σ(xi - x)2 ]
            double[] x = Y;
            double[] y = PhaseDifference;
            double x_avg = x.Average();
            double sumX = 0.0, sumY = 0.0;
            for (int i = 0; i < x.Length; i++)
            {
                sumX += Math.Pow(x[i] - x_avg, 2);
                sumY += Math.Pow(y[i] - LinearFunction(x[i]), 2);
            }
            return Math.Sqrt(sumY / (y.Length - 2)) / Math.Sqrt(sumX);
        }

        private double CalculateLoadingMassDifference()
        {
            double load1 = double.Parse(PackageName1.Substring(1, 2)) / 10.0;
            double load2 = double.Parse(PackageName2.Substring(1, 2)) / 10.0;
            return load2 - load1;
        }

        private void textBoxVerticalResolution_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return) DoMagic();
        }
    }
}
