namespace Chapter7
{
    partial class Form1
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
            this.lstColumns = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdRunReport = new System.Windows.Forms.Button();
            this.cmdSaveSelections = new System.Windows.Forms.Button();
            this.cmdRunCrystalReport = new System.Windows.Forms.Button();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.cmdDynamicCrystalReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstColumns
            // 
            this.lstColumns.FormattingEnabled = true;
            this.lstColumns.Location = new System.Drawing.Point(12, 32);
            this.lstColumns.Name = "lstColumns";
            this.lstColumns.Size = new System.Drawing.Size(218, 274);
            this.lstColumns.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Columns:";
            // 
            // cmdRunReport
            // 
            this.cmdRunReport.Location = new System.Drawing.Point(12, 313);
            this.cmdRunReport.Name = "cmdRunReport";
            this.cmdRunReport.Size = new System.Drawing.Size(106, 23);
            this.cmdRunReport.TabIndex = 2;
            this.cmdRunReport.Text = "Run Report";
            this.cmdRunReport.UseVisualStyleBackColor = true;
            this.cmdRunReport.Click += new System.EventHandler(this.cmdRunReport_Click);
            // 
            // cmdSaveSelections
            // 
            this.cmdSaveSelections.Location = new System.Drawing.Point(124, 313);
            this.cmdSaveSelections.Name = "cmdSaveSelections";
            this.cmdSaveSelections.Size = new System.Drawing.Size(106, 23);
            this.cmdSaveSelections.TabIndex = 3;
            this.cmdSaveSelections.Text = "Save Selections";
            this.cmdSaveSelections.UseVisualStyleBackColor = true;
            this.cmdSaveSelections.Click += new System.EventHandler(this.cmdSaveSelections_Click);
            // 
            // cmdRunCrystalReport
            // 
            this.cmdRunCrystalReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRunCrystalReport.Location = new System.Drawing.Point(240, 467);
            this.cmdRunCrystalReport.Name = "cmdRunCrystalReport";
            this.cmdRunCrystalReport.Size = new System.Drawing.Size(120, 23);
            this.cmdRunCrystalReport.TabIndex = 4;
            this.cmdRunCrystalReport.Text = "Run Crystal Report";
            this.cmdRunCrystalReport.UseVisualStyleBackColor = true;
            this.cmdRunCrystalReport.Click += new System.EventHandler(this.cmdRunCrystalReport_Click);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.DisplayGroupTree = false;
            this.crystalReportViewer1.Location = new System.Drawing.Point(248, 8);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.SelectionFormula = "";
            this.crystalReportViewer1.Size = new System.Drawing.Size(592, 456);
            this.crystalReportViewer1.TabIndex = 5;
            this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // cmdDynamicCrystalReport
            // 
            this.cmdDynamicCrystalReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDynamicCrystalReport.Location = new System.Drawing.Point(376, 467);
            this.cmdDynamicCrystalReport.Name = "cmdDynamicCrystalReport";
            this.cmdDynamicCrystalReport.Size = new System.Drawing.Size(160, 23);
            this.cmdDynamicCrystalReport.TabIndex = 6;
            this.cmdDynamicCrystalReport.Text = "Dynamic Crystal Report";
            this.cmdDynamicCrystalReport.UseVisualStyleBackColor = true;
            this.cmdDynamicCrystalReport.Click += new System.EventHandler(this.cmdDynamicCrystalReport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 496);
            this.Controls.Add(this.cmdDynamicCrystalReport);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.cmdRunCrystalReport);
            this.Controls.Add(this.cmdSaveSelections);
            this.Controls.Add(this.cmdRunReport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstColumns);
            this.Name = "Form1";
            this.Text = "Personnel Report";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lstColumns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdRunReport;
        private System.Windows.Forms.Button cmdSaveSelections;
        private System.Windows.Forms.Button cmdRunCrystalReport;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Button cmdDynamicCrystalReport;
    }
}

