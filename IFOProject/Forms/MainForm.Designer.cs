namespace IFOProject.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonAddImages = new System.Windows.Forms.Button();
            this.panelImage = new System.Windows.Forms.Panel();
            this.panelRowProfile = new System.Windows.Forms.Panel();
            this.panelColumnProfile = new System.Windows.Forms.Panel();
            this.labelPatternIndex = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBoxPackage = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBoxProfiles = new System.Windows.Forms.GroupBox();
            this.checkBoxProfiles = new System.Windows.Forms.CheckBox();
            this.labelIntensity1x1 = new System.Windows.Forms.Label();
            this.labelColumnProfile = new System.Windows.Forms.Label();
            this.labelIntensity5x5 = new System.Windows.Forms.Label();
            this.labelIntensity3x3 = new System.Windows.Forms.Label();
            this.labelRowProfile = new System.Windows.Forms.Label();
            this.labelAverageIntensity = new System.Windows.Forms.Label();
            this.groupBoxSmoothing = new System.Windows.Forms.GroupBox();
            this.checkBoxUseSmoothed = new System.Windows.Forms.CheckBox();
            this.buttonSmooth = new System.Windows.Forms.Button();
            this.textBoxSmoothRadius = new System.Windows.Forms.TextBox();
            this.labelRadius = new System.Windows.Forms.Label();
            this.groupBoxCalculate = new System.Windows.Forms.GroupBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textBoxBottom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxLeft = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxRight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCalculateAll = new System.Windows.Forms.Button();
            this.buttonCalculateCurrent = new System.Windows.Forms.Button();
            this.textBoxTop = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRowsStep = new System.Windows.Forms.TextBox();
            this.labelRowsStep = new System.Windows.Forms.Label();
            this.checkBoxShowSelection = new System.Windows.Forms.CheckBox();
            this.groupBoxPackage.SuspendLayout();
            this.groupBoxProfiles.SuspendLayout();
            this.groupBoxSmoothing.SuspendLayout();
            this.groupBoxCalculate.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAddImages
            // 
            this.buttonAddImages.Location = new System.Drawing.Point(6, 19);
            this.buttonAddImages.Name = "buttonAddImages";
            this.buttonAddImages.Size = new System.Drawing.Size(147, 23);
            this.buttonAddImages.TabIndex = 0;
            this.buttonAddImages.Text = "Add images";
            this.buttonAddImages.UseVisualStyleBackColor = true;
            this.buttonAddImages.Click += new System.EventHandler(this.buttonAddImages_Click);
            // 
            // panelImage
            // 
            this.panelImage.BackColor = System.Drawing.SystemColors.Window;
            this.panelImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelImage.Location = new System.Drawing.Point(178, 12);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(604, 504);
            this.panelImage.TabIndex = 18;
            // 
            // panelRowProfile
            // 
            this.panelRowProfile.BackColor = System.Drawing.SystemColors.Window;
            this.panelRowProfile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelRowProfile.Location = new System.Drawing.Point(178, 522);
            this.panelRowProfile.Name = "panelRowProfile";
            this.panelRowProfile.Size = new System.Drawing.Size(604, 132);
            this.panelRowProfile.TabIndex = 20;
            // 
            // panelColumnProfile
            // 
            this.panelColumnProfile.BackColor = System.Drawing.SystemColors.Window;
            this.panelColumnProfile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelColumnProfile.Location = new System.Drawing.Point(788, 12);
            this.panelColumnProfile.Name = "panelColumnProfile";
            this.panelColumnProfile.Size = new System.Drawing.Size(132, 504);
            this.panelColumnProfile.TabIndex = 19;
            // 
            // labelPatternIndex
            // 
            this.labelPatternIndex.AutoSize = true;
            this.labelPatternIndex.Location = new System.Drawing.Point(2, 124);
            this.labelPatternIndex.Name = "labelPatternIndex";
            this.labelPatternIndex.Size = new System.Drawing.Size(67, 13);
            this.labelPatternIndex.TabIndex = 7;
            this.labelPatternIndex.Text = "Current : 0/0";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(2, 144);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(50, 13);
            this.labelWidth.TabIndex = 8;
            this.labelWidth.Text = "Width : 0";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(2, 162);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(53, 13);
            this.labelHeight.TabIndex = 9;
            this.labelHeight.Text = "Height : 0";
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(87, 124);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(66, 23);
            this.buttonRemove.TabIndex = 4;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(87, 152);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(66, 23);
            this.buttonClear.TabIndex = 5;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // groupBoxPackage
            // 
            this.groupBoxPackage.Controls.Add(this.listBox1);
            this.groupBoxPackage.Controls.Add(this.buttonAddImages);
            this.groupBoxPackage.Controls.Add(this.buttonClear);
            this.groupBoxPackage.Controls.Add(this.buttonRemove);
            this.groupBoxPackage.Controls.Add(this.labelHeight);
            this.groupBoxPackage.Controls.Add(this.labelWidth);
            this.groupBoxPackage.Controls.Add(this.labelPatternIndex);
            this.groupBoxPackage.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPackage.Name = "groupBoxPackage";
            this.groupBoxPackage.Size = new System.Drawing.Size(160, 184);
            this.groupBoxPackage.TabIndex = 12;
            this.groupBoxPackage.TabStop = false;
            this.groupBoxPackage.Text = "Package";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 49);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(147, 69);
            this.listBox1.TabIndex = 10;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBoxProfiles
            // 
            this.groupBoxProfiles.Controls.Add(this.checkBoxProfiles);
            this.groupBoxProfiles.Controls.Add(this.labelIntensity1x1);
            this.groupBoxProfiles.Controls.Add(this.labelColumnProfile);
            this.groupBoxProfiles.Controls.Add(this.labelIntensity5x5);
            this.groupBoxProfiles.Controls.Add(this.labelIntensity3x3);
            this.groupBoxProfiles.Controls.Add(this.labelRowProfile);
            this.groupBoxProfiles.Controls.Add(this.labelAverageIntensity);
            this.groupBoxProfiles.Location = new System.Drawing.Point(12, 202);
            this.groupBoxProfiles.Name = "groupBoxProfiles";
            this.groupBoxProfiles.Size = new System.Drawing.Size(160, 116);
            this.groupBoxProfiles.TabIndex = 13;
            this.groupBoxProfiles.TabStop = false;
            this.groupBoxProfiles.Text = "Profiles";
            // 
            // checkBoxProfiles
            // 
            this.checkBoxProfiles.AutoSize = true;
            this.checkBoxProfiles.Location = new System.Drawing.Point(7, 20);
            this.checkBoxProfiles.Name = "checkBoxProfiles";
            this.checkBoxProfiles.Size = new System.Drawing.Size(89, 17);
            this.checkBoxProfiles.TabIndex = 6;
            this.checkBoxProfiles.Text = "Show profiles";
            this.checkBoxProfiles.UseVisualStyleBackColor = true;
            this.checkBoxProfiles.CheckedChanged += new System.EventHandler(this.checkBoxProfiles_CheckedChanged);
            // 
            // labelIntensity1x1
            // 
            this.labelIntensity1x1.AutoSize = true;
            this.labelIntensity1x1.Location = new System.Drawing.Point(5, 70);
            this.labelIntensity1x1.Name = "labelIntensity1x1";
            this.labelIntensity1x1.Size = new System.Drawing.Size(36, 13);
            this.labelIntensity1x1.TabIndex = 32;
            this.labelIntensity1x1.Text = "1 x 1 :";
            // 
            // labelColumnProfile
            // 
            this.labelColumnProfile.AutoSize = true;
            this.labelColumnProfile.Location = new System.Drawing.Point(6, 40);
            this.labelColumnProfile.Name = "labelColumnProfile";
            this.labelColumnProfile.Size = new System.Drawing.Size(20, 13);
            this.labelColumnProfile.TabIndex = 29;
            this.labelColumnProfile.Text = "X :";
            // 
            // labelIntensity5x5
            // 
            this.labelIntensity5x5.AutoSize = true;
            this.labelIntensity5x5.Location = new System.Drawing.Point(5, 96);
            this.labelIntensity5x5.Name = "labelIntensity5x5";
            this.labelIntensity5x5.Size = new System.Drawing.Size(36, 13);
            this.labelIntensity5x5.TabIndex = 34;
            this.labelIntensity5x5.Text = "5 x 5 :";
            // 
            // labelIntensity3x3
            // 
            this.labelIntensity3x3.AutoSize = true;
            this.labelIntensity3x3.Location = new System.Drawing.Point(5, 83);
            this.labelIntensity3x3.Name = "labelIntensity3x3";
            this.labelIntensity3x3.Size = new System.Drawing.Size(36, 13);
            this.labelIntensity3x3.TabIndex = 33;
            this.labelIntensity3x3.Text = "3 x 3 :";
            // 
            // labelRowProfile
            // 
            this.labelRowProfile.AutoSize = true;
            this.labelRowProfile.Location = new System.Drawing.Point(76, 40);
            this.labelRowProfile.Name = "labelRowProfile";
            this.labelRowProfile.Size = new System.Drawing.Size(20, 13);
            this.labelRowProfile.TabIndex = 30;
            this.labelRowProfile.Text = "Y :";
            // 
            // labelAverageIntensity
            // 
            this.labelAverageIntensity.AutoSize = true;
            this.labelAverageIntensity.Location = new System.Drawing.Point(5, 57);
            this.labelAverageIntensity.Name = "labelAverageIntensity";
            this.labelAverageIntensity.Size = new System.Drawing.Size(94, 13);
            this.labelAverageIntensity.TabIndex = 31;
            this.labelAverageIntensity.Text = "Average intensity :";
            // 
            // groupBoxSmoothing
            // 
            this.groupBoxSmoothing.Controls.Add(this.checkBoxUseSmoothed);
            this.groupBoxSmoothing.Controls.Add(this.buttonSmooth);
            this.groupBoxSmoothing.Controls.Add(this.textBoxSmoothRadius);
            this.groupBoxSmoothing.Controls.Add(this.labelRadius);
            this.groupBoxSmoothing.Location = new System.Drawing.Point(12, 324);
            this.groupBoxSmoothing.Name = "groupBoxSmoothing";
            this.groupBoxSmoothing.Size = new System.Drawing.Size(160, 94);
            this.groupBoxSmoothing.TabIndex = 14;
            this.groupBoxSmoothing.TabStop = false;
            this.groupBoxSmoothing.Text = "Smoothing";
            // 
            // checkBoxUseSmoothed
            // 
            this.checkBoxUseSmoothed.AutoSize = true;
            this.checkBoxUseSmoothed.Location = new System.Drawing.Point(7, 73);
            this.checkBoxUseSmoothed.Name = "checkBoxUseSmoothed";
            this.checkBoxUseSmoothed.Size = new System.Drawing.Size(94, 17);
            this.checkBoxUseSmoothed.TabIndex = 9;
            this.checkBoxUseSmoothed.Text = "Use smoothed";
            this.checkBoxUseSmoothed.UseVisualStyleBackColor = true;
            this.checkBoxUseSmoothed.CheckedChanged += new System.EventHandler(this.checkBoxUseSmoothed_CheckedChanged);
            // 
            // buttonSmooth
            // 
            this.buttonSmooth.Location = new System.Drawing.Point(6, 44);
            this.buttonSmooth.Name = "buttonSmooth";
            this.buttonSmooth.Size = new System.Drawing.Size(147, 23);
            this.buttonSmooth.TabIndex = 8;
            this.buttonSmooth.Text = "Smooth";
            this.buttonSmooth.UseVisualStyleBackColor = true;
            this.buttonSmooth.Click += new System.EventHandler(this.buttonSmooth_Click);
            // 
            // textBoxSmoothRadius
            // 
            this.textBoxSmoothRadius.Location = new System.Drawing.Point(107, 17);
            this.textBoxSmoothRadius.Name = "textBoxSmoothRadius";
            this.textBoxSmoothRadius.Size = new System.Drawing.Size(47, 20);
            this.textBoxSmoothRadius.TabIndex = 7;
            // 
            // labelRadius
            // 
            this.labelRadius.AutoSize = true;
            this.labelRadius.Location = new System.Drawing.Point(7, 20);
            this.labelRadius.Name = "labelRadius";
            this.labelRadius.Size = new System.Drawing.Size(94, 13);
            this.labelRadius.TabIndex = 0;
            this.labelRadius.Text = "Smoothing radius :";
            // 
            // groupBoxCalculate
            // 
            this.groupBoxCalculate.Controls.Add(this.buttonCancel);
            this.groupBoxCalculate.Controls.Add(this.progressBar);
            this.groupBoxCalculate.Controls.Add(this.textBoxBottom);
            this.groupBoxCalculate.Controls.Add(this.label4);
            this.groupBoxCalculate.Controls.Add(this.textBoxLeft);
            this.groupBoxCalculate.Controls.Add(this.label3);
            this.groupBoxCalculate.Controls.Add(this.textBoxRight);
            this.groupBoxCalculate.Controls.Add(this.label1);
            this.groupBoxCalculate.Controls.Add(this.buttonCalculateAll);
            this.groupBoxCalculate.Controls.Add(this.buttonCalculateCurrent);
            this.groupBoxCalculate.Controls.Add(this.textBoxTop);
            this.groupBoxCalculate.Controls.Add(this.label2);
            this.groupBoxCalculate.Controls.Add(this.textBoxRowsStep);
            this.groupBoxCalculate.Controls.Add(this.labelRowsStep);
            this.groupBoxCalculate.Controls.Add(this.checkBoxShowSelection);
            this.groupBoxCalculate.Location = new System.Drawing.Point(12, 420);
            this.groupBoxCalculate.Name = "groupBoxCalculate";
            this.groupBoxCalculate.Size = new System.Drawing.Size(160, 230);
            this.groupBoxCalculate.TabIndex = 15;
            this.groupBoxCalculate.TabStop = false;
            this.groupBoxCalculate.Text = "Calculate";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(5, 201);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(147, 23);
            this.buttonCancel.TabIndex = 49;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(7, 172);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(145, 23);
            this.progressBar.TabIndex = 48;
            // 
            // textBoxBottom
            // 
            this.textBoxBottom.Location = new System.Drawing.Point(52, 62);
            this.textBoxBottom.Name = "textBoxBottom";
            this.textBoxBottom.Size = new System.Drawing.Size(25, 20);
            this.textBoxBottom.TabIndex = 13;
            this.textBoxBottom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxesModifySelection_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Rows step :";
            // 
            // textBoxLeft
            // 
            this.textBoxLeft.Location = new System.Drawing.Point(127, 36);
            this.textBoxLeft.Name = "textBoxLeft";
            this.textBoxLeft.Size = new System.Drawing.Size(25, 20);
            this.textBoxLeft.TabIndex = 12;
            this.textBoxLeft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxesModifySelection_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Left:";
            // 
            // textBoxRight
            // 
            this.textBoxRight.Location = new System.Drawing.Point(127, 62);
            this.textBoxRight.Name = "textBoxRight";
            this.textBoxRight.Size = new System.Drawing.Size(25, 20);
            this.textBoxRight.TabIndex = 14;
            this.textBoxRight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxesModifySelection_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Right:";
            // 
            // buttonCalculateAll
            // 
            this.buttonCalculateAll.Location = new System.Drawing.Point(5, 143);
            this.buttonCalculateAll.Name = "buttonCalculateAll";
            this.buttonCalculateAll.Size = new System.Drawing.Size(149, 23);
            this.buttonCalculateAll.TabIndex = 17;
            this.buttonCalculateAll.Text = "Calculate all";
            this.buttonCalculateAll.UseVisualStyleBackColor = true;
            this.buttonCalculateAll.Click += new System.EventHandler(this.buttonCalculateAll_Click);
            // 
            // buttonCalculateCurrent
            // 
            this.buttonCalculateCurrent.Location = new System.Drawing.Point(5, 114);
            this.buttonCalculateCurrent.Name = "buttonCalculateCurrent";
            this.buttonCalculateCurrent.Size = new System.Drawing.Size(148, 23);
            this.buttonCalculateCurrent.TabIndex = 16;
            this.buttonCalculateCurrent.Text = "Calculate current";
            this.buttonCalculateCurrent.UseVisualStyleBackColor = true;
            this.buttonCalculateCurrent.Click += new System.EventHandler(this.buttonCalculateCurrent_Click);
            // 
            // textBoxTop
            // 
            this.textBoxTop.Location = new System.Drawing.Point(52, 36);
            this.textBoxTop.Name = "textBoxTop";
            this.textBoxTop.Size = new System.Drawing.Size(25, 20);
            this.textBoxTop.TabIndex = 11;
            this.textBoxTop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxesModifySelection_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Top:";
            // 
            // textBoxRowsStep
            // 
            this.textBoxRowsStep.Location = new System.Drawing.Point(89, 88);
            this.textBoxRowsStep.Name = "textBoxRowsStep";
            this.textBoxRowsStep.Size = new System.Drawing.Size(63, 20);
            this.textBoxRowsStep.TabIndex = 15;
            this.textBoxRowsStep.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxesModifySelection_KeyDown);
            // 
            // labelRowsStep
            // 
            this.labelRowsStep.AutoSize = true;
            this.labelRowsStep.Location = new System.Drawing.Point(3, 65);
            this.labelRowsStep.Name = "labelRowsStep";
            this.labelRowsStep.Size = new System.Drawing.Size(43, 13);
            this.labelRowsStep.TabIndex = 37;
            this.labelRowsStep.Text = "Bottom:";
            // 
            // checkBoxShowSelection
            // 
            this.checkBoxShowSelection.AutoSize = true;
            this.checkBoxShowSelection.Location = new System.Drawing.Point(6, 19);
            this.checkBoxShowSelection.Name = "checkBoxShowSelection";
            this.checkBoxShowSelection.Size = new System.Drawing.Size(98, 17);
            this.checkBoxShowSelection.TabIndex = 10;
            this.checkBoxShowSelection.Text = "Show selection";
            this.checkBoxShowSelection.UseVisualStyleBackColor = true;
            this.checkBoxShowSelection.CheckedChanged += new System.EventHandler(this.checkBoxShowSelection_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 662);
            this.Controls.Add(this.groupBoxCalculate);
            this.Controls.Add(this.groupBoxSmoothing);
            this.Controls.Add(this.groupBoxProfiles);
            this.Controls.Add(this.groupBoxPackage);
            this.Controls.Add(this.panelRowProfile);
            this.Controls.Add(this.panelColumnProfile);
            this.Controls.Add(this.panelImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interferometry with Four-point bending";
            this.groupBoxPackage.ResumeLayout(false);
            this.groupBoxPackage.PerformLayout();
            this.groupBoxProfiles.ResumeLayout(false);
            this.groupBoxProfiles.PerformLayout();
            this.groupBoxSmoothing.ResumeLayout(false);
            this.groupBoxSmoothing.PerformLayout();
            this.groupBoxCalculate.ResumeLayout(false);
            this.groupBoxCalculate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAddImages;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.Panel panelRowProfile;
        private System.Windows.Forms.Panel panelColumnProfile;
        private System.Windows.Forms.Label labelPatternIndex;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.GroupBox groupBoxPackage;
        private System.Windows.Forms.GroupBox groupBoxProfiles;
        private System.Windows.Forms.CheckBox checkBoxProfiles;
        private System.Windows.Forms.Label labelIntensity1x1;
        private System.Windows.Forms.Label labelColumnProfile;
        private System.Windows.Forms.Label labelIntensity5x5;
        private System.Windows.Forms.Label labelIntensity3x3;
        private System.Windows.Forms.Label labelRowProfile;
        private System.Windows.Forms.Label labelAverageIntensity;
        private System.Windows.Forms.GroupBox groupBoxSmoothing;
        private System.Windows.Forms.CheckBox checkBoxUseSmoothed;
        private System.Windows.Forms.Button buttonSmooth;
        private System.Windows.Forms.TextBox textBoxSmoothRadius;
        private System.Windows.Forms.Label labelRadius;
        private System.Windows.Forms.GroupBox groupBoxCalculate;
        private System.Windows.Forms.Button buttonCalculateCurrent;
        private System.Windows.Forms.TextBox textBoxRowsStep;
        private System.Windows.Forms.Label labelRowsStep;
        private System.Windows.Forms.CheckBox checkBoxShowSelection;
        private System.Windows.Forms.Button buttonCalculateAll;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxBottom;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListBox listBox1;

    }
}

