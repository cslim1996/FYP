using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FYP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        static string strCon = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
        public SqlConnection con = new SqlConnection(strCon);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["Block"] = ((Label)GridView1.Rows[e.NewEditIndex].FindControl("Label1")).Text;
            Response.Redirect("UpdateBlock.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Session["Block"] = ((Label)GridView1.Rows[e.RowIndex].FindControl("Label1")).Text;
            Response.Redirect("DeleteBlock.aspx");
        }
    }
}