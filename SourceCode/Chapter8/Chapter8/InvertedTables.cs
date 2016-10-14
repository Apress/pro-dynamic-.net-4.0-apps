using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Chapter8
{
    public partial class InvertedTables : Form
    {
        public InvertedTables()
        {
            InitializeComponent();
        }

        private void cmdLoadInvertedData_Click(object sender, EventArgs e)
        {
            DataSet oDS = GetInvertedData();
            DataTable oStructureDT = oDS.Tables[0];
            DataTable oInvertedDT = oDS.Tables[1];
            DataTable oNormalDT = new DataTable();
            DataRow oNewDR = null;
            int iPrimaryKeyID;

            //Create the normalized structure
            foreach (DataRow oDR in oStructureDT.Rows)
            {
                oNormalDT.Columns.Add(new DataColumn(oDR["ColumnName"].ToString(), 
                    ConvertType(oDR["DataType"].ToString())));
            }

            if (oInvertedDT.Rows.Count == 0)
                return;

            iPrimaryKeyID = 0;

            //Iterate the inverted data set
            foreach (DataRow oDR in oInvertedDT.Rows)
            {
                //When the primary key changes, we can add the new row...
                if (iPrimaryKeyID != int.Parse(oDR["DataElementID"].ToString()))
                {
                    //...except the first time through
                    if (iPrimaryKeyID != 0)
                        oNormalDT.Rows.Add(oNewDR);

                    //Create a new row object and set the primary key value
                    oNewDR = oNormalDT.NewRow();

                    iPrimaryKeyID = int.Parse(oDR["DataElementID"].ToString());
                }

                //Add the data to the named column
                oNewDR[oDR["ColumnName"].ToString()] = oDR["DataValue"].ToString();

            }

            oNormalDT.Rows.Add(oNewDR);

            dataGridView1.DataSource = oNormalDT;
        }

        private Type ConvertType(string szSQLServerType)
        {
            Type oType = null;
            string szDotNetType = string.Empty;

            switch (szSQLServerType)
            {
                case "nvarchar":
                case "nchar":
                case "char":
                case "varchar":
                    oType = typeof(string);
                    break;

                case "datetime":
                case "date":
                case "time":
                    oType = typeof(DateTime);
                    break;

                case "int":
                    oType = typeof(int);
                    break;

                case "money":
                    oType = typeof(decimal);
                    break;
            }

            return oType;
        }

        private DataSet GetInvertedData()
        {
            SqlDatabase oDatabase;
            DbCommand oDbCommand;
            DataSet oDS;

            oDatabase = new SqlDatabase("Data Source=SETON-NOTEBOOK;Initial catalog=Chapter8;Integrated security=SSPI;Persist security info=False");

            using (oDbCommand = oDatabase.GetStoredProcCommand("spc_GetInvertedData"))
            {
                oDS = oDatabase.ExecuteDataSet(oDbCommand);
            }

            return oDS;

        }
    }


}
