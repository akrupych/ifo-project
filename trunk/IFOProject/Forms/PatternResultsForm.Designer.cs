namespace IFOProject.Forms
{
    partial class PatternResultsForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBoxShowInitial = new System.Windows.Forms.CheckBox();
            this.textBoxFixedComponent = new System.Windows.Forms.TextBox();
            this.textBoxAmplitude = new System.Windows.Forms.TextBox();
            this.textBoxPeriod = new System.Windows.Forms.TextBox();
            this.textBoxInitialPhase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.plotApproximation = new ZedGraph.ZedGraphControl();
            this.buttonRecalculate = new System.Windows.Forms.Button();
            this.checkBoxShowOnPlot = new System.Windows.Forms.CheckBox();
            this.plotPhase = new ZedGraph.ZedGraphControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewResults.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridViewResults.Location = new System.Drawing.Point(12, 191);
            this.dataGridViewResults.MultiSelect = false;
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.RowHeadersVisible = false;
            this.dataGridViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResults.Size = new System.Drawing.Size(190, 306);
            this.dataGridViewResults.TabIndex = 0;
            this.dataGridViewResults.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewResults_RowEnter);
            this.dataGridViewResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewResults_KeyDown);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.HeaderText = "Row";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Phase value, degree";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // checkBoxShowInitial
            // 
            this.checkBoxShowInitial.AutoSize = true;
            this.checkBoxShowInitial.Location = new System.Drawing.Point(13, 12);
            this.checkBoxShowInitial.Name = "checkBoxShowInitial";
            this.checkBoxShowInitial.Size = new System.Drawing.Size(147, 17);
            this.checkBoxShowInitial.TabIndex = 6;
            this.checkBoxShowInitial.Text = "Show initial approximation";
            this.checkBoxShowInitial.UseVisualStyleBackColor = true;
            this.checkBoxShowInitial.CheckedChanged += new System.EventHandler(this.checkBoxShowInitial_CheckedChanged);
            // 
            // textBoxFixedComponent
            // 
            this.textBoxFixedComponent.Location = new System.Drawing.Point(109, 35);
            this.textBoxFixedComponent.Name = "textBoxFixedComponent";
            this.textBoxFixedComponent.Size = new System.Drawing.Size(93, 20);
            this.textBoxFixedComponent.TabIndex = 7;
            this.textBoxFixedComponent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // textBoxAmplitude
            // 
            this.textBoxAmplitude.Location = new System.Drawing.Point(109, 61);
            this.textBoxAmplitude.Name = "textBoxAmplitude";
            this.textBoxAmplitude.Size = new System.Drawing.Size(93, 20);
            this.textBoxAmplitude.TabIndex = 10;
            this.textBoxAmplitude.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // textBoxPeriod
            // 
            this.textBoxPeriod.Location = new System.Drawing.Point(109, 87);
            this.textBoxPeriod.Name = "textBoxPeriod";
            this.textBoxPeriod.Size = new System.Drawing.Size(93, 20);
            this.textBoxPeriod.TabIndex = 13;
            this.textBoxPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // textBoxInitialPhase
            // 
            this.textBoxInitialPhase.Location = new System.Drawing.Point(109, 113);
            this.textBoxInitialPhase.Name = "textBoxInitialPhase";
            this.textBoxInitialPhase.Size = new System.Drawing.Size(93, 20);
            this.textBoxInitialPhase.TabIndex = 16;
            this.textBoxInitialPhase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Fixed component:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Amplitude:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Period:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Initial phase:";
            // 
            // plotApproximation
            // 
            this.plotApproximation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plotApproximation.Location = new System.Drawing.Point(208, 247);
            this.plotApproximation.Name = "plotApproximation";
            this.plotApproximation.ScrollGrace = 0D;
            this.plotApproximation.ScrollMaxX = 0D;
            this.plotApproximation.ScrollMaxY = 0D;
            this.plotApproximation.ScrollMaxY2 = 0D;
            this.plotApproximation.ScrollMinX = 0D;
            this.plotApproximation.ScrollMinY = 0D;
            this.plotApproximation.ScrollMinY2 = 0D;
            this.plotApproximation.Size = new System.Drawing.Size(553, 250);
            this.plotApproximation.TabIndex = 24;
            // 
            // buttonRecalculate
            // 
            this.buttonRecalculate.Location = new System.Drawing.Point(12, 162);
            this.buttonRecalculate.Name = "buttonRecalculate";
            this.buttonRecalculate.Size = new System.Drawing.Size(190, 23);
            this.buttonRecalculate.TabIndex = 25;
            this.buttonRecalculate.Text = "Recalculate";
            this.buttonRecalculate.UseVisualStyleBackColor = true;
            this.buttonRecalculate.Click += new System.EventHandler(this.buttonRecalculate_Click);
            // 
            // checkBoxShowOnPlot
            // 
            this.checkBoxShowOnPlot.AutoSize = true;
            this.checkBoxShowOnPlot.Location = new System.Drawing.Point(109, 139);
            this.checkBoxShowOnPlot.Name = "checkBoxShowOnPlot";
            this.checkBoxShowOnPlot.Size = new System.Drawing.Size(89, 17);
            this.checkBoxShowOnPlot.TabIndex = 26;
            this.checkBoxShowOnPlot.Text = "Show on Plot";
            this.checkBoxShowOnPlot.UseVisualStyleBackColor = true;
            this.checkBoxShowOnPlot.CheckedChanged += new System.EventHandler(this.checkBoxShowOnPlot_CheckedChanged);
            // 
            // plotPhase
            // 
            this.plotPhase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plotPhase.Location = new System.Drawing.Point(208, 12);
            this.plotPhase.Name = "plotPhase";
            this.plotPhase.ScrollGrace = 0D;
            this.plotPhase.ScrollMaxX = 0D;
            this.plotPhase.ScrollMaxY = 0D;
            this.plotPhase.ScrollMaxY2 = 0D;
            this.plotPhase.ScrollMinX = 0D;
            this.plotPhase.ScrollMinY = 0D;
            this.plotPhase.ScrollMinY2 = 0D;
            this.plotPhase.Size = new System.Drawing.Size(553, 229);
            this.plotPhase.TabIndex = 27;
            // 
            // PatternResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 507);
            this.Controls.Add(this.plotPhase);
            this.Controls.Add(this.checkBoxShowOnPlot);
            this.Controls.Add(this.buttonRecalculate);
            this.Controls.Add(this.plotApproximation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxInitialPhase);
            this.Controls.Add(this.textBoxPeriod);
            this.Controls.Add(this.textBoxAmplitude);
            this.Controls.Add(this.textBoxFixedComponent);
            this.Controls.Add(this.checkBoxShowInitial);
            this.Controls.Add(this.dataGridViewResults);
            this.Name = "PatternResultsForm";
            this.Text = "Results";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.CheckBox checkBoxShowInitial;
        private System.Windows.Forms.TextBox textBoxFixedComponent;
        private System.Windows.Forms.TextBox textBoxAmplitude;
        private System.Windows.Forms.TextBox textBoxPeriod;
        private System.Windows.Forms.TextBox textBoxInitialPhase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private ZedGraph.ZedGraphControl plotApproximation;
        private System.Windows.Forms.Button buttonRecalculate;
        private System.Windows.Forms.CheckBox checkBoxShowOnPlot;
        private ZedGraph.ZedGraphControl plotPhase;

    }
}