using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016.CSTEST
{
    public partial class ConstraintAndSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainTimeslotVenueControl mTimeslotControl = new MaintainTimeslotVenueControl();
            MaintainExaminationControl mExamControl = new MaintainExaminationControl();
            MaintainCourseControl mCourseControl = new MaintainCourseControl();
            MaintainFacultyControl mFacultyControl = new MaintainFacultyControl();

            List<int> durationList = mCourseControl.selectDistinctDuration();
            List<string> sessionList = mTimeslotControl.selectAllSession();
            List<char> paperTypeList = mExamControl.getDistinctExamPaperType();
            List<Faculty> facultyList = mFacultyControl.getFacultyList();

            //session ddl
            foreach (string session in sessionList)
            {
                DropDownListSession.Items.Add(session);
            }

            //duration ddl
            foreach (int duration in durationList)
            {
                ddlDuration.Items.Add(new ListItem(duration.ToString(),duration.ToString()));
            }

            //paper type ddl
            foreach (char paperType in paperTypeList)
            {
                ddlPaperType.Items.Add(new ListItem(paperType.ToString(), paperType.ToString()));
            }

            //faculty ddl
            foreach(Faculty faculty in facultyList)
            {
                ddlFacultyExam.Items.Add(new ListItem(faculty.FacultyName, faculty.FacultyCode.ToString()));
            }

            mFacultyControl.shutDown();
            mExamControl.shutDown();
            mTimeslotControl.shutDown();
            mCourseControl.shutDown();
        }

        protected void btnResetExam_Click(object sender, EventArgs e)
        {
            //session ddl
            DropDownListSession.ClearSelection();
            DropDownListSession.Items.FindByText("Not Specified").Selected = true;

            //duration ddl
            ddlDuration.ClearSelection();
            ddlDuration.Items.FindByText("Not Specified").Selected = true;

            //catofInvi ddl
            ddlCategoryOfInvigilation.ClearSelection();
            ddlCategoryOfInvigilation.SelectedIndex = 0;

            //paper type ddl
            ddlPaperType.ClearSelection();
            ddlPaperType.SelectedIndex = 0;

            //faculty ddl
            ddlFacultyExam.ClearSelection();
            ddlFacultyExam.SelectedIndex = 0;

            ///day of exam
            ddlDayOfExam.ClearSelection();
            ddlDayOfExam.SelectedIndex = 0;

            //checkboxes
            CheckBoxCnbl.Checked = false;
            CheckBoxDoubleSeating.Checked = false;
            
        }

        protected void btnResetStaff_Click(object sender, EventArgs e)
        {

            //ddl muslim
            ddlMuslim.ClearSelection();
            ddlMuslim.SelectedIndex = 0;

            //ddl experienced invigilator
            ddlExperiencedInvigilator.ClearSelection();
            ddlExperiencedInvigilator.SelectedIndex = 0;

            //ddl chief invigilator
            ddlChief.ClearSelection();
            ddlChief.SelectedIndex = 0;

            //ddl stsphd
            ddlSTSPhd.ClearSelection();
            ddlSTSPhd.SelectedIndex = 0;
        }
    }
}