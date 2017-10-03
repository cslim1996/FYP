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
    public partial class DeleteBlock : System.Web.UI.Page
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
                    txt_Floor.Enabled = false;
                    ddl_Campus.Enabled = false;


                    con.Close();
                }
            }
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmdDel = new SqlCommand("Delete From Block where blockCode = @bid", con);
            cmdDel.Parameters.AddWithValue("@bid", txt_BlockCode.Text);
            cmdDel.ExecuteNonQuery();
            clearFields();
            con.Close();

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