using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace DataDrivenProgramming
{
    public partial class GridDemo : Form
    {
        public GridDemo()
        {
            InitializeComponent();
        }

        private void GridDemo_Load(object sender, EventArgs e)
        {
            DataTable oDT = new DataTable();
            DataRow oDR;

            oDT.Columns.Add(new DataColumn("LastName"));
            oDT.Columns.Add(new DataColumn("FirstName"));
            oDT.Columns.Add(new DataColumn("DOB"));

            oDR = oDT.NewRow();

            oDR["LastName"] = "Smith";
            oDR["FirstName"] = "John";
            oDR["DOB"] = "3/1/1972";

            oDT.Rows.Add(oDR);

            oDR = oDT.NewRow();

            oDR["LastName"] = "Jones";
            oDR["FirstName"] = "Fred";
            oDR["DOB"] = "12/15/1967";

            oDT.Rows.Add(oDR);

            dataGridView1.DataSource = oDT;

            if (File.Exists(Application.StartupPath + @"\grid.xml"))
            {
                GridTools.GridInfo oGridInfo = new GridTools.GridInfo(dataGridView1);
                XmlDocument oXmlDocument = new XmlDocument();
                oXmlDocument.Load(Application.StartupPath + @"\grid.xml");
                oGridInfo.LoadFromXML(oXmlDocument, dataGridView1.Name);

                oGridInfo.ApplyGridSettings(dataGridView1, oGridInfo);
            }
        }

        private void GridDemo_FormClosed(object sender, FormClosedEventArgs e)
        {
            GridTools.GridInfo oGridInfo = new GridTools.GridInfo(dataGridView1);
            XmlDocument oXmlDocument = oGridInfo.WriteToXML();
            oXmlDocument.Save(Application.StartupPath + @"\grid.xml");
        }

        private void cmdSelectColumns_Click(object sender, EventArgs e)
        {
            GridTools.GridInfo oGridInfo = new GridTools.GridInfo(dataGridView1);

            oGridInfo.SelectColumnsForm().Show(); 
        }
    }
}
