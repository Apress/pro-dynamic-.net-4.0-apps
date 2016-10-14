using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace ReflectionDemo
{
    public partial class ReflectionTypes : Form
    {
        public ReflectionTypes()
        {
            InitializeComponent();
        }

            private void button1_Click(object sender, EventArgs e)
        {
            Type oType;
                //Application.StartupPath
            Assembly oAssembly = Assembly.LoadFrom(Application.StartupPath + @"\Reflection.exe");
            oType = oAssembly.GetType("ReflectionDemo.Invoice");

            LoadAssemblyInfo(oAssembly);

            //MethodInfo[] aMethodInfo = oType.GetMethods(BindingFlags.Instance |
            //                                            BindingFlags.DeclaredOnly |
            //                                            BindingFlags.Public);
            MethodInfo[] aMethodInfo = oType.GetMethods();
            LoadMethods(aMethodInfo);

            FieldInfo[] aFieldInfo = oType.GetFields();
            LoadFields(aFieldInfo);

            PropertyInfo[] aPropertyInfo = oType.GetProperties();
            LoadProperties(aPropertyInfo);

            Type[] aInterfaces = oType.GetInterfaces();
            LoadInterfaces(aInterfaces);

            ConstructorInfo[] aConstructorInfo = oType.GetConstructors();
            LoadConstructors(aConstructorInfo);

            EventInfo[] aEventInfo = oType.GetEvents();
            LoadEvents(aEventInfo);

            MemberInfo[] aMemberInfo = oType.GetMembers();
            
        }

        private void LoadAssemblyInfo(Assembly oAssembly)
        {
            TreeNode oTN;

            trvAssembly.Nodes.Clear();

            oTN = trvAssembly.Nodes.Add(oAssembly.FullName);
            oTN.NodeFont = new Font("Arial", 10, FontStyle.Bold);

            oTN.Nodes.Add("CodeBase: " + oAssembly.CodeBase);
            oTN.Nodes.Add("EntryPoint.Name: " + oAssembly.EntryPoint.Name);
            oTN.Nodes.Add("GlobalAssemblyCache: " + oAssembly.GlobalAssemblyCache.ToString());
            oTN.Nodes.Add("Location: " + oAssembly.Location);

            oTN = oTN.Nodes.Add("Modules");
            oTN.NodeFont = new Font("Arial", 10, FontStyle.Bold);

            foreach (Module oModule in oAssembly.GetModules())
            {
                oTN.Nodes.Add("Name: " + oModule.Name);
            }

            trvAssembly.ExpandAll();
        }

        private void LoadMethods(MethodInfo[] aMethodInfo)
        {
            TreeNode oTN;

            trvMethod.Nodes.Clear();

            foreach (MethodInfo oMI in aMethodInfo)
            {
                oTN = trvMethod.Nodes.Add(oMI.Name);
                oTN.NodeFont = new Font("Arial", 10, FontStyle.Bold);
                
                oTN.Nodes.Add("IsAbstract: " + oMI.IsAbstract);
                oTN.Nodes.Add("IsGenericMethod: " + oMI.IsGenericMethod);
                oTN.Nodes.Add("IsPrivate: " + oMI.IsPrivate);
                oTN.Nodes.Add("IsPublic: " + oMI.IsPublic);
                oTN.Nodes.Add("IsStatic: " + oMI.IsStatic);
                oTN.Nodes.Add("IsVirtual: " + oMI.IsVirtual);
                oTN.Nodes.Add("ReturnType: " + oMI.ReturnType);

                if (oMI.GetParameters().Length > 0)
                {
                    oTN = oTN.Nodes.Add("Parameters:");
                    oTN.NodeFont = new Font("Arial", 8, FontStyle.Bold);
                }

                foreach (ParameterInfo oPI in oMI.GetParameters())
                {
                    oTN = oTN.Nodes.Add(oPI.Name);

                    oTN.Nodes.Add("DefaultValue: " + oPI.DefaultValue); 
                    oTN.Nodes.Add("IsIn: " + oPI.IsIn);
                    oTN.Nodes.Add("IsOut: " + oPI.IsOut);
                    oTN.Nodes.Add("IsOptional: " + oPI.IsOptional);
                    oTN.Nodes.Add("Position: " + oPI.Position);
                }

            }

            trvMethod.ExpandAll(); 
        }

        private void LoadFields(FieldInfo[] aFieldInfo)
        {
            TreeNode oTN;

            trvField.Nodes.Clear();

            foreach (FieldInfo oFI in aFieldInfo)
            {
                oTN = trvField.Nodes.Add(oFI.Name);
                oTN.NodeFont = new Font("Arial", 10, FontStyle.Bold);

                oTN.Nodes.Add("IsPrivate: " + oFI.IsPrivate);
                oTN.Nodes.Add("IsPublic: " + oFI.IsPublic);
                oTN.Nodes.Add("IsStatic: " + oFI.IsStatic);
            }

            trvField.ExpandAll();
        }

        private void LoadProperties(PropertyInfo[] aPropertyInfo)
        {
            TreeNode oTN;

            trvProperty.Nodes.Clear();

            foreach (PropertyInfo oPI in aPropertyInfo)
            {
                oTN = trvProperty.Nodes.Add(oPI.Name);
                oTN.NodeFont = new Font("Arial", 10, FontStyle.Bold);

                oTN.Nodes.Add("CanRead: " + oPI.CanRead);
                oTN.Nodes.Add("CanWrite: " + oPI.CanWrite);
            }

            trvProperty.ExpandAll();
        }


        private void LoadInterfaces(Type[] aInterfaces)
        {
            TreeNode oTN;

            trvInterfaces.Nodes.Clear();

            foreach (Type oType in aInterfaces)
            {
                oTN = trvInterfaces.Nodes.Add(oType.FullName);
                oTN.NodeFont = new Font("Arial", 10, FontStyle.Bold);
            }

            trvInterfaces.ExpandAll();
        }


        private void LoadConstructors(ConstructorInfo[] aConstructorInfo)
        {
            TreeNode oTN;
            TreeNode oSubTN;

            trvConstructors.Nodes.Clear();

            foreach (ConstructorInfo oCI in aConstructorInfo)
            {
                oTN = trvConstructors.Nodes.Add(oCI.Name);
                oTN.NodeFont = new Font("Arial", 10, FontStyle.Bold);

                foreach (ParameterInfo oPI in oCI.GetParameters())
                {
                    oSubTN = oTN.Nodes.Add(oPI.Name);

                    oSubTN.Nodes.Add("DefaultValue: " + oPI.DefaultValue);
                    oSubTN.Nodes.Add("IsIn: " + oPI.IsIn);
                    oSubTN.Nodes.Add("IsOut: " + oPI.IsOut);
                    oSubTN.Nodes.Add("IsOptional: " + oPI.IsOptional);
                    oSubTN.Nodes.Add("Position: " + oPI.Position.ToString());
                }
            }

            trvConstructors.ExpandAll();
        }


        private void LoadEvents(EventInfo[] aEventInfo)
        {
            TreeNode oTN;

            trvEvents.Nodes.Clear();

            foreach (EventInfo oEI in aEventInfo)
            {
                oTN = trvEvents.Nodes.Add(oEI.Name);
                oTN.NodeFont = new Font("Arial", 10, FontStyle.Bold);

                oTN = oTN.Nodes.Add(oEI.EventHandlerType.Name.ToString());
            }

            trvEvents.ExpandAll();
        }

        private void cmdInvokeMethod_Click(object sender, EventArgs e)
        {
            Type oType;
            MethodInfo oMI;
            object oInvoice;
            object oResult;

            oType = Type.GetType("ReflectionDemo.Invoice");
            oInvoice = Activator.CreateInstance(oType);

            oMI = oInvoice.GetType().GetMethod("GetTotal");
            oResult = oMI.Invoke(oInvoice, null);


            object[] aParams = new object[1];
            aParams[0] = .05;

            oMI = oInvoice.GetType().GetMethod("ApplyDiscount");
            oResult = oMI.Invoke(oInvoice, aParams);

            PropertyInfo oPI;
            oPI = oType.GetProperty("SalesRepID");
            oPI.SetValue(oInvoice, 123, null);

            //oResult = oPI.GetValue("SalesRepID", null);


            Assembly oAssembly;
            object oObject;
            oAssembly = Assembly.LoadFrom(Application.StartupPath + @"\Reflection.exe");
            oObject = oAssembly.CreateInstance("ReflectionDemo.Invoice");
            oType = oObject.GetType();

            MessageBox.Show(oType.Name + " instance created.");
        }


    }


}