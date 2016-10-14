namespace DataDrivenProgramming
{
    partial class Chapter4
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
            this.cmdMember = new System.Windows.Forms.Button();
            this.cmdSelectForm = new System.Windows.Forms.Button();
            this.cmdWinForms = new System.Windows.Forms.Button();
            this.cmdGridDemo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdMember
            // 
            this.cmdMember.Location = new System.Drawing.Point(104, 80);
            this.cmdMember.Name = "cmdMember";
            this.cmdMember.Size = new System.Drawing.Size(75, 23);
            this.cmdMember.TabIndex = 0;
            this.cmdMember.Text = "Member";
            this.cmdMember.UseVisualStyleBackColor = true;
            this.cmdMember.Click += new System.EventHandler(this.cmdMember_Click);
            // 
            // cmdSelectForm
            // 
            this.cmdSelectForm.Location = new System.Drawing.Point(104, 112);
            this.cmdSelectForm.Name = "cmdSelectForm";
            this.cmdSelectForm.Size = new System.Drawing.Size(75, 23);
            this.cmdSelectForm.TabIndex = 1;
            this.cmdSelectForm.Text = "Select Form";
            this.cmdSelectForm.UseVisualStyleBackColor = true;
            this.cmdSelectForm.Click += new System.EventHandler(this.cmdSelectForm_Click);
            // 
            // cmdWinForms
            // 
            this.cmdWinForms.Location = new System.Drawing.Point(104, 144);
            this.cmdWinForms.Name = "cmdWinForms";
            this.cmdWinForms.Size = new System.Drawing.Size(75, 23);
            this.cmdWinForms.TabIndex = 2;
            this.cmdWinForms.Text = "WinForms";
            this.cmdWinForms.UseVisualStyleBackColor = true;
            this.cmdWinForms.Click += new System.EventHandler(this.cmdWinForms_Click);
            // 
            // cmdGridDemo
            // 
            this.cmdGridDemo.Location = new System.Drawing.Point(104, 176);
            this.cmdGridDemo.Name = "cmdGridDemo";
            this.cmdGridDemo.Size = new System.Drawing.Size(75, 23);
            this.cmdGridDemo.TabIndex = 3;
            this.cmdGridDemo.Text = "Grid Demo";
            this.cmdGridDemo.UseVisualStyleBackColor = true;
            this.cmdGridDemo.Click += new System.EventHandler(this.cmdGridDemo_Click);
            // 
            // Chapter4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.cmdGridDemo);
            this.Controls.Add(this.cmdWinForms);
            this.Controls.Add(this.cmdSelectForm);
            this.Controls.Add(this.cmdMember);
            this.Name = "Chapter4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chapter 4";
            this.Load += new System.EventHandler(this.Chapter4_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdMember;
        private System.Windows.Forms.Button cmdSelectForm;
        private System.Windows.Forms.Button cmdWinForms;
        private System.Windows.Forms.Button cmdGridDemo;
    }
}