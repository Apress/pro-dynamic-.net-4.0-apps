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
using System.ComponentModel;
using System.Reflection;

namespace GridTools
{
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

        public DataDrivenProgramming.SelectColumns SelectColumnsForm()
        {
            return new DataDrivenProgramming.SelectColumns();
        }

        public void LoadFromXML(XmlDocument oXmlDocument, string szName)
        {
            XmlNode oXmlMainNode;

            if (oXmlDocument.ChildNodes.Count == 0)
                return;

            oXmlMainNode = oXmlDocument.ChildNodes[0];

            this.Name = oXmlMainNode.Attributes["name"].Value;
            this.SortedColumn = oXmlMainNode.Attributes["sortedcolumn"].Value;

            if (oXmlMainNode.Attributes["sortorder"].Value != string.Empty)
                this.ListSortDirection = 
                    (ListSortDirection)Enum.Parse(typeof(ListSortDirection), 
                    oXmlDocument.ChildNodes[0].Attributes["sortorder"].Value, true);
            else
                this.ListSortDirection = ListSortDirection.Ascending;

            foreach (XmlNode oXmlNode in oXmlDocument.ChildNodes[0].ChildNodes)
            {
                GridColumn oGridColumn = new GridColumn();

                oGridColumn.Name = oXmlNode.Attributes["name"].Value;
                oGridColumn.Visible = bool.Parse(oXmlNode.Attributes["visible"].Value);
                oGridColumn.DisplayIndex = 
                    int.Parse(oXmlNode.Attributes["displayindex"].Value);
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

    public class Common
    {
        public static void AddXMLAttribute(XmlDocument oXmlDocument, XmlNode oXMLParentNode, string szAttributeName, string szValue)
        {
            XmlAttribute oXmlAttribute = oXmlDocument.CreateAttribute(szAttributeName);
            oXmlAttribute.Value = szValue;
            oXMLParentNode.Attributes.Append(oXmlAttribute);
        }

    }
}
