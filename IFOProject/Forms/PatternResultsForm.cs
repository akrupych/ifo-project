using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using IFOProject.Experimental;
using IFOProject.DataStructures;

namespace IFOProject.Forms
{
    public partial class PatternResultsForm : Form
    {
        private RowCalculations[] results;
        private RowCalculations[] Results
        {
            get { return results; }
            set
            {
                results = value;
                dataGridViewResults.Rows.Clear();
                foreach (var row in Results)
                    dataGridViewResults.Rows.Add(row.Index,
                        String.Format("{0:F3}", row.Phase));
            }
        }

        public PatternResultsForm()
        {
            InitializeComponent();
            this.Text = Program.Package.CurrentPattern.Name;

            Results = Program.Package.CurrentPattern.Calculations;
            dataGridViewResults.Rows[0].Selected = true;

            SetupApproximationPlot();
            SetupPhasePlot();
            RefreshPhasePlot();
        }

        private void SetupApproximationPlot()
        {
            GraphPane pane = plotApproximation.GraphPane;

            pane.Title.Text = "Approximation plot";
            pane.XAxis.Title.Text = "X";
            pane.YAxis.Title.Text = "Intensity";

            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;
        }

        private void SetupPhasePlot()
        {
            GraphPane pane = plotPhase.GraphPane;

            pane.Title.Text = "Phase plot";
            pane.XAxis.Title.Text = "Y";
            pane.YAxis.Title.Text = "Phase";

            pane.XAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.IsVisible = true;
        }

        private void RefreshApproximationPlot(int index, bool showCustom)
        {
            Pattern pattern = Program.Package.CurrentPattern;
            GraphPane graphPane = plotApproximation.GraphPane;
            graphPane.CurveList.Clear();

            PointPairList final = new PointPairList();
            PointPairList first = new PointPairList();
            PointPairList exact = new PointPairList();

            if (showCustom)
            {
                double[] a = new double[4];
                if (double.TryParse(textBoxFixedComponent.Text, out a[0]) &&
                    double.TryParse(textBoxAmplitude.Text, out a[1]) &&
                    double.TryParse(textBoxPeriod.Text, out a[2]) &&
                    double.TryParse(textBoxInitialPhase.Text, out a[3]))
                {
                    Coefficients coefs = new Coefficients(a);
                    PointPairList custom = new PointPairList();
                    for (int x = pattern.Selection.Left; x < pattern.Selection.Right; x++)
                        custom.Add(new PointPair(x, RowCalculations.FunctionI(coefs, x)));
                    LineItem lineCustom = graphPane.AddCurve(
                        "Custom setup", custom, Color.Magenta, SymbolType.None);
                    lineCustom.Line.Width = 2;
                    lineCustom.Line.IsAntiAlias = true;
                }
                else MessageBox.Show("Invalid field value");
            }

            for (int x = pattern.Selection.Left; x < pattern.Selection.Right; x++)
            {
                final.Add(new PointPair(x, RowCalculations.FunctionI(Results[index].Final, x)));
                first.Add(new PointPair(x, RowCalculations.FunctionI(Results[index].Approximation, x)));
                exact.Add(new PointPair(x, Results[index].Data[x]));
            }

            LineItem points = graphPane.AddCurve("Experimental points",
                exact, Color.Red, SymbolType.Circle);
            points.Line.IsVisible = false;

            if (checkBoxShowInitial.Checked)
            {
                LineItem lineFirst = graphPane.AddCurve("Initial approximation",
                    first, Color.Green, SymbolType.None);
                lineFirst.Line.Width = 2;
                lineFirst.Line.IsAntiAlias = true;
            }

            LineItem lineFinal = graphPane.AddCurve("Final result",
                final, Color.Blue, SymbolType.None);
            lineFinal.Line.Width = 3;
            lineFinal.Line.IsAntiAlias = true;

            graphPane.AxisChange();
            plotApproximation.Invalidate();
        }

        private void RefreshPhasePlot()
        {
            GraphPane pane = plotPhase.GraphPane;
            pane.CurveList.Clear();
            PointPairList points = new PointPairList();
            foreach (RowCalculations row in Results)
                points.Add(row.Index, row.Phase);
            pane.AddCurve("Phase values", points, Color.Black, SymbolType.None);
            pane.AxisChange();
            plotPhase.Refresh();
        }

        private void dataGridViewResults_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            textBoxFixedComponent.Text = string.Format("{0:F3}", Results[index].Final.MeanLevel);
            textBoxAmplitude.Text = string.Format("{0:F3}", Results[index].Final.Amplitude);
            textBoxPeriod.Text = string.Format("{0:F3}", Results[index].Final.Period);
            textBoxInitialPhase.Text = string.Format("{0:F3}", Results[index].Final.InitialPhase);
            checkBoxShowOnPlot.Checked = false;
            RefreshApproximationPlot(index, false);
        }

        private void dataGridViewResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.C && e.Control)
            {
                DataObject data = dataGridViewResults.GetClipboardContent();
                Clipboard.SetDataObject(data, true);
            }
        }

        private void checkBoxShowInitial_CheckedChanged(object sender, EventArgs e)
        {
            RefreshApproximationPlot(dataGridViewResults.SelectedRows[0].Index, false);
        }

        private void checkBoxShowOnPlot_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridViewResults.SelectedRows.Count > 0)
                RefreshApproximationPlot(dataGridViewResults.SelectedRows[0].Index, checkBoxShowOnPlot.Checked);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                RefreshApproximationPlot(dataGridViewResults.SelectedRows[0].Index, true);
        }

        private void buttonRecalculate_Click(object sender, EventArgs e)
        {
            double[] a = new double[]
            {
                double.Parse(textBoxFixedComponent.Text),
                double.Parse(textBoxAmplitude.Text),
                double.Parse(textBoxPeriod.Text),
                double.Parse(textBoxInitialPhase.Text)
            };
            int index = dataGridViewResults.SelectedCells[0].RowIndex;
            Cursor.Current = Cursors.WaitCursor;
            Results = Program.Package.CurrentPattern.Calculate(index, new Coefficients(a));
            Cursor.Current = Cursors.Default;
            RefreshPhasePlot();
            dataGridViewResults.Rows[index].Selected = true;
        }
    }
}
