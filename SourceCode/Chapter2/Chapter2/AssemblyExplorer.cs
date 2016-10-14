using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ReflectionDemo
{
    public partial class AssemblyExplorer : Form
    {
        public AssemblyExplorer()
        {
            InitializeComponent();
        }


        private void LoadObjects()
        {
            Assembly oAssembly = Assembly.LoadFrom(Application.StartupPath + @"\MyApp.exe");
            Form oForm;
            TreeNode oTreeNode;
            string szName;
            string szBaseType;

            trvObjects.Nodes.Clear();

            try
            {
                foreach (Type oType in oAssembly.GetTypes())
                {
                    szName = oType.FullName;

                    if (oType.BaseType != null)
                    {
                        szBaseType = oType.BaseType.Name;

                        if (szBaseType == "Form")
                        {
                            oForm = (Form)Activator.CreateInstance(oAssembly.GetType(szName));

                            oTreeNode = trvObjects.Nodes.Add(oForm.Name);
                            DrillControls(oForm.Controls, oTreeNode);

                            oForm.Dispose();
                        }
                    }

                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder oExceptionSB = new StringBuilder(); 
                Exception[] aException = ex.LoaderExceptions;

                foreach (Exception oException in aException)
                {
                    oExceptionSB.Append(oException.Message);
                    oExceptionSB.Append("\n");
                }

                MessageBox.Show(oExceptionSB.ToString());  
            }

            trvObjects.ExpandAll();
        }


        private void DrillControls(Control.ControlCollection oControls, TreeNode oTreeNode)
        {
            TreeNode oSubTreeNode = null;
            string szControlType;

            foreach (Control oControl in oControls)
            {
                szControlType = oControl.GetType().Name;

                oSubTreeNode = oTreeNode.Nodes.Add(oControl.Name);

                if (oControl.HasChildren)
                    DrillControls(oControl.Controls, oSubTreeNode);

            }
        }


        private void DrillMenu(MenuStrip oMenuStrip, TreeNode oFormTreeNode)
        {
            TreeNode oSubTreeNode;
            Common.Security oSecurity;

            foreach (ToolStripMenuItem oToolStripMenuItem in oMenuStrip.Items)
            {
                if (oToolStripMenuItem.Tag != null)
                {
                    oSecurity = ((Common.Security)(oToolStripMenuItem.Tag));
                    oSubTreeNode = oFormTreeNode.Nodes.Add(oSecurity.Title + " " + oSecurity.Level.ToString());
                }

                if (oToolStripMenuItem.HasDropDownItems)
                    DrillMenuItem(oToolStripMenuItem, oFormTreeNode);
            }
        }

        private void DrillMenuItem(ToolStripMenuItem oToolStripMenuItem, TreeNode oTreeNode)
        {
            TreeNode oSubTreeNode;

            foreach (ToolStripItem oMenuItem in oToolStripMenuItem.DropDownItems)
            {
                oSubTreeNode = oTreeNode.Nodes.Add(oMenuItem.Name);
            }
        }

        private void cmdGetFormControl_Click(object sender, EventArgs e)
        {
            LoadObjects();
        }
    }


}