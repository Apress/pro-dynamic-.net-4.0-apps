using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Chapter8
{
    public partial class BuildSQL : Form
    {
        public BuildSQL()
        {
            InitializeComponent();

        }

        private void GetSQL(System.Windows.Forms.Control.ControlCollection oControls, 
            StringBuilder oUPDATE,
            StringBuilder oINSERTColumns,
            StringBuilder oINSERTValues)
        {
            string szControlType;
            string szColumnName;

            //iterate through every control in the control 
            //collection of the owner object
            foreach (Control oSubControl in oControls)
            {
                //Get the name of the control type - ListBox, CheckBox, etc.
                szControlType = oSubControl.GetType().Name;

                //Find the control's saved data in the XML
                if (oSubControl.Tag != null)
                {
                    szColumnName = oSubControl.Tag.ToString();

                    oUPDATE.Append(szColumnName);
                    oUPDATE.Append(" = ");

                    oINSERTColumns.Append(szColumnName);

                    switch (szControlType)
                    {
                        case "TextBox":
                            oUPDATE.Append("'");
                            oUPDATE.Append(((TextBox)oSubControl).Text);
                            oUPDATE.Append("'");

                            oINSERTValues.Append("'");
                            oINSERTValues.Append(((TextBox)oSubControl).Text);
                            oINSERTValues.Append("'");
                            break;

                        case "CheckBox":
                            oUPDATE.Append(((CheckBox)oSubControl).Checked ? "1" : "0");
                            oINSERTValues.Append(((CheckBox)oSubControl).Checked ? "1" : "0");
                            break;
                    }

                    oUPDATE.Append(", ");
                    oINSERTValues.Append(", ");
                    oINSERTColumns.Append(", ");
                }

                //Perform recursion to handle child controls
                if (oSubControl.HasChildren)
                    GetSQL(oSubControl.Controls, oUPDATE, oINSERTColumns, oINSERTValues);
            }
        }

        private void cmdGetSQL_Click(object sender, EventArgs e)
        {
            StringBuilder oUPDATE = new StringBuilder();
            StringBuilder oINSERT = new StringBuilder();
            StringBuilder oINSERTColumns = new StringBuilder();
            StringBuilder oINSERTValues = new StringBuilder();
            int iPrimaryKeyID = 12;
            string szTag = this.Tag.ToString();

            oINSERT.Append("INSERT INTO ");
            oINSERT.Append(szTag.Substring(0, szTag.IndexOf("|")));
            oINSERT.Append(" (");

            oUPDATE.Append("UPDATE ");
            oUPDATE.Append(szTag.Substring(0, szTag.IndexOf("|")));
            oUPDATE.Append(" SET ");

            GetSQL(panel1.Controls, oUPDATE, oINSERTColumns, oINSERTValues);

            //Remove the trailing comma
            oUPDATE.Remove(oUPDATE.Length - 2, 2);

            oUPDATE.Append(" WHERE ");
            oUPDATE.Append(szTag.Substring(szTag.IndexOf("|") + 1));
            oUPDATE.Append(" = ");
            oUPDATE.Append(iPrimaryKeyID.ToString());

            //Remove the trailing comma
            oINSERTColumns.Remove(oINSERTColumns.Length - 2, 2);
            oINSERTValues.Remove(oINSERTValues.Length - 2, 2);

            oINSERT.Append(oINSERTColumns.ToString());
            oINSERT.Append(")");
            oINSERT.Append(" VALUES ");
            oINSERT.Append("(");
            oINSERT.Append(oINSERTValues.ToString());
            oINSERT.Append(")");

            MessageBox.Show(oUPDATE.ToString());
            MessageBox.Show(oINSERT.ToString()); 
        }

        private void Chapter8_Load(object sender, EventArgs e)
        {
            this.Tag = "Employees|EmployeeID";

            txtLastName.Tag = "LastName";
            txtFirstName.Tag = "FirstName";
            chkFullTime.Tag = "FullTime";
        }

    }
}