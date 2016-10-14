namespace DataDrivenProgramming
{
    partial class WinForms
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
            this.cmdLoadReflectionInfo = new System.Windows.Forms.Button();
            this.cmdNestedControls = new System.Windows.Forms.Button();
            this.cmdDisplayForm = new System.Windows.Forms.Button();
            this.cmdDisplayButtonForm = new System.Windows.Forms.Button();
            this.cmdSuspendResumeLayout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdLoadReflectionInfo
            // 
            this.cmdLoadReflectionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdLoadReflectionInfo.Location = new System.Drawing.Point(12, -189);
            this.cmdLoadReflectionInfo.Name = "cmdLoadReflectionInfo";
            this.cmdLoadReflectionInfo.Size = new System.Drawing.Size(128, 23);
            this.cmdLoadReflectionInfo.TabIndex = 0;
            this.cmdLoadReflectionInfo.Text = "Load Reflection Info";
            this.cmdLoadReflectionInfo.UseVisualStyleBackColor = true;
            // 
            // cmdNestedControls
            // 
            this.cmdNestedControls.Location = new System.Drawing.Point(32, 16);
            this.cmdNestedControls.Name = "cmdNestedControls";
            this.cmdNestedControls.Size = new System.Drawing.Size(160, 23);
            this.cmdNestedControls.TabIndex = 9;
            this.cmdNestedControls.Text = "Nested Controls";
            this.cmdNestedControls.UseVisualStyleBackColor = true;
            this.cmdNestedControls.Click += new System.EventHandler(this.cmdNestedControls_Click);
            // 
            // cmdDisplayForm
            // 
            this.cmdDisplayForm.Location = new System.Drawing.Point(32, 45);
            this.cmdDisplayForm.Name = "cmdDisplayForm";
            this.cmdDisplayForm.Size = new System.Drawing.Size(160, 23);
            this.cmdDisplayForm.TabIndex = 10;
            this.cmdDisplayForm.Text = "Display Form";
            this.cmdDisplayForm.UseVisualStyleBackColor = true;
            this.cmdDisplayForm.Click += new System.EventHandler(this.cmdDisplayForm_Click);
            // 
            // cmdDisplayButtonForm
            // 
            this.cmdDisplayButtonForm.Location = new System.Drawing.Point(32, 74);
            this.cmdDisplayButtonForm.Name = "cmdDisplayButtonForm";
            this.cmdDisplayButtonForm.Size = new System.Drawing.Size(160, 23);
            this.cmdDisplayButtonForm.TabIndex = 11;
            this.cmdDisplayButtonForm.Text = "Display Button Form";
            this.cmdDisplayButtonForm.UseVisualStyleBackColor = true;
            this.cmdDisplayButtonForm.Click += new System.EventHandler(this.cmdDisplayButtonForm_Click);
            // 
            // cmdSuspendResumeLayout
            // 
            this.cmdSuspendResumeLayout.Location = new System.Drawing.Point(32, 105);
            this.cmdSuspendResumeLayout.Name = "cmdSuspendResumeLayout";
            this.cmdSuspendResumeLayout.Size = new System.Drawing.Size(160, 23);
            this.cmdSuspendResumeLayout.TabIndex = 12;
            this.cmdSuspendResumeLayout.Text = "Suspend/Resume Layout";
            this.cmdSuspendResumeLayout.UseVisualStyleBackColor = true;
            this.cmdSuspendResumeLayout.Click += new System.EventHandler(this.cmdSuspendResumeLayout_Click);
            // 
            // WinForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 145);
            this.Controls.Add(this.cmdSuspendResumeLayout);
            this.Controls.Add(this.cmdDisplayButtonForm);
            this.Controls.Add(this.cmdDisplayForm);
            this.Controls.Add(this.cmdNestedControls);
            this.Controls.Add(this.cmdLoadReflectionInfo);
            this.Name = "WinForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinForms";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button cmdLoadReflectionInfo;
        private System.Windows.Forms.Button cmdNestedControls;
        private System.Windows.Forms.Button cmdDisplayForm;
        private System.Windows.Forms.Button cmdDisplayButtonForm;

        #endregion
        private System.Windows.Forms.Button cmdSuspendResumeLayout;

    }
}