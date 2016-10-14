using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace DataDrivenProgramming
{
    public partial class SelectColumns : Form
    {
        GridTools.GridInfo _oGridInfo;

        public GridTools.GridInfo GridInfo
        {
            get { return _oGridInfo; }
            set { _oGridInfo = value; }
        }

        public SelectColumns()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int sCnt = 0;

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Text = string.Empty;
            this.ControlBox = false;

            lstColumns.CheckOnClick = true;

            foreach (GridTools.GridColumn oGridColumn in this.GridInfo.Columns)
            {
                lstColumns.Items.Add(oGridColumn.Name);
                lstColumns.SetItemChecked(sCnt, oGridColumn.Visible);

                sCnt++;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            XmlDocument oXmlDocument;

            for (int x = 0; x <= lstColumns.Items.Count - 1; x++)
            {
                if (lstColumns.GetItemChecked(x))
                    this.GridInfo.Column(lstColumns.Items[x].ToString()).Visible = true;
                else
                    this.GridInfo.Column(lstColumns.Items[x].ToString()).Visible = false;
            }

            oXmlDocument = this.GridInfo.WriteToXML();
            //oXmlDocument.Save(@"c:\temp\grid.xml");

            this.Close();
        }

        private void cmdSelectAll_Click(object sender, EventArgs e)
        {
            CheckListBox(true);
        }

        private void CheckListBox(bool bToggle)
        {
            for (int x = 0; x <= lstColumns.Items.Count - 1; x++)
            {
                lstColumns.SetItemChecked(x, bToggle);
            }
        }

        private void cmdDeselectAll_Click(object sender, EventArgs e)
        {
            CheckListBox(false);
        }
    }
}