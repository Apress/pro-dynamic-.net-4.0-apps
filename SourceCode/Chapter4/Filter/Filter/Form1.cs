using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Filter
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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            comboBox1.Items.Clear();
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Text";
            comboBox1.Items.Add(new ListItem("101", "Washington"));
            comboBox1.Items.Add(new ListItem("102", "Adams"));
            comboBox1.Items.Add(new ListItem("103", "Jefferson"));

            checkedListBox1.Items.Clear();
            checkedListBox1.ValueMember = "Value";
            checkedListBox1.DisplayMember = "Text";
            checkedListBox1.Items.Add(new ListItem("201", "Harding"));
            checkedListBox1.Items.Add(new ListItem("202", "Coolidge"));
            checkedListBox1.Items.Add(new ListItem("203", "Hoover"));

            listBox1.Items.Clear();
            listBox1.ValueMember = "Value";
            listBox1.DisplayMember = "Text";
            listBox1.Items.Add(new ListItem("301", "Kennedy"));
            listBox1.Items.Add(new ListItem("302", "Johnson"));
            listBox1.Items.Add(new ListItem("303", "Nixon"));


            DataTable oDT = new DataTable();
            DataRow oDR;
            DataGridViewColumn oDataGridViewColumn = new DataGridViewCheckBoxColumn();
  
            oDT.Columns.Add(new DataColumn("UserID"));
            oDT.Columns.Add(new DataColumn("LastName"));
            oDT.Columns.Add(new DataColumn("FirstName"));

            oDR = oDT.NewRow();
            oDR["UserID"] = 11;
            oDR["LastName"] = "Smith";
            oDR["FirstName"] = "John";
            oDT.Rows.Add(oDR);

            oDR = oDT.NewRow();
            oDR["UserID"] = 12;
            oDR["LastName"] = "Jones";
            oDR["FirstName"] = "Fred";
            oDT.Rows.Add(oDR);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.DataSource = oDT;

            oDataGridViewColumn.HeaderText = string.Empty;
            oDataGridViewColumn.Name = "IsSelected";
            oDataGridViewColumn.DisplayIndex = 0;


            dataGridView1.Columns.Add(oDataGridViewColumn); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            XmlElement oXmlElement;
            SqlDatabase oSqlDatabase;
            DbCommand oDbCommand;
            DataTable oDT;

            LoadData();

            oSqlDatabase = new SqlDatabase("Data Source=GBM-DEV-VM1;Initial Catalog=Contracts;User ID=sa;Password=galaxy;Persist Security Info=false");

            //Retrieve the XML containing the control settings
            using (oDbCommand = oSqlDatabase.GetStoredProcCommand("spc_sel_Filter"))
            {
                oSqlDatabase.AddInParameter(oDbCommand, "@UserName", DbType.String, "cganz");
                oSqlDatabase.AddInParameter(oDbCommand, "@FormName", DbType.String, "Item");

                oDT = oSqlDatabase.ExecuteDataSet(oDbCommand).Tables[0];
            }

            oDT = oSqlDatabase.ExecuteDataSet(oDbCommand).Tables[0];

            if (oDT.Rows.Count == 0)
                return;

            //Load this XML into an XmlDocument object
            oXmlDocument.LoadXml(oDT.Rows[0]["FilterData"].ToString());

            //Obtain a reference to the root elemnt
            oXmlElement = oXmlDocument.DocumentElement;

            LoadFilter(panel1, oXmlElement);

            LoadGridSettings(dataGridView1, "LastName");
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXMLMainNode;
            XmlDeclaration oXmlDeclaration;
            SqlDatabase oSqlDatabase;
            DbCommand oDbCommand;

            oXmlDeclaration = oXmlDocument.CreateXmlDeclaration("1.0",
                "UTF-8", null);

            oXmlDocument.AppendChild(oXmlDeclaration);

            oXMLMainNode = oXmlDocument.CreateNode(XmlNodeType.Element, 
                "Controls", string.Empty);
            oXmlDocument.AppendChild(oXMLMainNode);

            oXmlDocument = SaveFilter(panel1, oXmlDocument, oXMLMainNode);

            oSqlDatabase = new SqlDatabase("Data Source=GBM-DEV-VM1;Initial Catalog=Contracts;User ID=sa;Password=galaxy;Persist Security Info=false");

            using (oDbCommand = oSqlDatabase.GetStoredProcCommand("spc_ins_Filter"))
            {
                oSqlDatabase.AddInParameter(oDbCommand, "@UserName", DbType.String, "cganz");
                oSqlDatabase.AddInParameter(oDbCommand, "@FormName", DbType.String, "Item");
                oSqlDatabase.AddInParameter(oDbCommand, "@FilterData", DbType.Xml, oXmlDocument.InnerXml);

                oSqlDatabase.ExecuteNonQuery(oDbCommand);
            }

            //oDT.DefaultView.Sort = "LastName";
            SaveGridSettings(dataGridView1.Columns, "dataGridView1", "LastName");

        }

        #region "Grid Settings"

        private void SaveGridSettings(DataGridViewColumnCollection oDataGridViewColumnCollection, string szName, string szSortColumn)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXMLMainNode;
            XmlNode oXMLNode;
            XmlAttribute oXmlAttribute;

            oXmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);

            oXMLMainNode = oXmlDocument.CreateNode(XmlNodeType.Element, "DataGridView", string.Empty);
            oXmlDocument.AppendChild(oXMLMainNode);

            oXmlAttribute = oXmlDocument.CreateAttribute("name");
            oXmlAttribute.Value = szName;
            oXMLMainNode.Attributes.Append(oXmlAttribute);

            oXmlAttribute = oXmlDocument.CreateAttribute("sortcolumn");
            oXmlAttribute.Value = szSortColumn;
            oXMLMainNode.Attributes.Append(oXmlAttribute);

            foreach (DataGridViewColumn oDataGridViewColumn in oDataGridViewColumnCollection)
            {

                oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element, "Column", string.Empty);
                oXMLMainNode.AppendChild(oXMLNode);

                oXmlAttribute = oXmlDocument.CreateAttribute("name");
                oXmlAttribute.Value = oDataGridViewColumn.Name;
                oXMLNode.Attributes.Append(oXmlAttribute);

                oXmlAttribute = oXmlDocument.CreateAttribute("displayindex");
                oXmlAttribute.Value = oDataGridViewColumn.DisplayIndex.ToString();
                oXMLNode.Attributes.Append(oXmlAttribute);

                oXmlAttribute = oXmlDocument.CreateAttribute("width");
                oXmlAttribute.Value = oDataGridViewColumn.Width.ToString();
                oXMLNode.Attributes.Append(oXmlAttribute);

                oXmlAttribute = oXmlDocument.CreateAttribute("visible");
                oXmlAttribute.Value = oDataGridViewColumn.Visible.ToString();
                oXMLNode.Attributes.Append(oXmlAttribute);
            }

            oXmlDocument.Save(@"c:\temp\grid.xml");
        }

        private void LoadGridSettings(DataGridView oDataGridView, string szSortColumn)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXMLMainNode;
            XmlNode oXMLNode;
            XmlAttribute oXmlAttribute;
            XmlNodeList oXmlNodeList;
            XmlElement oXmlElement;
            DataGridViewColumn oDataGridViewColumn;
            string szNodeQuery;

            foreach (DataGridViewColumn oColumn in oDataGridView.Columns)
            {
                oColumn.Visible = false;
            }

            oXmlDocument.Load(@"c:\temp\grid.xml");

            //Find the control's saved data in the XML
            szNodeQuery = "/DataGridView[@name='" + oDataGridView.Name + "']";

            //Obtain a reference to that control's information
            oXmlElement = oXmlDocument.DocumentElement;
            oXmlNodeList = oXmlElement.SelectNodes(szNodeQuery);

            if (oXmlNodeList.Count > 0)
            {
                foreach (XmlNode oXmlNode in oXmlNodeList[0].ChildNodes)
                {
                    oDataGridViewColumn = oDataGridView.Columns[oXmlNode.Attributes["name"].Value];
                    oDataGridViewColumn.Visible = bool.Parse(oXmlNode.Attributes["visible"].Value); 
                    oDataGridViewColumn.DisplayIndex = int.Parse(oXmlNode.Attributes["displayindex"].Value); 
                    oDataGridViewColumn.Width = int.Parse(oXmlNode.Attributes["width"].Value);                     
                }
            }

        }

        #endregion

        /*
<Controls>
  <Control type="CheckBox" name="checkBox2" value="False" />
  <Control type="TextBox" name="textBox1" value="Fred" />
  <Control type="ListBox" name="listBox1">
    <value>302</value>
    <value>303</value>
  </Control>
  <Control type="RadioButton" name="radioButton3" value="False" />
  <Control type="RadioButton" name="radioButton2" value="False" />
  <Control type="RadioButton" name="radioButton1" value="True" />
  <Control type="DateTimePicker" name="dateTimePicker1" value="Tuesday, October 14, 2008" />
  <Control type="ComboBox" name="comboBox1" value="103" />
  <Control type="CheckedListBox" name="checkedListBox1">
    <value>201</value>
  </Control>
  <Control type="CheckBox" name="checkBox1" value="True" />
</Controls>
*/

        #region "Filter Settings"

        private void LoadFilter(Control oControl, XmlElement oXmlElement)
        {                        
            XmlNodeList oXmlNodeList;
            string szControlType;
            string szNodeQuery;            

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
                        case "DataGridView":
                            foreach (XmlNode oXmlNode in oXmlNodeList[0].ChildNodes)
                            {
                                CheckDataGridViewItem(oXmlNode.InnerXml, "IsSelected", "UserID", ((DataGridView)oSubControl).Rows);
                            }

                            break;

                        case "CheckBox":
                            ((CheckBox)oSubControl).Checked = 
                                bool.Parse(oXmlNodeList[0].Attributes["value"].Value);
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

        #region "Check Items"

        private void CheckItem(ComboBox oComboBox, string szValue)
        {
            int iCnt = 0;

            foreach (ListItem oItem in oComboBox.Items)
            {
                if (oItem.Value == szValue)
                {
                    oComboBox.SelectedIndex = iCnt;
                    break;
                }

                iCnt++;

            }
        }

        private void CheckItem(CheckedListBox oCheckedListBox, string szValue)
        {
            int iCnt = 0;

            foreach (ListItem oItem in oCheckedListBox.Items)
            {
                if (oItem.Value == szValue)
                {
                    oCheckedListBox.SetItemChecked(iCnt, true);
                    break;
                }

                iCnt++;

            }
        }

        private void CheckItem(ListBox oListBox, string szValue)
        {
            int iCnt = 0;

            foreach (ListItem oItem in oListBox.Items)
            {
                if (oItem.Value == szValue)
                {
                    oListBox.SetSelected(iCnt, true); 
                    break;
                }

                iCnt++;

            }
        }

        private void CheckDataGridViewItem(string szValue, string szCheckColumn, string szDataColumn, DataGridViewRowCollection oDataGridViewRowCollection)
        {
            foreach (DataGridViewRow oDataGridViewRow in oDataGridViewRowCollection)
            {
                if (oDataGridViewRow.Cells[szDataColumn].Value != null)
                {
                    if (oDataGridViewRow.Cells[szDataColumn].Value.ToString() == szValue)
                    {
                        oDataGridViewRow.Cells[szCheckColumn].Value = "True";
                        break;
                    }
                }
            }
        }

        #endregion

        private XmlDocument SaveFilter(Control oControl, 
                                       XmlDocument oXmlDocument, 
                                       XmlNode oXMLMainNode)
        {
            string szControlType;
            string szName;
            string szValue;
            object oSelectedItem;

            foreach (Control oSubControl in oControl.Controls)
            {
                //Get the name of the control type - ListBox, CheckBox, etc.
                szControlType = oSubControl.GetType().Name;

                //Get the name of the control to serve as a unique identifier
                szName = oSubControl.Name;

                switch (szControlType)
                {
                    case "DataGridView":
                        oXmlDocument = AddDataGridViewItems(oXmlDocument, oXMLMainNode, szControlType, szName, "IsSelected", "UserID", ((DataGridView)oSubControl).Rows);
                        break;

                    case "CheckBox":
                        oXmlDocument = AddNode(oXmlDocument, oXMLMainNode, szControlType, 
                            szName, ((CheckBox)oSubControl).Checked.ToString());
                        break;

                    case "ComboBox":
                        oSelectedItem = (((ComboBox)oSubControl)).SelectedItem;
                        if (oSelectedItem != null)
                            szValue = ((ListItem)oSelectedItem).Value;
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

        private XmlDocument AddCheckedListBoxItems(XmlDocument oXmlDocument,
                            XmlNode oXMLMainNode,
                            string szControlType,
                            string szName,
                            CheckedListBox.CheckedItemCollection oItems)
        {
            XmlNode oXMLNode;
            XmlNode oXMLItemNode;
            XmlAttribute oXmlAttribute;

            oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element, "Control", string.Empty);
            oXMLMainNode.AppendChild(oXMLNode);

            oXmlAttribute = oXmlDocument.CreateAttribute("type");
            oXmlAttribute.Value = szControlType;
            oXMLNode.Attributes.Append(oXmlAttribute);

            oXmlAttribute = oXmlDocument.CreateAttribute("name");
            oXmlAttribute.Value = szName;
            oXMLNode.Attributes.Append(oXmlAttribute);

            foreach (ListItem oItem in oItems)
            {
                oXMLItemNode = oXmlDocument.CreateNode(XmlNodeType.Element, "value", string.Empty);
                oXMLItemNode.InnerText = oItem.Value.ToString();
                oXMLNode.AppendChild(oXMLItemNode);
            }

            return oXmlDocument;
        }

        private XmlDocument AddListBoxItems(XmlDocument oXmlDocument,
                    XmlNode oXMLMainNode,
                    string szControlType,
                    string szName,
                    ListBox.SelectedObjectCollection oItems)
        {
            XmlNode oXMLNode;
            XmlNode oXMLItemNode;
            XmlAttribute oXmlAttribute;

            oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element, "Control", string.Empty);
            oXMLMainNode.AppendChild(oXMLNode);

            oXmlAttribute = oXmlDocument.CreateAttribute("type");
            oXmlAttribute.Value = szControlType;
            oXMLNode.Attributes.Append(oXmlAttribute);

            oXmlAttribute = oXmlDocument.CreateAttribute("name");
            oXmlAttribute.Value = szName;
            oXMLNode.Attributes.Append(oXmlAttribute);

            foreach (ListItem oItem in oItems)
            {
                oXMLItemNode = oXmlDocument.CreateNode(XmlNodeType.Element, "value", string.Empty);
                oXMLItemNode.InnerText = oItem.Value.ToString();
                oXMLNode.AppendChild(oXMLItemNode);
            }

            return oXmlDocument;
        }

        private XmlDocument AddDataGridViewItems(XmlDocument oXmlDocument,
                    XmlNode oXMLMainNode,
                    string szControlType,
                    string szName,
                    string szCheckColumn,
                    string szDataColumn,
                    DataGridViewRowCollection oDataGridViewRowCollection)
        {
            XmlNode oXMLNode;
            XmlNode oXMLItemNode;
            XmlAttribute oXmlAttribute;

            oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element, "Control", string.Empty);
            oXMLMainNode.AppendChild(oXMLNode);

            oXmlAttribute = oXmlDocument.CreateAttribute("type");
            oXmlAttribute.Value = szControlType;
            oXMLNode.Attributes.Append(oXmlAttribute);

            oXmlAttribute = oXmlDocument.CreateAttribute("name");
            oXmlAttribute.Value = szName;
            oXMLNode.Attributes.Append(oXmlAttribute);

            foreach (DataGridViewRow oDataGridViewRow in oDataGridViewRowCollection)
            {
                if (oDataGridViewRow.Cells[szCheckColumn].Value != null)
                {
                    if (oDataGridViewRow.Cells[szCheckColumn].Value.ToString() == "True")
                    {
                        oXMLItemNode = oXmlDocument.CreateNode(XmlNodeType.Element, "value", string.Empty);
                        oXMLItemNode.InnerText = oDataGridViewRow.Cells[szDataColumn].Value.ToString();
                        oXMLNode.AppendChild(oXMLItemNode);
                    }
                }
            }

            return oXmlDocument;
        }

        private XmlDocument AddNode(XmlDocument oXmlDocument, 
                                    XmlNode oXMLMainNode, 
                                    string szControlType, 
                                    string szName,
                                    string szValue)
        {
            XmlNode oXMLNode;
            XmlAttribute oXmlAttribute;

            oXMLNode = oXmlDocument.CreateNode(XmlNodeType.Element, 
                "Control", string.Empty);
            oXMLMainNode.AppendChild(oXMLNode);

            oXmlAttribute = oXmlDocument.CreateAttribute("type");
            oXmlAttribute.Value = szControlType;
            oXMLNode.Attributes.Append(oXmlAttribute);

            oXmlAttribute = oXmlDocument.CreateAttribute("name");
            oXmlAttribute.Value = szName;
            oXMLNode.Attributes.Append(oXmlAttribute);

            oXmlAttribute = oXmlDocument.CreateAttribute("value");
            oXmlAttribute.Value = szValue;
            oXMLNode.Attributes.Append(oXmlAttribute);

            return oXmlDocument;
        }

        #endregion

        #endregion
    }


    public class ListItem
    {
        string mValue;
        string mText;
        string mOtherText;

        public ListItem(string szValue, string szText)
        {
            mValue = szValue;
            mText = szText;
        }

        public ListItem(string szValue, string szText, string szOtherText)
        {
            mValue = szValue;
            mText = szText;
            mOtherText = szOtherText;
        }

        public new string ToString()
        {
            return mText;
        }

        public string GetText()
        {
            return mText;
        }

        public string Value
        {
            get
            {
                return mValue;
            }
        }

        public string Text
        {
            get
            {
                return mText;
            }
        }

        public string OtherText
        {
            get
            {
                return mOtherText;
            }
        }

    }
}