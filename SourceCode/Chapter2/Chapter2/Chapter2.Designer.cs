namespace ReflectionDemo
{
    partial class Chapter2
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
            this.cmdReflectionTypes = new System.Windows.Forms.Button();
            this.cmdLoadingSharedAssemblies = new System.Windows.Forms.Button();
            this.cmdAssemblyExplorer = new System.Windows.Forms.Button();
            this.cmdSelectForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdReflectionTypes
            // 
            this.cmdReflectionTypes.Location = new System.Drawing.Point(64, 24);
            this.cmdReflectionTypes.Name = "cmdReflectionTypes";
            this.cmdReflectionTypes.Size = new System.Drawing.Size(160, 23);
            this.cmdReflectionTypes.TabIndex = 0;
            this.cmdReflectionTypes.Text = "Reflection Types";
            this.cmdReflectionTypes.UseVisualStyleBackColor = true;
            this.cmdReflectionTypes.Click += new System.EventHandler(this.cmdReflectionTypes_Click);
            // 
            // cmdLoadingSharedAssemblies
            // 
            this.cmdLoadingSharedAssemblies.Location = new System.Drawing.Point(64, 56);
            this.cmdLoadingSharedAssemblies.Name = "cmdLoadingSharedAssemblies";
            this.cmdLoadingSharedAssemblies.Size = new System.Drawing.Size(160, 23);
            this.cmdLoadingSharedAssemblies.TabIndex = 1;
            this.cmdLoadingSharedAssemblies.Text = "Loading Shared Assemblies";
            this.cmdLoadingSharedAssemblies.UseVisualStyleBackColor = true;
            this.cmdLoadingSharedAssemblies.Click += new System.EventHandler(this.cmdLoadingSharedAssemblies_Click);
            // 
            // cmdAssemblyExplorer
            // 
            this.cmdAssemblyExplorer.Location = new System.Drawing.Point(64, 88);
            this.cmdAssemblyExplorer.Name = "cmdAssemblyExplorer";
            this.cmdAssemblyExplorer.Size = new System.Drawing.Size(160, 23);
            this.cmdAssemblyExplorer.TabIndex = 2;
            this.cmdAssemblyExplorer.Text = "Assembly Explorer";
            this.cmdAssemblyExplorer.UseVisualStyleBackColor = true;
            this.cmdAssemblyExplorer.Click += new System.EventHandler(this.cmdAssemblyExplorer_Click);
            // 
            // cmdSelectForm
            // 
            this.cmdSelectForm.Location = new System.Drawing.Point(64, 120);
            this.cmdSelectForm.Name = "cmdSelectForm";
            this.cmdSelectForm.Size = new System.Drawing.Size(160, 23);
            this.cmdSelectForm.TabIndex = 3;
            this.cmdSelectForm.Text = "Select Form";
            this.cmdSelectForm.UseVisualStyleBackColor = true;
            this.cmdSelectForm.Click += new System.EventHandler(this.cmdSelectForm_Click);
            // 
            // Chapter2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 179);
            this.Controls.Add(this.cmdSelectForm);
            this.Controls.Add(this.cmdAssemblyExplorer);
            this.Controls.Add(this.cmdLoadingSharedAssemblies);
            this.Controls.Add(this.cmdReflectionTypes);
            this.Name = "Chapter2";
            this.Text = "Chapter 2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdReflectionTypes;
        private System.Windows.Forms.Button cmdLoadingSharedAssemblies;
        private System.Windows.Forms.Button cmdAssemblyExplorer;
        private System.Windows.Forms.Button cmdSelectForm;
    }
}