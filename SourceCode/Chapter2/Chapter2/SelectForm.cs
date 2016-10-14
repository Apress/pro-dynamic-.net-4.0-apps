using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Xml; 

namespace ReflectionDemo
{
    public partial class SelectForm : Form
    {
        public SelectForm()
        {
            InitializeComponent();
        }

        private void cmdFormXML_Click(object sender, EventArgs e)
        {
            Form2XML();
        }
        
        private void Form2XML()
        {
            Assembly oAssembly = Assembly.LoadFrom(Application.StartupPath + @"\SampleForm.exe");
            Form oForm;
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXMLFormNode;
            XmlNode oXMLControlsNode;
            XmlAttribute oXmlAttribute;
            string szName;
            string szBaseType;

            oXmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);

            oXMLFormNode = oXmlDocument.CreateNode(XmlNodeType.Element,
                "Form", string.Empty);
            oXmlDocument.AppendChild(oXMLFormNode);

            foreach (Type oType in oAssembly.GetTypes())
            {
                szName = oType.FullName;

                if (oType.BaseType != null)
                {
                    szBaseType = oType.BaseType.Name;

                    if (szBaseType == "Form")
                    {
                        oForm = (Form)Activator.CreateInstance(oAssembly.GetType(szName));

                        oXmlAttribute = oXmlDocument.CreateAttribute("name");
                        oXmlAttribute.Value = oForm.Name;
                        oXMLFormNode.Attributes.Append(oXmlAttribute);

                        oXmlAttribute = oXmlDocument.CreateAttribute("text");
                        oXmlAttribute.Value = oForm.Text;
                        oXMLFormNode.Attributes.Append(oXmlAttribute);

                        oXmlAttribute = oXmlDocument.CreateAttribute("width");
                        oXmlAttribute.Value = oForm.Width.ToString();
                        oXMLFormNode.Attributes.Append(oXmlAttribute);

                        oXmlAttribute = oXmlDocument.CreateAttribute("height");
                        oXmlAttribute.Value = oForm.Height.ToString();
                        oXMLFormNode.Attributes.Append(oXmlAttribute);

                        oXmlAttribute = oXmlDocument.CreateAttribute("top");
                        oXmlAttribute.Value = oForm.Top.ToString();
                        oXMLFormNode.Attributes.Append(oXmlAttribute);

                        oXmlAttribute = oXmlDocument.CreateAttribute("left");
                        oXmlAttribute.Value = oForm.Left.ToString();
                        oXMLFormNode.Attributes.Append(oXmlAttribute);

                        oXMLControlsNode = oXmlDocument.CreateNode(XmlNodeType.Element,
                            "Controls", string.Empty);
                        oXMLFormNode.AppendChild(oXMLControlsNode);

                        DrillControls(oForm.Controls, oXmlDocument, oXMLControlsNode);

                        oForm.Dispose();
                        break;

                    }
                }
            }

            oXmlDocument.Save(Application.StartupPath + @"\forms.xml");
        }

        private void DrillControls(Control.ControlCollection oControls, 
            XmlDocument oXmlDocument, XmlNode oXMLControlsNode)
        {
            XmlAttribute oXmlAttribute;
            XmlNode oXMLControlNode;
            string szControlType;

            foreach (Control oControl in oControls)
            {
                szControlType = oControl.GetType().Name;

                oXMLControlNode = oXmlDocument.CreateNode(XmlNodeType.Element,
                    "Control", string.Empty);
                oXMLControlsNode.AppendChild(oXMLControlNode);

                oXmlAttribute = oXmlDocument.CreateAttribute("name");
                oXmlAttribute.Value = oControl.Name;
                oXMLControlNode.Attributes.Append(oXmlAttribute);

                oXmlAttribute = oXmlDocument.CreateAttribute("type");
                oXmlAttribute.Value = oControl.GetType().Name;
                oXMLControlNode.Attributes.Append(oXmlAttribute);

                oXmlAttribute = oXmlDocument.CreateAttribute("width");
                oXmlAttribute.Value = oControl.Width.ToString();
                oXMLControlNode.Attributes.Append(oXmlAttribute);

                oXmlAttribute = oXmlDocument.CreateAttribute("height");
                oXmlAttribute.Value = oControl.Height.ToString();
                oXMLControlNode.Attributes.Append(oXmlAttribute);

                oXmlAttribute = oXmlDocument.CreateAttribute("top");
                oXmlAttribute.Value = oControl.Top.ToString();
                oXMLControlNode.Attributes.Append(oXmlAttribute);

                oXmlAttribute = oXmlDocument.CreateAttribute("left");
                oXmlAttribute.Value = oControl.Left.ToString();
                oXMLControlNode.Attributes.Append(oXmlAttribute);

                if (szControlType == "CheckBox" ||
                    szControlType == "Button" ||
                    szControlType == "Label" ||
                    szControlType == "GroupBox" ||
                    szControlType == "TabPage")
                {
                    oXmlAttribute = oXmlDocument.CreateAttribute("text");
                    oXmlAttribute.Value = oControl.Text.ToString();
                    oXMLControlNode.Attributes.Append(oXmlAttribute);
                }

                if (szControlType == "ComboBox")
                {
                    oXmlAttribute = oXmlDocument.CreateAttribute("dropdownstyle");
                    oXmlAttribute.Value = ((ComboBox) oControl).DropDownStyle.ToString();
                    oXMLControlNode.Attributes.Append(oXmlAttribute);
                }

                if (oControl.HasChildren)
                    DrillControls(oControl.Controls, oXmlDocument, oXMLControlNode);

            }
        }

        private void cmdReCreateForm_Click(object sender, EventArgs e)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            XmlElement oXmlElement;
            Form oForm = new Form();

            oXmlDocument.Load(Application.StartupPath + @"\forms.xml");

            oXmlElement = oXmlDocument.DocumentElement;

            oForm.Name = oXmlElement.Attributes["name"].Value;
            oForm.Text = oXmlElement.Attributes["text"].Value;
            oForm.Width = int.Parse(oXmlElement.Attributes["width"].Value);
            oForm.Height = int.Parse(oXmlElement.Attributes["height"].Value);
            oForm.Top = int.Parse(oXmlElement.Attributes["top"].Value);
            oForm.Left = int.Parse(oXmlElement.Attributes["left"].Value);

            LoadControls(oForm.Controls, oXmlElement.ChildNodes[0].ChildNodes);

            oForm.ShowDialog(); 
        }

        private void LoadControls(Control.ControlCollection oControls, XmlNodeList oXmlNodeList)
        {
            Button oButton = null;
            CheckBox oCheckBox = null;
            GroupBox oGroupBox = null;
            Label oLabel = null;
            TextBox oTextBox = null;
            ComboBox oComboBox = null;
            DateTimePicker oDateTimePicker = null;
            CheckedListBox oCheckedListBox = null;
            TabControl oTabControl = null;
            TabPage oTabPage = null;
            string szControlType;
            string szName;
            int iTop = 0;
            int iLeft = 0;
            int iWidth = 0;
            int iHeight = 0;

            foreach (XmlNode oXmlNode in oXmlNodeList)
            {
                szControlType = oXmlNode.Attributes["type"].Value;
                szName = oXmlNode.Attributes["name"].Value;
                iWidth = int.Parse(oXmlNode.Attributes["width"].Value);
                iHeight = int.Parse(oXmlNode.Attributes["height"].Value);
                iTop = int.Parse(oXmlNode.Attributes["top"].Value);
                iLeft = int.Parse(oXmlNode.Attributes["left"].Value);
  
                switch (szControlType)
                {
                    case "Button":
                        oButton = new Button();
                        oButton.Name = szName;
                        oButton.Text = oXmlNode.Attributes["text"].Value;
                        oButton.Width = iWidth;
                        oButton.Height = iHeight;
                        oButton.Top = iTop;
                        oButton.Left = iLeft;
                        oControls.Add(oButton);
                        break;

                    case "CheckBox":
                        oCheckBox = new CheckBox();
                        oCheckBox.Name = szName;
                        oCheckBox.Text = oXmlNode.Attributes["text"].Value;
                        oCheckBox.Width = iWidth;
                        oCheckBox.Height = iHeight;
                        oCheckBox.Top = iTop;
                        oCheckBox.Left = iLeft;
                        oControls.Add(oCheckBox);
                        break;

                    case "GroupBox":
                        oGroupBox = new GroupBox();
                        oGroupBox.Name = szName;
                        oGroupBox.Text = oXmlNode.Attributes["text"].Value;
                        oGroupBox.Width = iWidth;
                        oGroupBox.Height = iHeight;
                        oGroupBox.Top = iTop;
                        oGroupBox.Left = iLeft;
                        oControls.Add(oGroupBox);

                        if (oXmlNode.HasChildNodes)
                            LoadControls(oGroupBox.Controls, oXmlNode.ChildNodes);

                        break;

                    case "Label":
                        oLabel = new Label();
                        oLabel.Name = szName;
                        oLabel.Text = oXmlNode.Attributes["text"].Value;
                        oLabel.Width = iWidth;
                        oLabel.Height = iHeight;
                        oLabel.Top = iTop;
                        oLabel.Left = iLeft;
                        oControls.Add(oLabel);
                        break;

                    case "TextBox":
                        oTextBox = new TextBox();
                        oTextBox.Name = szName;
                        oTextBox.Width = iWidth;
                        oTextBox.Height = iHeight;
                        oTextBox.Top = iTop;
                        oTextBox.Left = iLeft;
                        oControls.Add(oTextBox);
                        break;

                    case "ComboBox":
                        oComboBox = new ComboBox();
                        oComboBox.Name = szName;
                        oComboBox.Width = iWidth;
                        oComboBox.Height = iHeight;
                        oComboBox.Top = iTop;
                        oComboBox.Left = iLeft;

                        oComboBox.DropDownStyle = (ComboBoxStyle)Enum.Parse(typeof(ComboBoxStyle), oXmlNode.Attributes["dropdownstyle"].Value, true);

                        oControls.Add(oComboBox);
                        break;

                    case "DateTimePicker":
                        oDateTimePicker = new DateTimePicker();
                        oDateTimePicker.Name = szName;
                        oDateTimePicker.Width = iWidth;
                        oDateTimePicker.Height = iHeight;
                        oDateTimePicker.Top = iTop;
                        oDateTimePicker.Left = iLeft;
                        oControls.Add(oDateTimePicker);
                        break;

                    case "CheckedListBox":
                        oCheckedListBox = new CheckedListBox();
                        oCheckedListBox.Name = szName;
                        oCheckedListBox.Width = iWidth;
                        oCheckedListBox.Height = iHeight;
                        oCheckedListBox.Top = iTop;
                        oCheckedListBox.Left = iLeft;
                        oControls.Add(oCheckedListBox);
                        break;

                    case "TabControl":
                        oTabControl = new TabControl();
                        oTabControl.Name = szName;
                        oTabControl.Width = iWidth;
                        oTabControl.Height = iHeight;
                        oTabControl.Top = iTop;
                        oTabControl.Left = iLeft;
                        oControls.Add(oTabControl);

                        if (oXmlNode.HasChildNodes)
                            LoadControls(oTabControl.Controls, oXmlNode.ChildNodes);

                        break;

                    case "TabPage":
                        oTabPage = new TabPage();
                        oTabPage.Name = szName;
                        oTabPage.Text = oXmlNode.Attributes["text"].Value;
                        oTabPage.Width = iWidth;
                        oTabPage.Height = iHeight;
                        oTabPage.Top = iTop;
                        oTabPage.Left = iLeft;
                        oControls.Add(oTabPage);

                        if (oXmlNode.HasChildNodes)
                            LoadControls(oTabPage.Controls, oXmlNode.ChildNodes);

                        break;   
                }
               

            }
        }

        private void cmdLoadForms_Click(object sender, EventArgs e)
        {
            LoadForms();
        }

        private void LoadForms()
        {
            Assembly oAssembly = Assembly.LoadFrom(Application.StartupPath + @"\SampleForm.exe");
            string szName;

            foreach (Type oType in oAssembly.GetTypes())
            {
                szName = oType.FullName;

                if (oType.BaseType != null)
                {
                    if (oType.BaseType.Name == "Form")
                        lstForms.Items.Add(szName); 
                }
            }
        }


    }
}