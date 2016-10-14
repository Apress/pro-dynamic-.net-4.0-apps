namespace ReflectionDemo
{
    partial class AssemblyExplorer
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
            this.cmdGetFormControl = new System.Windows.Forms.Button();
            this.trvObjects = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // cmdGetFormControl
            // 
            this.cmdGetFormControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGetFormControl.Location = new System.Drawing.Point(1, 392);
            this.cmdGetFormControl.Name = "cmdGetFormControl";
            this.cmdGetFormControl.Size = new System.Drawing.Size(522, 30);
            this.cmdGetFormControl.TabIndex = 15;
            this.cmdGetFormControl.Text = "Get Forms and Controls";
            this.cmdGetFormControl.UseVisualStyleBackColor = true;
            this.cmdGetFormControl.Click += new System.EventHandler(this.cmdGetFormControl_Click);
            // 
            // trvObjects
            // 
            this.trvObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvObjects.Location = new System.Drawing.Point(1, -1);
            this.trvObjects.Name = "trvObjects";
            this.trvObjects.Size = new System.Drawing.Size(522, 395);
            this.trvObjects.TabIndex = 16;
            // 
            // AssemblyExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 423);
            this.Controls.Add(this.trvObjects);
            this.Controls.Add(this.cmdGetFormControl);
            this.Name = "AssemblyExplorer";
            this.Text = "Assembly Explorer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdGetFormControl;
        private System.Windows.Forms.TreeView trvObjects;
    }
}