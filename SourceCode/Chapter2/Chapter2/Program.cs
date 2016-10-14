using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ReflectionDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ReflectionDemo.Chapter2());
        }

        static string GetBaseType(Type oType)
        {
            Type oTypeInfo = oType.BaseType;
            string szResult = string.Empty;

            while (szResult != "Form" && szResult != "UserControl")
            {
                oTypeInfo = oType.BaseType;

                oType = oTypeInfo;

                if (oTypeInfo == null)
                    break;

                if (oType.Name == "Form" || oType.Name == "UserControl")
                {
                    szResult = oType.Name;
                    break;
                }
            }

            return szResult;

        }

    }


}