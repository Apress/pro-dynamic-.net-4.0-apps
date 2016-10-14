namespace Menus
{
    partial class Form1
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
            this.cmdLoadFromXML = new System.Windows.Forms.Button();
            this.cmdLoadFromRegistry = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdLoadFromXML
            // 
            this.cmdLoadFromXML.Location = new System.Drawing.Point(96, 104);
            this.cmdLoadFromXML.Name = "cmdLoadFromXML";
            this.cmdLoadFromXML.Size = new System.Drawing.Size(112, 23);
            this.cmdLoadFromXML.TabIndex = 0;
            this.cmdLoadFromXML.Text = "Load From XML";
            this.cmdLoadFromXML.UseVisualStyleBackColor = true;
            this.cmdLoadFromXML.Click += new System.EventHandler(this.cmdLoadFromXML_Click);
            // 
            // cmdLoadFromRegistry
            // 
            this.cmdLoadFromRegistry.Location = new System.Drawing.Point(96, 136);
            this.cmdLoadFromRegistry.Name = "cmdLoadFromRegistry";
            this.cmdLoadFromRegistry.Size = new System.Drawing.Size(112, 23);
            this.cmdLoadFromRegistry.TabIndex = 1;
            this.cmdLoadFromRegistry.Text = "Load From Registry";
            this.cmdLoadFromRegistry.UseVisualStyleBackColor = true;
            this.cmdLoadFromRegistry.Click += new System.EventHandler(this.cmdLoadFromRegistry_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.cmdLoadFromRegistry);
            this.Controls.Add(this.cmdLoadFromXML);
            this.Name = "Form1";
            this.Text = "Dynamic Menus";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdLoadFromXML;
        private System.Windows.Forms.Button cmdLoadFromRegistry;




    }
}

