namespace DataDrivenProgramming
{
    partial class GridDemo
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmdSelectColumns = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 8);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(588, 288);
            this.dataGridView1.TabIndex = 0;
            // 
            // cmdSelectColumns
            // 
            this.cmdSelectColumns.Location = new System.Drawing.Point(8, 296);
            this.cmdSelectColumns.Name = "cmdSelectColumns";
            this.cmdSelectColumns.Size = new System.Drawing.Size(104, 23);
            this.cmdSelectColumns.TabIndex = 1;
            this.cmdSelectColumns.Text = "Select Columns";
            this.cmdSelectColumns.UseVisualStyleBackColor = true;
            this.cmdSelectColumns.Click += new System.EventHandler(this.cmdSelectColumns_Click);
            // 
            // GridDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 323);
            this.Controls.Add(this.cmdSelectColumns);
            this.Controls.Add(this.dataGridView1);
            this.Name = "GridDemo";
            this.Text = "GridDemo";
            this.Load += new System.EventHandler(this.GridDemo_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GridDemo_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button cmdSelectColumns;
    }
}