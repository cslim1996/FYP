using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016.View.InvigilationMaintenance
{
    public partial class ExaminationSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MaintainConstraintSettingControl mSettingControl = new MaintainConstraintSettingControl();
                ConstraintSetting setting = mSettingControl.readSettingFromDatabase();
                mSettingControl.shutDown();

                if (setting != null)
                {
                    if (setting.AssignToExaminer == true)
                    {
                        ddlAssignToExaminer.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlAssignToExaminer.SelectedIndex = 1;
                    }

                    tbEvening.Text = setting.MaxEveningSession.ToString();
                    tbExtra.Text = setting.MaxExtraSession.ToString();
                    tbRelief.Text = setting.MaxReliefSession.ToString();
                    tbSaturday.Text = setting.MaxSaturdaySession.ToString();
                    tbExemptionForExaminer.Text = setting.DayOfExemptionForExaminer.ToString();
                    tbMaxConsecutiveDuty.Text = setting.MaxConsecutiveDayDuty.ToString();
                    tbMaxStaffToOwnFaculty.Text = setting.MaxInvigilatorAssignToOwnFaculty.ToString();
                }
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            MaintainConstraintSettingControl mSettingControl = new MaintainConstraintSettingControl();
            ConstraintSetting setting = mSettingControl.readSettingFromDatabase();
            mSettingControl.shutDown();

            if (setting != null)
            {
                if (setting.AssignToExaminer == true)
                {
                    ddlAssignToExaminer.ClearSelection();
                    ddlAssignToExaminer.SelectedIndex = 0;
                }
                else
                {
                    ddlAssignToExaminer.ClearSelection();
                    ddlAssignToExaminer.SelectedIndex = 1;
                }

                tbEvening.Text = setting.MaxEveningSession.ToString();
                tbExtra.Text = setting.MaxExtraSession.ToString();
                tbRelief.Text = setting.MaxReliefSession.ToString();
                tbSaturday.Text = setting.MaxSaturdaySession.ToString();
                tbExemptionForExaminer.Text = setting.DayOfExemptionForExaminer.ToString();
                tbMaxConsecutiveDuty.Text = setting.MaxConsecutiveDayDuty.ToString();
                tbMaxStaffToOwnFaculty.Text = setting.MaxInvigilatorAssignToOwnFaculty.ToString();
            }
            else
            {
                ddlAssignToExaminer.ClearSelection();
                ddlAssignToExaminer.SelectedIndex = 0;
                tbEvening.Text = "";
                tbExtra.Text = "";
                tbRelief.Text = "";
                tbSaturday.Text = "";
                tbExemptionForExaminer.Text = "";
                tbMaxConsecutiveDuty.Text = "";
                tbMaxStaffToOwnFaculty.Text = "";
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ConstraintSetting newSetting = new ConstraintSetting();
            if (ddlAssignToExaminer.SelectedIndex == 0)
            {
                newSetting.AssignToExaminer = true;
            }
            else if(ddlAssignToExaminer.SelectedIndex == 1)
            {
                newSetting.AssignToExaminer = false;
            }

            newSetting.MaxEveningSession = Convert.ToInt16(tbEvening.Text);
            newSetting.MaxExtraSession = Convert.ToInt16(tbExtra.Text);
            newSetting.MaxReliefSession = Convert.ToInt16(tbRelief.Text);
            newSetting.MaxSaturdaySession = Convert.ToInt16(tbSaturday.Text);
            newSetting.MaxInvigilatorAssignToOwnFaculty = Convert.ToInt16(tbMaxStaffToOwnFaculty.Text);
            newSetting.MaxConsecutiveDayDuty = Convert.ToInt16(tbMaxConsecutiveDuty.Text);
            newSetting.DayOfExemptionForExaminer = Convert.ToInt16(tbExemptionForExaminer.Text);

            MaintainConstraintSettingControl mSettingControl = new MaintainConstraintSettingControl();
            mSettingControl.saveIntoDatabase(newSetting);
            mSettingControl.shutDown();
            

        }
    }
}