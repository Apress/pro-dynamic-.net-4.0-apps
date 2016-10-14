using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using Microsoft.Win32;

namespace Menus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadFromXML()
        {
            XmlDocument oXmlDocument = new XmlDocument();
            MenuStrip oMenuStrip = new MenuStrip();
            ToolStripMenuItem oToolStripMenuItem;

            oXmlDocument.Load(@Application.StartupPath + @"\menu.xml");

            foreach (XmlNode oXmlNode in oXmlDocument.FirstChild)
            {
                oToolStripMenuItem = new ToolStripMenuItem();
                oToolStripMenuItem.Text = oXmlNode.Attributes["Text"].Value;
                oToolStripMenuItem.Name = oXmlNode.Attributes["Name"].Value;
                oToolStripMenuItem.Enabled = 
                    bool.Parse(oXmlNode.Attributes["Enabled"].Value);
                oToolStripMenuItem.Checked = 
                    bool.Parse(oXmlNode.Attributes["Checked"].Value);
                
                if (oXmlNode.HasChildNodes)
                    BuildMenu(oXmlNode, oToolStripMenuItem);
                
                oMenuStrip.Items.Add(oToolStripMenuItem);
                
            }

            this.Controls.Add(oMenuStrip); 
        }

        private void BuildMenu(XmlNode oXmlNode, ToolStripMenuItem oTopMenuItem)
        {
            ToolStripMenuItem oToolStripMenuItem;

            foreach (XmlNode oXmlMenuNode in oXmlNode.ChildNodes)
            {
                oToolStripMenuItem = new ToolStripMenuItem();
                oToolStripMenuItem.Text = oXmlMenuNode.Attributes["Text"].Value;
                oToolStripMenuItem.Name = oXmlMenuNode.Attributes["Name"].Value;
                oToolStripMenuItem.Enabled = 
                    bool.Parse(oXmlMenuNode.Attributes["Enabled"].Value);
                oToolStripMenuItem.Checked = 
                    bool.Parse(oXmlMenuNode.Attributes["Checked"].Value);

                WireEvent(oToolStripMenuItem, this, "Click", oXmlMenuNode.Attributes["Click"].Value);

                oTopMenuItem.DropDownItems.Add(oToolStripMenuItem);

                if (oXmlMenuNode.HasChildNodes)
                    BuildMenu(oXmlMenuNode, oToolStripMenuItem);
            }
        }

        private void WireEvent(ToolStripMenuItem oToolStripMenuItem, 
                               Form oForm, 
                               string szEventName, 
                               string szMethodName)
        {
            MethodInfo oMethodInfo;
            Delegate oDelegate;
            EventInfo oEventInfo = oToolStripMenuItem.GetType().GetEvent(szEventName);
            Type oType = oForm.GetType();

            oMethodInfo = oType.GetMethod(
                           szMethodName,
                           System.Reflection.BindingFlags.IgnoreCase |
                           System.Reflection.BindingFlags.Instance |
                           System.Reflection.BindingFlags.NonPublic);

            if (oMethodInfo != null)
            {
                oDelegate = Delegate.CreateDelegate(oEventInfo.EventHandlerType,
                    oForm, oMethodInfo.Name);

                oEventInfo.AddEventHandler(oToolStripMenuItem, oDelegate);
            }

        }

        private void Menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oToolStripMenuItem;

            oToolStripMenuItem = (ToolStripMenuItem)sender;

            MessageBox.Show(oToolStripMenuItem.Text);
        }

        private void Receivables(object sender, EventArgs e)
        {
            MessageBox.Show("Receivables");
        }

        private void Payables(object sender, EventArgs e)
        {
            MessageBox.Show("Payables");
        }

        private void Overdue(object sender, EventArgs e)
        {
            MessageBox.Show("Overdue");
        }

        private void Aging(object sender, EventArgs e)
        {
            MessageBox.Show("Aging");
        }

        private void PersonnelMgt(object sender, EventArgs e)
        {
            MessageBox.Show("PersonnelMgt");
        }

        private void Roster(object sender, EventArgs e)
        {
            MessageBox.Show("Roster");
        }

        private void LoadFromRegistry()
        {
            //RegistryKey oRegistryKey =
            //    Registry.CurrentUser.CreateSubKey("Software\\My Application");
            //oRegistryKey.SetValue("File1", @"c:\temp\File1.doc");
            //oRegistryKey.SetValue("File2", @"c:\docs\resume.doc");
            //oRegistryKey.SetValue("File3", @"c:\docs\BusinessPlan.xls");

            MenuStrip oMenuStrip = new MenuStrip();
            ToolStripMenuItem oToolStripMainMenuItem;
            ToolStripMenuItem oToolStripMenuItem;
            RegistryKey oRegistryKey = 
                Registry.CurrentUser.OpenSubKey("Software\\My Application", true); 

            oToolStripMainMenuItem = new ToolStripMenuItem();
            oToolStripMainMenuItem.Text = "Last Used Files";

            oMenuStrip.Items.Add(oToolStripMainMenuItem);

            foreach (string szValue in oRegistryKey.GetValueNames())
            {
                oToolStripMenuItem = new ToolStripMenuItem();
                oToolStripMenuItem.Text = oRegistryKey.GetValue(szValue).ToString();

                oToolStripMainMenuItem.DropDownItems.Add(oToolStripMenuItem);
            }

            this.Controls.Add(oMenuStrip); 
        }

        private void cmdLoadFromXML_Click(object sender, EventArgs e)
        {
            LoadFromXML();
        }

        private void cmdLoadFromRegistry_Click(object sender, EventArgs e)
        {
            LoadFromRegistry();
        }


    }
}