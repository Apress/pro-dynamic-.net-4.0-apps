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
    public partial class Chapter2 : Form
    {
        public Chapter2()
        {
            InitializeComponent();
        }

        private void cmdReflectionTypes_Click(object sender, EventArgs e)
        {
            ReflectionTypes oReflectionTypes = new ReflectionTypes();
            oReflectionTypes.ShowDialog();
        }

        private void cmdLoadingSharedAssemblies_Click(object sender, EventArgs e)
        {
            //This will only work on your machine if you have this component installed.
            Assembly oAssembly =
                Assembly.Load(@"Infragistics2.Win.UltraWinListBar.v9.1, 
                            Version=9.1.20091.1000, 
                            PublicKeyToken=7dd5c3163f2cd0cb, 
                            Culture = """);

            Type oType = oAssembly.GetType("Infragistics.Win.UltraWinListBar.UltraListBar");

            MessageBox.Show(oType.Name + " instance created."); 

        }

        private void cmdSelectForm_Click(object sender, EventArgs e)
        {
            SelectForm oSelectForm = new SelectForm();
            oSelectForm.ShowDialog(); 
        }

        private void cmdAssemblyExplorer_Click(object sender, EventArgs e)
        {
            AssemblyExplorer oAssemblyExplorer = new AssemblyExplorer();
            oAssemblyExplorer.ShowDialog(); 
        }
    }
}
