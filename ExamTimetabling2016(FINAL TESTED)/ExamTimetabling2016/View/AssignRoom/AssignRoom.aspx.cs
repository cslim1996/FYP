using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.Venue_Maintenance
{
    public partial class AssignRoom : System.Web.UI.Page
    {

        static string strCon = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
        public SqlConnection con = new SqlConnection(strCon);

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                initiliazeUnassigned();
            }
        }

        protected void ddl_Block_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_Venue.Items.Clear();
            initilizeVenue();
            initializeList();

        }

        private void initilizeVenue()
        {
            con.Open();
            SqlCommand cmdSelect = new SqlCommand("Select * from Venue where location = @blockCode", con);
            cmdSelect.Parameters.AddWithValue("@blockCode", ddl_Block.SelectedValue);
            SqlDataReader dr = cmdSelect.ExecuteReader();

            while (dr.Read())
            {
                ddl_Venue.Items.Add(dr["venueid"].ToString());
            }

            con.Close();

        }

        protected void ddl_Block_DataBound(object sender, EventArgs e)
        {
            initilizeVenue();
            initializeList();
        }

        protected void ddl_Venue_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from room where venueID = @vid", con);
            da.SelectCommand.Parameters.AddWithValue("@vid",ddl_Venue.SelectedValue);
            DataSet ds = new DataSet();
            da.Fill(ds);

            con.Close();

            ListView1.DataSource = ds;
            ListView1.DataBind();

        }

        protected void ddl_Venue_DataBound(object sender, EventArgs e)
        {
            initializeList();
        }

        private void initializeList()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from room where venueID = @vid", con);
            da.SelectCommand.Parameters.AddWithValue("@vid", ddl_Venue.SelectedValue);
            DataSet ds = new DataSet();
            da.Fill(ds);

            con.Close();

            ListView1.DataSource = ds;
            ListView1.DataBind();
        }

        private void initiliazeUnassigned()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from room where venueID IS NULL", con);
            DataSet ds = new DataSet();
            da.Fill(ds);

            con.Close();

            ListView2.DataSource = ds;
            ListView2.DataBind();
        }

        protected void ListView2_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            con.Open();
            SqlCommand cmdFloor = new SqlCommand("Select floor from venue where venueid = @vid",con);
            cmdFloor.Parameters.AddWithValue("@vid", ddl_Venue.SelectedValue);
            int floor = (int)cmdFloor.ExecuteScalar();

            SqlCommand cmdAssign = new SqlCommand("Update Room SET VenueID = @vid, BlockCode = @bid, Floor = @floor where roomCode = @rid",con);
            string roomID = ((Label)ListView2.Items[e.ItemIndex].FindControl("RoomCodeLabel")).Text;

            cmdAssign.Parameters.AddWithValue("@vid", ddl_Venue.SelectedValue);
            cmdAssign.Parameters.AddWithValue("@bid", ddl_Block.SelectedValue);
            cmdAssign.Parameters.AddWithValue("@floor", floor);
            cmdAssign.Parameters.AddWithValue("@rid", roomID);
            cmdAssign.ExecuteNonQuery();
            con.Close();
            refreshList();

        }

        protected void ListView1_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            con.Open();
            SqlCommand cmdUnassign = new SqlCommand("Update Room SET VenueID = NULL, BlockCode = NULL, Floor = NULL where roomCode=@rid", con);
            string roomID = ((Label)ListView1.Items[e.ItemIndex].FindControl("RoomCodeLabel")).Text;
            cmdUnassign.Parameters.AddWithValue("@rid", roomID);
            cmdUnassign.ExecuteNonQuery();
            con.Close();
            refreshList();
        }

        private void refreshList()
        {
            initializeList();
            initiliazeUnassigned();
        }
    }
}