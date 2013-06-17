namespace IFOProject.Forms
{
    partial class PackageResultsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageResultsForm));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonRecalculate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxInitialPhase = new System.Windows.Forms.TextBox();
            this.textBoxPeriod = new System.Windows.Forms.TextBox();
            this.textBoxAmplitude = new System.Windows.Forms.TextBox();
            this.textBoxMeanLevel = new System.Windows.Forms.TextBox();
            this.checkBoxShowInitial = new System.Windows.Forms.CheckBox();
            this.checkBoxCustomSetup = new System.Windows.Forms.CheckBox();
            this.patternPlot = new IFOProject.CustomControls.Plot();
            this.packagePlot = new IFOProject.CustomControls.Plot();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 87);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(393, 423);
            this.dataGridView.TabIndex = 26;
            this.dataGridView.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEnter);
            // 
            // buttonRecalculate
            // 
            this.buttonRecalculate.Location = new System.Drawing.Point(312, 8);
            this.buttonRecalculate.Name = "buttonRecalculate";
            this.buttonRecalculate.Size = new System.Drawing.Size(93, 23);
            this.buttonRecalculate.TabIndex = 38;
            this.buttonRecalculate.Text = "Recalculate";
            this.buttonRecalculate.UseVisualStyleBackColor = true;
            this.buttonRecalculate.Click += new System.EventHandler(this.buttonRecalculate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Initial phase:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Period:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Amplitude:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Mean level:";
            // 
            // textBoxInitialPhase
            // 
            this.textBoxInitialPhase.Location = new System.Drawing.Point(312, 61);
            this.textBoxInitialPhase.Name = "textBoxInitialPhase";
            this.textBoxInitialPhase.Size = new System.Drawing.Size(93, 20);
            this.textBoxInitialPhase.TabIndex = 33;
            this.textBoxInitialPhase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // textBoxPeriod
            // 
            this.textBoxPeriod.Location = new System.Drawing.Point(312, 35);
            this.textBoxPeriod.Name = "textBoxPeriod";
            this.textBoxPeriod.Size = new System.Drawing.Size(93, 20);
            this.textBoxPeriod.TabIndex = 32;
            this.textBoxPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // textBoxAmplitude
            // 
            this.textBoxAmplitude.Location = new System.Drawing.Point(79, 61);
            this.textBoxAmplitude.Name = "textBoxAmplitude";
            this.textBoxAmplitude.Size = new System.Drawing.Size(93, 20);
            this.textBoxAmplitude.TabIndex = 31;
            this.textBoxAmplitude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // textBoxMeanLevel
            // 
            this.textBoxMeanLevel.Location = new System.Drawing.Point(79, 35);
            this.textBoxMeanLevel.Name = "textBoxMeanLevel";
            this.textBoxMeanLevel.Size = new System.Drawing.Size(93, 20);
            this.textBoxMeanLevel.TabIndex = 30;
            this.textBoxMeanLevel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // checkBoxShowInitial
            // 
            this.checkBoxShowInitial.AutoSize = true;
            this.checkBoxShowInitial.Location = new System.Drawing.Point(12, 12);
            this.checkBoxShowInitial.Name = "checkBoxShowInitial";
            this.checkBoxShowInitial.Size = new System.Drawing.Size(147, 17);
            this.checkBoxShowInitial.TabIndex = 29;
            this.checkBoxShowInitial.Text = "Show initial approximation";
            this.checkBoxShowInitial.UseVisualStyleBackColor = true;
            this.checkBoxShowInitial.CheckedChanged += new System.EventHandler(this.checkBoxShowInitial_CheckedChanged);
            // 
            // checkBoxCustomSetup
            // 
            this.checkBoxCustomSetup.AutoSize = true;
            this.checkBoxCustomSetup.Location = new System.Drawing.Point(165, 12);
            this.checkBoxCustomSetup.Name = "checkBoxCustomSetup";
            this.checkBoxCustomSetup.Size = new System.Drawing.Size(90, 17);
            this.checkBoxCustomSetup.TabIndex = 39;
            this.checkBoxCustomSetup.Text = "Custom setup";
            this.checkBoxCustomSetup.UseVisualStyleBackColor = true;
            this.checkBoxCustomSetup.CheckedChanged += new System.EventHandler(this.checkBoxCustomSetup_CheckedChanged);
            // 
            // patternPlot
            // 
            this.patternPlot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.patternPlot.IsAntiAlias = true;
            this.patternPlot.Location = new System.Drawing.Point(411, 261);
            this.patternPlot.Name = "patternPlot";
            this.patternPlot.ScrollGrace = 0D;
            this.patternPlot.ScrollMaxX = 0D;
            this.patternPlot.ScrollMaxY = 0D;
            this.patternPlot.ScrollMaxY2 = 0D;
            this.patternPlot.ScrollMinX = 0D;
            this.patternPlot.ScrollMinY = 0D;
            this.patternPlot.ScrollMinY2 = 0D;
            this.patternPlot.Size = new System.Drawing.Size(530, 249);
            this.patternPlot.TabIndex = 28;
            // 
            // packagePlot
            // 
            this.packagePlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.packagePlot.IsAntiAlias = true;
            this.packagePlot.Location = new System.Drawing.Point(411, 8);
            this.packagePlot.Name = "packagePlot";
            this.packagePlot.ScrollGrace = 0D;
            this.packagePlot.ScrollMaxX = 0D;
            this.packagePlot.ScrollMaxY = 0D;
            this.packagePlot.ScrollMaxY2 = 0D;
            this.packagePlot.ScrollMinX = 0D;
            this.packagePlot.ScrollMinY = 0D;
            this.packagePlot.ScrollMinY2 = 0D;
            this.packagePlot.Size = new System.Drawing.Size(530, 247);
            this.packagePlot.TabIndex = 27;
            // 
            // PackageResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 520);
            this.Controls.Add(this.checkBoxCustomSetup);
            this.Controls.Add(this.buttonRecalculate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxInitialPhase);
            this.Controls.Add(this.textBoxPeriod);
            this.Controls.Add(this.textBoxAmplitude);
            this.Controls.Add(this.textBoxMeanLevel);
            this.Controls.Add(this.checkBoxShowInitial);
            this.Controls.Add(this.patternPlot);
            this.Controls.Add(this.packagePlot);
            this.Controls.Add(this.dataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PackageResultsForm";
            this.Text = "Package results";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private CustomControls.Plot packagePlot;
        private CustomControls.Plot patternPlot;
        private System.Windows.Forms.Button buttonRecalculate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxInitialPhase;
        private System.Windows.Forms.TextBox textBoxPeriod;
        private System.Windows.Forms.TextBox textBoxAmplitude;
        private System.Windows.Forms.TextBox textBoxMeanLevel;
        private System.Windows.Forms.CheckBox checkBoxShowInitial;
        private System.Windows.Forms.CheckBox checkBoxCustomSetup;
    }
}