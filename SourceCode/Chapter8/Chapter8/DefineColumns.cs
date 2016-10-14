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
    public partial class DefineColumns : Form
    {
        static string szConnect = "Data Source=SETON-NOTEBOOK;Initial catalog=Northwind;Integrated security=SSPI;Persist security info=False";

        public DefineColumns()
        {
            InitializeComponent();
        }

        private void Chapter8_Load(object sender, EventArgs e)
        {
            cmbSourceTable.DisplayMember = "TABLE_NAME";
            cmbSourceTable.ValueMember = "TABLE_NAME";
            cmbSourceTable.DataSource = GetTables();

            cmbForeignKeyTable.DisplayMember = "TABLE_NAME";
            cmbForeignKeyTable.ValueMember = "TABLE_NAME";
            cmbForeignKeyTable.DataSource = GetTables();

            cmbDataType.DisplayMember = "NAME";
            cmbDataType.ValueMember = "NAME";
            cmbDataType.DataSource = GetDataTypes();
        }

        public static DataTable GetTables()
        {
            SqlDatabase oDatabase;
            DbCommand oDbCommand;
            DataTable oDT;
            string szSQL = "SELECT TABLE_NAME " +
                            "FROM INFORMATION_SCHEMA.TABLES " +
                            "WHERE TABLE_TYPE = 'BASE TABLE' " +
                            "ORDER BY TABLE_NAME";

            oDatabase = new SqlDatabase(szConnect);

            using (oDbCommand = oDatabase.GetSqlStringCommand(szSQL))
            {
                oDT = oDatabase.ExecuteDataSet(oDbCommand).Tables[0];
            }

            return oDT;

        }


        public static DataTable GetColumns(string szTableName)
        {
            SqlDatabase oDatabase;
            DbCommand oDbCommand;
            DataTable oDT;
            string szSQL = "SELECT COLUMN_NAME " +
                            "FROM INFORMATION_SCHEMA.COLUMNS " +
                            "WHERE TABLE_NAME = '" + szTableName +
                            "' AND DATA_TYPE = 'int' " +
                            "ORDER BY ORDINAL_POSITION";

            if (szTableName == string.Empty)
                return null;

            oDatabase = new SqlDatabase(szConnect);

            using (oDbCommand = oDatabase.GetSqlStringCommand(szSQL))
            {
                oDT = oDatabase.ExecuteDataSet(oDbCommand).Tables[0];
            }

            return oDT;

        }

        public static DataTable GetDataTypes()
        {
            SqlDatabase oDatabase;
            DbCommand oDbCommand;
            DataTable oDT;
            string szSQL = "SELECT name " + 
                           "FROM sys.types " + 
                           "ORDER BY name";

            oDatabase = new SqlDatabase(szConnect);

            using (oDbCommand = oDatabase.GetSqlStringCommand(szSQL))
            {
                oDT = oDatabase.ExecuteDataSet(oDbCommand).Tables[0];
            }

            return oDT;

        }

        public static void ApplySQL(string szSQL)
        {
            SqlDatabase oDatabase;
            DbCommand oDbCommand;

            oDatabase = new SqlDatabase(szConnect);

            using (oDbCommand = oDatabase.GetSqlStringCommand(szSQL))
            {
                oDatabase.ExecuteNonQuery(oDbCommand);
            }
        }

        private void cmbForeignKeyTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbForeignKey.DisplayMember = "COLUMN_NAME";
            cmbForeignKey.ValueMember = "COLUMN_NAME";
            cmbForeignKey.DataSource = GetColumns(cmbForeignKeyTable.Text);
        }

        private void cmdSaveChanges_Click(object sender, EventArgs e)
        {
            string szSQL;
            string szSourceTable = cmbSourceTable.Text;
            string szSourceTableColumn = txtColumnName.Text;
            string szForeignKeyTable = cmbForeignKeyTable.Text;
            string szForeignKeyColumn = cmbForeignKey.Text;
            string szDataType = cmbDataType.Text;

            szSQL = "ALTER TABLE " + szSourceTable + " ADD " + 
                szSourceTableColumn + " " + szDataType;

            ApplySQL(szSQL);

            szSQL = "ALTER TABLE " + szSourceTable +
                    " ADD CONSTRAINT FK_" + szSourceTable + "_" + szSourceTableColumn + 
                    " FOREIGN KEY (" + szSourceTableColumn + ") " +
                    " REFERENCES " + szForeignKeyTable + " (" + szForeignKeyColumn + ")";

            ApplySQL(szSQL);

            /*
                ALTER TABLE Employees
                ADD CONSTRAINT FK_Employees_FavoriteBeerID FOREIGN KEY (FavoriteBeerID)
                REFERENCES Dictionary (DictionaryID) ;
            */
        }
    }
}
