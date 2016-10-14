using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDrivenProgramming
{
    public partial class SuspendResumeLayout : Form
    {
        public SuspendResumeLayout()
        {
            TextBox oTextBox = new TextBox();

            oTextBox.TextChanged += new EventHandler(this.Text_Changed);
            oTextBox.Layout += new LayoutEventHandler(this.Text_Layout);

            oTextBox.SuspendLayout();

            oTextBox.Text = "Hello World";
            oTextBox.Size = new Size(150, 24);

            this.Controls.Add(oTextBox);

            oTextBox.ResumeLayout();

        }

        private void Text_Changed(object sender, EventArgs e)
        {
            MessageBox.Show("Text_Changed fired");
        }

        private void Text_Layout(object sender, LayoutEventArgs e)
        {
            MessageBox.Show("Text_Layout fired");
        }

        private void SuspendResumeLayout_Load(object sender, EventArgs e)
        {

        }

    }
}
