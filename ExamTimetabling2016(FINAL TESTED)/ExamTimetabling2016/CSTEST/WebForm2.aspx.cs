using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Diagnostics;
using Microsoft.CSharp;
using System.IO;
using System.Text;
using System.Reflection;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ExamTimetabling2016.CSTEST
{

    class RunClass
    {
        private object _Compiled = null;

        public RunClass()
        {
            RunClassConstruct();
        }

        private void RunClassConstruct()
        {
            //input file name
            string fileName = "HelloWorld.cs";

            //code compiler and provider
            ICodeCompiler cc = new CSharpCodeProvider().CreateCompiler();

            //compiler parameters
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("system.dll"); //includes
            cp.GenerateExecutable = true; //generate executable

            //run the compiler
            CompilerResults cr = cc.CompileAssemblyFromFile(cp, fileName);

            //check for compilation errors...
            if (cr.Errors.HasErrors)
            {
                StringBuilder error = new StringBuilder();
                error.Append("Error Compiling Expression: ");
                foreach (CompilerError err in cr.Errors)
                {
                    error.AppendFormat("{0}\n", err.ErrorText);
                }
                throw new Exception("Error Compiling Expression: " + error.ToString());
            }

            //load the newly generated assembly and create an instance to run...
            Assembly a = cr.CompiledAssembly;
            _Compiled = a.CreateInstance("Samples.HelloWorld");
        }

        //		public object run(string name)
        //		{
        //			MethodInfo mi = _Compiled.GetType().GetMethod(name);
        //			return mi.Invoke(_Compiled, null);
        //		}
    }

    class GenerateClass
    {
        static void Main(string[] args)
        {
            //output file name
            string fileName = "HelloWorld.cs";

            //text writer to write the code
            TextWriter tw = new StreamWriter(new FileStream(fileName, FileMode.Create));

            //code generator and code provider
            ICodeGenerator codeGenerator = new CSharpCodeProvider().CreateGenerator();
            CSharpCodeProvider cdp = new CSharpCodeProvider();
            codeGenerator = cdp.CreateGenerator();

            //namespace and includes
            CodeNamespace Samples = new CodeNamespace("Samples");
            Samples.Imports.Add(new CodeNamespaceImport("System"));

            //declare a class
            CodeTypeDeclaration Class1 = new CodeTypeDeclaration("HelloWorld");
            Samples.Types.Add(Class1);
            Class1.IsClass = true;

            //reference to a method - writeHelloWorld
            CodeMethodReferenceExpression Method1Ref = new CodeMethodReferenceExpression();
            Method1Ref.MethodName = "writeHelloWorld";
            CodeMethodInvokeExpression mi1 = new CodeMethodInvokeExpression();
            mi1.Method = Method1Ref;

            //constructor for the class
            CodeConstructor cc = new CodeConstructor();
            cc.Attributes = MemberAttributes.Public;
            cc.Statements.Add(mi1);
            Class1.Members.Add(cc);

            //method - writeHelloWorld
            CodeMemberMethod Method1 = new CodeMemberMethod();
            Method1.Name = "writeHelloWorld";
            Method1.ReturnType = new CodeTypeReference(typeof(void));
            //Method1.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "text"));
            CodeMethodInvokeExpression cs1 = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression("System.Console"), "WriteLine", new CodePrimitiveExpression("Hello World!!"));
            Method1.Statements.Add(cs1);
            Class1.Members.Add(Method1);

            //method - Main
            CodeEntryPointMethod Start = new CodeEntryPointMethod();
            Start.Statements.Add(new CodeSnippetStatement("HelloWorld hw = new HelloWorld();"));
            Class1.Members.Add(Start);

            //generate the source code file
            codeGenerator.GenerateCodeFromNamespace(Samples, tw, null);

            //close the text writer
            tw.Close();

            RunClass sampleRun = new RunClass();
        }
    }
    public partial class WebForm2 : System.Web.UI.Page
    {
        string assemblyAuthorName = "CX";
        string classAuthorName = "CS";
        string methodAuthorName = "CB";
        
        /*
        protected String first()
        {
            CodeCompileUnit unit = new CodeCompileUnit();
            CodeAttributeArgument[] arguments = new CodeAttributeArgument[2];
            arguments[0] = new CodeAttributeArgument(
                new CodePrimitiveExpression(assemblyAuthorName));//Create parameter for attribute 

            arguments[1] = new CodeAttributeArgument(
                new CodeSnippetExpression("DynamicCodeGeneration.CustomAttributes.GenerationMode.CodeDOM"));
            CodeAttributeDeclaration assemblyLevelAttribute = new CodeAttributeDeclaration(
                new CodeTypeReference("DynamicCodeGeneration.CustomAttributes.AssemblyLevelAttribute"),
                arguments);//Create attribute to be added to assembly
            unit.AssemblyCustomAttributes.Add(assemblyLevelAttribute);
            unit.ReferencedAssemblies.Add("DynamicCodeGeneration.Base.dll");
            unit.ReferencedAssemblies.Add("System.dll");
            unit.ReferencedAssemblies.Add("DynamicCodeGeneration.CustomAttributes.dll");
            unit.ReferencedAssemblies.Add("Microsoft.Crm.SdkTypeProxy.dll");
            CodeNamespace customEntityRoot = new CodeNamespace("DerivedRoot");//Create a namespace
            unit.Namespaces.Add(customEntityRoot);

            customEntityRoot.Imports.Add(new CodeNamespaceImport("System"));//Add references
            customEntityRoot.Imports.Add(new CodeNamespaceImport(
                             "DynamicCodeGeneration.Base"));//Add references
            customEntityRoot.Imports.Add(new CodeNamespaceImport(
                             "DynamicCodeGeneration.CustomAttributes"));//Add references

            CodeTypeDeclaration derived = new CodeTypeDeclaration("Derived");//Create class
            customEntityRoot.Types.Add(derived);//Add the class to namespace defined above

            CodeConstructor derivedClassConstructor = new CodeConstructor();//Create constructor
            derivedClassConstructor.Attributes = MemberAttributes.Public;
            derived.Members.Add(derivedClassConstructor);//Add constructor to class

            CodeAttributeArgument argument = new CodeAttributeArgument(
                new CodePrimitiveExpression(classAuthorName));

            CodeAttributeDeclaration classLevelAttribute =
                new CodeAttributeDeclaration(
                    new CodeTypeReference("DynamicCodeGeneration.CustomAttributes.ClassLevelAttribute"),
                        argument);//Create attribute to be added to class
            derived.CustomAttributes.Add(classLevelAttribute);

            derived.BaseTypes.Add(new CodeTypeReference("Base"));
            CodeMemberMethod derivedMethod = new CodeMemberMethod();
            derivedMethod.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            //Make this method an override of base class's method
            derivedMethod.Comments.Add(new CodeCommentStatement(new CodeComment("TestComment")));
            derivedMethod.Name = "Method";
            derivedMethod.ReturnType = new CodeTypeReference(typeof(void));

            arguments = new CodeAttributeArgument[2];
            arguments[0] = new CodeAttributeArgument(
                new CodeSnippetExpression("ComplexityLevel.SuperComplex"));
            //Create parameter for attribute
            arguments[1] = new CodeAttributeArgument(
                new CodePrimitiveExpression(methodAuthorName));
            CodeAttributeDeclaration methodLevelAttribute = new CodeAttributeDeclaration(
                new CodeTypeReference("DynamicCodeGeneration.CustomAttributes.MethodLevelAttribute"),
                arguments);//Create attribute to be added to method

            derivedMethod.CustomAttributes.Add(methodLevelAttribute);//Add attribute to method

            CodeSnippetStatement code = new CodeSnippetStatement("base.Method();");
            derivedMethod.Statements.Add(code);
            derived.Members.Add(derivedMethod);//Add method to the class

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeGenerator codeGenerator = codeProvider.CreateGenerator();
            StringBuilder generatedCode = new StringBuilder();
            StringWriter codeWriter = new StringWriter(generatedCode);

            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            //Keep the braces on the line following the statement or 
            //declaration that they are associated with
            codeGenerator.GenerateCodeFromCompileUnit(unit, codeWriter, options);

            Label1.Text = generatedCode.ToString();

            return BuildGeneratedCode();
        }
        protected String BuildGeneratedCode()
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler codeCompiler = codeProvider.CreateCompiler();

            CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("DynamicCodeGeneration.Base.dll");
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("DynamicCodeGeneration.CustomAttributes.dll");
            parameters.GenerateInMemory = false;

            CompilerResults results = codeCompiler.CompileAssemblyFromSource(parameters, this.Code);
            if (results.Errors.HasErrors)
            {
                string errorMessage = "";
                errorMessage = results.Errors.Count.ToString() + " Errors:";
                for (int x = 0; x < results.Errors.Count; x++)
                {
                    errorMessage = errorMessage + "\r\nLine: " +
                        results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;
                }
                return errorMessage;
            }
            return results.PathToAssembly;

        }*/

        protected void third()
        {
            

        }

        protected void forth()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
        var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll" }, "foo.exe", true);
        parameters.GenerateExecutable = true;
        CompilerResults results = csc.CompileAssemblyFromSource(parameters,
        @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Diagnostics;
using Microsoft.CSharp;
using System.IO;
using System.Text;
using System.Reflection;
using System.Data.Sql;
using System.Data.SqlClient;


            class Program {
              public static void Main(string[] args) {
                SqlConnection connection = new SqlConnection(""Data Source=lkb-pc\\sqlexpress;Initial Catalog=ExamTimetabling;Integrated Security=True;MultipleActiveResultSets=True"");
               string query = ""insert into ExamTimetabling.dbo.Branch(BranchCode,BranchName) values (11,88)"";
               SqlCommand myCommand = new SqlCommand(query, connection);
               connection.Open();
               myCommand.ExecuteNonQuery();
               connection.Close();
              }
            }");
        results.Errors.Cast<CompilerError>().ToList().ForEach(error => Console.WriteLine(error.ErrorText));
   
            }

        
        }
    }
