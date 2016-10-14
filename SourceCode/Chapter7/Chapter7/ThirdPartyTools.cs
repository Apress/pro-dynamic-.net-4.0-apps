using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
//using Syncfusion.Pdf;
//using Syncfusion.Pdf.Tables;
//using Syncfusion.Pdf.Graphics;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace Chapter7
{
    public partial class ThirdPartyTools : Form
    {

        public ThirdPartyTools()
        {
            InitializeComponent();
        }

        //NOTE: You'll need to download the demo versions of the 
        //spreadsheet tools to run these demos

        private void XLSIODemo()
        {
            //ExcelEngine oExcelEngine;
            //IWorkBook oWorkBook;
            //IWorkSheet oWS;
            //DataTable oDT;
            //int sRow = 0;

            ////Instantiate the spreadsheet creation engine
            //oExcelEngine = new ExcelEngine();
            ////Create a workbook

            //oWorkBook = oExcelEngine.Excel.Workbooks.Create();
            ////Reference the first worksheet
            //oWS = oWorkBook.Worksheets[0];

            ////Set orientation and paper size
            //oWS.PageSetup.Orientation = ExcelPageOrientation.Portrait;
            //oWS.PageSetup.PaperSize = ExcelPaperSize.PaperLetter;

            ////Set margins
            //oWS.PageSetup.LeftMargin = 0.25;
            //oWS.PageSetup.RightMargin = 0.25;
            //oWS.PageSetup.TopMargin = 1.25;
            //oWS.PageSetup.BottomMargin = 1.0;

            ////Set the first row to print at the top of every page
            //oWS.PageSetup.PrintTitleRows = "$A$1:$IV$1";

            ////Set header and footer text
            //oWS.PageSetup.LeftFooter = "Page &P of &N\n&D &T";
            //oWS.PageSetup.CenterHeader = "Sample Report";

            ////Set column widths
            //oWS.SetColumnWidth(1, 20);
            //oWS.SetColumnWidth(2, 10);

            ////Set workbook’s summary and custom document properties
            //oWorkBook.BuiltInDocumentProperties.Author = "Essential Essential XlsIO";
            //oWorkBook.CustomDocumentProperties["Today"].DateTime = DateTime.Today;

            ////Set column headers
            //oWS.Range[sRow, 1].Text = "Product";
            //oWS.Range[sRow, 2].Text = "Sales";

            ////Display headers in bold, centered, with a yellow background
            //oWS.Range[sRow, 1, sRow, 2].CellStyle.Color = Color.Yellow;
            //oWS.Range[sRow, 1, sRow, 2].CellStyle.HorizontalAlignment =
            //ExcelHAlign.HAlignCenter;
            //oWS.Range[sRow, 1, sRow, 2].CellStyle.Font.Bold = true;
            //sRow++;

            ////Get sample data, move through the results, and write data to cells
            //oDT = GetData();

            //foreach(DataRow oDR in oDT.Rows)
            //{
            //   oWS.Range[sRow, 1].Text = oDR["Product"].ToString();
            //   oWS.Range[sRow, 2].Value = oDR["Sales"].ToString();
            //   sRow++;
            //}
            //sRow++;

            ////Display total line via formula in bold
            //oWS.Range[sRow, 1].Text = "Grand Total";
            //oWS.Range[sRow, 2].Formula = "SUM(B2:B" + (sRow - 1).ToString() + ")";
            //oWS.Range[sRow, 1, sRow, 2].CellStyle.Font.Bold = true;

            ////Format Sales column
            //oWS.Range[2, 2, sRow, 2].NumberFormat = "0.00";
            //oWorkBook.SaveAs(@"c:\temp\sample.xls");
            //oWorkBook.Close();

        }

        private void XLSIOTemplateDemo()
        {
            //ExcelEngine oExcelEngine;
            //IWorkBook oWorkBook;
            //IWorkSheet oWS;
            //DataTable oDT;
            //int sRow = 0;

            ////Instantiate the spreadsheet creation engine
            //oExcelEngine = new ExcelEngine();

            ////Create a workbook
            //oWorkBook = oExcelEngine.Excel.Workbooks.Open(@"..\..\Data\Template.xls", ExcelOpenType.Automatic);

            ////Reference the first worksheet
            //oWS = oWorkBook.Worksheets[0];

            ////Get sample data
            //oDT = GetTable();

            ////Import DataTable into worksheet
            //oWS.ImportDataTable(oDT,false,2,1,-1,-1,false);

            ////Display total line via formula in bold
            //oWS.Range[oDT.Rows.Count+1, 1].Text = "Grand Total";
            //oWS.Range[oDT.Rows.Count+1, 2].Formula =
            //   "SUM(B2:B" + (oDT.Rows.Count - 1).ToString() + ")";
            //oWS.Range[oDT.Rows.Count+1, 1, oDT.Rows.Count+1, 2].CellStyle.Font.Bold = true;

        }

        private void ExcelWriter()
        {
            //ExcelApplication oExcelApplication;
            //Workbook oWB;
            //Worksheet oWS;
            //GlobalStyle oGlobalStyle;
            //Area oArea;
            //DataTable oDT;
            //int sRow = 0;

            ////Create a workbook
            //oExcelApplication = new ExcelApplication();
            //oWB = oExcelApplication.Create();

            ////Reference the first worksheet
            //oWS = oWB.Worksheets[0];

            ////Set orientation and paper size
            //oWS.PageSetup.Orientation = PageSetup.PageOrientation.Portrait;
            //oWS.PageSetup.PaperSize = PageSetup.PagePaperSize.Letter;

            ////Set margins
            //oWS.PageSetup.LeftMargin = 0.25;
            //oWS.PageSetup.RightMargin = 0.25;
            //oWS.PageSetup.TopMargin = 1.25;
            //oWS.PageSetup.BottomMargin = 1.0;
            //oWS.PageSetup.Zoom = 100;

            ////Set the first row to print at the top of every page
            //oWS.PageSetup.SetPrintTitleRows(0, 2);

            ////Set header and footer text
            //oWS.PageSetup.LeftFooter = "Page &P of &N\n&D &T";
            //oWS.PageSetup.CenterHeader = "Sample Report";

            ////Set column widths
            //oWS.GetColumnProperties(0).Width = 100;
            //oWS.GetColumnProperties(1).Width = 50;

            //            //Set column headers
            //oWS.Cells[sRow, 0].Value = "Product";
            //oWS.Cells[sRow, 1].Value = "Sales";
            ////Display headers in bold, centered, with a yellow background
            //oWS.Cells[sRow, 0].Style.BackgroundColor = Color.SystemColor.Yellow;
            //oWS.Cells[sRow, 0].Style.HorizontalAlignment = Style.HAlign.Center;
            //oWS.Cells[sRow, 0].Style.Font.Bold = true;
            //oWS.Cells[sRow, 1].Style.BackgroundColor = Color.SystemColor.Yellow;
            //oWS.Cells[sRow, 1].Style.HorizontalAlignment = Style.HAlign.Center;
            //oWS.Cells[sRow, 1].Style.Font.Bold = true;
            //sRow++;

            ////Get sample data, move through the results, and write data to cells
            //oDT = GetData();
            //foreach(DataRow oDR in oDT.Rows)
            //{
            //   oWS.Cells[sRow, 0].Value = oDR["Product"].ToString();
            //   oWS.Cells[sRow, 1].Value = int.Parse(oDR["Sales"].ToString());
            //  sRow++;
            //}
            //sRow++;

            ////Display total line via formula in bold
            //oWS.Cells[sRow, 0].Value = "Grand Total";
            //oWS.Cells[sRow, 1].Formula = "=SUM(B2:B" + (sRow - 1).ToString() + ")";
            //oWS.Cells[sRow, 0].Style.Font.Bold = true;
            //oWS.Cells[sRow, 1].Style.Font.Bold = true;

            ////Format Sales column
            //oGlobalStyle = oWB.CreateStyle();
            //oGlobalStyle.NumberFormat = "0.00";
            //oArea = oWS.CreateArea(1, 1, sRow, 1);
            //oArea.SetStyle(oGlobalStyle);
            //oExcelApplication.Save(oWB, @"c:\temp\sample.xls");
            //oDT.Dispose();

        }

        private void PDFIODemo()
        {
            //PdfDocument oPdfDocument = new PdfDocument();
            //PdfPage oPdfPage = oPdfDocument.Pages.Add();
            //PdfLightTable oPdfLightTable = new PdfLightTable();
            //PdfLayoutFormat oPdfLayoutFormat = new PdfLayoutFormat();

            ////Get the data
            //oPdfLightTable.DataSource = GetData();

            ////Dynamically add the header...
            //oPdfLightTable.Style.ShowHeader = true;

            ////made up of the column names...
            //oPdfLightTable.Style.HeaderSource = PdfHeaderSource.ColumnCaptions;

            ////and repeat on every page
            //oPdfLightTable.Style.RepeatHeader = true;

            ////Set layout properties
            //oPdfLayoutFormat.Break = PdfLayoutBreakType.FitElement;

            ////Draw table
            //oPdfLightTable.Draw(oPdfPage, new PointF(0, 10), oPdfLayoutFormat);

            ////Save to disk
            //oPdfDocument.Save("Sample.pdf");

            //System.Diagnostics.Process.Start("Sample.pdf");

        }

        private DataTable GetData()
        {
            SqlDatabase oDatabase;
            DbCommand oDbCommand;
            DataTable oDT;

            oDatabase = new SqlDatabase("Data Source=SETON-NOTEBOOK;Initial catalog=Northwind;Integrated security=SSPI;Persist security info=False");

            using (oDbCommand = oDatabase.GetStoredProcCommand("Ten Most Expensive Products"))
            {
                oDT = oDatabase.ExecuteDataSet(oDbCommand).Tables[0];
            }

            return oDT;

        }

        private void iTextSharp()
        {
            SqlDatabase oSqlDatabase = new SqlDatabase("Data Source=SETON-NOTEBOOK;Initial catalog=Northwind;Integrated security=SSPI;Persist security info=False");
            Document oDocument = new Document();
            PdfWriter oPdfWriter;
            PdfPTable oPdfPTable;
            DataTable oDT;

            oDT = GetData();

            oPdfWriter = PdfWriter.GetInstance(oDocument,
                new FileStream(@"c:\temp\Chap0101.pdf", FileMode.Create));

            //Assign instance of object which handles page events
            oPdfWriter.PageEvent = new PDFPageEvent(oDT);

            oDocument.Open();

            //Create a PdfPTable to with enough columns to handle the 
            //columns in the DataTable
            oPdfPTable = new PdfPTable(oDT.Columns.Count);

            //Add the data to the PdfPTable dynamically
            foreach (DataRow oDR in oDT.Rows)
            {
                foreach (DataColumn oDC in oDT.Columns)
                {
                    oPdfPTable.AddCell(oDR[oDC.ColumnName].ToString());
                }
            }

            //Add the table to the document object
            oDocument.Add(oPdfPTable);

            oDocument.Close();

        }

        private void ThirdPartyTools_Load(object sender, EventArgs e)
        {
            iTextSharp();
        }


    }

    public class PDFPageEvent : IPdfPageEvent
    {
        DataTable _oDT;

        public PDFPageEvent(DataTable oDT)
        {
            _oDT = oDT;
        }

        private void DisplayHeader(PdfWriter oWriter, Document oDocument)
        {
            PdfPTable oPdfPTable = new PdfPTable(_oDT.Columns.Count);

            foreach (DataColumn oDC in _oDT.Columns)
            {
                oPdfPTable.AddCell(oDC.ColumnName.ToString());
            }

            oDocument.Add(oPdfPTable);
        }

        public void OnOpenDocument(PdfWriter oWriter, Document oDocument)
        {
        }

        public void OnStartPage(PdfWriter oWriter, Document oDocument)
        {
            DisplayHeader(oWriter, oDocument);
        }

        public void OnEndPage(PdfWriter oWriter, Document oDocument)
        {
        }

        public void OnCloseDocument(PdfWriter oWriter, Document oDocument)
        {
        }

        public void OnParagraph(PdfWriter oWriter, Document oDocument,
            float paragraphPosition)
        {
        }

        public void OnParagraphEnd(PdfWriter oWriter, Document oDocument,
            float paragraphPosition)
        {
        }

        public void OnChapter(PdfWriter oWriter, Document oDocument,
            float paragraphPosition, Paragraph title)
        {
        }

        public void OnChapterEnd(PdfWriter oWriter, Document oDocument,
            float paragraphPosition)
        {
        }

        public void OnSection(PdfWriter oWriter, Document oDocument,
            float paragraphPosition, int depth, Paragraph title)
        {
        }

        public void OnSectionEnd(PdfWriter oWriter, Document oDocument,
            float paragraphPosition)
        {
        }

        public void OnGenericTag(PdfWriter oWriter, Document oDocument,
            Rectangle rect, string text)
        {
        }
    }

}
