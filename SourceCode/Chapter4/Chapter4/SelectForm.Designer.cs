namespace DataDrivenProgramming
{
    partial class SelectForm
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
            this.cmdFormXML = new System.Windows.Forms.Button();
            this.cmdReCreateForm = new System.Windows.Forms.Button();
            this.cmdLoadForms = new System.Windows.Forms.Button();
            this.cmbForms = new System.Windows.Forms.ComboBox();
            this.cmdFormXAML = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdFormXML
            // 
            this.cmdFormXML.Location = new System.Drawing.Point(12, 87);
            this.cmdFormXML.Name = "cmdFormXML";
            this.cmdFormXML.Size = new System.Drawing.Size(111, 23);
            this.cmdFormXML.TabIndex = 14;
            this.cmdFormXML.Text = "Create Form XML";
            this.cmdFormXML.UseVisualStyleBackColor = true;
            this.cmdFormXML.Click += new System.EventHandler(this.cmdFormXML_Click);
            // 
            // cmdReCreateForm
            // 
            this.cmdReCreateForm.Location = new System.Drawing.Point(169, 87);
            this.cmdReCreateForm.Name = "cmdReCreateForm";
            this.cmdReCreateForm.Size = new System.Drawing.Size(111, 23);
            this.cmdReCreateForm.TabIndex = 15;
            this.cmdReCreateForm.Text = "ReCreate Form";
            this.cmdReCreateForm.UseVisualStyleBackColor = true;
            this.cmdReCreateForm.Click += new System.EventHandler(this.cmdReCreateForm_Click);
            // 
            // cmdLoadForms
            // 
            this.cmdLoadForms.Location = new System.Drawing.Point(12, 12);
            this.cmdLoadForms.Name = "cmdLoadForms";
            this.cmdLoadForms.Size = new System.Drawing.Size(268, 23);
            this.cmdLoadForms.TabIndex = 17;
            this.cmdLoadForms.Text = "Load Forms";
            this.cmdLoadForms.UseVisualStyleBackColor = true;
            this.cmdLoadForms.Click += new System.EventHandler(this.cmdLoadForms_Click);
            // 
            // cmbForms
            // 
            this.cmbForms.FormattingEnabled = true;
            this.cmbForms.Location = new System.Drawing.Point(12, 41);
            this.cmbForms.Name = "cmbForms";
            this.cmbForms.Size = new System.Drawing.Size(268, 21);
            this.cmbForms.TabIndex = 18;
            // 
            // cmdFormXAML
            // 
            this.cmdFormXAML.Location = new System.Drawing.Point(12, 113);
            this.cmdFormXAML.Name = "cmdFormXAML";
            this.cmdFormXAML.Size = new System.Drawing.Size(111, 23);
            this.cmdFormXAML.TabIndex = 19;
            this.cmdFormXAML.Text = "Create Form XAML";
            this.cmdFormXAML.UseVisualStyleBackColor = true;
            this.cmdFormXAML.Click += new System.EventHandler(this.cmdFormXAML_Click);
            // 
            // SelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 148);
            this.Controls.Add(this.cmdFormXAML);
            this.Controls.Add(this.cmbForms);
            this.Controls.Add(this.cmdLoadForms);
            this.Controls.Add(this.cmdReCreateForm);
            this.Controls.Add(this.cmdFormXML);
            this.Name = "SelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdFormXML;
        private System.Windows.Forms.Button cmdReCreateForm;
        private System.Windows.Forms.Button cmdLoadForms;
        private System.Windows.Forms.ComboBox cmbForms;
        private System.Windows.Forms.Button cmdFormXAML;
    }
}