namespace Chapter8
{
    partial class InvertedTables
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
            this.cmdLoadInvertedData = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdLoadInvertedData
            // 
            this.cmdLoadInvertedData.Location = new System.Drawing.Point(8, 8);
            this.cmdLoadInvertedData.Name = "cmdLoadInvertedData";
            this.cmdLoadInvertedData.Size = new System.Drawing.Size(128, 23);
            this.cmdLoadInvertedData.TabIndex = 0;
            this.cmdLoadInvertedData.Text = "Load Inverted Data";
            this.cmdLoadInvertedData.UseVisualStyleBackColor = true;
            this.cmdLoadInvertedData.Click += new System.EventHandler(this.cmdLoadInvertedData_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(424, 176);
            this.dataGridView1.TabIndex = 1;
            // 
            // InvertedTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 266);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmdLoadInvertedData);
            this.Name = "InvertedTables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inverted Tables";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdLoadInvertedData;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}