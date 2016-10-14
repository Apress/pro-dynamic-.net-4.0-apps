namespace Chapter8
{
    partial class Chapter8
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
            this.cmdBuildSQL = new System.Windows.Forms.Button();
            this.cmdDefineColumns = new System.Windows.Forms.Button();
            this.cmdInvertedTables = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdBuildSQL
            // 
            this.cmdBuildSQL.Location = new System.Drawing.Point(96, 80);
            this.cmdBuildSQL.Name = "cmdBuildSQL";
            this.cmdBuildSQL.Size = new System.Drawing.Size(104, 23);
            this.cmdBuildSQL.TabIndex = 0;
            this.cmdBuildSQL.Text = "Build SQL";
            this.cmdBuildSQL.UseVisualStyleBackColor = true;
            this.cmdBuildSQL.Click += new System.EventHandler(this.cmdBuildSQL_Click);
            // 
            // cmdDefineColumns
            // 
            this.cmdDefineColumns.Location = new System.Drawing.Point(96, 112);
            this.cmdDefineColumns.Name = "cmdDefineColumns";
            this.cmdDefineColumns.Size = new System.Drawing.Size(104, 23);
            this.cmdDefineColumns.TabIndex = 1;
            this.cmdDefineColumns.Text = "Define Columns";
            this.cmdDefineColumns.UseVisualStyleBackColor = true;
            this.cmdDefineColumns.Click += new System.EventHandler(this.cmdDefineColumns_Click);
            // 
            // cmdInvertedTables
            // 
            this.cmdInvertedTables.Location = new System.Drawing.Point(96, 144);
            this.cmdInvertedTables.Name = "cmdInvertedTables";
            this.cmdInvertedTables.Size = new System.Drawing.Size(104, 23);
            this.cmdInvertedTables.TabIndex = 2;
            this.cmdInvertedTables.Text = "Inverted Tables";
            this.cmdInvertedTables.UseVisualStyleBackColor = true;
            this.cmdInvertedTables.Click += new System.EventHandler(this.cmdInvertedTables_Click);
            // 
            // Chapter8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.cmdInvertedTables);
            this.Controls.Add(this.cmdDefineColumns);
            this.Controls.Add(this.cmdBuildSQL);
            this.Name = "Chapter8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chapter8";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdBuildSQL;
        private System.Windows.Forms.Button cmdDefineColumns;
        private System.Windows.Forms.Button cmdInvertedTables;
    }
}