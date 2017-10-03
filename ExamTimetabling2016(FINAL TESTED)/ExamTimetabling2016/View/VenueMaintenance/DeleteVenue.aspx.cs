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
    public partial class DeleteVenue : System.Web.UI.Page
    {
        static string strCon = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
        public SqlConnection con = new SqlConnection(strCon);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setFields();
                if (Session["Delete"] != null)
                {
                    con.Open();
                    SqlCommand cmdSelect = new SqlCommand("Select * from venue where venueid = @vid", con);
                    cmdSelect.Parameters.AddWithValue("@vid", Session["Delete"]);
                    SqlDataReader da = cmdSelect.ExecuteReader();

                    while (da.Read())
                    {
                        dd_block.SelectedValue = da["Location"].ToString();

                        txt_Venue.Text = da["VenueID"].ToString();
                        txt_Cap.Text = da["Capacity"].ToString();

                        SqlCommand cmdS = new SqlCommand("Select TotalFloor from Block where blockcode = @blockcode", con);
                        cmdS.Parameters.AddWithValue("@blockcode", da["Location"]);
                        int totalFloor = (int)cmdS.ExecuteScalar();


                        for (int i = 0; i < totalFloor; i++)
                        {
                            dd_floor.Items.Add("" + (i + 1));
                        }

                        dd_floor.Items.FindByValue(da["Floor"].ToString()).Selected = true;

                        rbl_eastWest.SelectedValue = da["EastOrWest"].ToString();

                        txt_prefPaper.Text = da["PreferencePaper"].ToString();
                        txt_maxProg.Text = da["MaximumProgramme"].ToString();
                        txt_exitLoc.Text = da["ExitLocation"].ToString();
                    }
                }

                con.Close();
            }
        }


        private void initiliazeFloor()
        {
            con.Open();
            SqlCommand cmdSelect = new SqlCommand("Select TotalFloor from Block where blockcode = @blockcode", con);
            cmdSelect.Parameters.AddWithValue("@blockcode", dd_block.SelectedValue);
            int totalFloor = (int)cmdSelect.ExecuteScalar();
            for (int i = 0; i < totalFloor; i++)
            {
                dd_floor.Items.Add("" + (i + 1));
            }
            con.Close();
        }

        private void setFields()
        {
            rbl_eastWest.Enabled = false;
            dd_block.Enabled = false;
            dd_floor.Enabled = false;
            txt_Venue.Enabled = false;
            txt_Cap.Enabled = false;
            txt_exitLoc.Enabled = false;
            txt_maxProg.Enabled = false;
            txt_prefPaper.Enabled = false;
        }

        protected void dd_block_SelectedIndexChanged(object sender, EventArgs e)
        {
            dd_floor.Items.Clear();
            initiliazeFloor();
        }

        private void clearFields()
        {
            dd_block.ClearSelection();
            dd_floor.ClearSelection();
            rbl_eastWest.ClearSelection();

            txt_Cap.Text = "";
            txt_exitLoc.Text = "";
            txt_maxProg.Text = "";
            txt_prefPaper.Text = "";
            txt_Venue.Text = "";
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmdDel = new SqlCommand("Delete From Venue where venueID = @vid", con);
            cmdDel.Parameters.AddWithValue("@vid", txt_Venue.Text);
            cmdDel.ExecuteNonQuery();
            clearFields();

            Response.Redirect("VenueMaintenance.aspx");
            con.Close();
        }
    }
}