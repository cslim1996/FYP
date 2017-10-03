using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016.View
{
    public partial class InvigilationMaintainInv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //For future SqlDataSource select command if login function is implemented, with condition where logged in user = which faculty
            //SqlDataSource1.SelectCommand = "SELECT [StaffID], [Title], [Name], [Gender], [Position], [Department], [isMuslim], [isChiefInvi], [isInviAbove2Years] FROM [Staff]";

            lblGuideline.Text = "1. In Staff Details section, click Edit button to edit details of that particular staff.<br />Click Update button after done editing to save changes, or Cancel button to cancel changes.<br /><br />" +
                "2. In Exemption Details section, ID for staff must first be selected to add, edit or delete Exemption for that particular staff.<br />To add exemption, enter Date and select Session, then click Insert button.<br />" +
                "To edit exemption, click Edit button, then click Update button after done editing to save changes, or Cancel button to cancel changes.<br />" +
                "To delete exemption, click Delete button.";
        }

        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //retrieve listview item control, to get staffID
            ListViewItem item = ListView1.Items[ListView1.SelectedIndex];
            var staffid = (LinkButton)item.FindControl("StaffIDLabel");

            Session["StaffID"] = staffid.Text;
        }

        protected void CustomValidatorSession_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string textBoxName = ((CustomValidator)source).ControlToValidate;
            var textbox = ((CustomValidator)source).Parent.FindControl(textBoxName) as TextBox;

            if (textbox != null)
            {
                if (textbox.Text.Equals("AM") || textbox.Text.Equals("PM") || textbox.Text.Equals("EV"))
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }
    }
}