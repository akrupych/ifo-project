using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IFOProject.Experimental;
using IFOProject.DataStructures;
using ZedGraph;
using IFOProject.Interfaces;

namespace IFOProject.Forms
{
    /// <summary>
    /// Allows user to setup approximation for pattern calculation
    /// </summary>
    public partial class CalculationSetupForm : Form
    {
        /// <summary>
        /// Receives this form result before closing
        /// </summary>
        internal IResultListener ResultListener { get; set; }

        /// <summary>
        /// Calculation results of the first row from current pattern
        /// </summary>
        RowCalculations SetupResults { get; set; }

        /// <summary>
        /// Initializes form, creates default initial setup and updates views
        /// </summary>
        /// <param name="package">True for package calculation, false otherwise</param>
        public CalculationSetupForm(IResultListener listener)
        {
            InitializeComponent();
            ResultListener = listener;
            dataGridView.Rows.Add(5);
            dataGridView.Rows[0].HeaderCell.Value = "Mean level";
            dataGridView.Rows[1].HeaderCell.Value = "Amplitude";
            dataGridView.Rows[2].HeaderCell.Value = "Period";
            dataGridView.Rows[3].HeaderCell.Value = "Initial phase";
            dataGridView.Rows[4].HeaderCell.Value = "RSS";
            // setup read-only fields style
            dataGridView.Rows[4].ReadOnly = true;
            dataGridView.Rows[4].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView.Columns[1].DefaultCellStyle.BackColor = Color.LightGray;
            // get initial results with default approximation
            SetupResults = Program.Package.CurrentPattern.CalculateFirstRow();
            RefreshView();
        }

        /// <summary>
        /// Recalculates setup and updates all visible results
        /// </summary>
        private void RefreshView()
        {
            // editable fields with get/set
            InitialSetup = SetupResults.Approximation;
            // read-only fields
            UpdateResults();
            // approximation, results and exact values plots
            UpdatePlot();
        }

        /// <summary>
        /// Gets or sets modifiable approximation values
        /// </summary>
        private Coefficients InitialSetup
        {
            get
            {
                // parse from textboxes
                return new Coefficients(new double[] {
                    double.Parse(dataGridView[0, 0].Value.ToString()),
                    double.Parse(dataGridView[0, 1].Value.ToString()),
                    double.Parse(dataGridView[0, 2].Value.ToString()),
                    double.Parse(dataGridView[0, 3].Value.ToString())
                });
            }
            set
            {
                // set to textboxes
                dataGridView[0, 0].Value = String.Format("{0:F3}", value.MeanLevel);
                dataGridView[0, 1].Value = String.Format("{0:F3}", value.Amplitude);
                dataGridView[0, 2].Value = String.Format("{0:F3}", value.Period);
                dataGridView[0, 3].Value = String.Format("{0:F3}", value.InitialPhase);
            }
        }

        /// <summary>
        /// Fills non-modifiable results textboxes with SetupResults
        /// </summary>
        private void UpdateResults()
        {
            dataGridView[1, 0].Value = String.Format("{0:F3}",
                SetupResults.Final.MeanLevel);
            dataGridView[1, 1].Value = String.Format("{0:F3}",
                SetupResults.Final.Amplitude);
            dataGridView[1, 2].Value = String.Format("{0:F3}",
                SetupResults.Final.Period);
            dataGridView[1, 3].Value = String.Format("{0:F3}",
                SetupResults.Final.InitialPhase);
            dataGridView[0, 4].Value = String.Format("{0:F3}",
                SetupResults.ResidualSquareSum(SetupResults.Approximation));
            dataGridView[1, 4].Value = String.Format("{0:F3}",
                SetupResults.ResidualSquareSum(SetupResults.Final));
        }

        /// <summary>
        /// Updates plot with exact values, approximation and results
        /// for the first row of the current pattern
        /// </summary>
        private void UpdatePlot()
        {
            // prepare data
            GraphPane plot = zedGraphControl.GraphPane;
            MyRectangle selection = Program.Package.CurrentPattern.Selection;

            // add exact values graph
            if (plot.CurveList.Count == 0)
            {
                // fill exact points list
                PointPairList exact = new PointPairList();
                for (int x = selection.Left; x < selection.Right; x++)
                    exact.Add(new PointPair(x, SetupResults.Data[x]));
                // show points list on a plot
                plot.AddCurve("Experimental points", exact, Color.Red,
                    SymbolType.Circle).Line.IsVisible = false;
            }
            // remove other graphs
            else plot.CurveList.RemoveRange(1, plot.CurveList.Count - 1);

            // recreate initial and final graphs
            PointPairList initial = new PointPairList();
            PointPairList final = new PointPairList();
            for (int x = selection.Left; x < selection.Right; x++)
            {
                initial.Add(new PointPair(x, RowCalculations.FunctionI(
                    SetupResults.Approximation, x)));
                final.Add(new PointPair(x, RowCalculations.FunctionI(
                    SetupResults.Final, x)));
            }
            // add them to a plot
            plot.AddCurve("Initial approximation", initial,
                Color.Green, SymbolType.None).Line.Width = 2;
            plot.AddCurve("Final result", final,
                Color.Blue, SymbolType.None).Line.Width = 3;

            // refresh view
            plot.AxisChange();
            zedGraphControl.Refresh();
        }

        /// <summary>
        /// Calculates results for first row of a current pattern with a given approximation.
        /// Then, table and plot views will be refreshed.
        /// </summary>
        private void buttonTry_Click(object sender, EventArgs e)
        {
            try
            {
                SetupResults = Program.Package.CurrentPattern.CalculateFirstRow(InitialSetup);
                RefreshView();
            }
            catch
            {
                MessageBox.Show("Invalid approximation values", "Error");
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            Close();
            if (ResultListener != null)
                ResultListener.ProcessResult(SetupResults.Final);
        }
    }
}
