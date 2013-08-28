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
    public partial class PackageResultsForm : Form
    {
        private const string columnNameRow = "Row";
        private const string columnNameAverage = "Average";

        private const string packagePlotTitle = "Package Results";
        private const string packagePlotAxisXTitle = "Row Number";
        private const string packagePlotAxisYTitle = "Phase (angular degrees)";

        private const string patternPlotTitle = "Row Data Fitting";
        private const string patternPlotAxisXTitle = "X coordinate (pixels)";
        private const string patternPlotAxisYTitle = "Intensity (a.u.)";

        private const string graphTitleExact = "Experimental points";
        private const string graphTitleResult = "Fitting result";
        private const string graphTitleInitial = "Initial guess";
        private const string graphTitleCustom = "Manual guess";

        private const int averageLineWidth = 4;
        private const int patternLineWidth = 2;

        /// <summary>
        /// Curves colors
        /// </summary>
        private Color[] Colors =
        {
            Color.FromArgb(0, 0, 250),
            Color.FromArgb(0, 250, 0),
            Color.FromArgb(250, 0, 0),
            Color.FromArgb(0, 250, 250),
            Color.FromArgb(250, 250, 0),
            Color.FromArgb(250, 0, 250)
        };

        /// <summary>
        /// Gets or sets coefficients in TextBoxes
        /// </summary>
        public Coefficients CustomSetup
        {
            get
            {
                double[] values =
                {
                    double.Parse(textBoxMeanLevel.Text),
                    double.Parse(textBoxAmplitude.Text),
                    double.Parse(textBoxPeriod.Text),
                    double.Parse(textBoxInitialPhase.Text)
                };
                return new Coefficients(values);
            }

            set
            {
                textBoxMeanLevel.Text = string.Format("{0:F3}", value.MeanLevel);
                textBoxAmplitude.Text = string.Format("{0:F3}", value.Amplitude);
                textBoxPeriod.Text = string.Format("{0:F3}", value.Period);
                textBoxInitialPhase.Text = string.Format("{0:F3}", value.InitialPhase);
            }
        }

        /// <summary>
        /// Gets image selected in DataGridView
        /// </summary>
        private int Image { get { return dataGridView.CurrentCell.ColumnIndex - 1; } }

        /// <summary>
        /// Gets row index from DataGridView
        /// </summary>
        private int Row { get { return dataGridView.CurrentCell.RowIndex; } }

        /// <summary>
        /// Creates form with package calculation results
        /// </summary>
        public PackageResultsForm()
        {
            InitializeComponent();
            InitializeResultsTable();
            InitializePlots();
        }

        /// <summary>
        /// Loads data onto table and sets the default selection
        /// </summary>
        private void InitializeResultsTable()
        {
            dataGridView.DefaultCellStyle.NullValue = 0;
            LoadData();
            dataGridView.CurrentCell = dataGridView[1, 0];
        }

        /// <summary>
        /// Sets plots attributes and draws package plot
        /// </summary>
        private void InitializePlots()
        {
            packagePlot.GraphPane.Title.Text = packagePlotTitle;
            packagePlot.GraphPane.XAxis.Title.Text = packagePlotAxisXTitle;
            packagePlot.GraphPane.YAxis.Title.Text = packagePlotAxisYTitle;

            patternPlot.GraphPane.Title.Text = patternPlotTitle;
            patternPlot.GraphPane.XAxis.Title.Text = patternPlotAxisXTitle;
            patternPlot.GraphPane.YAxis.Title.Text = patternPlotAxisYTitle;

            DrawPackagePlot();
        }

        /// <summary>
        /// Loads package calculation results onto DataGridView
        /// </summary>
        private void LoadData()
        {
            int rowsCount = Program.Package.CurrentPattern.Calculations.Length;
            dataGridView.Columns.Clear();
            // row column
            dataGridView.Columns.Add(columnNameRow, columnNameRow);
            dataGridView.Columns[columnNameRow].ReadOnly = true;
            dataGridView.Rows.Add(rowsCount);
            for (int i = 0; i < rowsCount; i++)
                dataGridView[columnNameRow, i].Value =
                    Program.Package.CurrentPattern.Calculations[i].Index;
            // data columns
            for (int i = 0; i < Program.Package.Patterns.Count; i++)
            {
                Pattern pattern = Program.Package.Patterns[i];
                dataGridView.Columns.Add(pattern.Name, pattern.Name);
                for (int j = 0; j < rowsCount; j++)
                    dataGridView[i + 1, j].Value =
                        String.Format("{0:F3}", pattern.Calculations[j].Phase);
            }
            // add average column
            dataGridView.Columns.Add(columnNameAverage, columnNameAverage);
            dataGridView.Columns[columnNameAverage].ReadOnly = true;
            for (int i = 0; i < rowsCount; i++)
                dataGridView[columnNameAverage, i].Value = Program.Package.AveragePhase[i];
        }

        /// <summary>
        /// Draws package phase plot
        /// </summary>
        private void DrawPackagePlot()
        {
            packagePlot.GraphPane.CurveList.Clear();
            // add average graph
            PointPairList average = new PointPairList();
            foreach (DataGridViewRow row in dataGridView.Rows)
                average.Add(new PointPair(Convert.ToDouble(row.Cells[columnNameRow].Value),
                    Convert.ToDouble(row.Cells[columnNameAverage].Value)));
            packagePlot.GraphPane.AddCurve(columnNameAverage, average,
                Color.Black, SymbolType.None).Line.Width = averageLineWidth;
            // add patterns graphs
            for (int i = 0; i < Program.Package.Patterns.Count; i++)
            {
                PointPairList points = new PointPairList();
                Pattern pattern = Program.Package.Patterns[i];
                foreach (RowCalculations calc in pattern.Calculations)
                    points.Add(new PointPair(calc.Index, calc.Phase));
                LineItem line = packagePlot.GraphPane.AddCurve(pattern.Name, points,
                    Colors[i % Colors.Length], SymbolType.None);
                line.Line.Style = System.Drawing.Drawing2D.DashStyle.Dot;
                line.Line.Width = patternLineWidth;
            }
            // refresh view
            packagePlot.AxisChange();
            packagePlot.Refresh();
        }

        /// <summary>
        /// Draws pattern approximation plot
        /// </summary>
        private void DrawPatternPlot()
        {
            // accessed data
            RowCalculations result = Program.Package.Patterns[Image].Calculations[Row];
            MyRectangle selection = Program.Package.Patterns[Image].Selection;
            GraphPane graphPane = patternPlot.GraphPane;
            graphPane.CurveList.Clear();

            // required curves: experimantal and results
            PointPairList finalPoints = new PointPairList();
            PointPairList exactPoints = new PointPairList();
            for (int x = selection.Left; x <= selection.Right; x++)
            {
                finalPoints.Add(new PointPair(x, RowCalculations.FunctionI(result.Final, x)));
                exactPoints.Add(new PointPair(x, result.Data[x]));
            }
            LineItem points = graphPane.AddCurve(graphTitleExact,
                exactPoints, Color.Red, SymbolType.Circle);
            points.Line.IsVisible = false;
            LineItem lineFinal = graphPane.AddCurve(graphTitleResult,
                finalPoints, Color.Blue, SymbolType.None);
            lineFinal.Line.Width = 3;
            lineFinal.Line.IsAntiAlias = true;

            // initial approximation curve
            if (checkBoxShowInitial.Checked)
            {
                PointPairList initialPoints = new PointPairList();
                for (int x = selection.Left; x <= selection.Right; x++)
                    initialPoints.Add(new PointPair(x,
                        RowCalculations.FunctionI(result.Approximation, x)));
                LineItem lineFirst = graphPane.AddCurve(graphTitleInitial,
                    initialPoints, Color.Green, SymbolType.None);
                lineFirst.Line.Width = 2;
                lineFirst.Line.IsAntiAlias = true;
            }

            // custom setup curve
            if (checkBoxCustomSetup.Checked)
            {
                PointPairList customPoints = new PointPairList();
                for (int x = selection.Left; x <= selection.Right; x++)
                    customPoints.Add(new PointPair(x,
                        RowCalculations.FunctionI(CustomSetup, x)));
                LineItem lineCustom = graphPane.AddCurve(graphTitleCustom,
                    customPoints, Color.Magenta, SymbolType.None);
                lineCustom.Line.Width = 2;
                lineCustom.Line.IsAntiAlias = true;
            }

            // redraw graphs
            patternPlot.AxisChange();
            patternPlot.Refresh();
        }

        /// <summary>
        /// Selected row/image changed. Redraw pattern plot.
        /// </summary>
        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // column[0] is a row index, last column is average
            if (e.ColumnIndex != 0 && e.ColumnIndex != dataGridView.Columns.Count - 1)
            {
                CustomSetup = Program.Package.Patterns[Image].Calculations[Row].Final;
                DrawPatternPlot();
            }
        }

        /// <summary>
        /// Changed show/hide initial approximation on plot
        /// </summary>
        private void checkBoxShowInitial_CheckedChanged(object sender, EventArgs e)
        {
            DrawPatternPlot();
        }

        /// <summary>
        /// Changed show/hide custom setup on plot
        /// </summary>
        private void checkBoxCustomSetup_CheckedChanged(object sender, EventArgs e)
        {
            DrawPatternPlot();
        }

        /// <summary>
        /// When Enter pressed, custom setup should be recalculated and showed
        /// </summary>
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && checkBoxCustomSetup.Checked)
                DrawPatternPlot();
        }

        /// <summary>
        /// Recalculate pattern starting on selected row, or recalculate the whole package.
        /// Then, update data table and plots.
        /// </summary>
        private void buttonRecalculate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // when the first row changes, all the next patterns should be recalculated
            if (Row == 0) Program.Package.Calculate(Image, CustomSetup);
            else Program.Package.Patterns[Image].Calculate(Row, CustomSetup);
            LoadData();
            DrawPackagePlot();
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Save package results for later comparing
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Vertical coordinates dependence (*.vcd)|*.vcd";
            dialog.FileName = Program.Package.Name;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = dialog.FileName;
                if (!fileName.EndsWith(".vcd"))
                    fileName += ".vcd";
                Program.Package.SaveResults(fileName);
                MessageBox.Show("Saved to " + fileName, "Done");
            }
        }

        /// <summary>
        /// Show new form with phase difference
        /// </summary>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Vertical coordinates dependence (*.vcd)|*.vcd";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                new PhaseDifferenceForm(dialog.FileName).Show();
        }
    }
}
