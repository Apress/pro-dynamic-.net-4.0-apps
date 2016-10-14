using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom.Compiler;

namespace SupportedLanguages
{
    class Program
    {
        static void Main(string[] args)
        {
            CompilerInfo[] aCompilerInfo = CodeDomProvider.GetAllCompilerInfo();

            foreach (CompilerInfo oCompilerInfo in aCompilerInfo)
            {
                foreach (string szLanguage in oCompilerInfo.GetLanguages())
                {
                    Console.Write(szLanguage + "\n");
                }
            }

        }
    }
}
