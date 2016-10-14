using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Xml; 

namespace DataDrivenProgramming
{
    public partial class SelectForm : Form
    {
        Assembly oAssembly;

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
            //Assembly oAssembly = Assembly.LoadFrom(@"C:\DOCS\DataDrivenProgramming\OASISForm\OASISForm\bin\Debug\OASISForm.exe");
            Assembly oAssembly = Assembly.LoadFrom(Application.ExecutablePath);
            Form oForm;
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXMLFormNode;
            XmlNode oXMLControlsNode;
            XmlDeclaration oXmlDeclaration;
            XmlAttribute oXmlAttribute;
            string szName;
            string szBaseType;

            oXmlDeclaration = oXmlDocument.CreateXmlDeclaration("1.0", 
                "UTF-8", null);

            oXmlDocument.AppendChild(oXmlDeclaration);

            oXMLFormNode = oXmlDocument.CreateNode(XmlNodeType.Element,
                "Form", string.Empty);
            oXmlDocument.AppendChild(oXMLFormNode);

            szName = cmbForms.SelectedItem.ToString();
            Type oType = oAssembly.GetType(szName);

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

                //if (szControlType == "CheckBox" ||
                //    szControlType == "ComboBox" ||
                //    szControlType == "CheckedListBox" ||
                //    szControlType == "ListBox" ||
                //    szControlType == "RadioButton" ||
                //    szControlType == "TextBox" ||
                //    szControlType == "DateTimePicker" ||
                //    szControlType == "Button")
                //{

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
            string szCode;

            oXmlDocument.Load(Application.StartupPath + @"\forms.xml");

            oXmlElement = oXmlDocument.DocumentElement;

            oForm.Name = oXmlElement.Attributes["name"].Value;
            oForm.Text = oXmlElement.Attributes["text"].Value;
            oForm.Width = int.Parse(oXmlElement.Attributes["width"].Value);
            oForm.Height = int.Parse(oXmlElement.Attributes["height"].Value);
            oForm.Top = int.Parse(oXmlElement.Attributes["top"].Value);
            oForm.Left = int.Parse(oXmlElement.Attributes["left"].Value);
  
            if (oXmlElement.Attributes["DataTable"] != null)
                oForm.Tag = oXmlElement.Attributes["DataTable"].Value;

            szCode = LoadControls(oForm.Controls, oXmlElement.ChildNodes[0].ChildNodes);

            oAssembly = CompileCode(szCode);

            oForm.ShowDialog(); 
        }

        private string LoadControls(Control.ControlCollection oControls, XmlNodeList oXmlNodeList)
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
            StringBuilder oCodeSB = new StringBuilder();
            string szControlType;
            string szName;
            int iTop = 0;
            int iLeft = 0;
            int iWidth = 0;
            int iHeight = 0;

            oCodeSB.Append("using System;");
            oCodeSB.Append("using System.Windows.Forms;");
            oCodeSB.Append("using DataDriven;");
            oCodeSB.Append("namespace RunTimeCompile");
            oCodeSB.Append("{");
            oCodeSB.Append("public class EventHandler");
            oCodeSB.Append("{");
            oCodeSB.Append("public Form oForm = null;");
            oCodeSB.Append("public object sender = null;");
            oCodeSB.Append("public EventArgs e = null;");

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

                        foreach (XmlNode oXmlEventNode in oXmlNode.ChildNodes)
                        {
                            if (oXmlEventNode.Name == "Click")
                            {
                                BuildCode(oCodeSB, oXmlEventNode, szName);

                                oButton.Click += new EventHandler(Button_Click);
                            }
                        }

                        break;

                    case "CheckBox":
                        oCheckBox = new CheckBox();
                        oCheckBox.Name = szName;
                        oCheckBox.Text = oXmlNode.Attributes["text"].Value;
                        oCheckBox.Width = iWidth;
                        oCheckBox.Height = iHeight;
                        oCheckBox.Top = iTop;
                        oCheckBox.Left = iLeft;

                        if (oXmlNode.Attributes["DataColumn"] != null)
                            oCheckBox.Tag = oXmlNode.Attributes["DataColumn"].Value;

                        oControls.Add(oCheckBox);

                        foreach (XmlNode oXmlEventNode in oXmlNode.ChildNodes)
                        {
                            if (oXmlEventNode.Name == "CheckedChanged")
                            {
                                BuildCode(oCodeSB, oXmlEventNode, szName);

                                oCheckBox.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
                            }   
                        }

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

                        if (oXmlNode.Attributes["DataColumn"] != null)
                            oCheckBox.Tag = oXmlNode.Attributes["DataColumn"].Value;

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

                        if (oXmlNode.Attributes["DataColumn"] != null)
                            oCheckBox.Tag = oXmlNode.Attributes["DataColumn"].Value;

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

                        if (oXmlNode.Attributes["DataColumn"] != null)
                            oCheckBox.Tag = oXmlNode.Attributes["DataColumn"].Value;

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

            oCodeSB.Append("}");
            oCodeSB.Append("}");

            return oCodeSB.ToString();
        }

        private void BuildCode(StringBuilder oCodeSB, XmlNode oXmlEventNode, string szName)
        {
            oCodeSB.Append("public void ");
            oCodeSB.Append(szName);
            oCodeSB.Append("_" + oXmlEventNode.Name + "()");
            oCodeSB.Append("{");
            oCodeSB.Append(oXmlEventNode.InnerText);
            oCodeSB.Append("}");
        }

        private void Button_Click(object sender, EventArgs e)
        {
            string szName = ((Button)sender).Name;

            RunMethod(szName + "_Click", this, sender, e);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string szName = ((CheckBox)sender).Name;

            RunMethod(szName + "_CheckedChanged", this, sender, e);
        }
        

        public Assembly CompileCode(string szCode)
        {
            CodeDomProvider oCodeDomProvider;
            CompilerParameters oCompilerParameters;
            CompilerResults oCompilerResults;
            Assembly oAssembly = null;

            //Instantiate the CSharpCodeProvider and ICodeCompiler objects. 
            //CSharpCodeProvider encapsulates both the .NET code generator 
            //and runtime compiler ICodeCompiler refers specifically 
            //to the runtime code compiler object 
            oCodeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            oCompilerParameters = new CompilerParameters();

            //Reference assemblies needed by the code to be compiled 
            //and indicate that the compiled assembly is to be generated 
            //in memory as opposed to an EXE or DLL file on disk. If you 
            //cannot anticipate what assemblies the user will need in advance 
            //you could require the user to enter using statements and then 
            //parse the code to retrieve the appropriate assembly names. 
            oCompilerParameters.ReferencedAssemblies.Add("system.dll");
            oCompilerParameters.ReferencedAssemblies.Add("system.xml.dll");
            oCompilerParameters.ReferencedAssemblies.Add("system.data.dll");
            oCompilerParameters.ReferencedAssemblies.Add("system.windows.forms.dll");
            oCompilerParameters.ReferencedAssemblies.Add("system.drawing.dll");
            oCompilerParameters.ReferencedAssemblies.Add(@"C:\DOCS\DataDrivenProgramming\DataDriven\DataDriven\bin\Debug\DataDriven.dll");
            oCompilerParameters.GenerateExecutable = false;
            oCompilerParameters.GenerateInMemory = false;
            oCompilerParameters.OutputAssembly = Application.StartupPath + @"\temp.dll";
            
            try
            {
                //Compile the source code                  
                oCompilerResults = oCodeDomProvider.CompileAssemblyFromSource(oCompilerParameters, szCode);

                //If there are any errors found, end processing. Optionally you can 
                //display the exact line number and error message to the user using 
                //the GetErrors function below. This can be helpful when providing the 
                //user with a simplified IDE to enter their code and check it for 
                //compile errors 
                if (oCompilerResults.Errors.Count > 0)
                {
                    MessageBox.Show(oCompilerResults.Errors[0].ToString(), "Errors found");
                    return null;
                }
                //Once the code is compiled obtain a reference to the in-memory assembly 
                oAssembly = oCompilerResults.CompiledAssembly;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Errors found");
            }

            return oAssembly;

        }

        private void RunMethod(string szMethodName,
                               Form oForm,
                               object sender,
                               EventArgs e)
        {
            MethodInfo oMethodInfo;
            FieldInfo oFieldInfo;
            Type oType;
            object oObject;
            object oReturnValue;

            oAssembly = Assembly.LoadFrom(Application.StartupPath + @"\temp.dll");

            //Instantiate the DataValidation class and obtain a Type object reference to it 
            oObject = oAssembly.CreateInstance("RunTimeCompile.EventHandler");
            oType = oObject.GetType();

            //Obtain a reference to the indicated property, 
            //and set the value of this property to the value you entered on the form. 
            oFieldInfo = oType.GetField("oForm");
            oFieldInfo.SetValue(oObject, oForm);

            oFieldInfo = oType.GetField("sender");
            oFieldInfo.SetValue(oObject, sender);

            oFieldInfo = oType.GetField("e");
            oFieldInfo.SetValue(oObject, e);

            //Obtain a reference to the event handler
            oMethodInfo = oType.GetMethod(szMethodName);

            //Now that the property is set invoke the method
            oReturnValue = oMethodInfo.Invoke(oObject, null);
        }

        private void cmdLoadForms_Click(object sender, EventArgs e)
        {
            LoadForms();
        }

        private void LoadForms()
        {
            Assembly oAssembly = Assembly.LoadFrom(Application.ExecutablePath);
            string szName;

            cmbForms.Items.Clear();

            foreach (Type oType in oAssembly.GetTypes())
            {
                szName = oType.FullName;

                if (oType.BaseType != null)
                {
                    if (oType.BaseType.Name == "Form")
                        cmbForms.Items.Add(szName); 
                }
            }
        }

        #region "XAML"

        private void cmdFormXAML_Click(object sender, EventArgs e)
        {
            Form2XAML();
        }

        private void Form2XAML()
        {
            Assembly oAssembly = Assembly.LoadFrom(Application.ExecutablePath);
            Form oForm;
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXMLFormNode;
            XmlNode oXMLControlsNode;
            XmlAttribute oXmlAttribute;
            string szName;
            string szBaseType;

            oXMLFormNode = oXmlDocument.CreateNode(XmlNodeType.Element,
                "Window", string.Empty);

            oXmlAttribute = oXmlDocument.CreateAttribute("x:Class");
            oXmlAttribute.Value = "DataDrivenWPF.ReadXAML";
            oXMLFormNode.Attributes.Append(oXmlAttribute);

            oXmlAttribute = oXmlDocument.CreateAttribute("xmlns");
            oXmlAttribute.Value = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
            oXMLFormNode.Attributes.Append(oXmlAttribute);

            oXmlAttribute = oXmlDocument.CreateAttribute("xmlns:x");
            oXmlAttribute.Value = "http://schemas.microsoft.com/winfx/2006/xaml";
            oXMLFormNode.Attributes.Append(oXmlAttribute);

            oXmlDocument.AppendChild(oXMLFormNode);

            szName = cmbForms.SelectedItem.ToString();
            Type oType = oAssembly.GetType(szName);

            szName = oType.FullName;

            if (oType.BaseType != null)
            {
                szBaseType = oType.BaseType.Name;

                if (szBaseType == "Form")
                {
                    oForm = (Form)Activator.CreateInstance(oAssembly.GetType(szName));

                    oXmlAttribute = oXmlDocument.CreateAttribute("Title");
                    oXmlAttribute.Value = oForm.Text;
                    oXMLFormNode.Attributes.Append(oXmlAttribute);

                    oXMLControlsNode = oXmlDocument.CreateNode(XmlNodeType.Element,
                        "Grid", string.Empty);
                    oXMLFormNode.AppendChild(oXMLControlsNode);

                    DrillControlsXAML(oForm.Controls, oXmlDocument, oXMLControlsNode);

                    oForm.Dispose();

                }

            }

            oXmlDocument.Save(Application.StartupPath + @"\forms.xaml");
        }

        private void DrillControlsXAML(Control.ControlCollection oControls,
            XmlDocument oXmlDocument, XmlNode oXMLControlsNode)
        {
            XmlAttribute oXmlAttribute;
            XmlNode oXMLControlNode;
            string szControlType;

            foreach (Control oControl in oControls)
            {
                szControlType = oControl.GetType().Name;

                oXMLControlNode = oXmlDocument.CreateNode(XmlNodeType.Element,
                    szControlType, string.Empty);
                oXMLControlsNode.AppendChild(oXMLControlNode);

                oXmlAttribute = oXmlDocument.CreateAttribute("Name");
                oXmlAttribute.Value = oControl.Name;
                oXMLControlNode.Attributes.Append(oXmlAttribute);

                if (szControlType == "Label" || szControlType == "Button")
                {
                    oXmlAttribute = oXmlDocument.CreateAttribute("Content");
                    oXmlAttribute.Value = oControl.Text;
                    oXMLControlNode.Attributes.Append(oXmlAttribute);
                }

                if (szControlType == "GroupBox")
                {
                    oXmlAttribute = oXmlDocument.CreateAttribute("Header");
                    oXmlAttribute.Value = oControl.Text.ToString();
                    oXMLControlNode.Attributes.Append(oXmlAttribute);

                    oXMLControlsNode = oXmlDocument.CreateNode(XmlNodeType.Element, "Grid", string.Empty);
                    oXMLControlNode.AppendChild(oXMLControlsNode);
                }

                if (oControl.HasChildren)
                    DrillControlsXAML(oControl.Controls, oXmlDocument, oXMLControlNode);

            }
        }

        #endregion

    }
}