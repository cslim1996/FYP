using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.Venue_Maintenance
{
    public partial class UpdateBlock : System.Web.UI.Page
    {
        static string strCon = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
        public SqlConnection con = new SqlConnection(strCon);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Block"] != null)
                {
                    con.Open();
                    SqlCommand cmdSelect = new SqlCommand("Select * from Block where BlockCode = @bid", con);
                    cmdSelect.Parameters.AddWithValue("@bid", Session["Block"]);
                    SqlDataReader da = cmdSelect.ExecuteReader();

                    while (da.Read())
                    {
                        txt_BlockCode.Text = da["BlockCode"].ToString();
                        txt_Floor.Text = da["TotalFloor"].ToString();
                        ddl_Campus.SelectedValue = da["Campus"].ToString();
                        
                    }
                    txt_BlockCode.Enabled = false;
                    con.Close();
                }
            }
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmdUpdate = new SqlCommand("Update Block Set TotalFloor = @tF, Campus = @cP where blockCode = @bid", con);
            cmdUpdate.Parameters.AddWithValue("@tF", txt_Floor.Text);
            cmdUpdate.Parameters.AddWithValue("@cP", ddl_Campus.SelectedValue);
            cmdUpdate.Parameters.AddWithValue("@bid", txt_BlockCode.Text);

            cmdUpdate.ExecuteNonQuery();
            con.Close();

            clearFields();
            Response.Redirect("BlockMaintenance.aspx");
        }

        private void clearFields()
        {
            txt_BlockCode.Text = "";
            txt_Floor.Text = "";
            ddl_Campus.ClearSelection();
        }
    }
}