using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016
{
    public partial class InvigilationConstraintUpdate : System.Web.UI.Page
    {
        protected string stringPass;
        protected string success;
        protected int linenumber;
        protected string updateline;
        protected string[] variable;
        protected string return_line
        {
            get
            {
                string previousUpdateline = Request.QueryString["lineNumber"];
                return previousUpdateline.ToString();

            }
        }
        protected string return_constraint
        {
            get
            {
                updateline = Request.QueryString["LineUpdate"];
                var myArray = updateline.Split(' ');

                return new JavaScriptSerializer().Serialize(myArray);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            this.linenumber = Convert.ToInt32(Request.Form["linenumber"]);
            this.stringPass = Request.Form["stringPass"];
            this.success = Request.Form["success"];
            string errorMessage = updateConstraint();
            if (IsPostBack)
            {
                Response.Redirect("InvigilationConstraintUpdateDelete.aspx");
            }


        }

        protected string updateConstraint()
        {
            if (success == "yes")
            {
                String line = null;
                int line_number = 0;
                string tempFile = Path.GetTempFileName();
                string[] currentConstraint = System.IO.File.ReadAllLines(@"D:\ExamTimetabling2016(Combined)\FYP\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt");
                this.variable = Request.Form["arrVariable"].Split(',');
                string[] checkVariable = stringPass.Split(' ');
                using (var sr = new StreamReader(@"D:\ExamTimetabling2016(Combined)\FYP\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt"))
                using (var sw = new StreamWriter(tempFile))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        line_number++;

                        if (line_number == linenumber)
                        {
                            sw.WriteLine(stringPass);
                            continue;
                        }
                        else
                        {
                            sw.WriteLine(line);
                        }

                    }
                }
                File.Delete(@"D:\ExamTimetabling2016(Combined)\FYP\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt");
                File.Move(tempFile, @"D:\ExamTimetabling2016(Combined)\FYP\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt");

                return " ";

            } return " ";
        }
    }
}

