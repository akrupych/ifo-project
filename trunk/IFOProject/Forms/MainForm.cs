using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using IFOProject.CustomControls;
using IFOProject.DataStructures;
using IFOProject.Experimental;
using IFOProject.Interfaces;
using System.Threading;

namespace IFOProject.Forms
{
    public partial class MainForm : Form, IProgressUpdater, IResultListener
    {
        //////////////////////////// PROPERTIES /////////////////////////////

        /// <summary>
        /// Image drawing control
        /// </summary>
        private ImageCanvas PatternImageView { get; set; }

        /// <summary>
        /// Row profile plot
        /// </summary>
        private ProfileCanvas RowProfileView { get; set; }

        /// <summary>
        /// Column profile plot
        /// </summary>
        private ProfileCanvas ColumnProfileView { get; set; }

        /// <summary>
        /// Thread for Calculate-All
        /// </summary>
        public Thread CalculationsThread { get; set; }

        /// <summary>
        /// Profiles position: X = column index, Y = row index
        /// </summary>
        public Location ProfilesPoint { get; set; }

        /// <summary>
        /// Editing selection corner
        /// </summary>
        public Corner FocusedInSelection { get; set; }

        /// <summary>
        /// True for package, false for pattern calculation
        /// </summary>
        public bool CalculatingPackage { get; set; }

        /// <summary>
        /// Checks if profiles checkbox selected
        /// </summary>
        public bool ProfilesSelected
        {
            get { return checkBoxProfiles.Checked; }
            set { checkBoxProfiles.Checked = value; }
        }

        /// <summary>
        /// Checks if smoothing checkbox selected
        /// </summary>
        private bool SmoothingSelected
        {
            get { return checkBoxUseSmoothed.Checked; }
        }

        /// <summary>
        /// Checks if selection checkbox selected
        /// </summary>
        public bool SelectionSelected 
        {
            get { return checkBoxShowSelection.Checked; }
        }

        /// <summary>
        /// Checks if package is empty
        /// </summary>
        private bool Empty
        {
            get { return Program.Package.Patterns.Count == 0; }
        }

        public bool PackageChanged
        {
            get
            {
                List<string> loaded = (List<string>)listBox1.DataSource;
                List<string> package = Program.Package.Patterns.Select(current => current.Name).ToList();
                return loaded == null || !loaded.SequenceEqual(package);
            }
        }

        ////////////////////////////// ACTIONS ///////////////////////////////

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            Program.Package = new Package();
            PatternImageView = new ImageCanvas(this, panelImage.ClientRectangle.Size);
            ColumnProfileView = new ProfileCanvas();
            RowProfileView = new ProfileCanvas();
            panelImage.Controls.Add(PatternImageView);
            panelColumnProfile.Controls.Add(ColumnProfileView);
            panelRowProfile.Controls.Add(RowProfileView);
            MaximumSize = MinimumSize = Size;
            ProfilesPoint = new Location();
            RefreshAll();
        }

        /// <summary>
        /// Adds image files to package
        /// </summary>
        private void AddPatterns()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                if (dialog.FileNames.Length == 1) Program.Package.Add(dialog.FileName);
                else Program.Package.Add(dialog.FileNames);
                RefreshAll();
                Cursor.Current = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Refreshes all visual info in thw window
        /// </summary>
        public void RefreshAll()
        {
            RefreshImage();
            RefreshPackageInfo();
            RefreshProfiles();
            RefreshSmoothing();
            RefreshCalculating();
        }

        /// <summary>
        /// Refresh pattern image
        /// </summary>
        public void RefreshImage()
        {
            if (Empty) PatternImageView.Image = null;
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                PatternImageView.Image = Program.Package.CurrentPattern.Bitmap;
                Cursor.Current = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Refresh package info
        /// </summary>
        public void RefreshPackageInfo()
        {
            if (Empty)
            {
                listBox1.DataSource = null;
                labelPatternIndex.Text = "Current: 0/0";
                labelWidth.Text = "Width: 0";
                labelHeight.Text = "Height: 0";
            }
            else
            {
                if (PackageChanged)
                    listBox1.DataSource =
                        Program.Package.Patterns.Select(current => current.Name).ToList();
                labelPatternIndex.Text = string.Format("Current: {0}/{1}",
                    Program.Package.CurrentIndex + 1, Program.Package.Patterns.Count);
                labelWidth.Text = string.Format("Width: {0}", Program.Package.CurrentPattern.Width);
                labelHeight.Text = string.Format("Height: {0}", Program.Package.CurrentPattern.Height);
            }
        }

        /// <summary>
        /// Refresh profiles view
        /// </summary>
        public void RefreshProfiles()
        {
            // enable or disable profiles
            if (Empty) groupBoxProfiles.Enabled = false;
            else groupBoxProfiles.Enabled = true;

            if (Empty && ProfilesSelected)
            {
                checkBoxProfiles.Checked = false;
            }
            else if (ProfilesSelected)
            {
                Pattern current = Program.Package.CurrentPattern;
                PatternImageView.Select();

                // draw profile views
                if (!ProfilesPoint.Exists) ProfilesPoint =
                    new Location(current.Width / 2, current.Height / 2);
                byte[] columnProfile = current.ColumnProfile(ProfilesPoint.X);
                byte[] rowProfile = current.RowProfile(ProfilesPoint.Y);
                ColumnProfileView.DrawProfile(columnProfile, false);
                panelColumnProfile.Invalidate();
                RowProfileView.DrawProfile(rowProfile, true);

                // show numeric values
                labelColumnProfile.Text = string.Format("X : {0}", ProfilesPoint.X);
                labelRowProfile.Text = string.Format("Y : {0}", ProfilesPoint.Y);
                labelIntensity1x1.Text = string.Format("1 x 1 : {0}",
                    current.AverageIntensity(ProfilesPoint, 0));
                labelIntensity3x3.Text = string.Format("3 x 3 : {0}",
                    current.AverageIntensity(ProfilesPoint, 1));
                labelIntensity5x5.Text = string.Format("5 x 5 : {0}",
                    current.AverageIntensity(ProfilesPoint, 2));
            }
            else
            {
                // hide profile views
                ColumnProfileView.Clear();
                RowProfileView.Clear();
                PatternImageView.Invalidate();

                // hide numeric values
                labelColumnProfile.Text = "X :";
                labelRowProfile.Text = "Y :";
                labelIntensity1x1.Text = "1 x 1 :";
                labelIntensity3x3.Text = "3 x 3 :";
                labelIntensity5x5.Text = "5 x 5 :";
            }
        }

        /// <summary>
        /// Refresh smoothing mode
        /// </summary>
        public void RefreshSmoothing()
        {
            if (Empty)
            {
                groupBoxSmoothing.Enabled = false;
                textBoxSmoothRadius.Text = "";
                checkBoxUseSmoothed.Checked = false;
            }
            else
            {
                groupBoxSmoothing.Enabled = true;
                Pattern current = Program.Package.CurrentPattern;
                textBoxSmoothRadius.Text = current.SmoothingRadius.ToString();
                checkBoxUseSmoothed.Checked = current.UseSmoothing;
            }
        }

        /// <summary>
        /// Refresh selection and rows step
        /// </summary>
        public void RefreshCalculating()
        {
            if (Empty)
            {
                groupBoxCalculate.Enabled = false;
                if (SelectionSelected)
                    checkBoxShowSelection.Checked = false;
            }
            else
            {
                groupBoxCalculate.Enabled = true;
                MyRectangle selection = Program.Package.CurrentPattern.Selection;
                textBoxLeft.Text = selection.Left.ToString();
                textBoxTop.Text = selection.Top.ToString();
                textBoxRight.Text = selection.Right.ToString();
                textBoxBottom.Text = selection.Bottom.ToString();
                textBoxRowsStep.Text = Program.Package.CurrentPattern.SelectionRowStep.ToString();
            }
        }

        /// <summary>
        /// Removes current pattern from package
        /// </summary>
        private void RemovePattern()
        {
            Program.Package.Remove();
            RefreshAll();
        }

        /// <summary>
        /// Removes all patterns from package
        /// </summary>
        private void ClearPackage()
        {
            Program.Package.Clear();
            RefreshAll();
        }

        /// <summary>
        /// Smoothes current image
        /// </summary>
        /// <param name="radius">Smoothing radius</param>
        private void SmoothImage(int radius)
        {
            if (SelectionSelected)
                Program.Package.CurrentPattern.SmoothSelection(radius);
            else Program.Package.CurrentPattern.Smooth(radius);
        }

        ///////////////////////////// CALLBACKS //////////////////////////////

        /// <summary>
        /// Add patterns button clicked
        /// </summary>
        private void buttonAddImages_Click(object sender, EventArgs e)
        {
            AddPatterns();
        }

        /// <summary>
        /// Removes current pattern from package
        /// </summary>
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            RemovePattern();
        }

        /// <summary>
        /// Clears package
        /// </summary>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearPackage();
        }

        /// <summary>
        /// Smooth image and show smoothed
        /// </summary>
        private void buttonSmooth_Click(object sender, EventArgs e)
        {
            if (Empty) return;
            int radius = 0;
            if (int.TryParse(textBoxSmoothRadius.Text, out radius))
            {
                Cursor.Current = Cursors.WaitCursor;
                SmoothImage(radius);
                Cursor.Current = Cursors.Default;
                checkBoxUseSmoothed.Checked = true;
                RefreshAll();
            }
            else
            {
                MessageBox.Show("Wrong value for smoothing radius", "Error");
            }
        }

        /// <summary>
        /// Show or hide profiles view
        /// </summary>
        private void checkBoxProfiles_CheckedChanged(object sender, EventArgs e)
        {
            RefreshProfiles();
        }

        /// <summary>
        /// Use smoothed image or not
        /// </summary>
        private void checkBoxUseSmoothed_CheckedChanged(object sender, EventArgs e)
        {
            if (Empty) return;
            Program.Package.CurrentPattern.UseSmoothing = checkBoxUseSmoothed.Checked;
            RefreshAll();
        }

        /// <summary>
        /// Change showing selection margin on image
        /// </summary>
        private void checkBoxShowSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (Empty) return;
            Program.Package.CurrentPattern.ShowSelection = checkBoxShowSelection.Checked;
            PatternImageView.Invalidate();
        }

        /// <summary>
        /// Handles Enter key for all 4 selection TextBoxes
        /// </summary>
        private void textBoxesModifySelection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                MyRectangle selection = Program.Package.CurrentPattern.Selection;
                int parsed = selection.Left;
                if (int.TryParse(((TextBox)sender).Text, out parsed))
                {
                    if (sender == textBoxLeft) selection.Left = parsed;
                    else if (sender == textBoxRight) selection.Right = parsed;
                    else if (sender == textBoxTop) selection.Top = parsed;
                    else if (sender == textBoxBottom) selection.Bottom = parsed;
                }
                checkBoxShowSelection.Checked = true;
                PatternImageView.Invalidate();
            }
        }

        /// <summary>
        /// Calculate current pattern
        /// </summary>
        private void buttonCalculateCurrent_Click(object sender, EventArgs e)
        {
            if (Empty) return;
            CalculatingPackage = false;
            new CalculationSetupForm(this).Show();
        }

        /// <summary>
        /// Calculate whole package
        /// </summary>
        private void buttonCalculateAll_Click(object sender, EventArgs e)
        {
            if (Empty) return;
            CalculatingPackage = true;
            new CalculationSetupForm(this).Show();
        }

        /// <summary>
        /// Stops Package calculation thread execution
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            CalculationsThread.Abort();
            progressBar.Value = 0;
            Application.UseWaitCursor = false;
        }

        /// <summary>
        /// Called from Package calculation thread after calculating each pattern
        /// </summary>
        /// <param name="percents">Calculated / total</param>
        public void SetProgress(int percents)
        {
            try
            {
                Invoke((MethodInvoker)delegate()
                {
                    progressBar.Value = percents;
                    if (percents == 100)
                    {
                        // calculations done; stop progress and show results form
                        progressBar.Value = 0;
                        new PackageResultsForm().Show();
                        Application.UseWaitCursor = false;
                    }
                });
            }
            catch { }
        }

        /// <summary>
        /// Called after finishing Calculation Setup Form
        /// </summary>
        /// <param name="coefficients">Custom setup</param>
        public void ProcessResult(Coefficients coefficients)
        {
            Cursor.Current = Cursors.WaitCursor;
            int step = 0;
            if (int.TryParse(textBoxRowsStep.Text, out step))
            {
                Program.Package.CurrentPattern.SelectionRowStep = step;
                if (CalculatingPackage)
                {
                    // start in new thread (updates will be sent to SetProgress)
                    CalculationsThread = new Thread(new ThreadStart(
                        delegate() { Program.Package.Calculate(this); }));
                    CalculationsThread.Start();
                    // Cursor.Current = Cursors.WaitCursor is not working.. MAGIC!
                    Application.UseWaitCursor = true;
                }
                else
                {
                    // calculate and show results form
                    Program.Package.CurrentPattern.Calculate(coefficients);
                    new PatternResultsForm().Show();
                    Cursor.Current = Cursors.Default;
                }
            }
            else MessageBox.Show("Wrong value for step number", "Error");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Package.CurrentIndex = listBox1.SelectedIndex;
            RefreshAll();
        }
    }
}
