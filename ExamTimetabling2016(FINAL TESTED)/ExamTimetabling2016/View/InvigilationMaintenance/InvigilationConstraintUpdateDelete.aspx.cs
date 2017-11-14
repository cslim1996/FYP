using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016
{
    public partial class InvigilationConstraintUpdateDelete : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            read_all();
        }

        protected void CreateDiv(String divID, String text)
        {
            String lineNumber;
            if (divID.Length == 11)
            {
                lineNumber = divID.Substring(divID.Length - 1);
            }
            else if (divID.Length == 12)
            {
                lineNumber = divID.Substring(divID.Length - 2);
            }
            else
            {
                lineNumber = divID.Substring(divID.Length - 3);
            }
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("id", divID);
            div.Attributes.Add("class", "constraints");
            boxD.Controls.Add(div);
            div.InnerHtml = text;
            Button deleteBtn = new Button();
            Button updateBtn = new Button();
            deleteBtn.Attributes.Add("id", "delete" + lineNumber);
            deleteBtn.Attributes.Add("class", "delete");
            deleteBtn.Attributes.Add("CustomParameter", lineNumber);
            deleteBtn.Text = "Delete";
            deleteBtn.OnClientClick += "return Confirm();";
            deleteBtn.Click += (sender, EventArgs) => { deleteBtnClick(sender, EventArgs); };
            updateBtn.Attributes.Add("id", "update" + lineNumber);
            updateBtn.Attributes.Add("class", "update");
            updateBtn.Text = "Update";
            updateBtn.Click += (sender, EventArgs) => { updateBtnClick(sender, EventArgs, text, lineNumber); };
            updateBtn.Attributes.Add("CustomParameter", lineNumber);
            div.Controls.Add(deleteBtn);
            div.Controls.Add(updateBtn);
        }
        protected void deleteBtnClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int deleleLine = Convert.ToInt32(clickedButton.Attributes["CustomParameter"].ToString());
            String line = null;
            int line_number = 0;
            string myScriptValue = "function callMe() {alert('You pressed Me!'); }";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myScriptName", myScriptValue, true);
            string tempFile = Path.GetTempFileName();

            using (var sr = new StreamReader(@"D:\ExamTimetabling2016(Combined)\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt"))
            using (var sw = new StreamWriter(tempFile))
            {

                while ((line = sr.ReadLine()) != null)
                {
                    line_number++;

                    if (line_number == deleleLine)
                        continue;

                    sw.WriteLine(line);
                }
            }

            File.Delete(@"D:\ExamTimetabling2016(Combined)\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt");
            File.Move(tempFile, @"D:\ExamTimetabling2016(Combined)\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt");
            Response.Redirect("InvigilationConstraintUpdateDelete.aspx");


        }
        protected void updateBtnClick(object sender, EventArgs e, string text, string line)
        {
            Button clickedButton = (Button)sender;
            string updateLine = text;
            String lineNumber = line;
            String newText = "";
            var myArray = text.Split(' ');
            int a;
            for (a = 0; a < myArray.Length; a++)
            {
                if (myArray[a] == "&&")
                    myArray[a] = "AND";
                if (myArray[a] == "||")
                    myArray[a] = "OR";
                newText += myArray[a] + " ";
            }
            if (a == myArray.Length)
                newText = newText.Substring(0, newText.Length - 1);
            updateLine = newText;
            Response.Redirect("InvigilationConstraintUpdate.aspx?LineUpdate=" + updateLine + "&lineNumber=" + lineNumber);
        }

        protected void read_all()
        {
            string[] text = System.IO.File.ReadAllLines(@"D:\ExamTimetabling2016(Combined)\FYP\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt");
            for (int a = 0; a < text.Length; a++)
            {

                CreateDiv("constraint" + (a + 1), text[a]);
            }


        }
    }
}