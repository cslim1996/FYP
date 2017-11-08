using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ExamTimetabling2016.CSTEST
{
    public partial class WebForm3 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        //handle overlapping
        public void method1(string invigilatorQuery, string timetableQuery)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MaintainConstraint3Control mConstraintControl = new MaintainConstraint3Control();
            List<Constraint3> constraintList = mConstraintControl.loadFullConstraintList();

            foreach (Constraint3 constraint in constraintList)
            {
                Label1.Text += (constraint.ConstraintID.ToString() + constraint.ConstraintImportanceValue.ToString());
            }
        }
    }
}