using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using System.Xml;
using Microsoft.Win32;


namespace Chapter3
{
    public partial class AddReference : Form
    {
        public AddReference()
        {
            InitializeComponent();
        }

        private void AddReference_Load(object sender, EventArgs e)
        {
            LoadAvailable();
            LoadChosen();
        }


        private void LoadAvailable()
        {
            List<string> oKeyList = new List<string>();
            List<string> oFileList = new List<string>();
            RegistryKey oRegistryKey;
            RegistryKey oSubKey;
            Assembly oAssembly;
            AssemblyFileVersionAttribute oAssemblyFileVersionAttribute;
            object[] aAssemblyFileVersionAttribute;
            DataTable oDT = new DataTable();
            DataRow oDR;
            string szDirectory = string.Empty;
            string szVersion = string.Empty;

            oDT.Columns.Add(new DataColumn("Name"));
            oDT.Columns.Add(new DataColumn("Version"));
            oDT.Columns.Add(new DataColumn("RunTime"));
            oDT.Columns.Add(new DataColumn("Path"));

            oKeyList.Add(@"SOFTWARE\Microsoft\.NETFramework\AssemblyFolders\");
            oKeyList.Add(@"SOFTWARE\Microsoft\.NETFramework\v2.0.50727\AssemblyFoldersEx\");

            //begin iterating through the two main registry entries
            foreach (string szRegistryKey in oKeyList)
            {
                //Get a reference to the registry entry
                oRegistryKey = Registry.LocalMachine.OpenSubKey(szRegistryKey);

                //for each entry in this registry entry
                foreach (string szKey in oRegistryKey.GetSubKeyNames())
                {
                    oSubKey = oRegistryKey.OpenSubKey(szKey);
                    
                    //iterate through every subkey contained within it.
                    foreach (string szValueName in oSubKey.GetValueNames())
                    {
                        //if that subkey has a value...
                        if (oRegistryKey.OpenSubKey(szKey).GetValue(szValueName) != null)
                        {
                            //it will be a reference to a directory on disk
                            szDirectory = 
                                oRegistryKey.OpenSubKey(szKey).GetValue(szValueName).ToString();
                            
                            //if that directory exists...
                            if (Directory.Exists(szDirectory))
                            {
                                //iterate through the files contained within it.
                                foreach (string szFile in Directory.
                                    GetFiles(szDirectory, "*.dll"))
                                {

                                    try
                                    {
                                        //if that file has not already been counted
                                        if (!oFileList.Contains(szFile))
                                        {
                                            oFileList.Add(szFile);

                                            szVersion = string.Empty;

                                            //...load it into an Assembly object
                                            oAssembly = Assembly.LoadFrom(szFile);

                                            //...and extract the version information
                                            aAssemblyFileVersionAttribute = 
                                                oAssembly.GetCustomAttributes(
                                                typeof(AssemblyFileVersionAttribute), 
                                                false);

                                            if (aAssemblyFileVersionAttribute.Length > 0)
                                            {
                                                oAssemblyFileVersionAttribute = 
                                                    (AssemblyFileVersionAttribute)
                                                    aAssemblyFileVersionAttribute[0];
                                                szVersion = oAssemblyFileVersionAttribute.Version;
                                            }

                                            //...which is added to the DataTable for 
                                            //binding to the DataGridView
                                            oDR = oDT.NewRow();

                                            oDR["Name"] = oAssembly.GetName().Name;
                                            oDR["Version"] = oAssembly.GetName().Version;
                                            oDR["RunTime"] = oAssembly.ImageRuntimeVersion;
                                            oDR["Path"] = szFile;

                                            oDT.Rows.Add(oDR);
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                    }

                                }

                            }

                        }
                    }
                }
            }

            FormatGrid(dgAvailable, oDT);

            dgAvailable.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgAvailable.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void LoadChosen()
        {
            Assembly oAssembly;
            AssemblyFileVersionAttribute oAssemblyFileVersionAttribute;
            object[] aAssemblyFileVersionAttribute;
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXmlMainNode;
            DataTable oDT = new DataTable();
            DataRow oDR;
            string szFileName = Application.StartupPath + @"\Settings.xml";
            string szFile;

            oXmlDocument.Load(szFileName);

            oXmlMainNode = oXmlDocument.ChildNodes[1];

            oDT.Columns.Add(new DataColumn("Name"));
            oDT.Columns.Add(new DataColumn("Version"));
            oDT.Columns.Add(new DataColumn("RunTime"));
            oDT.Columns.Add(new DataColumn("Path"));

            foreach (XmlNode oXmlNode in oXmlMainNode)
            {
                szFile = oXmlNode.Attributes["filename"].Value;

                oAssembly = Assembly.LoadFrom(szFile);

                aAssemblyFileVersionAttribute = oAssembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);

                if (aAssemblyFileVersionAttribute.Length > 0)
                {
                    oAssemblyFileVersionAttribute = (AssemblyFileVersionAttribute)aAssemblyFileVersionAttribute[0];
                    //szVersion = oAssemblyFileVersionAttribute.Version;
                }

                oDR = oDT.NewRow();

                oDR["Name"] = oAssembly.GetName().Name;
                oDR["Version"] = oAssembly.GetName().Version;
                oDR["RunTime"] = oAssembly.ImageRuntimeVersion;
                oDR["Path"] = szFile;

                oDT.Rows.Add(oDR);
            }

            FormatGrid(dgChosen, oDT);

            dgChosen.Columns[0].Width = dgAvailable.Columns[0].Width;
            dgChosen.Columns[1].Width = dgAvailable.Columns[1].Width;
        }

        private void FormatGrid(DataGridView oDGV, DataTable oDT)
        {
            oDGV.RowHeadersVisible = false;
            oDGV.CellBorderStyle = DataGridViewCellBorderStyle.None;
            oDGV.RowTemplate.Height = 16;
            oDGV.DataSource = oDT;
            oDGV.MultiSelect = true;
            oDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            oDGV.AllowUserToResizeRows = false;

            oDGV.Columns[0].HeaderText = "Component Name";

        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            XmlNode oXmlMainNode;
            XmlNode oXmlNode;
            XmlAttribute oXmlAttribute;
            string szFileName = Application.StartupPath + @"\Settings.xml";

            oXmlDocument.Load(szFileName);

            oXmlMainNode = oXmlDocument.ChildNodes[1];

            oXmlNode = oXmlDocument.ChildNodes[1];
            oXmlNode.RemoveAll();

            foreach (DataGridViewRow oRow in dgAvailable.SelectedRows)
            {
                oXmlNode = oXmlDocument.CreateElement("Reference");
                oXmlAttribute = oXmlDocument.CreateAttribute("filename");
                oXmlAttribute.Value = oRow.Cells["Path"].Value.ToString();
                oXmlNode.Attributes.Append(oXmlAttribute);
                oXmlMainNode.AppendChild(oXmlNode);
            }

            oXmlDocument.Save(szFileName);

            this.Close();
        }


    }
}