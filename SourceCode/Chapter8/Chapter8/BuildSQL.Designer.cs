namespace Chapter8
{
    partial class BuildSQL
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
            this.cmdGetSQL = new System.Windows.Forms.Button();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkFullTime = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdGetSQL
            // 
            this.cmdGetSQL.Location = new System.Drawing.Point(72, 144);
            this.cmdGetSQL.Name = "cmdGetSQL";
            this.cmdGetSQL.Size = new System.Drawing.Size(75, 23);
            this.cmdGetSQL.TabIndex = 0;
            this.cmdGetSQL.Text = "Get SQL";
            this.cmdGetSQL.UseVisualStyleBackColor = true;
            this.cmdGetSQL.Click += new System.EventHandler(this.cmdGetSQL_Click);
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(80, 16);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(100, 20);
            this.txtLastName.TabIndex = 1;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(80, 48);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(100, 20);
            this.txtFirstName.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkFullTime);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtFirstName);
            this.panel1.Controls.Add(this.txtLastName);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 120);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Last Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "First Name:";
            // 
            // chkFullTime
            // 
            this.chkFullTime.AutoSize = true;
            this.chkFullTime.Location = new System.Drawing.Point(16, 88);
            this.chkFullTime.Name = "chkFullTime";
            this.chkFullTime.Size = new System.Drawing.Size(68, 17);
            this.chkFullTime.TabIndex = 5;
            this.chkFullTime.Text = "Full Time";
            this.chkFullTime.UseVisualStyleBackColor = true;
            // 
            // Chapter8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 174);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdGetSQL);
            this.Name = "Chapter8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chapter 8";
            this.Load += new System.EventHandler(this.Chapter8_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdGetSQL;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkFullTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

