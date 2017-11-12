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
    public partial class InvigilationConstraintView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            read_all();
        }

        protected void CreateDiv(String divID, String text)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("id", divID);
            div.Attributes.Add("class", "constraints");
            boxD.Controls.Add(div);
            div.InnerHtml = text;
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