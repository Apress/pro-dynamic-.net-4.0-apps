using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using System.Reflection;
using Common;

namespace Chapter3
{
    public partial class CompileSourceCode : Form
    {
        public CompileSourceCode()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void cmdCompileCode_Click(object sender, EventArgs e)
        {
            string szCode =
            @"using System;  
              using System.Windows.Forms;  

              namespace RunTimeCompile   
              {         
                  public class MyClass      
                  {                             
                      public void DisplayMessage()
                      {
                         MessageBox.Show(""Hello World"");
                         //  MessageBox.Show(txtFirstName.Text);
                      }         
                   }    
              }";

            //for (int x=0; x<=1000; x++)
            CompileCode(szCode);
        }

        public Assembly CompileCode(string szCode)
        {
            CodeDomProvider oCodeDomProvider;
            CompilerParameters oCompilerParameters;
            CompilerResults oCompilerResults;
            Assembly oAssembly = null;
            MethodInfo oMethodInfo;
            Type oType;
            object oObject;
            object oReturnValue;

            oCodeDomProvider = CodeDomProvider.CreateProvider("CSharp");
            oCompilerParameters = new CompilerParameters();

            oCompilerParameters.ReferencedAssemblies.Add("system.dll");
            //oCompilerParameters.ReferencedAssemblies.Add("system.xml.dll");
            //oCompilerParameters.ReferencedAssemblies.Add("system.data.dll");
            oCompilerParameters.ReferencedAssemblies.Add("system.windows.forms.dll");
            // oCompilerParameters.ReferencedAssemblies.Add("system.drawing.dll");
            oCompilerParameters.GenerateExecutable = false;
            oCompilerParameters.GenerateInMemory = true;

            //oCompilerParameters.OutputAssembly = @"c:\temp\HelloWorld.exe";

            //pass the compiler command-line switch to select the name of the EXE
            //oCompilerParameters.CompilerOptions = @"/out:c:\temp\HelloWorld.exe";

            //Create a PDB file to allow debugging
            //oCompilerParameters.IncludeDebugInformation = true;

            //Name the main class of the EXE
            //oCompilerParameters.MainClass = "RunTimeCompile.MyClass";

            //Treat warnings like errors, thereby terminating compilation
            //oCompilerParameters.TreatWarningsAsErrors = true;

            //Any warning level 2 or above will count as an error
            //oCompilerParameters.WarningLevel = 2;


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
                    //return null;
                }

                //StringCollection oOutput = oCompilerResults.Output;
                //StringBuilder oOutputSB = new StringBuilder();

                //foreach (string szOutput in oOutput)
                //{
                //    oOutputSB.Append(szOutput);
                //}

                //MessageBox.Show(oOutputSB.ToString()); 


                //Once the code is compiled obtain a reference to the in-memory assembly 
                oAssembly = oCompilerResults.CompiledAssembly;

                //Instantiate the DataValidation class and obtain a Type object reference to it 
                oObject = oAssembly.CreateInstance("RunTimeCompile.MyClass");
                oType = oObject.GetType();

                //Obtain a reference to the event handler
                oMethodInfo = oType.GetMethod("DisplayMessage");

                //Now that the property is set invoke the method
                oReturnValue = oMethodInfo.Invoke(oObject, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Errors found");
            }

            return oAssembly;

        }

        private void cmdAddReferences_Click(object sender, EventArgs e)
        {
            AddReference oAddReference = new AddReference();
            oAddReference.ShowDialog();
        }


   }

}