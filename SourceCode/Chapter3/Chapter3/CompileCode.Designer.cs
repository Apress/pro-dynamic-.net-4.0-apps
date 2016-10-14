namespace Chapter3
{
    partial class CompileSourceCode
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
            this.cmdCompileCode = new System.Windows.Forms.Button();
            this.cmdAddReferences = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdCompileCode
            // 
            this.cmdCompileCode.Location = new System.Drawing.Point(93, 56);
            this.cmdCompileCode.Name = "cmdCompileCode";
            this.cmdCompileCode.Size = new System.Drawing.Size(104, 23);
            this.cmdCompileCode.TabIndex = 0;
            this.cmdCompileCode.Text = "Compile Code";
            this.cmdCompileCode.UseVisualStyleBackColor = true;
            this.cmdCompileCode.Click += new System.EventHandler(this.cmdCompileCode_Click);
            // 
            // cmdAddReferences
            // 
            this.cmdAddReferences.Location = new System.Drawing.Point(93, 102);
            this.cmdAddReferences.Name = "cmdAddReferences";
            this.cmdAddReferences.Size = new System.Drawing.Size(104, 23);
            this.cmdAddReferences.TabIndex = 1;
            this.cmdAddReferences.Text = "Add References";
            this.cmdAddReferences.UseVisualStyleBackColor = true;
            this.cmdAddReferences.Click += new System.EventHandler(this.cmdAddReferences_Click);
            // 
            // CompileSourceCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 212);
            this.Controls.Add(this.cmdAddReferences);
            this.Controls.Add(this.cmdCompileCode);
            this.Name = "CompileSourceCode";
            this.Text = "Chapter 3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCompileCode;
        private System.Windows.Forms.Button cmdAddReferences;

    }
}