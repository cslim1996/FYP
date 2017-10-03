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
    public partial class UpdateVenue : System.Web.UI.Page
    {
        static string strCon = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
        public SqlConnection con = new SqlConnection(strCon);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setFields();
                if (Session["Venue"] != null)
                {
                    con.Open();
                    SqlCommand cmdSelect = new SqlCommand("Select * from venue where venueid = @vid", con);
                    cmdSelect.Parameters.AddWithValue("@vid", Session["Venue"]);
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

                        if (!da["Floor"].ToString().Equals(""))
                            dd_floor.Items.FindByValue(da["Floor"].ToString()).Selected = true;

                        rbl_eastWest.SelectedValue = da["EastOrWest"].ToString();

                        txt_prefPaper.Text = da["PreferencePaper"].ToString();
                        txt_maxProg.Text = da["MaximumProgramme"].ToString();
                        txt_exitLoc.Text = da["ExitLocation"].ToString();
                    }

                    con.Close();
                }
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
            txt_Venue.Enabled = false;
        }

        protected void dd_block_SelectedIndexChanged(object sender, EventArgs e)
        {
            dd_floor.Items.Clear();
            initiliazeFloor();
        }

        protected void dd_block_DataBound(object sender, EventArgs e)
        {

        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmdUpdate = new SqlCommand("Update Venue Set Location = @loc, capacity = @cap, floor = @floor, MaximumProgramme = @mp, PreferencePaper = @pp, EastOrWest = @ew, ExitLocation = @exitLoc where VenueID = @vid", con);
            cmdUpdate.Parameters.AddWithValue("@loc", dd_block.SelectedValue);
            cmdUpdate.Parameters.AddWithValue("@cap", txt_Cap.Text);
            cmdUpdate.Parameters.AddWithValue("@floor", dd_floor.SelectedValue);
            cmdUpdate.Parameters.AddWithValue("@mp", txt_maxProg.Text);
            cmdUpdate.Parameters.AddWithValue("@pp", txt_prefPaper.Text);
            cmdUpdate.Parameters.AddWithValue("@ew", rbl_eastWest.SelectedValue);
            cmdUpdate.Parameters.AddWithValue("@exitLoc", txt_exitLoc.Text);
            cmdUpdate.Parameters.AddWithValue("@vid", txt_Venue.Text);

            cmdUpdate.ExecuteNonQuery();
            con.Close();

            clearFields();
            Response.Redirect("VenueMaintenance.aspx");
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
    }
}