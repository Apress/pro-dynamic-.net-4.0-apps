using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.Enterprise; 

namespace Chapter7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private DataTable GetFieldList()
        {
            SqlDatabase oDatabase;
            DbCommand oDbCommand;
            DataTable oDT;

            /*
            CREATE PROCEDURE [dbo].[spc_GetFieldList]

            AS

            SELECT * FROM fn_listextendedproperty (Null, 'schema', 'dbo', 'table', 'Employees', 'column', Null);
            */

            oDatabase = new SqlDatabase("Data Source=SETON-NOTEBOOK;Initial catalog=Northwind;Integrated security=SSPI;Persist security info=False");

            using (oDbCommand = oDatabase.GetStoredProcCommand("spc_GetFieldList"))
            {
                oDT = oDatabase.ExecuteDataSet(oDbCommand).Tables[0];
            }

            return oDT;

        }

        //private DataSet1 ExecuteSQL(string szSQL)
        //{
        //    SqlDatabase oDatabase;
        //    DbCommand oDbCommand;
        //    DataSet1 oDS;

        //    oDatabase = new SqlDatabase("Data Source=SETON-NOTEBOOK;Initial catalog=Northwind;Integrated security=SSPI;Persist security info=False");

        //    using (oDbCommand = oDatabase.GetSqlStringCommand(szSQL))
        //    {
        //        oDS = ((DataSet1) oDatabase.ExecuteDataSet(oDbCommand));
        //    }

        //    return oDS;

        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable oDT = GetFieldList();

            lstColumns.DisplayMember = "Text";
            lstColumns.ValueMember = "Value";

            lstColumns.CheckOnClick = true;

            foreach (DataRow oDR in oDT.Rows)
            {
                lstColumns.Items.Add(new ListItem(oDR["objname"].ToString(), oDR["value"].ToString()));
            }

        }

        private void cmdRunReport_Click(object sender, EventArgs e)
        {
            StringBuilder oSQL = new StringBuilder("SELECT ");

            //iterate the checked items and add them to the column list
            foreach (object oItem in lstColumns.CheckedItems)
            {
                oSQL.Append(((ListItem)oItem).Value);
                oSQL.Append(", ");
            }

            //Remove the trailing comma
            oSQL.Remove(oSQL.Length - 2, 2);

            oSQL.Append(" FROM Employees");
        }

        private void cmdSaveSelections_Click(object sender, EventArgs e)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXMLMainNode;
            XmlNode oXMLNode;
            XmlAttribute oXmlAttribute;

            oXmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);

            oXMLMainNode = oXmlDocument.CreateNode(XmlNodeType.Element,
                "Columns", string.Empty);
            oXmlDocument.AppendChild(oXMLMainNode);

            //iterate the checked items and add them to the column list
            foreach (object oItem in lstColumns.CheckedItems)
            {
                oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element, "ColumnName", string.Empty);
                oXMLMainNode.AppendChild(oXMLNode);

                oXmlAttribute = oXmlDocument.CreateAttribute("name");
                oXmlAttribute.Value = ((ListItem)oItem).Value;
                oXMLNode.Attributes.Append(oXmlAttribute);
            }

            oXmlDocument.Save(@"c:\temp\columns.xml");
            }

        private void cmdRunCrystalReport_Click(object sender, EventArgs e)
        {
            //DynamicCrystalReportSDK();
        }

        //private string DynamicCrystalReportSDK()
        //{
            //CrystalDecisions.CrystalReports.Engine.ReportDocument oReportDocument;
            //ISCDReportClientDocument oReportClientDocument;
            //Section oSection;
            //FontColor oDetailHeaderTextFont;
            //FontColor oDetailHeaderFieldFont;
            //System.Data.DataSet oDS;
            //string szSQL;
            //int iLeft = 10;
            //int iTop = 1;
            //int iWidth = 2000;
            //int iHeight = 200;

            ////Define font objects
            //oDetailHeaderTextFont = CreateFont("Arial", 10, false, true, false, false, System.Drawing.Color.Black);
            //oDetailHeaderFieldFont = CreateFont("Arial", 10, false, false, false, false, System.Drawing.Color.Black);

            ////Create a new ReportDocument
            //oReportDocument = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

            ////Access the ReportClientDocument in the ReportDocument 
            //oReportClientDocument = oReportDocument.ReportClientDocument;

            ////create new report document
            //oReportClientDocument.New();

            ////Iterate through the selected columns to build an SQL string
            //szSQL = BuildSQL();

            ////Add the data source to the report. This is needed so the data 
            ////structure is available when the field objects are added.
            //oDS = AddTable(oReportClientDocument,
            //               szConnectString,
            //               szSQL);

            ////Iterate through the selected columns
            //foreach (object oItem in lstColumns.CheckedItems)
            //{
            //    //Obtain a reference to the page header section
            //    oSection = oReportClientDocument.ReportDefinition.PageHeaderArea.Sections[0];

            //    //Add a text object as a column header
            //    AddTextField(oReportClientDocument, oSection, oDetailHeaderTextFont,
            //        CrAlignmentEnum.crAlignmentLeft, ((ListItem)oItem).Text,
            //        iLeft, iTop, iWidth, iHeight);

            //    //Obtain a reference to the report details section
            //    oSection = oReportClientDocument.ReportDefinition.DetailArea.Sections[0];

            //    //Add the data field bound to a data source column
            //    AddField(oReportClientDocument, oSection, oDetailHeaderFieldFont,
            //        CrAlignmentEnum.crAlignmentLeft, "Table", ((ListItem)oItem).Value,
            //        iLeft, iTop, iWidth, iHeight);

            //    iLeft += (iWidth + 20);

            //}

            //oReportDocument.SetDataSource(oDS);

            //crystalReportViewer1.ReportSource = oReportDocument;

        //}

        private void cmdDynamicCrystalReport_Click(object sender, EventArgs e)
        {
            DynamicCrystalReports();
        }

        private string DynamicCrystalReports()
        {
            DynamicCrystalReport oDynamicCrystalReport;
            ParameterFields oParameterFields;
            ParameterField oParameterField;
            ParameterDiscreteValue oParameterDiscreteValue;
            StringBuilder oSQL = new StringBuilder("SELECT ");
            DataSet1 oDS = new DataSet1();
            SqlConnection oSqlConnection;
            SqlDataAdapter oSqlDataAdapter;
            SqlCommand oSqlCommand;
            int iColumn = 1;
            int iColCnt = 1;

            oDynamicCrystalReport = new DynamicCrystalReport();
            oParameterFields = new ParameterFields();

            //Assign the description text to the parameter objects for the selected 
            //fields. The iColumn variable assigns the parameters in sequence based on name.
            foreach (object oItem in lstColumns.CheckedItems)
            {
                oParameterField = new ParameterField();
                oParameterField.Name = "DataColumn" + iColumn.ToString();
                oParameterDiscreteValue = new ParameterDiscreteValue();
                oParameterDiscreteValue.Value = ((ListItem)oItem).Text;
                oParameterField.CurrentValues.Add(oParameterDiscreteValue);
                oParameterFields.Add(oParameterField);

                oSQL.Append(((ListItem)oItem).Value + " AS DColumn" + iColumn.ToString());
                oSQL.Append(", ");

                iColumn++;
            }

            //There are 10 hard-coded columns in this report. Place an empty string
            //in the header fields for the unused columns
            iColCnt = oDynamicCrystalReport.ParameterFields.Count;

            for (int i = iColumn; i <= iColCnt; i++)
            {
                oParameterField = new ParameterField();
                oParameterField.Name = "DataColumn" + iColumn.ToString();
                oParameterDiscreteValue = new ParameterDiscreteValue();
                oParameterDiscreteValue.Value = string.Empty;
                oParameterField.CurrentValues.Add(oParameterDiscreteValue);
                oParameterFields.Add(oParameterField);

                iColumn++;
            }

            //Pass in the collection of 10 named parameter objects
            crystalReportViewer1.ParameterFieldInfo = oParameterFields;

            //Whack the trailing comma
            oSQL.Remove(oSQL.Length - 2, 2);

            oSQL.Append(" FROM Employees");

            //Execute the SQL
            oSqlConnection = new SqlConnection("Data Source=SETON-NOTEBOOK;Initial catalog=Northwind;" +
                    "Integrated security=SSPI;Persist security info=False");
            oSqlConnection.Open();

            oSqlDataAdapter = new SqlDataAdapter();
            oSqlCommand = new SqlCommand();

            oSqlCommand.CommandText = oSQL.ToString();
            oSqlCommand.Connection = oSqlConnection;
            oSqlDataAdapter.SelectCommand = oSqlCommand;

            //Make sure the table name matches the one used in DataSet1.Designer.cs.
            //DataTable1 is the default name. If you fail to name it properly, the 
            //headers will appear but the report data will not.
            oSqlDataAdapter.Fill(oDS, "DataTable1");

            //Assign the DataTable to the report
            oDynamicCrystalReport.SetDataSource(oDS);
            crystalReportViewer1.ReportSource = oDynamicCrystalReport;

            return oSQL.ToString();
        }

        //private void AddField(ISCDReportClientDocument oReportClientDocument,
        //                     Section oSection,
        //                     FontColor oFontColor,
        //                     CrAlignmentEnum sAlignmentEnum,
        //                     string szTableName,
        //                     string szFieldName,
        //                     int iLeft,
        //                     int iTop,
        //                     int iWidth,
        //                     int iHeight)
        //{
        //    //ISCRTable oISCRTable;
        //    //ISCRField oField;
        //    //ISCRReportObject oISCRReportObject;
        //    //FieldObject oFieldObject;
        //    //CrystalDecisions.ReportAppServer.DataDefModel.Table oTable;

        //    //oISCRTable = oReportClientDocument.Database.
        //    //    Tables.FindTableByAlias(szTableName);

        //    ////Extract the table or stored procedure and cast it to a Table object
        //    //oTable = ((CrystalDecisions.ReportAppServer.DataDefModel.Table)oISCRTable);

        //    ////Cast this field reference to a Field object
        //    //oField = ((Field)oTable.DataFields.FindField(szFieldName,
        //    //    CrFieldDisplayNameTypeEnum.crFieldDisplayNameName,
        //    //    CrystalDecisions.ReportAppServer.DataDefModel.
        //    //    CeLocale.ceLocaleUserDefault));

        //    //////Instantiate a FieldObject and set the properties 
        //    //////and display the data and position on the page.
        //    //oFieldObject = new FieldObject();
        //    //oFieldObject.Kind = CrReportObjectKindEnum.crReportObjectKindField;
        //    //oFieldObject.DataSource = oField.FormulaForm;
        //    //oFieldObject.Left = iLeft;
        //    //oFieldObject.Top = iTop;
        //    //oFieldObject.Width = iWidth;
        //    //oFieldObject.Height = iHeight;
        //    //oFieldObject.FieldValueType = oField.Type;
        //    //oFieldObject.FontColor = oFontColor;
        //    //oFieldObject.Format.HorizontalAlignment = sAlignmentEnum;

        //    //oISCRReportObject = ((ISCRReportObject)oFieldObject);

        //    //////Add the field to the report
        //    //oReportClientDocument.ReportDefController.ReportObjectController.
        //    //    Add(oISCRReportObject, oSection, 0);

        //}

        //private void AddTextField(ISCDReportClientDocument oReportClientDocument,
        //                            Section oSection,
        //                            FontColor oFontColor,
        //                            CrAlignmentEnum sAlignmentEnum,
        //                            string szText,
        //                            int iLeft,
        //                            int iTop,
        //                            int iWidth,
        //                            int iHeight)
        //{
        //    //TextObject oTextObject;
        //    //ISCRReportObject oISCRReportObject;
        //    //Paragraphs oParagraphs;
        //    //Paragraph oParagraph;
        //    //ParagraphElements oParagraphElements;
        //    //ParagraphTextElement oParagraphTextElement;

        //    ////Instantiate the necessary objects
        //    //oTextObject = new TextObject();
        //    //oParagraphs = new Paragraphs();
        //    //oParagraph = new Paragraph();
        //    //oParagraphElements = new ParagraphElements();
        //    //oParagraphTextElement = new ParagraphTextElement();

        //    ////Set the displayed text to the ParagraphTextElement object
        //    //oParagraphTextElement.Text = szText;
        //    //oParagraphTextElement.Kind = CrParagraphElementKindEnum.
        //    //    crParagraphElementKindText;

        //    ////Add the ParagraphTextElement to the ParagraphTextElements collection
        //    //oParagraphElements.Add(oParagraphTextElement);

        //    ////Set the ParagraphTextElements collection to the ParagraphElements 
        //    ////property of the Paragraph object and set the text alignment
        //    //oParagraph.ParagraphElements = oParagraphElements;
        //    //oParagraph.Alignment = sAlignmentEnum;

        //    ////Add the Paragraph object to the Paragraphs collection
        //    //oParagraphs.Add(oParagraph);

        //    ////Set up the TextObject by assigning the Paragraphs collection object to
        //    ////its Paragraphs property. Also, set the size and position.
        //    //oTextObject.Kind = CrReportObjectKindEnum.crReportObjectKindText;
        //    //oTextObject.Paragraphs = oParagraphs;
        //    //oTextObject.Left = iLeft;
        //    //oTextObject.Top = iTop;
        //    //oTextObject.Width = iWidth;
        //    //oTextObject.Height = iHeight;
        //    //oTextObject.FontColor = oFontColor;

        //    //oISCRReportObject = ((ISCRReportObject)oTextObject);

        //    ////Finally, add the TextObject to the report
        //    //oReportClientDocument.ReportDefController.ReportObjectController.
        //    //    Add(oISCRReportObject, oSection, -1);
        //}

    }

    public class ListItem
    {
        public string Value {get; set;}
        public string Text {get; set;}

        public ListItem(string szValue, string szText)
        {
            this.Value = szValue;
            this.Text = szText;
        }

        public new string ToString()
        {
            return this.Text;
        }

        public string GetText()
        {
            return this.Text;
        }

        public string GetValue()
        {
            return this.Value;
        }
    }

}
