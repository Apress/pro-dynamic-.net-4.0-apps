using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataDrivenProgramming
{
    public partial class Chapter4 : Form
    {
        public Chapter4()
        {
            InitializeComponent();
        }

        private void Chapter4_Load(object sender, EventArgs e)
        {

        }

        private void cmdMember_Click(object sender, EventArgs e)
        {
            Member oMember = new Member();
            oMember.ShowDialog(); 
        }

        private void cmdSelectForm_Click(object sender, EventArgs e)
        {
            SelectForm oSelectForm = new SelectForm();
            oSelectForm.ShowDialog(); 
        }

        private void cmdWinForms_Click(object sender, EventArgs e)
        {
            WinForms oWinForms = new WinForms();
            oWinForms.ShowDialog(); 
        }

        private void cmdGridDemo_Click(object sender, EventArgs e)
        {
            GridDemo oGridDemo = new GridDemo();
            oGridDemo.ShowDialog();
        }
    }
}
