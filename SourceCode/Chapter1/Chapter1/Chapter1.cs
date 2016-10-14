using System;
using System.Reflection;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Chapter1
{
    public partial class Form1 : Form
    {
        private Button cmdCheckSupportedLanguages;
        private Button cmdGenerateConstants;
        private Button cmdGenerateEnums;
        private Button cmdCustomCodeGeneration;
        private Button cmdGenerateCode;
    
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void GenerateCode()
        {
            CodeCreate oCodeCreate = new CodeCreate();

            oCodeCreate.CreateNamespaceAndClass("CodeDOM",
                        "MyClass",
                        TypeAttributes.Public | TypeAttributes.Abstract);

            oCodeCreate.AddConstructor();

            oCodeCreate.AddField(MemberAttributes.Private,
                "_szCustomerName",
                "Name of the customer",
                "System.String");

            oCodeCreate.AddField(MemberAttributes.Private, 
                "_iSalesRepID", 
                "Primary key ID of sales rep", 
                "System.Int32");

            oCodeCreate.AddProperty(MemberAttributes.Public | MemberAttributes.Final, 
                "CustomerName", 
                "Comment goes here", 
                "System.String", 
                true, 
                true, 
                "_szCustomerName");

            oCodeCreate.AddMethod(MemberAttributes.Public, 
                "DeleteCustomer", 
                "Executes SQL that deletes a customer", 
                "System.Void");

            oCodeCreate.GenerateCode(@"c:\temp\SampleCode.cs", "CSharp");

            MessageBox.Show(@"Code exported to c:\temp\SampleCode.cs");
        }


        private void GenerateConstants()
        {
            CodeCreate oCodeCreate = new CodeCreate();

            oCodeCreate.CreateNamespaceAndClass("CodeDOM",
                        "MyClass",
                        TypeAttributes.Public | TypeAttributes.Abstract);

            List<string> oList = new List<string>();

            oList.Add("Car");
            oList.Add("Bus");
            oList.Add("Limo");
            oList.Add("Tank");

            oCodeCreate.AddConstants(oList);

            oCodeCreate.GenerateCode(@"c:\temp\SampleCode.cs", "CSharp");
        }

        private void GenerateEnums()
        {
            CodeCreate oCodeCreate = new CodeCreate();

            oCodeCreate.CreateNamespaceAndClass("CodeDOM",
                        "MyClass",
                        TypeAttributes.Public | TypeAttributes.Abstract);

            List<string> oList = new List<string>();

            oList.Add("Car");
            oList.Add("Bus");
            oList.Add("Limo");
            oList.Add("Tank");

            oCodeCreate.AddEnum("Vehicles", oList);

            oCodeCreate.GenerateCode(@"c:\temp\SampleCode.cs", "CSharp");
        }

        private void SupportedLanguages()
        {
            CompilerInfo[] aCompilerInfo = CodeDomProvider.GetAllCompilerInfo();

            foreach (CompilerInfo oCompilerInfo in aCompilerInfo)
            {
                foreach (string szLanguage in oCompilerInfo.GetLanguages())
                {
                    MessageBox.Show(szLanguage);
                }
            }

            if (!CodeDomProvider.IsDefinedLanguage("GW-BASIC"))
                MessageBox.Show("GW-BASIC Not supported");

            if (!CodeDomProvider.IsDefinedLanguage("CSharp"))
                MessageBox.Show("CSharp Not supported");

            if (!CodeDomProvider.IsDefinedExtension(".BAS"))
                MessageBox.Show(".BAS Not supported");

            if (!CodeDomProvider.IsDefinedExtension(".cs"))
                MessageBox.Show(".cs Not supported"); 
        }

 


    class CodeCreate
    {
        CodeCompileUnit oCodeCompileUnit;
        CodeNamespace oCodeNamespace;
        CodeTypeDeclaration oCodeTypeDeclaration;
 
        public CodeCreate()
        {
            oCodeCompileUnit = new CodeCompileUnit();
        }

        public void CreateNamespaceAndClass(string szNamespace, 
                                            string szClassName, 
                                            TypeAttributes sTypeAttributes)
        {
            //Create a new instance of the CodeNamespace object 
            oCodeNamespace = new CodeNamespace(szNamespace);

            //Make namespaces available to the code, the equivalent of the "using" statement
            oCodeNamespace.Imports.Add(new CodeNamespaceImport("System.Data"));

            //Declare a CodeTypeDeclaration to create the class template, assign
            //it a name, indicate that it is indeed a class and set the attributes
            //as public and abstract.This is added to the CodeNamespace object which
            //in thurn is added to the CodeCompileUnit object
            oCodeTypeDeclaration = new CodeTypeDeclaration();
            oCodeTypeDeclaration.Name = szClassName;
            oCodeTypeDeclaration.IsClass = true;
            oCodeTypeDeclaration.TypeAttributes = sTypeAttributes;
            oCodeTypeDeclaration.BaseTypes.Add("MyBaseClass");
            oCodeNamespace.Types.Add(oCodeTypeDeclaration);

            oCodeCompileUnit.Namespaces.Add(oCodeNamespace);
        }

        public void AddEnum(string szName, List<string> oEnums)
        {
            //Instantiate a CodeTypeDeclaration and indicate that it is 
            //for the reation of an enumerator.
            CodeTypeDeclaration oCodeTypeDeclarationEnum = 
                new CodeTypeDeclaration(szName);
            oCodeTypeDeclarationEnum.IsEnum = true;

            int iCnt = 0;

            //Iterate through the generic list. Instantiate a CodeMemberField
            //object for each enum value. Assign a sequential number as a value,
            //and add the new enum value to the CodeTypeDeclaration Members collection
            foreach (string szEnum in oEnums)
            {
                CodeMemberField oCodeMemberField = new CodeMemberField();
                oCodeMemberField.Name = szEnum;
                oCodeMemberField.InitExpression = 
                    new CodePrimitiveExpression(iCnt++);
                oCodeTypeDeclarationEnum.Members.Add(oCodeMemberField);
            }

            oCodeNamespace.Types.Add(oCodeTypeDeclarationEnum);
            
        }

        public void AddConstants(List<string> oConstant)
        {
            int iCnt = 0;

            //Iterate through the generic list. Instantiate a CodeMemberField
            //object for each constant value. Assign a sequential number as a value,
            //and indicate that it is a public constant that we desire
            foreach (string szConstant in oConstant)
            {
                CodeMemberField oCodeMemberField =
                    new CodeMemberField(typeof(int), szConstant);
                oCodeMemberField.InitExpression = 
                    new CodePrimitiveExpression(iCnt++);
                oCodeMemberField.Attributes = 
                    MemberAttributes.Public | MemberAttributes.Const;
                oCodeTypeDeclaration.Members.Add(oCodeMemberField);
            }

        }

        public void AddField(MemberAttributes sMemberAttributes,
                              string szName,
                              string szComments,
                              string szType)
        {
            CodeMemberField oCodeMemberField = new CodeMemberField();
            oCodeMemberField.Attributes = sMemberAttributes;
            oCodeMemberField.Name = szName;
            oCodeMemberField.Type = new CodeTypeReference(szType);
            oCodeMemberField.Comments.Add(new CodeCommentStatement(szComments));
            oCodeTypeDeclaration.Members.Add(oCodeMemberField);
            
        }

        public void AddProperty(MemberAttributes sMemberAttributes,
                              string szName,
                              string szComments,
                              string szType,
                              bool bHasGet,
                              bool bHasSet,
                              string szReturnField)
        {
            //Instantiate a CodeMemberProperty object, indicate that this is a public method, 
            //assign the name and indicate that it has get and set accessors. Next, 
            //indicate the code that is associated with these accessors.
            CodeMemberProperty oCodeMemberProperty = new CodeMemberProperty();
            oCodeMemberProperty.Attributes = sMemberAttributes;
            oCodeMemberProperty.Name = szName;
            oCodeMemberProperty.HasGet = bHasGet;
            oCodeMemberProperty.HasSet = bHasSet;
            oCodeMemberProperty.Type = new CodeTypeReference(szType);
            oCodeMemberProperty.Comments.Add(new CodeCommentStatement(szComments));
            oCodeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), szReturnField)));
            oCodeMemberProperty.SetStatements.Add(new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), szReturnField), 
                new CodePropertySetValueReferenceExpression()));
            oCodeTypeDeclaration.Members.Add(oCodeMemberProperty);

        }

        public void AddMethod(MemberAttributes sMemberAttributes,
                              string szName,
                              string szComments,
                              string szType)
        {
            //Establish the structure of the method
            CodeMemberMethod oCodeMemberMethod = new CodeMemberMethod();
            oCodeMemberMethod.Attributes = sMemberAttributes;
            oCodeMemberMethod.Name = szName;
            oCodeMemberMethod.Comments.Add(new CodeCommentStatement(szComments));
            oCodeMemberMethod.ReturnType = new CodeTypeReference(szType);

            //Indicate the data type and name of the parameter
            oCodeMemberMethod.Parameters.Add(new 
                CodeParameterDeclarationExpression("System.Int32", "iCustomerID"));

            //declare a variable within the body of the method
            CodeVariableDeclarationStatement oCodeVariableDeclarationStatement = 
                new CodeVariableDeclarationStatement("System.String", "szSQL");
            CodeArgumentReferenceExpression oCodeArgumentReferenceExpression = 
                new CodeArgumentReferenceExpression("\"DELETE FROM\"");
            oCodeVariableDeclarationStatement.InitExpression = 
                oCodeArgumentReferenceExpression;
            oCodeMemberMethod.Statements.Add(oCodeVariableDeclarationStatement);


            //CodeVariableReferenceExpression oCodeVariableReferenceExpression =
            //    new CodeVariableReferenceExpression("oDatabase.Parameters");
            //CodeMethodInvokeExpression oCodeMethodInvokeExpression =
            //    new CodeMethodInvokeExpression(oCodeVariableReferenceExpression, "Add");
            //oCodeMethodInvokeExpression.Parameters.Add(
            //    new CodeVariableReferenceExpression("iCustomerID"));
            //oCodeMethodInvokeExpression.Parameters.Add(
            //    new CodePrimitiveExpression("12345"));
            //oCodeMemberMethod.Statements.Add(oCodeMethodInvokeExpression);

            CodeVariableReferenceExpression oCodeVariableReferenceExpression = 
                new CodeVariableReferenceExpression("i");
            CodePrimitiveExpression oCodePrimitiveExpression1 = 
                new CodePrimitiveExpression(1);
            CodePrimitiveExpression oCodePrimitiveExpression5 = 
                new CodePrimitiveExpression(5);
            CodeAssignStatement oCodeAssignStatement = 
                new CodeAssignStatement(oCodeVariableReferenceExpression, oCodePrimitiveExpression1);
            CodeBinaryOperatorExpression oCodeBinaryOperatorExpressionLessThan = 
                new CodeBinaryOperatorExpression(oCodeVariableReferenceExpression, 
                CodeBinaryOperatorType.LessThan, 
                oCodePrimitiveExpression5);
            CodeMethodInvokeExpression oCodeMethodInvokeExpression = 
                new CodeMethodInvokeExpression(oCodeVariableReferenceExpression, "ToString");
            CodeBinaryOperatorExpression oCodeBinaryOperatorExpression = 
                new CodeBinaryOperatorExpression(oCodeVariableReferenceExpression, 
                CodeBinaryOperatorType.Add, 
                oCodePrimitiveExpression1);
            CodeAssignStatement oCodeAssignStatement1 = 
                new CodeAssignStatement(oCodeVariableReferenceExpression, 
                oCodeBinaryOperatorExpression);
            CodeTypeReferenceExpression oCodeTypeReferenceExpression = 
                new CodeTypeReferenceExpression("MessageBox");
            CodeMethodReferenceExpression oCodeMethodReferenceExpression = 
                new CodeMethodReferenceExpression(oCodeTypeReferenceExpression, "Show");
            CodeExpressionStatement oCodeExpressionStatement = 
                new CodeExpressionStatement(
                new CodeMethodInvokeExpression(oCodeVariableReferenceExpression, "ToString"));
            CodeMethodInvokeExpression oCodeMethodInvokeExpressionToString = 
                new CodeMethodInvokeExpression(oCodeVariableReferenceExpression, "ToString");

            CodeIterationStatement oCodeIterationStatement = new CodeIterationStatement(
            oCodeAssignStatement,
            oCodeBinaryOperatorExpressionLessThan,
            oCodeAssignStatement1,
            new CodeStatement[] { new CodeExpressionStatement( 
                new CodeMethodInvokeExpression( 
                new CodeMethodReferenceExpression( 
            oCodeTypeReferenceExpression, "Show" ), 
                oCodeMethodInvokeExpressionToString ) ) });

            oCodeMemberMethod.Statements.Add(oCodeIterationStatement);

            oCodeTypeDeclaration.Members.Add(oCodeMemberMethod);
        }


        public void AddConstructor()
        {
            //Instantiate a CodeConstructor object
            CodeConstructor oCodeConstructor = new CodeConstructor();
            oCodeConstructor.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;

            //Declare the constructor's parameter data type and name
            oCodeConstructor.Parameters.Add(new CodeParameterDeclarationExpression(
                typeof(System.Int32), "iInvoiceID"));

            //Set the field name
            CodeFieldReferenceExpression oCodeFieldReferenceExpression =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "InvoiceID");

            //And set the variable containing the parameter to assign
            //to the Field.
            oCodeConstructor.Statements.Add(new 
                CodeAssignStatement(oCodeFieldReferenceExpression,
                new CodeArgumentReferenceExpression("iInvoiceID")));

            oCodeTypeDeclaration.Members.Add(oCodeConstructor);
        }


        public void GenerateCode(string szFileName, string szLanguage)
        {
            CodeDomProvider oCodeDomProvider = CodeDomProvider.CreateProvider(szLanguage);
            CodeGeneratorOptions oCodeGeneratorOptions = new CodeGeneratorOptions();
            oCodeGeneratorOptions.BracingStyle = "C";

            if (oCodeDomProvider.Supports(GeneratorSupport.DeclareEnums))
            {
                //output enums
            }
            else
            {
                //output constants
            }

            using (StreamWriter oStreamWriter = new StreamWriter(szFileName))
            {
                oCodeDomProvider.GenerateCodeFromCompileUnit(oCodeCompileUnit, oStreamWriter, oCodeGeneratorOptions);
            }

            if (oCodeDomProvider.Supports(GeneratorSupport.DeclareEnums))
            {
                    
            }
        }

    }

        private void InitializeComponent()
        {
            this.cmdGenerateCode = new System.Windows.Forms.Button();
            this.cmdCheckSupportedLanguages = new System.Windows.Forms.Button();
            this.cmdGenerateConstants = new System.Windows.Forms.Button();
            this.cmdGenerateEnums = new System.Windows.Forms.Button();
            this.cmdCustomCodeGeneration = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdGenerateCode
            // 
            this.cmdGenerateCode.Location = new System.Drawing.Point(64, 25);
            this.cmdGenerateCode.Name = "cmdGenerateCode";
            this.cmdGenerateCode.Size = new System.Drawing.Size(169, 23);
            this.cmdGenerateCode.TabIndex = 0;
            this.cmdGenerateCode.Text = "Generate Code";
            this.cmdGenerateCode.UseVisualStyleBackColor = true;
            this.cmdGenerateCode.Click += new System.EventHandler(this.cmdGenerateCode_Click);
            // 
            // cmdCheckSupportedLanguages
            // 
            this.cmdCheckSupportedLanguages.Location = new System.Drawing.Point(64, 54);
            this.cmdCheckSupportedLanguages.Name = "cmdCheckSupportedLanguages";
            this.cmdCheckSupportedLanguages.Size = new System.Drawing.Size(169, 23);
            this.cmdCheckSupportedLanguages.TabIndex = 1;
            this.cmdCheckSupportedLanguages.Text = "Check Supported Languages";
            this.cmdCheckSupportedLanguages.UseVisualStyleBackColor = true;
            this.cmdCheckSupportedLanguages.Click += new System.EventHandler(this.cmdCheckSupportedLanguages_Click);
            // 
            // cmdGenerateConstants
            // 
            this.cmdGenerateConstants.Location = new System.Drawing.Point(64, 83);
            this.cmdGenerateConstants.Name = "cmdGenerateConstants";
            this.cmdGenerateConstants.Size = new System.Drawing.Size(169, 23);
            this.cmdGenerateConstants.TabIndex = 2;
            this.cmdGenerateConstants.Text = "Generate Constants";
            this.cmdGenerateConstants.UseVisualStyleBackColor = true;
            this.cmdGenerateConstants.Click += new System.EventHandler(this.cmdGenerateConstants_Click);
            // 
            // cmdGenerateEnums
            // 
            this.cmdGenerateEnums.Location = new System.Drawing.Point(64, 112);
            this.cmdGenerateEnums.Name = "cmdGenerateEnums";
            this.cmdGenerateEnums.Size = new System.Drawing.Size(169, 23);
            this.cmdGenerateEnums.TabIndex = 3;
            this.cmdGenerateEnums.Text = "Generate Enums";
            this.cmdGenerateEnums.UseVisualStyleBackColor = true;
            this.cmdGenerateEnums.Click += new System.EventHandler(this.cmdGenerateEnums_Click);
            // 
            // cmdCustomCodeGeneration
            // 
            this.cmdCustomCodeGeneration.Location = new System.Drawing.Point(64, 140);
            this.cmdCustomCodeGeneration.Name = "cmdCustomCodeGeneration";
            this.cmdCustomCodeGeneration.Size = new System.Drawing.Size(169, 23);
            this.cmdCustomCodeGeneration.TabIndex = 4;
            this.cmdCustomCodeGeneration.Text = "Custom Code Generation";
            this.cmdCustomCodeGeneration.UseVisualStyleBackColor = true;
            this.cmdCustomCodeGeneration.Click += new System.EventHandler(this.cmdCustomCodeGeneration_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(292, 192);
            this.Controls.Add(this.cmdCustomCodeGeneration);
            this.Controls.Add(this.cmdGenerateEnums);
            this.Controls.Add(this.cmdGenerateConstants);
            this.Controls.Add(this.cmdCheckSupportedLanguages);
            this.Controls.Add(this.cmdGenerateCode);
            this.Name = "Form1";
            this.Text = "Chapter 1";
            this.ResumeLayout(false);

        }

        private void cmdCheckSupportedLanguages_Click(object sender, EventArgs e)
        {
            SupportedLanguages();
        }

        private void cmdGenerateCode_Click(object sender, EventArgs e)
        {
            GenerateCode();
        }

        private void cmdGenerateConstants_Click(object sender, EventArgs e)
        {
            GenerateConstants();
        }

        private void cmdGenerateEnums_Click(object sender, EventArgs e)
        {
            GenerateEnums();
        }

        private void cmdCustomCodeGeneration_Click(object sender, EventArgs e)
        {
            CustomCodeGeneration();
        }

        private string CSharpDataType(string szDBType)
        {
            string szResult = string.Empty;

            switch (szDBType.ToLower())
            {
                case "varchar":
                case "nvarchar":
                case "nchar":
                case "ntext":
                    szResult = "string";
                    break;

                case "int":
                    szResult = "int";
                    break;

                case "smallint":
                    szResult = "short";
                    break;

                case "tinyint":
                    szResult = "short";
                    break;

                case "money":
                    szResult = "float";
                    break;

                case "datetime":
                    szResult = "DateTime";
                    break;
            }

            return szResult;
        }

        private string CSharpPrefix(string szDBType)
        {
            string szResult = string.Empty;

            switch (szDBType.ToLower())
            {
                case "varchar":
                case "nvarchar":
                case "nchar":
                case "ntext":
                    szResult = "sz";
                    break;

                case "int":
                    szResult = "i";
                    break;

                case "smallint":
                    szResult = "s";
                    break;

                case "tinyint":
                    szResult = "s";
                    break;

                case "money":
                    szResult = "f";
                    break;

                case "datetime":
                    szResult = "d";
                    break;
            }

            return szResult;
        }

        private void CustomCodeGeneration()
        {
            SqlDatabase oDatabase;
            DbCommand oDbCommand;
            DataTable oDT;
            string szTableName = string.Empty;
            string szColumnName = string.Empty;
            string szDataType = string.Empty;
            string szSQL = @"SELECT TABLE_NAME, COLUMN_NAME, 
                             DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
                             FROM INFORMATION_SCHEMA.COLUMNS
                             ORDER BY TABLE_NAME, ORDINAL_POSITION";

            oDatabase = new SqlDatabase(@"Data Source=(local);Initial catalog=Northwind;Integrated security=SSPI");

            oDbCommand = oDatabase.GetSqlStringCommand(szSQL);

            oDT = oDatabase.ExecuteDataSet(oDbCommand).Tables[0];

            using (StreamWriter oStreamWriter = new StreamWriter(@"c:\temp\source.cs"))
            {
                foreach (DataRow oDR in oDT.Rows)
                {
                    //if a new table, begin a class declaration
                    if (szTableName != oDR["TABLE_NAME"].ToString())
                    {
                        //add a parens to close the previous class, but not
                        //for the first one
                        if (szTableName != string.Empty)
                        {
                            oStreamWriter.WriteLine("}");
                            oStreamWriter.WriteLine(string.Empty);
                        }

                        szTableName = oDR["TABLE_NAME"].ToString();

                        //declare the class
                        oStreamWriter.WriteLine("public class " + szTableName);
                        oStreamWriter.WriteLine("{");

                    }

                    szColumnName = oDR["COLUMN_NAME"].ToString();
                    szDataType = oDR["DATA_TYPE"].ToString();

                    //declare the internae class variable
                    oStreamWriter.WriteLine("\tprivate " + CSharpDataType(szDataType) + " _" +
                        CSharpPrefix(szDataType) + szColumnName + ";");

                    //declare the property
                    oStreamWriter.WriteLine("\tpublic " + CSharpDataType(szDataType) + " " +
                        szColumnName);
                    oStreamWriter.WriteLine("\t{");
                    oStreamWriter.WriteLine("\t\tget { return _" + CSharpPrefix(szDataType) +
                        szColumnName + "; }");
                    oStreamWriter.WriteLine("\t\tset { _" + CSharpPrefix(szDataType) +
                        szColumnName + " = value; }");
                    oStreamWriter.WriteLine("\t}");
                }

                //close the last table class
                oStreamWriter.WriteLine("}");

                oStreamWriter.Flush();
                oStreamWriter.Close();

            }
        }

    }
}