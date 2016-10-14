namespace Chapter3
{
    partial class AddReference
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
            this.dgAvailable = new System.Windows.Forms.DataGridView();
            this.dgChosen = new System.Windows.Forms.DataGridView();
            this.cmdOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgChosen)).BeginInit();
            this.SuspendLayout();
            // 
            // dgAvailable
            // 
            this.dgAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAvailable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAvailable.Location = new System.Drawing.Point(12, 12);
            this.dgAvailable.Name = "dgAvailable";
            this.dgAvailable.Size = new System.Drawing.Size(602, 226);
            this.dgAvailable.TabIndex = 0;
            // 
            // dgChosen
            // 
            this.dgChosen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgChosen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgChosen.Location = new System.Drawing.Point(12, 243);
            this.dgChosen.Name = "dgChosen";
            this.dgChosen.Size = new System.Drawing.Size(602, 115);
            this.dgChosen.TabIndex = 1;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(12, 363);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // AddReference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 394);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.dgChosen);
            this.Controls.Add(this.dgAvailable);
            this.Name = "AddReference";
            this.Text = "AddReference";
            this.Load += new System.EventHandler(this.AddReference_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgChosen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgAvailable;
        private System.Windows.Forms.DataGridView dgChosen;
        private System.Windows.Forms.Button cmdOK;
    }
}