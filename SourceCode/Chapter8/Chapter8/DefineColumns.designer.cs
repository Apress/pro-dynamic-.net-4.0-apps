namespace Chapter8
{
    partial class DefineColumns
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtColumnName = new System.Windows.Forms.TextBox();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.cmbForeignKeyTable = new System.Windows.Forms.ComboBox();
            this.cmbSourceTable = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdSaveChanges = new System.Windows.Forms.Button();
            this.cmbForeignKey = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Column Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Data Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Foreign Key Table:";
            // 
            // txtColumnName
            // 
            this.txtColumnName.Location = new System.Drawing.Point(96, 24);
            this.txtColumnName.Name = "txtColumnName";
            this.txtColumnName.Size = new System.Drawing.Size(176, 20);
            this.txtColumnName.TabIndex = 3;
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(96, 56);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(121, 21);
            this.cmbDataType.TabIndex = 4;
            // 
            // cmbForeignKeyTable
            // 
            this.cmbForeignKeyTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForeignKeyTable.FormattingEnabled = true;
            this.cmbForeignKeyTable.Location = new System.Drawing.Point(112, 24);
            this.cmbForeignKeyTable.Name = "cmbForeignKeyTable";
            this.cmbForeignKeyTable.Size = new System.Drawing.Size(160, 21);
            this.cmbForeignKeyTable.TabIndex = 5;
            this.cmbForeignKeyTable.SelectedIndexChanged += new System.EventHandler(this.cmbForeignKeyTable_SelectedIndexChanged);
            // 
            // cmbSourceTable
            // 
            this.cmbSourceTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceTable.FormattingEnabled = true;
            this.cmbSourceTable.Location = new System.Drawing.Point(96, 8);
            this.cmbSourceTable.Name = "cmbSourceTable";
            this.cmbSourceTable.Size = new System.Drawing.Size(192, 21);
            this.cmbSourceTable.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Source Table:";
            // 
            // cmdSaveChanges
            // 
            this.cmdSaveChanges.Location = new System.Drawing.Point(104, 240);
            this.cmdSaveChanges.Name = "cmdSaveChanges";
            this.cmdSaveChanges.Size = new System.Drawing.Size(96, 23);
            this.cmdSaveChanges.TabIndex = 8;
            this.cmdSaveChanges.Text = "Save Changes";
            this.cmdSaveChanges.UseVisualStyleBackColor = true;
            this.cmdSaveChanges.Click += new System.EventHandler(this.cmdSaveChanges_Click);
            // 
            // cmbForeignKey
            // 
            this.cmbForeignKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbForeignKey.FormattingEnabled = true;
            this.cmbForeignKey.Location = new System.Drawing.Point(112, 56);
            this.cmbForeignKey.Name = "cmbForeignKey";
            this.cmbForeignKey.Size = new System.Drawing.Size(160, 21);
            this.cmbForeignKey.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Foreign Key:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtColumnName);
            this.groupBox1.Controls.Add(this.cmbDataType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 88);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Column Information";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbForeignKeyTable);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbForeignKey);
            this.groupBox2.Location = new System.Drawing.Point(8, 144);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 88);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Foreign Key Information";
            // 
            // Chapter8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 270);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdSaveChanges);
            this.Controls.Add(this.cmbSourceTable);
            this.Controls.Add(this.label4);
            this.Name = "Chapter8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Chapter8_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtColumnName;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.ComboBox cmbForeignKeyTable;
        private System.Windows.Forms.ComboBox cmbSourceTable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdSaveChanges;
        private System.Windows.Forms.ComboBox cmbForeignKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

