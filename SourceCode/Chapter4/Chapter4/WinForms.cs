using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace DataDrivenProgramming
{
    public partial class WinForms : Form
    {
        public WinForms()
        {
            InitializeComponent();
        }

        private void cmdNestedControls_Click(object sender, EventArgs e)
        {
            Form oForm = new Form();
            GroupBox oGroupBox1 = new GroupBox();
            GroupBox oGroupBox2 = new GroupBox();
            GroupBox oGroupBox3 = new GroupBox();
            TextBox oTextBox = new TextBox();

            oGroupBox1.Size = new Size(250, 250);
            oGroupBox1.Text = "Box 1 owned by Form";

            oGroupBox2.Size = new Size(200, 200);
            oGroupBox2.Location = new Point(20, 20);
            oGroupBox2.Text = "Box 2 owned by Box 1";

            oGroupBox3.Size = new Size(150, 150);
            oGroupBox3.Location = new Point(20, 20);
            oGroupBox3.Text = "Box 3 owned by Box 2";

            oTextBox.Location = new Point(20, 20);
            oTextBox.Text = "Owned by Box 3";

            oGroupBox3.Controls.Add(oTextBox);
            oGroupBox2.Controls.Add(oGroupBox3);
            oGroupBox1.Controls.Add(oGroupBox2);
            oForm.Controls.Add(oGroupBox1);

            oForm.Show();
        }

        private void cmdDisplayForm_Click(object sender, EventArgs e)
        {
            Form oForm = new Form();
            Button oButton = new Button();

            oButton.Text = "Press Me";
            oButton.Click += new EventHandler(Button_Click);
            oForm.Controls.Add(oButton);

            oForm.Show();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button oButton = sender as Button;
            MessageBox.Show("Do something with " + oButton.Text); 
        }

        private void cmdDisplayButtonForm_Click(object sender, EventArgs e)
        {
            Form oForm = new Form();
            Button oButton1 = new Button();
            Button oButton2 = new Button();

            oButton1.Text = "Press This";
            oButton1.Click += new EventHandler(Button_Click);

            oButton2.Text = "Press That";
            oButton2.Click += new EventHandler(Button_Click);
            oButton2.Top = oButton1.Top + oButton1.Height + 10;

            oForm.Controls.Add(oButton1);
            oForm.Controls.Add(oButton2);

            oForm.Show();
        }

        private void cmdSuspendResumeLayout_Click(object sender, EventArgs e)
        { 
            SuspendResumeLayout oSuspendResumeLayout = new SuspendResumeLayout();
            oSuspendResumeLayout.ShowDialog();
        }

    }
}