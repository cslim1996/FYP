using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ExamTimetabling2016
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
            MaintainConstraintSettingControl mConstraintSetting = new MaintainConstraintSettingControl();
            ConstraintSetting setting = new ConstraintSetting();
            setting.AssignToExaminer = true;
            setting.MaxEveningSession = 2;
            mConstraintSetting.saveIntoDatabase(setting);
            mConstraintSetting.shutDown();
        }
    }
}