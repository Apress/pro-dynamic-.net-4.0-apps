using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Security.Principal;
using System.Net.Mail;
using Syncfusion.XlsIO;
using System.ComponentModel;
using System.Reflection;
using Telerik.WinControls.UI;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace InnovatixCommon
{
    public enum ControlType
    {
        CheckBox = 1,
        ComboBox = 2,
        CheckedListBox = 3,
        ListBox = 4,
        RadioButton = 5,
        TextBox = 6,
        DateTimePicker = 7
    }

    public class GridInfo
    {
        private List<GridColumn> _oList;
        public string _szName;
        public string _szSortedColumn;
        public ListSortDirection _sListSortDirection;

        public string Name
        {
            get { return _szName; }
            set { _szName = value; }
        }

        public string SortedColumn
        {
            get { return _szSortedColumn; }
            set { _szSortedColumn = value; }
        }

        public ListSortDirection ListSortDirection
        {
            get { return _sListSortDirection; }
            set { _sListSortDirection = value; }
        }

        public GridInfo()
        {
            _oList = new List<GridColumn>();
        }

        public GridInfo(DataGridView oDataGridView)
        {
            GridColumn oGridColumn;

            _oList = new List<GridColumn>();

            this.Name = oDataGridView.Name;

            if (oDataGridView.SortedColumn == null)
                this.SortedColumn = string.Empty;
            else
                this.SortedColumn = oDataGridView.SortedColumn.Name;

            if (oDataGridView.SortOrder == SortOrder.Descending)
                this.ListSortDirection = ListSortDirection.Descending;
            else
                this.ListSortDirection = ListSortDirection.Ascending;

            foreach (DataGridViewColumn oDataGridViewColumn in oDataGridView.Columns)
            {
                oGridColumn = new GridColumn();

                oGridColumn.Name = oDataGridViewColumn.Name;
                oGridColumn.DisplayIndex = oDataGridViewColumn.DisplayIndex;
                oGridColumn.Width = oDataGridViewColumn.Width;
                oGridColumn.Visible = oDataGridViewColumn.Visible;

                this.AddColumn(oGridColumn);
            }
        }

        public List<GridColumn> Columns
        {
            get { return _oList; }
        }

        public void AddColumn(GridColumn oGridColumn)
        {
            _oList.Add(oGridColumn);
        }

        public GridColumn Column(string szColumnName)
        {
            GridColumn oColumns = null;

            foreach (GridColumn oGridColumn in this.Columns)
            {
                if (oGridColumn.Name == szColumnName)
                {
                    oColumns = oGridColumn;
                    break;
                }

            }

            return oColumns;
        }

        public SelectColumns SelectColumnsForm()
        {
            return new SelectColumns();
        }

        public void LoadFromXML(XmlDocument oXmlDocument, string szName)
        {
            if (oXmlDocument.ChildNodes.Count == 0)
                return;

            this.Name = oXmlDocument.ChildNodes[0].Attributes["name"].Value;
            this.SortedColumn = oXmlDocument.ChildNodes[0].Attributes["sortedcolumn"].Value;

            if (oXmlDocument.ChildNodes[0].Attributes["sortorder"].Value != string.Empty)
                this.ListSortDirection = (ListSortDirection)Enum.Parse(typeof(ListSortDirection), oXmlDocument.ChildNodes[0].Attributes["sortorder"].Value, true);
            else
                this.ListSortDirection = ListSortDirection.Ascending;

            foreach (XmlNode oXmlNode in oXmlDocument.ChildNodes[0].ChildNodes)
            {
                GridColumn oGridColumn = new GridColumn();

                oGridColumn.Name = oXmlNode.Attributes["name"].Value;
                oGridColumn.Visible = bool.Parse(oXmlNode.Attributes["visible"].Value);
                oGridColumn.DisplayIndex = int.Parse(oXmlNode.Attributes["displayindex"].Value);
                oGridColumn.Width = int.Parse(oXmlNode.Attributes["width"].Value);

                this.Columns.Add(oGridColumn);
            }

        }

        public XmlDocument WriteToXML()
        {
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXMLMainNode;
            XmlNode oXMLNode;

            oXmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);

            oXMLMainNode = oXmlDocument.CreateNode(XmlNodeType.Element, "DataGridView", string.Empty);
            oXmlDocument.AppendChild(oXMLMainNode);

            Common.AddXMLAttribute(oXmlDocument, oXMLMainNode, "name", this.Name);
            Common.AddXMLAttribute(oXmlDocument, oXMLMainNode, "sortedcolumn", this.SortedColumn);
            Common.AddXMLAttribute(oXmlDocument, oXMLMainNode, "sortorder", this.ListSortDirection.ToString());

            foreach (GridColumn oGridColumn in this.Columns)
            {
                oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element, "Column", string.Empty);
                oXMLMainNode.AppendChild(oXMLNode);

                Common.AddXMLAttribute(oXmlDocument, oXMLNode, "name", oGridColumn.Name);
                Common.AddXMLAttribute(oXmlDocument, oXMLNode, "displayindex", oGridColumn.DisplayIndex.ToString());
                Common.AddXMLAttribute(oXmlDocument, oXMLNode, "width", oGridColumn.Width.ToString());
                Common.AddXMLAttribute(oXmlDocument, oXMLNode, "visible", oGridColumn.Visible.ToString());
            }

            return oXmlDocument;
        }

        public void ApplyGridSettings(DataGridView oDataGridView, GridInfo oGridInfo)
        {
            DataGridViewColumn oDataGridViewColumn;

            if (this.SortedColumn.ToString() != string.Empty)
            {
                if (oDataGridView.Columns[this.SortedColumn] != null)
                    oDataGridView.Sort(oDataGridView.Columns[this.SortedColumn], this.ListSortDirection);
            }

            foreach (DataGridViewColumn oColumn in oDataGridView.Columns)
            {
                oColumn.Visible = false;
            }

            foreach (GridColumn oGridColumn in oGridInfo.Columns)
            {
                oDataGridViewColumn = oDataGridView.Columns[oGridColumn.Name];

                if (oDataGridViewColumn != null)
                {
                    oDataGridViewColumn.Visible = oGridColumn.Visible;

                    if (oDataGridView.Columns.Count > oGridColumn.DisplayIndex)
                        oDataGridViewColumn.DisplayIndex = oGridColumn.DisplayIndex;

                    oDataGridViewColumn.Width = oGridColumn.Width;
                }
            }

        }
    }

    public class GridColumn
    {
        public string Name;
        public int DisplayIndex;
        public int Width;
        public bool Visible;
    }

    public class Filters
    {
        public static void PopulateFilter(Control oControl, string szUserName, string szFormName, string szConnectionStringConfig)
        {
            XmlElement oXmlElement;
            string szControlName = oControl.Name;

            oXmlElement = GetFilterData(szUserName, szFormName, szControlName, szConnectionStringConfig);

            if (oXmlElement != null)
                LoadFilter(oControl, oXmlElement);
        }

        private static XmlElement GetFilterData(string szUserName, string szFormName, string szControlName, string szConnectionStringConfig)
        {
            DataAccess.CommonDB oCommonDB = new DataAccess.CommonDB(szConnectionStringConfig);
            DataTable oDT = oCommonDB.GetSettings(szUserName, szFormName, szControlName);
            XmlDocument oXmlDocument = new XmlDocument();
            XmlElement oXmlElement = null;

            //Load this XML into an XmlDocument object
            if (oDT.Rows.Count > 0)
            {
                oXmlDocument.LoadXml(oDT.Rows[0]["Settings"].ToString());

                //Obtain a reference to the root elemnt
                oXmlElement = oXmlDocument.DocumentElement;
            }

            return oXmlElement;
        }

        public static void ResetFilter(Control oControl)
        {
            string szControlType;
            string szCheckColumn;

            //iterate through every control in the control 
            //collection of the owner object
            foreach (Control oSubControl in oControl.Controls)
            {
                //Get the name of the control type - ListBox, CheckBox, etc.
                szControlType = oSubControl.GetType().Name;

                switch (szControlType)
                {
                    case "InnovatixDataGridView":
                    case "DataGridView":
                        szCheckColumn = ((DataGridView)oSubControl).Columns[0].Name;

                        foreach (DataGridViewRow oDataGridViewRow in ((DataGridView)oSubControl).Rows)
                        {
                            oDataGridViewRow.Cells[szCheckColumn].Value = 0;
                        }

                        break;

                    case "CheckBox":
                        ((CheckBox)oSubControl).CheckState = CheckState.Indeterminate;
                        break;

                    case "RadComboBox":
                        ((RadComboBox)oSubControl).SelectedIndex = -1;
                        break;

                    case "ComboBox":
                        ((ComboBox)oSubControl).SelectedIndex = -1;
                        break;

                    case "CheckedListBox":
                        for (int i = 0; i <= ((CheckedListBox)oSubControl).Items.Count - 1; i++)
                        {
                            ((CheckedListBox)oSubControl).SetItemChecked(i, false);
                        }

                        break;

                    case "ListBox":
                        for (int i = 0; i < ((ListBox)oSubControl).Items.Count; i++)
                        {
                            ((ListBox)oSubControl).SetSelected(i, false);
                        }

                        break;

                    case "RadioButton":
                        ((RadioButton)oSubControl).Checked = false;
                        break;

                    case "TextBox":
                        ((TextBox)oSubControl).Text = string.Empty;
                        break;

                    case "DateTimePicker":
                        ((DateTimePicker)oSubControl).Text = string.Empty;
                        break;
                }

                //Perform recursion to handle child controls
                if (oSubControl.HasChildren)
                    ResetFilter(oSubControl);
            }
        }

        private static void LoadFilter(Control oControl, XmlElement oXmlElement)
        {
            XmlNodeList oXmlNodeList;
            string szControlType;
            string szNodeQuery;
            string szCheckColumn;
            string szDataColumn;

            //iterate through every control in the control 
            //collection of the owner object
            foreach (Control oSubControl in oControl.Controls)
            {
                //Get the name of the control type - ListBox, CheckBox, etc.
                szControlType = oSubControl.GetType().Name;

                //Find the control's saved data in the XML
                szNodeQuery = "Control[@name='" + oSubControl.Name + "']";

                //Obtain a reference to that control's information
                oXmlNodeList = oXmlElement.SelectNodes(szNodeQuery);

                if (oXmlNodeList.Count > 0)
                {
                    switch (szControlType)
                    {
                        case "InnovatixDataGridView":
                        case "DataGridView":
                            if (((DataGridView)oSubControl).Columns.Count != 0)
                            {
                                szCheckColumn = ((DataGridView)oSubControl).Columns[0].Name;
                                szDataColumn = ((DataGridView)oSubControl).Columns[1].Name;

                                foreach (XmlNode oXmlNode in oXmlNodeList[0].ChildNodes)
                                {
                                    CheckDataGridViewItem(oXmlNode.InnerXml, szCheckColumn, szDataColumn, ((DataGridView)oSubControl).Rows);
                                }
                            }

                            break;

                        //case "DataGridView":
                        //    if (((DataGridView)oSubControl).Columns.Count != 0)
                        //    {
                        //        szCheckColumn = ((DataGridView)oSubControl).Columns[0].Name;
                        //        szDataColumn = ((DataGridView)oSubControl).Columns[1].Name;

                        //        foreach (XmlNode oXmlNode in oXmlNodeList[0].ChildNodes)
                        //        {
                        //            CheckDataGridViewItem(oXmlNode.InnerXml, szCheckColumn, szDataColumn, ((DataGridView)oSubControl).Rows);
                        //        }
                        //    }

                        //    break;

                        case "CheckBox":
                            ((CheckBox)oSubControl).CheckState =
                                ((CheckState)Enum.Parse(typeof(CheckState), oXmlNodeList[0].Attributes["value"].Value, true));
                            break;

                        case "RadComboBox":
                            Common.SetComboValue(((RadComboBox)oSubControl), oXmlNodeList[0].Attributes["value"].Value);
                            //CheckItem(((RadComboBox)oSubControl), oXmlNodeList[0].Attributes["value"].Value);
                            break;

                        case "ComboBox":
                            CheckItem(((ComboBox)oSubControl), oXmlNodeList[0].Attributes["value"].Value);
                            break;

                        case "CheckedListBox":
                            foreach (XmlNode oXmlNode in oXmlNodeList[0].ChildNodes)
                            {
                                CheckItem(((CheckedListBox)oSubControl), oXmlNode.InnerXml);
                            }

                            break;

                        case "ListBox":
                            foreach (XmlNode oXmlNode in oXmlNodeList[0].ChildNodes)
                            {
                                CheckItem(((ListBox)oSubControl), oXmlNode.InnerXml);
                            }

                            break;

                        case "RadioButton":
                            ((RadioButton)oSubControl).Checked = bool.Parse(oXmlNodeList[0].Attributes["value"].Value);
                            break;

                        case "TextBox":
                            ((TextBox)oSubControl).Text = oXmlNodeList[0].Attributes["value"].Value;
                            break;

                        case "DateTimePicker":
                            ((DateTimePicker)oSubControl).Text = oXmlNodeList[0].Attributes["value"].Value;
                            break;
                    }
                }

                //Perform recursion to handle child controls
                if (oSubControl.HasChildren)
                    LoadFilter(oSubControl, oXmlElement);
            }
        }

        public static void PrepareFilterGrid()
        {

        }

        #region "Check Items"

        private static void CheckItem(ComboBox oComboBox, string szValue)
        {
            int iCnt = 0;

            if (oComboBox.Items.Count > 0)
            {
                if (oComboBox.Items[0] is DataRowView)
                {
                    foreach (DataRowView oItem in oComboBox.Items)
                    {
                        if (oItem[0].ToString() == szValue)
                        {
                            oComboBox.SelectedIndex = iCnt;
                            break;
                        }

                        iCnt++;

                    }
                }
                else
                {
                    foreach (string oItem in oComboBox.Items)
                    {
                        if (oItem[0].ToString() == szValue)
                        {
                            oComboBox.SelectedIndex = iCnt;
                            break;
                        }

                        iCnt++;

                    }
                }

            }

        }

        private static void CheckItem(RadComboBox oComboBox, string szValue)
        {
            int iCnt = 0;

            if (oComboBox.Items.Count > 0)
            {
                foreach (Telerik.WinControls.RadItem oItem in oComboBox.Items)
                {
                    if (oItem.ToString() == szValue)
                    {
                        oComboBox.SelectedIndex = iCnt;
                        break;
                    }

                    iCnt++;

                }

            }

        }

        private static void CheckItem(CheckedListBox oCheckedListBox, string szValue)
        {
            int iCnt = 0;

            if (oCheckedListBox.Items.Count > 0)
            {
                if (oCheckedListBox.Items[0] is DataRowView)
                {
                    foreach (DataRowView oItem in oCheckedListBox.Items)
                    {
                        if (oItem[1].ToString() == szValue)
                        {
                            oCheckedListBox.SetItemChecked(iCnt, true);
                            break;
                        }

                        iCnt++;

                    }
                }
                else
                {
                    {
                        foreach (string oItem in oCheckedListBox.Items)
                        {
                            if (oItem.ToString() == szValue)
                            {
                                oCheckedListBox.SetItemChecked(iCnt, true);
                                break;
                            }

                            iCnt++;

                        }
                    }
                }
            }
        }

        private static void CheckItem(ListBox oListBox, string szValue)
        {
            int iCnt = 0;

            if (oListBox.Items.Count > 0)
            {
                if (oListBox.Items[0] is DataRowView)
                {
                    foreach (DataRowView oItem in oListBox.Items)
                    {
                        if (oItem[1].ToString() == szValue)
                        {
                            oListBox.SetSelected(iCnt, true);
                            break;
                        }

                        iCnt++;

                    }
                }
                else
                {
                    foreach (string oItem in oListBox.Items)
                    {
                        if (oItem[1].ToString() == szValue)
                        {
                            oListBox.SetSelected(iCnt, true);
                            break;
                        }

                        iCnt++;

                    }
                }
            }
        }

        private static void CheckDataGridViewItem(string szValue, string szCheckColumn, string szDataColumn, DataGridViewRowCollection oDataGridViewRowCollection)
        {
            foreach (DataGridViewRow oDataGridViewRow in oDataGridViewRowCollection)
            {
                if (oDataGridViewRow.Cells[szDataColumn].Value != null)
                {
                    if (oDataGridViewRow.Cells[szDataColumn].Value.ToString() == szValue)
                    {
                        oDataGridViewRow.Cells[szCheckColumn].Value = 1; // "True";
                        break;
                    }
                }
            }
        }

        #endregion

        public static void CommitFilter(Control oControl, string szUserName, string szFormName, string szConnectionStringConfig)
        {
            DataAccess.CommonDB oCommonDB = new DataAccess.CommonDB(szConnectionStringConfig);
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXMLMainNode;

            oXmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);

            oXMLMainNode = oXmlDocument.CreateNode(XmlNodeType.Element,
                "Controls", string.Empty);
            oXmlDocument.AppendChild(oXMLMainNode);

            oXmlDocument = SaveFilter(oControl, oXmlDocument, oXMLMainNode);

            oCommonDB.SaveSettings(szUserName, szFormName, oControl.Name, oXmlDocument.InnerXml);

        }

        public static XmlDocument SaveFilter(Control oControl,
                                             XmlDocument oXmlDocument,
                                             XmlNode oXMLMainNode)
        {
            string szControlType;
            string szName;
            string szValue;
            string szCheckColumn;
            string szDataColumn;
            object oSelectedItem;

            foreach (Control oSubControl in oControl.Controls)
            {
                //Get the name of the control type - ListBox, CheckBox, etc.
                szControlType = oSubControl.GetType().Name;

                //Get the name of the control to serve as a unique identifier
                szName = oSubControl.Name;

                switch (szControlType)
                {
                    case "InnovatixDataGridView":
                        if (((DataGridView)oSubControl).Columns.Count > 0)
                        {
                            szCheckColumn = ((DataGridView)oSubControl).Columns[0].Name;
                            szDataColumn = ((DataGridView)oSubControl).Columns[1].Name;
                            oXmlDocument = AddDataGridViewItems(oXmlDocument, oXMLMainNode, szControlType, szName, szCheckColumn, szDataColumn, ((DataGridView)oSubControl).Rows);
                        }
                        break;

                    case "DataGridView":
                        if (((DataGridView)oSubControl).Columns.Count > 0)
                        {
                            szCheckColumn = ((DataGridView)oSubControl).Columns[0].Name;
                            szDataColumn = ((DataGridView)oSubControl).Columns[0].Name;
                            oXmlDocument = AddDataGridViewItems(oXmlDocument, oXMLMainNode, szControlType, szName, szCheckColumn, szDataColumn, ((DataGridView)oSubControl).Rows);
                        }
                        break;

                    case "CheckBox":
                        oXmlDocument = AddNode(oXmlDocument, oXMLMainNode, szControlType,
                            szName, ((CheckBox)oSubControl).CheckState.ToString());
                        break;

                    case "RadComboBox":
                        oSelectedItem = (((RadComboBox)oSubControl)).SelectedItem;
                        if (oSelectedItem != null)
                            szValue = ((RadComboBoxItem)oSelectedItem).Value.ToString(); //((ListItem)oSelectedItem).Value;
                        else
                            szValue = string.Empty;

                        oXmlDocument = AddNode(oXmlDocument, oXMLMainNode, szControlType, szName, szValue);
                        break;

                    case "ComboBox":
                        oSelectedItem = (((ComboBox)oSubControl)).SelectedItem;
                        if (oSelectedItem != null)
                            szValue = ((DataRowView)oSelectedItem)[0].ToString(); //((ListItem)oSelectedItem).Value;
                        else
                            szValue = string.Empty;

                        oXmlDocument = AddNode(oXmlDocument, oXMLMainNode, szControlType, szName, szValue);
                        break;

                    case "CheckedListBox":
                        oXmlDocument = AddCheckedListBoxItems(oXmlDocument, oXMLMainNode, szControlType,
                            szName, ((CheckedListBox)oSubControl).CheckedItems);
                        break;

                    case "ListBox":
                        oXmlDocument = AddListBoxItems(oXmlDocument, oXMLMainNode, szControlType, szName, ((ListBox)oSubControl).SelectedItems);
                        break;

                    case "RadioButton":
                        oXmlDocument = AddNode(oXmlDocument, oXMLMainNode, szControlType, szName, ((RadioButton)oSubControl).Checked.ToString());
                        break;

                    case "TextBox":
                        oXmlDocument = AddNode(oXmlDocument, oXMLMainNode, szControlType, szName, ((TextBox)oSubControl).Text);
                        break;

                    case "DateTimePicker":
                        oXmlDocument = AddNode(oXmlDocument, oXMLMainNode, szControlType, szName, ((DateTimePicker)oSubControl).Text);
                        break;
                }

                //Perform recursion to handle child controls
                if (oSubControl.HasChildren)
                    oXmlDocument = SaveFilter(oSubControl, oXmlDocument, oXMLMainNode);

            }

            return oXmlDocument;
        }

        #region "Add XML Nodes"

        private static XmlDocument AddCheckedListBoxItems(XmlDocument oXmlDocument,
                            XmlNode oXMLMainNode,
                            string szControlType,
                            string szName,
                            CheckedListBox.CheckedItemCollection oItems)
        {
            XmlNode oXMLNode;
            XmlNode oXMLItemNode;

            oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element, "Control", string.Empty);
            oXMLMainNode.AppendChild(oXMLNode);

            Common.AddXMLAttribute(oXmlDocument, oXMLNode, "type", szControlType);
            Common.AddXMLAttribute(oXmlDocument, oXMLNode, "name", szName);

            if (oItems.Count != 0)
            {
                if (oItems[0] is DataRowView)
                {
                    foreach (DataRowView oItem in oItems)
                    {
                        oXMLItemNode = oXmlDocument.CreateNode(XmlNodeType.Element, "value", string.Empty);
                        oXMLItemNode.InnerText = oItem[1].ToString();
                        oXMLNode.AppendChild(oXMLItemNode);
                    }
                }
                else
                {
                    foreach (string oItem in oItems)
                    {
                        oXMLItemNode = oXmlDocument.CreateNode(XmlNodeType.Element, "value", string.Empty);
                        oXMLItemNode.InnerText = oItem;
                        oXMLNode.AppendChild(oXMLItemNode);
                    }
                }
            }

            return oXmlDocument;
        }

        private static XmlDocument AddListBoxItems(XmlDocument oXmlDocument,
                    XmlNode oXMLMainNode,
                    string szControlType,
                    string szName,
                    ListBox.SelectedObjectCollection oItems)
        {
            XmlNode oXMLNode;
            XmlNode oXMLItemNode;

            oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element, "Control", string.Empty);
            oXMLMainNode.AppendChild(oXMLNode);

            Common.AddXMLAttribute(oXmlDocument, oXMLNode, "type", szControlType);
            Common.AddXMLAttribute(oXmlDocument, oXMLNode, "name", szName);

            foreach (ListItem oItem in oItems)
            {
                oXMLItemNode = oXmlDocument.CreateNode(XmlNodeType.Element, "value", string.Empty);
                oXMLItemNode.InnerText = oItem.Value.ToString();
                oXMLNode.AppendChild(oXMLItemNode);
            }

            return oXmlDocument;
        }

        private static XmlDocument AddDataGridViewItems(XmlDocument oXmlDocument,
                    XmlNode oXMLMainNode,
                    string szControlType,
                    string szName,
                    string szCheckColumn,
                    string szDataColumn,
                    DataGridViewRowCollection oDataGridViewRowCollection)
        {
            XmlNode oXMLNode;
            XmlNode oXMLItemNode;

            oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element, "Control", string.Empty);
            oXMLMainNode.AppendChild(oXMLNode);

            Common.AddXMLAttribute(oXmlDocument, oXMLNode, "type", szControlType);
            Common.AddXMLAttribute(oXmlDocument, oXMLNode, "name", szName);

            foreach (DataGridViewRow oDataGridViewRow in oDataGridViewRowCollection)
            {
                if (oDataGridViewRow.Cells[szCheckColumn].Value != null)
                {
                    if (oDataGridViewRow.Cells[szCheckColumn].Value.ToString() == "True" || oDataGridViewRow.Cells[szCheckColumn].Value.ToString() == "1")
                    {
                        oXMLItemNode = oXmlDocument.CreateNode(XmlNodeType.Element, "value", string.Empty);
                        oXMLItemNode.InnerText = oDataGridViewRow.Cells[szDataColumn].Value.ToString();
                        oXMLNode.AppendChild(oXMLItemNode);
                    }
                }
            }

            return oXmlDocument;
        }

        private static XmlDocument AddNode(XmlDocument oXmlDocument,
                                    XmlNode oXMLMainNode,
                                    string szControlType,
                                    string szName,
                                    string szValue)
        {
            XmlNode oXMLNode;

            oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element,
                "Control", string.Empty);
            oXMLMainNode.AppendChild(oXMLNode);

            Common.AddXMLAttribute(oXmlDocument, oXMLNode, "type", szControlType);
            Common.AddXMLAttribute(oXmlDocument, oXMLNode, "name", szName);
            Common.AddXMLAttribute(oXmlDocument, oXMLNode, "value", szValue);

            return oXmlDocument;
        }

        #endregion

    }

    public class ListItem
    {
        string _szValue;
        string _szText;
        string _szOtherText;

        public ListItem(string szValue, string szText)
        {
            _szValue = szValue;
            _szText = szText;
        }

        public ListItem(string szValue, string szText, string szOtherText)
        {
            _szValue = szValue;
            _szText = szText;
            _szOtherText = szOtherText;
        }

        public new string ToString()
        {
            return _szText;
        }

        public string GetText()
        {
            return _szText;
        }

        public string Value
        {
            get { return _szValue; }
        }

        public string Text
        {
            get { return _szText; }
        }

        public string OtherText
        {
            get { return _szOtherText; }
        }

    }

    public class ExcelExport
    {
        public void Export(DataGridView oDataGridView, string fileName)
        {
            IWorkbook oWorkbook;
            IWorksheet oWS;
            List<string> oVisibleList = new List<string>();
            int iColumn = 1;
            int iCol = 1;
            int iRow = 2;
            int iColCnt = oDataGridView.Columns.Count;

            oWorkbook = ExcelUtils.CreateWorkbook(1);

            oWS = oWorkbook.Worksheets[0];

            //Display headers for all visible columns in first row in bold
            foreach (DataGridViewColumn oDataGridViewColumn in oDataGridView.Columns)
            {
                if (oDataGridViewColumn.Visible &&
                    !oDataGridViewColumn.GetType().Name.Equals("DataGridViewCheckBoxColumn"))
                {
                    oVisibleList.Add(oDataGridViewColumn.Name);
                    oWS.Range[1, iCol].Text = oDataGridViewColumn.HeaderText;
                    oWS.Range[1, iCol].CellStyle.Font.Bold = true;
                    iCol++;
                }
            }

            foreach (DataGridViewRow oDataGridViewRow in oDataGridView.Rows)
            {
                iColumn = 1;

                for (iCol = 0; iCol < iColCnt; iCol++)
                {
                    DataGridViewColumn oDataGridViewColumn = oDataGridView.Columns[oDataGridViewRow.Cells[iCol].ColumnIndex];

                    if (oVisibleList.Contains(oDataGridViewColumn.Name))
                    {
                        if (oDataGridViewColumn.ValueType != null)
                        {
                            if (oDataGridViewColumn.ValueType.Name == "Decimal")
                            {
                                if (oDataGridViewRow.Cells[iCol].Value.ToString() != string.Empty)
                                    oWS.Range[iRow, iColumn].Number = double.Parse(oDataGridViewRow.Cells[iCol].Value.ToString());
                            }
                            else
                                oWS.Range[iRow, iColumn].Text = oDataGridViewRow.Cells[iCol].Value.ToString();
                        }
                        else
                            oWS.Range[iRow, iColumn].Text = oDataGridViewRow.Cells[iCol].Value.ToString();

                        iColumn++;
                    }
                }

                iRow++;
            }

            for (iCol = 1; iCol < iColCnt; iCol++)
            {
                oWS.AutofitColumn(iCol);
            }

            oWorkbook.SaveAs(fileName);

        }
    }

    public class CSVExport
    {
        public void Export(DataGridView oDataGridView, string fileName)
        {
            StreamWriter streamWriter = new StreamWriter(fileName, true);
            List<string> oVisibleList = new List<string>();
            StringBuilder dataRow = new StringBuilder();
            int iCol = 1;
            int iColumn = 1;
            int iRow = 2;
            int iColCnt = oDataGridView.Columns.Count;

            foreach (DataGridViewColumn oDataGridViewColumn in oDataGridView.Columns)
            {
                if (oDataGridViewColumn.Visible &&
                    !oDataGridViewColumn.GetType().Name.Equals("DataGridViewCheckBoxColumn"))
                {
                    oVisibleList.Add(oDataGridViewColumn.Name);
                    dataRow.Append("\"");
                    dataRow.Append(oDataGridViewColumn.HeaderText);
                    dataRow.Append("\"");
                    dataRow.Append(",");
                    iCol++;
                }
            }

            dataRow.Remove(dataRow.Length - 1, 1);

            dataRow.Append("\n");

            foreach (DataGridViewRow oDataGridViewRow in oDataGridView.Rows)
            {
                iColumn = 1;

                for (iCol = 0; iCol < iColCnt; iCol++)
                {
                    DataGridViewColumn oDataGridViewColumn = oDataGridView.Columns[oDataGridViewRow.Cells[iCol].ColumnIndex];

                    if (oVisibleList.Contains(oDataGridViewColumn.Name))
                    {
                        dataRow.Append("\"");
                        dataRow.Append(oDataGridViewRow.Cells[iCol].Value.ToString());
                        dataRow.Append("\"");
                        dataRow.Append(",");

                        iColumn++;
                    }
                }

                dataRow.Remove(dataRow.Length - 1, 1);
                dataRow.Append("\n");
                iRow++;
            }

            streamWriter.WriteLine(dataRow);

            streamWriter.Flush();
            streamWriter.Close();

        }
    }

    public class XMLExport
    {
        public string Export(DataGridView oDataGridView)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode oXMLMainNode;
            XmlNode oXMLNode;
            List<string> oVisibleList = new List<string>();
            string columnName;
            int iColumn = 1;
            int iCol = 1;
            int iRow = 2;
            int iColCnt = oDataGridView.Columns.Count;
            string value;

            xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);

            oXMLMainNode = xmlDocument.CreateNode(XmlNodeType.Element, "DataGridView", string.Empty);
            xmlDocument.AppendChild(oXMLMainNode);

            foreach (DataGridViewRow oDataGridViewRow in oDataGridView.Rows)
            {
                iColumn = 1;

                oXMLNode = xmlDocument.CreateNode(XmlNodeType.Element, "DataRow", string.Empty);
                oXMLMainNode.AppendChild(oXMLNode);

                for (iCol = 0; iCol < iColCnt; iCol++)
                {
                    DataGridViewColumn oDataGridViewColumn = oDataGridView.Columns[oDataGridViewRow.Cells[iCol].ColumnIndex];
                    columnName = oDataGridView.Columns[iCol].HeaderText.Replace(" ", string.Empty);

                    if (columnName != string.Empty)
                    {
                        if (oDataGridViewRow.Cells[iCol].Value == null)
                            value = string.Empty;
                        else
                            value = oDataGridViewRow.Cells[iCol].Value.ToString();

                        Common.AddXMLAttribute(xmlDocument, oXMLNode, columnName, value);
                    }
                }

                iRow++;
            }

            //xmlDocument.Save(@"c:\temp\data.xml");

            return xmlDocument.InnerXml;

        }
    }

    public class TiffUtil
    {
        #region Variable & Class Definitions

        private static System.Drawing.Imaging.ImageCodecInfo tifImageCodecInfo;

        private static EncoderParameter tifEncoderParameterMultiFrame;
        private static EncoderParameter tifEncoderParameterFrameDimensionPage;
        private static EncoderParameter tifEncoderParameterFlush;
        private static EncoderParameter tifEncoderParameterCompression;
        private static EncoderParameter tifEncoderParameterLastFrame;
        private static EncoderParameter tifEncoderParameter24BPP;
        private static EncoderParameter tifEncoderParameter1BPP;

        private static EncoderParameters tifEncoderParametersPage1;
        private static EncoderParameters tifEncoderParametersPageX;
        private static EncoderParameters tifEncoderParametersPageLast;

        private static System.Drawing.Imaging.Encoder tifEncoderSaveFlag;
        private static System.Drawing.Imaging.Encoder tifEncoderCompression;
        private static System.Drawing.Imaging.Encoder tifEncoderColorDepth;

        private static bool encoderAssigned;

        public static string tempDir;
        public static bool initComplete;

        public TiffUtil(string tempPath)
        {
            try
            {
                if (!initComplete)
                {
                    if (!tempPath.EndsWith("\\"))
                        tempDir = tempPath + "\\";
                    else
                        tempDir = tempPath;

                    Directory.CreateDirectory(tempDir);
                    initComplete = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region Retrieve Page Count of a multi-page TIFF file

        public int getPageCount(string fileName)
        {
            int pageCount = -1;

            try
            {
                Image img = Bitmap.FromFile(fileName);
                pageCount = img.GetFrameCount(FrameDimension.Page);
                img.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pageCount;
        }

        public int getPageCount(Image img)
        {
            int pageCount = -1;

            try
            {
                pageCount = img.GetFrameCount(FrameDimension.Page);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return pageCount;
        }

        #endregion

        #region Retrieve multiple single page images from a single multi-page TIFF file

        public Image[] getTiffImages(Image sourceImage, int[] pageNumbers)
        {
            MemoryStream ms = null;
            Image[] returnImage = new Image[pageNumbers.Length];

            try
            {
                Guid objGuid = sourceImage.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(objGuid);

                for (int i = 0; i < pageNumbers.Length; i++)
                {
                    ms = new MemoryStream();
                    sourceImage.SelectActiveFrame(objDimension, pageNumbers[i]);
                    sourceImage.Save(ms, ImageFormat.Tiff);
                    returnImage[i] = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ms.Close();
            }

            return returnImage;
        }

        public Image[] getTiffImages(Image sourceImage)
        {
            MemoryStream ms = null;
            int pageCount = getPageCount(sourceImage);

            Image[] returnImage = new Image[pageCount];

            try
            {
                Guid objGuid = sourceImage.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(objGuid);

                for (int i = 0; i < pageCount; i++)
                {
                    ms = new MemoryStream();
                    sourceImage.SelectActiveFrame(objDimension, i);
                    sourceImage.Save(ms, ImageFormat.Tiff);
                    returnImage[i] = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ms.Close();
            }

            return returnImage;
        }

        public Image[] getTiffImages(string sourceFile, int[] pageNumbers)
        {
            Image[] returnImage = new Image[pageNumbers.Length];

            try
            {
                Image sourceImage = Bitmap.FromFile(sourceFile);
                returnImage = getTiffImages(sourceImage, pageNumbers);
                sourceImage.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnImage = null;
            }

            return returnImage;
        }

        #endregion

        #region Retrieve a specific page from a multi-page TIFF image

        public Image getTiffImage(string sourceFile, int pageNumber)
        {
            Image returnImage = null;

            try
            {
                Image sourceImage = Image.FromFile(sourceFile);
                returnImage = getTiffImage(sourceImage, pageNumber);
                sourceImage.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnImage = null;
            }

            return returnImage;
        }

        public Image getTiffImage(Image sourceImage, int pageNumber)
        {
            MemoryStream ms = null;
            Image returnImage = null;

            try
            {
                ms = new MemoryStream();
                Guid objGuid = sourceImage.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(objGuid);
                sourceImage.SelectActiveFrame(objDimension, pageNumber);
                sourceImage.Save(ms, ImageFormat.Tiff);
                returnImage = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ms.Close();
            }

            return returnImage;
        }

        public bool getTiffImage(string sourceFile, string targetFile, int pageNumber)
        {
            bool response = false;

            try
            {
                Image returnImage = getTiffImage(sourceFile, pageNumber);
                returnImage.Save(targetFile);
                returnImage.Dispose();
                response = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        #endregion

        #region Split a multi-page TIFF file into multiple single page TIFF files

        public string[] splitTiffPages(string sourceFile, string targetDirectory)
        {
            string[] returnImages;

            try
            {
                Image sourceImage = Bitmap.FromFile(sourceFile);
                Image[] sourceImages = splitTiffPages(sourceImage);

                int pageCount = sourceImages.Length;

                returnImages = new string[pageCount];

                for (int i = 0; i < pageCount; i++)
                {
                    FileInfo fi = new FileInfo(sourceFile);
                    string babyImg = targetDirectory + "\\" + fi.Name.Substring(0, (fi.Name.Length - fi.Extension.Length)) + "_PAGE" + (i + 1).ToString().PadLeft(3, '0') + fi.Extension;
                    sourceImages[i].Save(babyImg);
                    returnImages[i] = babyImg;

                    //WriteLog(szLogFile, "Tif page extracted to " + babyImg); 
                }

                sourceImage.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                returnImages = null;
            }

            return returnImages;
        }

        public Image[] splitTiffPages(Image sourceImage)
        {
            Image[] returnImages;

            try
            {
                int pageCount = getPageCount(sourceImage);
                returnImages = new Image[pageCount];

                for (int i = 0; i < pageCount; i++)
                {
                    Image img = getTiffImage(sourceImage, i);
                    returnImages[i] = (Image)img.Clone();
                    img.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnImages = null;
            }

            return returnImages;
        }

        #endregion

        #region Merge multiple single page TIFF to a single multi page TIFF

        public bool mergeTiffPages(string[] sourceFiles, string targetFile)
        {
            bool response = false;

            try
            {
                assignEncoder();

                // If only 1 page was passed, copy directly to output
                if (sourceFiles.Length == 1)
                {
                    System.IO.File.Copy(sourceFiles[0], targetFile, true);
                    return true;
                }

                int pageCount = sourceFiles.Length;

                // First page
                Image finalImage = Image.FromFile(sourceFiles[0]);
                finalImage.Save(targetFile, tifImageCodecInfo, tifEncoderParametersPage1);

                // All other pages
                for (int i = 1; i < pageCount; i++)
                {
                    Image img = Image.FromFile(sourceFiles[i]);
                    finalImage.SaveAdd(img, tifEncoderParametersPageX);
                    img.Dispose();
                }

                // Last page
                finalImage.SaveAdd(tifEncoderParametersPageLast);
                finalImage.Dispose();
                response = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response = false;
            }

            return response;
        }

        public bool mergeTiffPages(string sourceFile, string targetFile, int[] pageNumbers)
        {
            bool response = false;

            try
            {
                assignEncoder();

                // Get individual Images from the original image 
                Image sourceImage = Bitmap.FromFile(sourceFile);
                MemoryStream ms = new MemoryStream();
                Image[] sourceImages = new Image[pageNumbers.Length];
                Guid guid = sourceImage.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(guid);
                for (int i = 0; i < pageNumbers.Length; i++)
                {
                    sourceImage.SelectActiveFrame(objDimension, pageNumbers[i]);
                    sourceImage.Save(ms, ImageFormat.Tiff);
                    sourceImages[i] = Image.FromStream(ms);
                }

                // Merge individual Images into one Image 
                // First page
                Image finalImage = sourceImages[0];
                finalImage.Save(targetFile, tifImageCodecInfo, tifEncoderParametersPage1);
                // All other pages
                for (int i = 1; i < pageNumbers.Length; i++)
                {
                    finalImage.SaveAdd(sourceImages[i], tifEncoderParametersPageX);
                }
                // Last page
                finalImage.SaveAdd(tifEncoderParametersPageLast);
                finalImage.Dispose();

                response = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public bool mergeTiffPagesAlternate(string sourceFile, string targetFile, int[] pageNumbers)
        {
            bool response = false;

            try
            {
                // Initialize the encoders, occurs once for the lifetime of the class
                assignEncoder();

                // Get individual Images from the original image 
                Image sourceImage = Bitmap.FromFile(sourceFile);
                MemoryStream[] msArray = new MemoryStream[pageNumbers.Length];
                Guid guid = sourceImage.FrameDimensionsList[0];
                FrameDimension objDimension = new FrameDimension(guid);
                for (int i = 0; i < pageNumbers.Length; i++)
                {
                    msArray[i] = new MemoryStream();
                    sourceImage.SelectActiveFrame(objDimension, pageNumbers[i]);
                    sourceImage.Save(msArray[i], ImageFormat.Tiff);
                }

                // Merge individual page streams into single stream
                MemoryStream ms = mergeTiffStreams(msArray);
                Image targetImage = Bitmap.FromStream(ms);
                targetImage.Save(targetFile);

                response = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public System.IO.MemoryStream mergeTiffStreams(System.IO.MemoryStream[] tifsStream)
        {
            EncoderParameters ep = null;
            System.IO.MemoryStream singleStream = new System.IO.MemoryStream();

            try
            {
                assignEncoder();

                Image imgTif = Image.FromStream(tifsStream[0]);

                if (tifsStream.Length > 1)
                {
                    // Multi-Frame
                    ep = new EncoderParameters(2);
                    ep.Param[0] = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.MultiFrame);
                    ep.Param[1] = new EncoderParameter(tifEncoderCompression, (long)EncoderValue.CompressionRle);
                }
                else
                {
                    // Single-Frame
                    ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(tifEncoderCompression, (long)EncoderValue.CompressionRle);
                }

                //Save the first page
                imgTif.Save(singleStream, tifImageCodecInfo, ep);

                if (tifsStream.Length > 1)
                {
                    ep = new EncoderParameters(2);
                    ep.Param[0] = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.FrameDimensionPage);

                    //Add the rest of pages
                    for (int i = 1; i < tifsStream.Length; i++)
                    {
                        Image pgTif = Image.FromStream(tifsStream[i]);

                        ep.Param[1] = new EncoderParameter(tifEncoderCompression, (long)EncoderValue.CompressionRle);

                        imgTif.SaveAdd(pgTif, ep);
                    }

                    ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.Flush);
                    imgTif.SaveAdd(ep);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ep != null)
                {
                    ep.Dispose();
                }
            }

            return singleStream;
        }

        #endregion

        #region Internal support functions

        private void assignEncoder()
        {
            try
            {
                if (encoderAssigned == true)
                    return;

                foreach (ImageCodecInfo ici in ImageCodecInfo.GetImageEncoders())
                {
                    if (ici.MimeType == "image/tiff")
                    {
                        tifImageCodecInfo = ici;
                    }
                }

                tifEncoderSaveFlag = System.Drawing.Imaging.Encoder.SaveFlag;
                tifEncoderCompression = System.Drawing.Imaging.Encoder.Compression;
                tifEncoderColorDepth = System.Drawing.Imaging.Encoder.ColorDepth;

                tifEncoderParameterMultiFrame = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.MultiFrame);
                tifEncoderParameterFrameDimensionPage = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.FrameDimensionPage);
                tifEncoderParameterFlush = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.Flush);
                tifEncoderParameterCompression = new EncoderParameter(tifEncoderCompression, (long)EncoderValue.CompressionRle);
                tifEncoderParameterLastFrame = new EncoderParameter(tifEncoderSaveFlag, (long)EncoderValue.LastFrame);
                tifEncoderParameter24BPP = new EncoderParameter(tifEncoderColorDepth, (long)24);
                tifEncoderParameter1BPP = new EncoderParameter(tifEncoderColorDepth, (long)8);

                // ******************************************************************* //
                // *** Have only 1 of the following 3 groups assigned for encoders *** //
                // ******************************************************************* //

                // Regular 
                tifEncoderParametersPage1 = new EncoderParameters(1);
                tifEncoderParametersPage1.Param[0] = tifEncoderParameterMultiFrame;
                tifEncoderParametersPageX = new EncoderParameters(1);
                tifEncoderParametersPageX.Param[0] = tifEncoderParameterFrameDimensionPage;
                tifEncoderParametersPageLast = new EncoderParameters(1);
                tifEncoderParametersPageLast.Param[0] = tifEncoderParameterFlush;

                //// Regular 
                //tifEncoderParametersPage1 = new EncoderParameters(2); 
                //tifEncoderParametersPage1.Param[0] = tifEncoderParameterMultiFrame;
                //tifEncoderParametersPage1.Param[1] = tifEncoderParameterCompression;
                //tifEncoderParametersPageX = new EncoderParameters(2); 
                //tifEncoderParametersPageX.Param[0] = tifEncoderParameterFrameDimensionPage; 
                //tifEncoderParametersPageX.Param[1] = tifEncoderParameterCompression;
                //tifEncoderParametersPageLast = new EncoderParameters(2); 
                //tifEncoderParametersPageLast.Param[0] = tifEncoderParameterFlush;
                //tifEncoderParametersPageLast.Param[1] = tifEncoderParameterLastFrame;

                //// 24 BPP Color 
                //tifEncoderParametersPage1 = new EncoderParameters(2); 
                //tifEncoderParametersPage1.Param[0] = tifEncoderParameterMultiFrame;
                //tifEncoderParametersPage1.Param[1] = tifEncoderParameter24BPP;
                //tifEncoderParametersPageX = new EncoderParameters(2); 
                //tifEncoderParametersPageX.Param[0] = tifEncoderParameterFrameDimensionPage;
                //tifEncoderParametersPageX.Param[1] = tifEncoderParameter24BPP;
                //tifEncoderParametersPageLast = new EncoderParameters(2); 
                //tifEncoderParametersPageLast.Param[0] = tifEncoderParameterFlush;
                //tifEncoderParametersPageLast.Param[1] = tifEncoderParameterLastFrame;

                //// 1 BPP BW 
                //tifEncoderParametersPage1 = new EncoderParameters(2); 
                //tifEncoderParametersPage1.Param[0] = tifEncoderParameterMultiFrame;
                //tifEncoderParametersPage1.Param[1] = tifEncoderParameterCompression;
                //tifEncoderParametersPageX = new EncoderParameters(2); 
                //tifEncoderParametersPageX.Param[0] = tifEncoderParameterFrameDimensionPage;
                //tifEncoderParametersPageX.Param[1] = tifEncoderParameterCompression;
                //tifEncoderParametersPageLast = new EncoderParameters(2); 
                //tifEncoderParametersPageLast.Param[0] = tifEncoderParameterFlush;
                //tifEncoderParametersPageLast.Param[1] = tifEncoderParameterLastFrame;

                encoderAssigned = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private Bitmap ConvertToGrayscale(Bitmap source)
        {
            try
            {
                Bitmap bm = new Bitmap(source.Width, source.Height);
                Graphics g = Graphics.FromImage(bm);

                ColorMatrix cm = new ColorMatrix(new float[][] { new float[] { 0.5f, 0.5f, 0.5f, 0, 0 }, new float[] { 0.5f, 0.5f, 0.5f, 0, 0 }, new float[] { 0.5f, 0.5f, 0.5f, 0, 0 }, new float[] { 0, 0, 0, 1, 0, 0 }, new float[] { 0, 0, 0, 0, 1, 0 }, new float[] { 0, 0, 0, 0, 0, 1 } });
                ImageAttributes ia = new ImageAttributes();
                ia.SetColorMatrix(cm);
                g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height), 0, 0, source.Width, source.Height, GraphicsUnit.Pixel, ia);
                g.Dispose();

                return bm;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion
    }

    public class Common
    {
        #region "Grid and Filter Settings"

        public static void AddXMLAttribute(XmlDocument oXmlDocument, XmlNode oXMLParentNode, string szAttributeName, string szValue)
        {
            XmlAttribute oXmlAttribute = oXmlDocument.CreateAttribute(szAttributeName);
            oXmlAttribute.Value = szValue;
            oXMLParentNode.Attributes.Append(oXmlAttribute);
        }

        public static XmlDocument GetSettings(string szUserName,
                                              string szFormName,
                                              string szControlName,
                                              string szConnectionStringConfig)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            DataAccess.CommonDB oCommonDB = new DataAccess.CommonDB(szConnectionStringConfig);
            DataTable oDT;

            oDT = oCommonDB.GetSettings(szUserName, szFormName, szControlName);

            //Load this XML into an XmlDocument object
            if (oDT.Rows.Count > 0)
                oXmlDocument.LoadXml(oDT.Rows[0]["Settings"].ToString());
            else
                oXmlDocument = null;

            return oXmlDocument;
        }

        public static void SaveSettings(string szUserName,
                                        string szFormName,
                                        string szControlName,
                                        string szConnectionStringConfig,
                                        XmlDocument oXmlDocument)
        {
            DataAccess.CommonDB oCommonDB = new DataAccess.CommonDB(szConnectionStringConfig);

            oCommonDB.SaveSettings(szUserName, szFormName, szControlName, oXmlDocument.InnerXml);
        }

        #endregion

        #region "Application Settings"

        public static string GetAppSetting(string szNodeName)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXmlNode;

            oXmlDocument.Load(Application.StartupPath + @"\settings.xml");

            oXmlNode = oXmlDocument.SelectSingleNode(@"//AppSettings/" + szNodeName);

            return oXmlNode.InnerText;
        }

        public static void WriteAppSetting(string szNodeName, string szValue)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXmlNode;

            oXmlDocument.Load(Application.StartupPath + @"\settings.xml");

            oXmlNode = oXmlDocument.SelectSingleNode(@"//AppSettings/" + szNodeName);

            oXmlNode.InnerText = szValue;

            oXmlDocument.Save(Application.StartupPath + @"\settings.xml");
        }

        #endregion

        #region "ComboBox utilities"

        public static void LoadComboBox(RadComboBox oComboBox, DataTable oDT, string szDisplayMember, string szValueMember, bool bAddBlank)
        {
            LoadComboBox(oComboBox, oDT, szDisplayMember, szValueMember);

            if (bAddBlank)
                oComboBox.Items.Add(new RadComboBoxItem(string.Empty, string.Empty));
        }

        public static void LoadComboBox(RadComboBox oComboBox, DataTable oDT, string szDisplayMember, string szValueMember)
        {
            oComboBox.Items.Clear();
            oComboBox.DisplayMember = szDisplayMember;
            oComboBox.ValueMember = szValueMember;
            oComboBox.DataSource = oDT;

            if (oComboBox.Items.Count > 0)
                oComboBox.SelectedIndex = oComboBox.Items.Count - 1;
        }

        public static void LoadComboBox(ComboBox oComboBox, DataTable oDT, string szDisplayMember, string szValueMember)
        {
            oComboBox.DataSource = null;

            oComboBox.Items.Clear();
            oComboBox.DisplayMember = szDisplayMember;
            oComboBox.ValueMember = szValueMember;
            oComboBox.DataSource = oDT;

            if (oComboBox.Items.Count > 0)
                oComboBox.SelectedIndex = oComboBox.Items.Count - 1;
        }

        public static void LoadComboBox(ComboBox oComboBox, DataTable oDT, string szDisplayMember, string szValueMember, bool bAddBlank)
        {

            if (bAddBlank)
            {
                DataRow dr = oDT.NewRow();

                dr[0] = DBNull.Value;
                dr[1] = string.Empty;

                oDT.Rows.Add(dr);

            }

            LoadComboBox(oComboBox, oDT, szDisplayMember, szValueMember);

        }

        public static string GetComboText(RadComboBox oCombo)
        {
            string szResult = string.Empty;

            if (oCombo.Text != null)
                szResult = oCombo.Text;

            return szResult;
        }

        public static object GetComboValue(RadComboBox oCombo)
        {
            object oResult;

            if (oCombo.SelectedValue == null)
                oResult = null;
            else
                oResult = oCombo.SelectedValue;

            if (oResult != null)
            {
                if (oResult.ToString() == string.Empty)
                    oResult = null;
            }

            return oResult;
        }

        public static void SetComboValue(RadComboBox oCombo, object oValue)
        {
            int i = 0;

            oCombo.SelectedIndex = -1;

            oValue = oValue.ToString().ToUpper();

            foreach (RadComboBoxItem oRadComboBoxItem in oCombo.Items)
            {
                if (oRadComboBoxItem.Value.ToString().ToUpper() == oValue.ToString())
                {
                    oCombo.SelectedIndex = i;
                    break;
                }
                i++;
            }

        }

        public static void SetComboValue(ComboBox oCombo, object oValue)
        {
            int i = 0;

            oCombo.SelectedIndex = -1;

            oValue = oValue.ToString().ToUpper();

            foreach (object oItem in oCombo.Items)
            {
                if (oItem.GetType() == typeof(System.Data.DataRowView))
                {
                    if (((System.Data.DataRowView)oItem).Row[oCombo.ValueMember].ToString().ToUpper() == oValue.ToString())
                    {
                        oCombo.SelectedIndex = i;
                        break;
                    }
                }
                else if (oItem.GetType() == typeof(ListItem))
                {
                    if (((ListItem)oItem).Value.ToString().ToUpper() == oValue.ToString())
                    {
                        oCombo.SelectedIndex = i;
                        break;
                    }
                }
                else
                {
                    if (oItem.ToString().ToUpper() == oValue.ToString())
                    {
                        oCombo.SelectedIndex = i;
                        break;
                    }
                }

                i++;
            }

        }

        #endregion

        #region "ListBox utilities"

        public static void SelectAllListBoxItems(CheckedListBox checkedListBox)
        {
            for (int i = 0; i <= checkedListBox.Items.Count - 1; i++)
            {
                checkedListBox.SetItemChecked(i, true);
            }
        }

        public static void ClearAllListBoxItems(CheckedListBox checkedListBox)
        {
            for (int i = 0; i <= checkedListBox.Items.Count - 1; i++)
            {
                checkedListBox.SetItemChecked(i, false);
            }
        }

        #endregion

        public static string GetUserName()
        {
            string szName = WindowsIdentity.GetCurrent().Name;

            if (szName.LastIndexOf(@"\") > 0)
                szName = szName.Substring(szName.LastIndexOf(@"\") + 1);

            return szName;
        }

        public static void MaxScreen(Form oForm)
        {
            Rectangle oRectangle = Screen.GetWorkingArea(oForm);
            oForm.Left = 0;
            oForm.Top = 0;
            oForm.Size = new Size(oRectangle.Width, oRectangle.Height);
        }

        public static void AlmostMaxScreen(Form oForm)
        {
            Rectangle oRectangle = Screen.GetWorkingArea(oForm);
            oForm.Left = 10;
            oForm.Top = 10;
            oForm.Size = new Size(oRectangle.Width - 20, oRectangle.Height - 50);
        }

        #region "Help"

        public static void Help(string helpPath, string helpXML, string helpExcel, string helpTopic)
        {
            XmlDocument xmlDocument;
            XmlNodeList xmlNodelist;
            string helpTitle = string.Empty;
            string helpHTML = string.Empty;
            string helpLastImportFileDate;
            DateTime helpExcelFileDate;

            MessageBox.Show("The new help system is currently under development.");
            return;


            bool recreateHelpMappings = false;

            if (!File.Exists(helpPath + helpExcel) && !File.Exists(helpPath + helpXML))
            {
                MessageBox.Show("The help system cannot be initialized. Please contact technical support."); 
                return;
            }

            if (File.Exists(helpPath + helpExcel))
            {
                helpExcelFileDate = File.GetLastWriteTime(helpPath + helpExcel);

                if (File.Exists(helpPath + helpXML))
                {
                    xmlDocument = new XmlDocument();

                    xmlDocument.Load(helpPath + helpXML);

                    xmlNodelist = xmlDocument.SelectNodes("HelpMappings");

                    helpLastImportFileDate = xmlNodelist[0].Attributes[0].Value.ToString();

                    if (helpLastImportFileDate != helpExcelFileDate.ToString())
                        recreateHelpMappings = true;
                }
                else
                    recreateHelpMappings = true;

            }

            if (recreateHelpMappings)
                RecreateHelpMappings(helpPath + helpExcel, helpPath + helpXML);


            if (File.Exists(helpPath + helpXML))
            {
                xmlDocument = new XmlDocument();

                xmlDocument.Load(helpPath + helpXML);

                xmlNodelist = xmlDocument.SelectNodes("HelpMappings/LinkTag");

                foreach (XmlNode xmlNode in xmlNodelist)
                {
                    if (xmlNode.Attributes[1].Value.ToString() == helpTopic)
                    {
                        helpTitle = xmlNode.Attributes[0].Value.ToString();
                        helpHTML = xmlNode.Attributes[2].Value.ToString();

                        if (File.Exists(helpPath + helpHTML))
                        {
                            InnovatixDLL.HelpViewer helpViewer = new InnovatixDLL.HelpViewer();
                            helpViewer.HelpTitle = helpTitle;
                            helpViewer.HelpHTML = helpPath + helpHTML;
                            helpViewer.Show(); 
                        }
                        else
                            MessageBox.Show("The help for this topic cannot be found. Please contact technical support.");  
 
                        break;
                    }
                }

                if (helpHTML == string.Empty)
                    MessageBox.Show("The help for this topic cannot be found. Please contact technical support.");  
            }
            else
                MessageBox.Show("The help system cannot be initialized. Please contact technical support.");  
        }

        private static void RecreateHelpMappings(string xlsFileName, string xmlFileName)
        {
            XmlDocument xmlData = new XmlDocument();
            XmlNode xmlMainNode;
            XmlNode oXMLNode;
            IWorkbook oWorkbook;
            IWorksheet oWS;
            int row = 3;

            oWorkbook = ExcelUtils.Open(xlsFileName);

            oWS = oWorkbook.Worksheets[0];

            xmlData.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlMainNode = xmlData.CreateNode(XmlNodeType.Element, "HelpMappings", string.Empty);

            Common.AddXMLAttribute(xmlData, xmlMainNode, "LastExcelDateTime", File.GetLastWriteTime(xlsFileName).ToString());
            
            xmlData.AppendChild(xmlMainNode);

            while (row <= oWS.Rows.GetLength(0))
            {
                if (int.Parse(oWS.Range[row, 12].DisplayText) != -1)
                {
                    oXMLNode = xmlData.CreateNode(XmlNodeType.Element, "LinkTag", string.Empty);
                    xmlMainNode.AppendChild(oXMLNode);

                    Common.AddXMLAttribute(xmlData, oXMLNode, "Title", oWS.Range[row, 3].DisplayText);
                    Common.AddXMLAttribute(xmlData, oXMLNode, "Name", oWS.Range[row, 6].DisplayText);
                    Common.AddXMLAttribute(xmlData, oXMLNode, "File", oWS.Range[row, 4].DisplayText + ".htm");
                }

                row++;

            }

            xmlData.Save(xmlFileName);
        }

        #endregion

        public static void BlankControls(System.Windows.Forms.Control.ControlCollection oControlCollection)
        {

            foreach (Control oControl in oControlCollection)
            {
                switch (oControl.GetType().Name.ToLower())
                {
                    case "checkbox":
                        (oControl as CheckBox).CheckState = CheckState.Unchecked;
                        break;

                    case "textbox":
                        (oControl as TextBox).Text = string.Empty;
                        break;

                    case "dropdownlist":
                        (oControl as ComboBox).SelectedValue = -1;
                        break;

                    case "datetimepicker":
                        (oControl as DateTimePicker).Value = DateTime.Now;
                        break;
                }

                if (oControl.HasChildren)
                {
                    BlankControls(oControl.Controls);
                }
            }

        }

        #region "Grid Utilities"

        public static void ClearGrid(RadGridView oRadGridView)
        {
            oRadGridView.CurrentRow = null;
            oRadGridView.Relations.Clear();
            oRadGridView.MasterGridViewTemplate.ChildGridViewTemplates.Clear();
            oRadGridView.MasterGridViewTemplate.Columns.Clear();
            oRadGridView.DataSource = null;
        }

        public static int GetGridInt32(GridViewRowInfo oGridViewRowInfo, string szColumnName)
        {
            int iResult = 0;

            if (oGridViewRowInfo.Cells[szColumnName] != null)
            {
                if (oGridViewRowInfo.Cells[szColumnName].Value.ToString() != string.Empty)
                    iResult = int.Parse(oGridViewRowInfo.Cells[szColumnName].Value.ToString());
            }

            return iResult;
        }

        #endregion

        #region "Data Type Methods"

        public static bool IsDate(string date)
        {
            DateTime dateTime;
            bool isDate = true;

            try
            {
                dateTime = DateTime.Parse(date);
                //If this cannot parse, the format is incorrect 
            }
            catch (FormatException e)
            {
                isDate = false;
            }

            return isDate;
        }

        public static bool IsInteger(string value)
        {
            try
            {
                Convert.ToInt32(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region "File Utilities"

        public static string GenerateTempFileName(string szExtension)
        {
            return DateTime.Now.Year.ToString() +
                DateTime.Now.Month.ToString() +
                DateTime.Now.Day.ToString() +
                DateTime.Now.Hour.ToString() +
                DateTime.Now.Minute.ToString() +
                DateTime.Now.Second.ToString() +
                DateTime.Now.Millisecond.ToString() +
                "." + szExtension;
        }

        //Note: Syncfusion does not offer the ability to convert from PDF to TIF
        public static void TIF2PDF(string szTIFFileName, string szPDFFileName)
        {
            PdfDocument oPdfDocument = new PdfDocument();
            PdfSection oPdfSection = oPdfDocument.Sections.Add();
            PdfPage oPdfPage;
            PdfGraphics oPdfGraphics;

            using (PdfBitmap oPdfBitmap = new PdfBitmap(szTIFFileName))
            {
                int iFrameCnt = oPdfBitmap.FrameCount;

                for (int i = 0; i < iFrameCnt; i++)
                {
                    oPdfPage = oPdfSection.Pages.Add();
                    oPdfSection.PageSettings.Margins.All = 0;
                    oPdfGraphics = oPdfPage.Graphics;
                    oPdfBitmap.ActiveFrame = i;
                    oPdfGraphics.DrawImage(oPdfBitmap, 0, 0, oPdfPage.GetClientSize().Width, oPdfPage.GetClientSize().Height);
                }
            }

            oPdfSection.PageSettings.Transition.PageDuration = 1;
            oPdfSection.PageSettings.Transition.Duration = 1;
            oPdfSection.PageSettings.Transition.Style = PdfTransitionStyle.Fly;

            oPdfDocument.Save(szPDFFileName);

        }

        public static byte[] ConvertFileToByteArray(string fileName)
        {
            BinaryReader binaryReader;
            byte[] aResult;

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                binaryReader = new BinaryReader(fileStream);
                aResult = binaryReader.ReadBytes((int)fileStream.Length);
                fileStream.Close();
            }

            return aResult;
        }

        public static void ConvertByteArrayToFile(byte[] aFile, string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            fileStream.Write(aFile, 0, aFile.Length);
            fileStream.Close();
        }

        #endregion

        #region "Logging"

        public static string ErrorMessage(Exception ex)
        {
            string szResult;

            szResult = "Error found in " + ex.Source + "\n";

            szResult += ex.Message + "\n";

            if (ex.InnerException != null)
                szResult += " " + ex.InnerException.Message + "\n";

            szResult += "Stack Trace: \n" + ex.StackTrace;
 
            return szResult;
        }


        public static void WriteLog(string szLogFile, string szData)
        {
            StreamWriter oStreamWriter;

            oStreamWriter = new StreamWriter(szLogFile, true);

            oStreamWriter.WriteLine(DateTime.Now.ToString() + " " + szData);

            oStreamWriter.Flush();
            oStreamWriter.Close();
        }

        #endregion

        #region "Email Utilities"

        public static void SendEmail(string szTo,
             string szFrom,
             string szSubject,
             string szBody,
             string szUserName,
             string szPassword,
             string szHost,
             int iPort)
        {
            SendEmail(szTo, szFrom, szSubject, szBody, string.Empty, szUserName, szPassword, szHost, iPort);
        }

        public static void SendEmail(string szTo,
                             string szFrom,
                             string szSubject,
                             string szBody,
                             string attachmentFileName,
                             string szUserName,
                             string szPassword,
                             string szHost,
                             int iPort)
        {
            MailMessage oMailMessage = new MailMessage();
            SmtpClient oSmtpClient = new SmtpClient();
            string[] aTo;
            string[] aAttachment;
            char[] delimiterChars = { ',' };

            aTo = szTo.Split(delimiterChars);
            aAttachment = attachmentFileName.Split(delimiterChars);

            foreach (string szEmail in aTo)
            {
                oMailMessage.To.Add(new MailAddress(szEmail));
            }

            oMailMessage.From = new MailAddress(szFrom);
            oMailMessage.Subject = szSubject;
            oMailMessage.Priority = MailPriority.High;
            oMailMessage.Body = szBody;

            foreach (string attachment in aAttachment)
            {
                if (attachment != string.Empty && File.Exists(attachment))
                    oMailMessage.Attachments.Add(new Attachment(attachment));
            }

            oSmtpClient.Host = szHost;
            oSmtpClient.Port = iPort;
            oSmtpClient.UseDefaultCredentials = true;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(szUserName, szPassword);
            oSmtpClient.Send(oMailMessage);
        }

        public static void CreateOutlookEmail(string szTo, string szCC, string szSubject, string szBody, string szAttachment)
        {
            Redemption.SafeMailItem oSafeMailItem;
            Outlook.Application oApplication = new Outlook.Application();
            Outlook.MailItem oMailItem = (Outlook.MailItem)oApplication.CreateItem(Outlook.OlItemType.olMailItem);

            oSafeMailItem = ((Redemption.SafeMailItem)Activator.CreateInstance(Type.GetTypeFromProgID("Redemption.SafeMailItem")));

            oMailItem = ((Outlook.MailItem)oApplication.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem));

            oSafeMailItem.Item = oMailItem;

            if (szTo != string.Empty)
                oSafeMailItem.Recipients.Add(szTo);

            if (szCC != string.Empty)
            {
                Redemption.SafeRecipient oSafeRecipient = oSafeMailItem.Recipients.Add(szCC);
                oSafeRecipient.Type = (int)Outlook.OlMailRecipientType.olCC;
            }

            SetPropValue(oSafeMailItem, "Subject", szSubject);
            oSafeMailItem.Body = szBody;
            oSafeMailItem.Recipients.ResolveAll();

            /*
            Per this link:
            http://bytes.com/groups/ms-access/675095-outlook-attachments-not-visible
            Attachments must be done though oMailItem, not oSafeMailItem, as otherwise
            they will not be visible when the email editor appears on the screen. The message
            is still in the email, just not visible. This is a quirk in the Redemption library.
            */
            oMailItem.Attachments.Add(szAttachment, Missing.Value, Missing.Value, Missing.Value);

            try
            {
                oMailItem.Display(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("You already have an Outlook mail dialog open. Please close it.");
            }

        }

        private static void SetPropValue(object item, string propertyName, object propertyValue)
        {
            try
            {
                object[] args = new Object[1];
                args[0] = propertyValue;
                Type type = item.GetType();
                type.InvokeMember(propertyName, BindingFlags.Public | BindingFlags.SetField | BindingFlags.SetProperty, null, item, args);
            }
            catch (SystemException ex)
            {
                Console.WriteLine(string.Format("SetPropValue for {0} Exception: {1} ", propertyName, ex.Message));
            }
            return;

        }

        #endregion

        #region "XLS Utilities"

        public static string XLS2XML(string fileName)
        {
            XmlDocument xmlData = new XmlDocument();
            XmlNode xmlMainNode;
            XmlNode oXMLNode;
            IWorkbook oWorkbook;
            IWorksheet oWS;
            string szAttributeName;
            int row;
            int col;

            if (File.Exists(fileName))
                oWorkbook = ExcelUtils.Open(fileName);
            else
            {
                return string.Empty;
            }

            oWS = oWorkbook.Worksheets[0];

            xmlData.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlMainNode = xmlData.CreateNode(XmlNodeType.Element, "Data", string.Empty);
            xmlData.AppendChild(xmlMainNode);

            row = 2;

            while (row <= oWS.Rows.GetLength(0))
            {
                oXMLNode = xmlData.CreateNode(XmlNodeType.Element, "DataRow", string.Empty);
                xmlMainNode.AppendChild(oXMLNode);

                col = 1;

                while (col <= oWS.Columns.GetLength(0))
                {
                    //szAttributeName = XmlConvert.EncodeName(oWS.Range[1, col].DisplayText);

                    szAttributeName = oWS.Range[1, col].DisplayText.Replace(" ", string.Empty);
                    szAttributeName = szAttributeName.Replace("#", "Number");
                    szAttributeName = szAttributeName.Replace("/", string.Empty);
                    szAttributeName = szAttributeName.Replace("?", string.Empty);

                    if (szAttributeName != string.Empty)
                        Common.AddXMLAttribute(xmlData, oXMLNode, szAttributeName, oWS.Range[row, col].DisplayText);

                    col++;
                }

                row++;

            }

            return xmlData.InnerXml;

        }

        public static int GetExcelColumnNumber(string szColumnLetter)
        {
            int iLength = szColumnLetter.Length;
            int iResult = 0;
            string szLetter;

            szColumnLetter = szColumnLetter.ToUpper();

            if (iLength == 1)
            {
                szLetter = szColumnLetter.Substring(0, 1);
                iResult += Asc(szLetter) - 64;
            }
            else
            {
                szLetter = szColumnLetter.Substring(0, 1);
                iResult += (26 * (Asc(szLetter) - 64));

                szLetter = szColumnLetter.Substring(1, 1);
                iResult += Asc(szLetter) - 64;
            }

            return iResult;
        }

        internal static int Asc(string p_strChar)
        {
            if (p_strChar.Length != 1)
            {
                throw new ArgumentOutOfRangeException("p_strChar", p_strChar,
                    "Must be a single character.");
            }
            char[] chrBuffer = { Convert.ToChar(p_strChar) };
            byte[] bytBuffer = System.Text.Encoding.GetEncoding(1252).GetBytes(chrBuffer);
            return (int)bytBuffer[0];
        }

        public static string GetExcelColumnName(int columnNumber) 
        { 
            int dividend = columnNumber; 
            string columnName = String.Empty; 
            int modulo; 

            while (dividend > 0) 
            {
                modulo = (dividend - 1) % 26; 
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName; 
                dividend = (int)((dividend - modulo) / 26); 
            } 

            return columnName; 
        }

        #endregion

    }
}
