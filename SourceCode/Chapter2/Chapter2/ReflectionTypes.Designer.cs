namespace ReflectionDemo
{
    partial class ReflectionTypes
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
            this.cmdLoadReflectionInfo = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.trvAssembly = new System.Windows.Forms.TreeView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.trvMethod = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.trvField = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.trvProperty = new System.Windows.Forms.TreeView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.trvInterfaces = new System.Windows.Forms.TreeView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.trvConstructors = new System.Windows.Forms.TreeView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.trvEvents = new System.Windows.Forms.TreeView();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.treeView6 = new System.Windows.Forms.TreeView();
            this.cmdInvokeMethod = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdLoadReflectionInfo
            // 
            this.cmdLoadReflectionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdLoadReflectionInfo.Location = new System.Drawing.Point(12, 373);
            this.cmdLoadReflectionInfo.Name = "cmdLoadReflectionInfo";
            this.cmdLoadReflectionInfo.Size = new System.Drawing.Size(128, 23);
            this.cmdLoadReflectionInfo.TabIndex = 0;
            this.cmdLoadReflectionInfo.Text = "Load Reflection Info";
            this.cmdLoadReflectionInfo.UseVisualStyleBackColor = true;
            this.cmdLoadReflectionInfo.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(649, 355);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.trvAssembly);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(641, 329);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Assembly";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // trvAssembly
            // 
            this.trvAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvAssembly.Location = new System.Drawing.Point(3, 3);
            this.trvAssembly.Name = "trvAssembly";
            this.trvAssembly.Size = new System.Drawing.Size(635, 359);
            this.trvAssembly.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.trvMethod);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(624, 365);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Method";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // trvMethod
            // 
            this.trvMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvMethod.Location = new System.Drawing.Point(6, 6);
            this.trvMethod.Name = "trvMethod";
            this.trvMethod.Size = new System.Drawing.Size(448, 359);
            this.trvMethod.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.trvField);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(624, 365);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Field";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // trvField
            // 
            this.trvField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvField.Location = new System.Drawing.Point(6, 3);
            this.trvField.Name = "trvField";
            this.trvField.Size = new System.Drawing.Size(448, 359);
            this.trvField.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.trvProperty);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(624, 365);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Property";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // trvProperty
            // 
            this.trvProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvProperty.Location = new System.Drawing.Point(3, 3);
            this.trvProperty.Name = "trvProperty";
            this.trvProperty.Size = new System.Drawing.Size(618, 359);
            this.trvProperty.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.trvInterfaces);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(624, 365);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Interface";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // trvInterfaces
            // 
            this.trvInterfaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvInterfaces.Location = new System.Drawing.Point(3, 3);
            this.trvInterfaces.Name = "trvInterfaces";
            this.trvInterfaces.Size = new System.Drawing.Size(618, 359);
            this.trvInterfaces.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.trvConstructors);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(624, 365);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Constructor";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // trvConstructors
            // 
            this.trvConstructors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvConstructors.Location = new System.Drawing.Point(3, 3);
            this.trvConstructors.Name = "trvConstructors";
            this.trvConstructors.Size = new System.Drawing.Size(618, 359);
            this.trvConstructors.TabIndex = 2;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.trvEvents);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(624, 365);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Event";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // trvEvents
            // 
            this.trvEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trvEvents.Location = new System.Drawing.Point(3, 0);
            this.trvEvents.Name = "trvEvents";
            this.trvEvents.Size = new System.Drawing.Size(618, 362);
            this.trvEvents.TabIndex = 2;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.treeView6);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(624, 365);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Member";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // treeView6
            // 
            this.treeView6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView6.Location = new System.Drawing.Point(3, 3);
            this.treeView6.Name = "treeView6";
            this.treeView6.Size = new System.Drawing.Size(618, 359);
            this.treeView6.TabIndex = 2;
            // 
            // cmdInvokeMethod
            // 
            this.cmdInvokeMethod.Location = new System.Drawing.Point(667, 34);
            this.cmdInvokeMethod.Name = "cmdInvokeMethod";
            this.cmdInvokeMethod.Size = new System.Drawing.Size(94, 23);
            this.cmdInvokeMethod.TabIndex = 2;
            this.cmdInvokeMethod.Text = "Invoke Method";
            this.cmdInvokeMethod.UseVisualStyleBackColor = true;
            this.cmdInvokeMethod.Click += new System.EventHandler(this.cmdInvokeMethod_Click);
            // 
            // ReflectionTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 407);
            this.Controls.Add(this.cmdInvokeMethod);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cmdLoadReflectionInfo);
            this.Name = "ReflectionTypes";
            this.Text = "Reflection Methods";
            this.tabControl1.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button cmdLoadReflectionInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TreeView trvMethod;
        private System.Windows.Forms.TreeView trvField;
        private System.Windows.Forms.TreeView trvProperty;
        private System.Windows.Forms.TreeView trvInterfaces;
        private System.Windows.Forms.TreeView trvConstructors;
        private System.Windows.Forms.TreeView trvEvents;
        private System.Windows.Forms.TreeView treeView6;
        private System.Windows.Forms.Button cmdInvokeMethod;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TreeView trvAssembly;

        #endregion
    }
}