using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Chapter8
{
    public partial class Chapter8 : Form
    {
        public Chapter8()
        {
            InitializeComponent();
        }

        private void cmdBuildSQL_Click(object sender, EventArgs e)
        {
            BuildSQL oBuildSQL = new BuildSQL();
            oBuildSQL.ShowDialog(); 
        }

        private void cmdDefineColumns_Click(object sender, EventArgs e)
        {
            DefineColumns oDefineColumns = new DefineColumns();
            oDefineColumns.ShowDialog(); 
        }

        private void cmdInvertedTables_Click(object sender, EventArgs e)
        {
            InvertedTables oInvertedTables = new InvertedTables();
            oInvertedTables.ShowDialog(); 
        }
    }
}
