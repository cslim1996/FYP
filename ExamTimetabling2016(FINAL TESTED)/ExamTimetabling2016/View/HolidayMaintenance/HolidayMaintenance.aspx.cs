using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.Holiday_Maintenance
{
    public partial class HolidayMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            Session["ID"] = ((Label)GridView1.Rows[e.NewEditIndex].FindControl("Label1")).Text;
            Response.Redirect("UpdateHoliday.aspx");

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Session["ID"] = ((Label)GridView1.Rows[e.RowIndex].FindControl("Label1")).Text;
            Response.Redirect("DeleteHoliday.aspx");


        }
    }
}