using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016
{
    public partial class ConstraintAndSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tbSession.Enabled = false;
            tbDuration.Enabled = false;
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
                DropDownListSession.Items.Add(new ListItem(session,session));
            }

            //duration ddl
            foreach (int duration in durationList)
            {
                ddlDuration.Items.Add(new ListItem(duration.ToString(), duration.ToString()));
            }

            //paper type ddl
            foreach (char paperType in paperTypeList)
            {
                ddlPaperType.Items.Add(new ListItem(paperType.ToString(), paperType.ToString()));
            }

            //faculty ddl
            foreach (Faculty faculty in facultyList)
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
            resetExam();
        }

        protected void btnResetStaff_Click(object sender, EventArgs e)
        {
            resetStaff();
        }

        protected void ddlDurationDuty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDurationDuty.SelectedIndex != 0)
            {
                ddlSessionAndDurationDuty.ClearSelection();
                ddlSessionAndDurationDuty.SelectedIndex = 0;
                ddlSessionDuty.ClearSelection();
                ddlSessionDuty.SelectedIndex = 0;
                tbDuration.Enabled = true;
            }
            else
            {
                tbSession.Enabled = false;
                tbDuration.Enabled = false;
            }
        }

        protected void ddlSessionDuty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSessionDuty.SelectedIndex != 0)
            {
                ddlSessionAndDurationDuty.ClearSelection();
                ddlSessionAndDurationDuty.SelectedIndex = 0;
                ddlDurationDuty.ClearSelection();
                ddlDurationDuty.SelectedIndex = 0;

                tbSession.Enabled = true;
            }
            else
            {
                tbSession.Enabled = false;
                tbDuration.Enabled = false;
            }
        }

        protected void ddlSessionAndDurationDuty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSessionAndDurationDuty.SelectedIndex != 0)
            {
                ddlDurationDuty.ClearSelection();
                ddlDurationDuty.SelectedIndex = 0;

                ddlSessionDuty.ClearSelection();
                ddlSessionDuty.SelectedIndex = 0;
                tbSession.Enabled = true;
                tbDuration.Enabled = true;
            }
            else
            {
                tbSession.Enabled = false;
                tbDuration.Enabled = false;
            }
        }

        protected void btnResetConstraint_Click(object sender, EventArgs e)
        {
            resetConstraint();
        }

        public void resetStaff()
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

            //ddl typeofemploy
            ddlEmployType.ClearSelection();
            ddlSTSPhd.SelectedIndex = 0;

            //ddl other duty
            ddlOtherDuty.ClearSelection();
            ddlOtherDuty.SelectedIndex = 0;

            //ddl sesion duty
            ddlSessionDuty.ClearSelection();
            ddlSessionDuty.SelectedIndex = 0;

            //ddl duration duty
            ddlDurationDuty.ClearSelection();
            ddlDurationDuty.SelectedIndex = 0;

            //ddl session and duration duty
            ddlSessionAndDurationDuty.ClearSelection();
            ddlSessionAndDurationDuty.SelectedIndex = 0;

            //disable text box
            tbSession.Enabled = false;
            tbDuration.Enabled = false;
        }

        public void resetExam()
        { //session ddl
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

        public void resetConstraint()
        {
            tbRemark.Text = "";
            ddlHardConstraint.ClearSelection();
            ddlHardConstraint.SelectedIndex = 0;
            ddlConstraintImportance.ClearSelection();
            ddlConstraintImportance.SelectedIndex = 0;
        }

        protected void btnResetAll_Click(object sender, EventArgs e)
        {
            resetStaff();
            resetExam();
            resetConstraint();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Constraint3 constraint = new Constraint3();

            if (DropDownListSession.SelectedIndex != 0)
            {
                constraint.InvigilationDuty.Session = DropDownListSession.SelectedItem.Value;
            }

            if( ddlDuration.SelectedIndex != 0)
            {
                constraint.InvigilationDuty.Duration = Convert.ToInt16(ddlDuration.SelectedItem.Text);
            }

            if (ddlCategoryOfInvigilation.SelectedIndex != 0)
            {
                constraint.InvigilationDuty.CategoryOfInvigilator = ddlCategoryOfInvigilation.SelectedItem.Value;
            }

            if (ddlFacultyExam.SelectedIndex != 0)
            {
                constraint.Examination.Faculty.FacultyCode = Convert.ToChar(ddlFacultyExam.SelectedItem.Value);
            }

            if (ddlPaperType.SelectedIndex != 0)
            {
                constraint.Examination.PaperType = Convert.ToChar(ddlPaperType.SelectedItem.Value);
            }
            
            if(CheckBoxDoubleSeating.Checked == true)
            {
                constraint.IsDoubleSeating = true;
            }

            if(CheckBoxCnbl.Checked == true)
            {
                constraint.IsCnblPaper = true;
            }

            //staff

            if (ddlMuslim.SelectedIndex != 0)
            {
                if (ddlMuslim.SelectedValue == "Yes")
                {
                    constraint.Invigilator.IsMuslim = true;
                }
                else if(ddlMuslim.SelectedValue == "No")
                {
                    constraint.Invigilator.IsMuslim = false;
                }
            }

            if (ddlChief.SelectedIndex != 0)
            {
                if (ddlChief.SelectedValue == "Yes")
                {
                    constraint.Invigilator.IsChiefInvi = true;
                }
                else if (ddlChief.SelectedValue == "No")
                {
                    constraint.Invigilator.IsChiefInvi = false;
                }
            }

            if (ddlExperiencedInvigilator.SelectedIndex != 0)
            {
                if(ddlExperiencedInvigilator.SelectedValue == "Yes")
                {
                    constraint.Invigilator.IsInviAbove2Years = true;
                }
                else if (ddlExperiencedInvigilator.SelectedValue == "No")
                {
                    constraint.Invigilator.IsInviAbove2Years = false;
                }
            }

            if (ddlSTSPhd.SelectedIndex != 0)
            {
                if(ddlSTSPhd.SelectedValue== "Yes")
                {
                    constraint.Invigilator.IsTakingSTSPhD = true;

                }else if(ddlSTSPhd.SelectedValue == "No")
                {
                    constraint.Invigilator.IsTakingSTSPhD = false;
                }
            }

            if(ddlEmployType.SelectedIndex != 0)
            {
                if (ddlEmployType.SelectedValue == "F")
                {
                    constraint.Invigilator.TypeOfEmploy = Convert.ToChar(ddlEmployType.SelectedValue);
                }
                if(ddlEmployType.SelectedValue == "P")
                {
                    constraint.Invigilator.TypeOfEmploy = Convert.ToChar(ddlEmployType.SelectedValue);
                }
            }

            if (ddlOtherDuty.SelectedIndex != 0)
            {
                if(ddlOtherDuty.SelectedValue == "Yes")
                {
                    constraint.HasOtherDutyOnSameDay = true;
                }
                else if(ddlOtherDuty.SelectedValue == "No")
                {
                    constraint.HasOtherDutyOnSameDay = false;
                }
            }

            if(ddlSessionAndDurationDuty.SelectedIndex != 0)
            {
                if(ddlSessionAndDurationDuty.SelectedValue == "Yes")
                {
                    constraint.HasSpecificSessionAndDurationDutyOnSameDay = true;
                }
                else if (ddlSessionAndDurationDuty.SelectedValue == "No")
                {
                    constraint.HasSpecificSessionAndDurationDutyOnSameDay = true;
                }
                constraint.HasSpecificSessionDutyOnSameDayString = tbSession.Text;
                constraint.HasSpecificDurationDutyOnSameDayInt = Convert.ToInt16(tbDuration.Text);
            }

            if (ddlSessionDuty.SelectedIndex != 0)
            {
                if(ddlSessionDuty.SelectedValue == "Yes")
                {
                    constraint.HasSpecificSessionDutyOnSameDay = true;
                }
                else if (ddlSessionDuty.SelectedValue == "No")
                {
                    constraint.HasSpecificSessionDutyOnSameDay = false;
                }
                constraint.HasSpecificSessionDutyOnSameDayString = tbSession.Text;
            }

            if (ddlDurationDuty.SelectedIndex != 0)
            {
                if(ddlDurationDuty.SelectedValue == "Yes")
                {
                    constraint.HasSpecificDurationDutyOnSameDay = false;
                }
                else if(ddlDurationDuty.SelectedValue == "No")
                {
                    constraint.HasSpecificDurationDutyOnSameDay = false;
                }
                constraint.HasSpecificDurationDutyOnSameDayInt = Convert.ToInt16(tbDuration.Text);
            }

            if (ddlConstraintImportance.SelectedValue == "Low")
            {
                constraint.ConstraintImportanceValue = 1;
            }
            else if (ddlConstraintImportance.SelectedValue == "Medium")
            {
                constraint.ConstraintImportanceValue = 10;
            }
            else if (ddlConstraintImportance.SelectedValue == "High")
            {
                constraint.ConstraintImportanceValue = 100;
            }

            if (ddlHardConstraint.SelectedIndex == 1)
            {
                constraint.IsHardConstraint = true;
            }
            else if (ddlHardConstraint.SelectedIndex ==0)
            {
                constraint.IsHardConstraint = false;
            }

            if (tbSession.Text != "" && tbRemark.Text!= null)
            {
                constraint.Remark = tbRemark.Text;
            }

            MaintainConstraint3Control mConstraintControl = new MaintainConstraint3Control();
            mConstraintControl.insertConstraintIntoDatabase(constraint);
            mConstraintControl.shutDown();

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Successfully Added');</script>");

            resetStaff();
            resetExam();
            resetConstraint();
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            lblValidate.Text = Convert.ToInt16(ddlDuration.SelectedItem.Text).ToString();

        }
    }
}