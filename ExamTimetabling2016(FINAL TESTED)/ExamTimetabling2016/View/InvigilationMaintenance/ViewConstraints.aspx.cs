using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace ExamTimetabling2016.View.InvigilationMaintenance
{
    public partial class ViewConstraints : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if(e.CommandName == "Select")
            {

                // Retrieve the row index stored in the 
                // CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = GridView1.Rows[index];

                int id = Convert.ToInt16(row.Cells[2].Text);
                Session["id"] = id;

                lblDetail.Visible = true;
                DetailsView1.Visible = true;

                //Response.Redirect("EditConstraint.aspx");

            }
            else if(e.CommandName == "Delete")
            {
                DialogResult dr = MessageBox.Show("Do you really want to remove this constraint?", "DeleteComfirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                
                    // Retrieve the row index stored in the 
                    // CommandArgument property.
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Retrieve the row that contains the button 
                    // from the Rows collection.
                    GridViewRow row = GridView1.Rows[index];

                    int id = Convert.ToInt16(GridView1.Rows[index].Cells[2].Text);
                    Session["id"] = id;
                    
                    MaintainConstraint3Control mConstraintControl = new MaintainConstraint3Control();
                    mConstraintControl.deleteConstraint(id);
                    mConstraintControl.shutDown();
                }

                else if (dr == DialogResult.Cancel)
                { 
                }   
                
                
            }
        }
    }
    

        
    }