using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016
{
    public partial class InvigilationConstraintAdd : System.Web.UI.Page
    {

        protected string stringPass;
        protected string success = "default";
        protected string[] variable;


        protected string return_currentConstraint
        {
            get
            {
                string[] text = System.IO.File.ReadAllLines(@"D:\ExamTimetabling2016(Combined)\FYP\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt");
                return new JavaScriptSerializer().Serialize(text);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            this.stringPass = Request.Form["stringPass"];
            this.success = Request.Form["success"];
            addConstraint();
            success = null;


        }
        protected void addConstraint()
        {
            if (success == "yes")
            {
                this.variable = Request.Form["arrVariable"].Split(',');
                String path = @"D:\ExamTimetabling2016(Combined)\FYP\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt";
                //int divVariableNumber = 0;
                //int count = 0;
                //int match = 0;
                //Boolean conflictConstraint = false;
                //string[] currentConstraint = System.IO.File.ReadAllLines(path);
                //foreach (string constraint in currentConstraint)
                //{
                //    string[] text = constraint.Split(' ');
                //for (int a = 0; a < text.Length; a++)
                //{
                //    if (Regex.IsMatch(text[a], @"^[a-zA-Z]+$") || Regex.IsMatch(text[a], @"[A-Za-z0-9_].*[0-9]"))
                //    {
                //        count++;
                //    }
                //}
                //for (int i = 0; i < text.Length; i++)
                //{
                //    if (Regex.IsMatch(text[i], @"^[a-zA-Z]+$") || Regex.IsMatch(text[i], @"[A-Za-z0-9_].*[0-9]"))
                //    {
                //        if (divVariableNumber == variable.Length)
                //        {
                //            break;
                //        }
                //        else if (String.Equals(text[i].ToUpper(), variable[divVariableNumber].ToUpper()))
                //        {
                //            i = -1;
                //            divVariableNumber++;
                //            match++;
                //        }

                //    }
                //}
                //using (StreamWriter sw = File.AppendText(path))
                //{
                //    sw.WriteLine(constraint+divVariableNumber+match+count);
                //}
                //divVariableNumber = 0;
                //if (match == count && match == variable.Length)
                //{
                //    conflictConstraint = true;
                //}
                //match = 0;
                //count = 0;
                //}
                //if (conflictConstraint == false)
                //{
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(stringPass);
                }
            }
        }
    }
}