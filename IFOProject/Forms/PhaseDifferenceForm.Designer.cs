﻿namespace IFOProject.Forms
{
    partial class PhaseDifferenceForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhaseDifferenceForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.textBoxRSS = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.textBoxCoD = new System.Windows.Forms.ToolStripTextBox();
            this.buttonCalculatePOCs = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.textBoxSlope = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.textBoxStandardError = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.textBoxVerticalResolution = new System.Windows.Forms.ToolStripTextBox();
            this.resultsPlot = new IFOProject.CustomControls.Plot();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel5,
            this.textBoxVerticalResolution,
            this.toolStripLabel1,
            this.textBoxRSS,
            this.toolStripLabel2,
            this.textBoxCoD,
            this.buttonCalculatePOCs,
            this.toolStripLabel3,
            this.textBoxSlope,
            this.toolStripLabel4,
            this.textBoxStandardError});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(864, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.buttonCalculatePOCsClicked);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(120, 22);
            this.toolStripLabel1.Text = "Residual Square Sum:";
            // 
            // textBoxRSS
            // 
            this.textBoxRSS.Name = "textBoxRSS";
            this.textBoxRSS.ReadOnly = true;
            this.textBoxRSS.Size = new System.Drawing.Size(80, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(161, 22);
            this.toolStripLabel2.Text = "Coefficient of Determination:";
            // 
            // textBoxCoD
            // 
            this.textBoxCoD.Name = "textBoxCoD";
            this.textBoxCoD.ReadOnly = true;
            this.textBoxCoD.Size = new System.Drawing.Size(36, 25);
            // 
            // buttonCalculatePOCs
            // 
            this.buttonCalculatePOCs.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonCalculatePOCs.BackColor = System.Drawing.Color.LightGreen;
            this.buttonCalculatePOCs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonCalculatePOCs.Image = ((System.Drawing.Image)(resources.GetObject("buttonCalculatePOCs.Image")));
            this.buttonCalculatePOCs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCalculatePOCs.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCalculatePOCs.Name = "buttonCalculatePOCs";
            this.buttonCalculatePOCs.Size = new System.Drawing.Size(92, 21);
            this.buttonCalculatePOCs.Text = "Calculate POCs";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel3.Text = "Slope:";
            // 
            // textBoxSlope
            // 
            this.textBoxSlope.Name = "textBoxSlope";
            this.textBoxSlope.ReadOnly = true;
            this.textBoxSlope.Size = new System.Drawing.Size(49, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(85, 22);
            this.toolStripLabel4.Text = "Standard Error:";
            // 
            // textBoxStandardError
            // 
            this.textBoxStandardError.Name = "textBoxStandardError";
            this.textBoxStandardError.ReadOnly = true;
            this.textBoxStandardError.Size = new System.Drawing.Size(36, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(108, 22);
            this.toolStripLabel5.Text = "Vertical Resolution:";
            // 
            // textBoxVerticalResolution
            // 
            this.textBoxVerticalResolution.Name = "textBoxVerticalResolution";
            this.textBoxVerticalResolution.Size = new System.Drawing.Size(36, 25);
            this.textBoxVerticalResolution.Text = "89";
            this.textBoxVerticalResolution.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxVerticalResolution_KeyDown);
            // 
            // resultsPlot
            // 
            this.resultsPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsPlot.IsAntiAlias = true;
            this.resultsPlot.Location = new System.Drawing.Point(0, 25);
            this.resultsPlot.Name = "resultsPlot";
            this.resultsPlot.ScrollGrace = 0D;
            this.resultsPlot.ScrollMaxX = 0D;
            this.resultsPlot.ScrollMaxY = 0D;
            this.resultsPlot.ScrollMaxY2 = 0D;
            this.resultsPlot.ScrollMinX = 0D;
            this.resultsPlot.ScrollMinY = 0D;
            this.resultsPlot.ScrollMinY2 = 0D;
            this.resultsPlot.Size = new System.Drawing.Size(864, 521);
            this.resultsPlot.TabIndex = 1;
            // 
            // PhaseDifferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 546);
            this.Controls.Add(this.resultsPlot);
            this.Controls.Add(this.toolStrip1);
            this.Name = "PhaseDifferenceForm";
            this.Text = "Phase Difference";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.Plot resultsPlot;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox textBoxRSS;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox textBoxCoD;
        private System.Windows.Forms.ToolStripButton buttonCalculatePOCs;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox textBoxSlope;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox textBoxStandardError;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox textBoxVerticalResolution;
    }
}