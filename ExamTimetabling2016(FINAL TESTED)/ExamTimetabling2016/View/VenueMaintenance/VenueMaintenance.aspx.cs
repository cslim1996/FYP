using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace FYP
{
    public partial class VenueMaintenance : System.Web.UI.Page
    {
        static string strCon = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
        public SqlConnection con = new SqlConnection(strCon);

        public string strSelect = "SELECT Venue.VenueID, Stuff((Select ','+ Room.RoomCode from Room room where Room.VenueID = Venue.VenueID for XML PATH('')),1,1,' ') as \"RoomAssigned\", Venue.Capacity, Venue.Floor FROM Venue LEFT JOIN Room ON Venue.VenueID = Room.VenueID Group by Venue.VenueID, Venue.Capacity, Venue.Floor Order by(substring(Venue.VenueID, 1, 1)), case when isNumeric(substring(Venue.VenueID, 2, 1)) = 1 THEN substring(Venue.VenueID, 3, 1) when isNumeric(substring(Venue.VenueID, 2, 1)) = 0 THEN substring(Venue.VenueID, 4, 1) end, substring(Venue.VenueID, 2, 1), substring(Venue.VenueID, 3, 1)";
        public string strSelectAll;

        protected void Page_Load(object sender, EventArgs e)
        {
            strSelectAll = "SELECT Venue.VenueID, Stuff((Select ','+ Room.RoomCode from Room room where Room.VenueID = Venue.VenueID for XML PATH('')),1,1,' ') as \"RoomAssigned\", Venue.Capacity, Venue.Floor FROM Block, Venue LEFT JOIN Room ON Venue.VenueID = Room.VenueID AND Venue.Location ='" + ddl_Campus.SelectedValue + "' Group by Venue.VenueID, Venue.Capacity, Venue.Floor Order by(substring(Venue.VenueID, 1, 1)), case when isNumeric(substring(Venue.VenueID, 2, 1)) = 1 THEN substring(Venue.VenueID, 3, 1) when isNumeric(substring(Venue.VenueID, 2, 1)) = 0 THEN substring(Venue.VenueID, 4, 1) end, substring(Venue.VenueID, 2, 1), substring(Venue.VenueID, 3, 1)";

            if (!IsPostBack)
            {
                getCampus();
                getBlock();
                getVenue();

            }
        }

        protected void ddl_Block_DataBound(object sender, EventArgs e)
        {
            ddl_Block.Items.Insert(0, new ListItem("Show All"));
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            getVenue();

            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["Venue"] = ((Label)GridView1.Rows[e.NewEditIndex].FindControl("lbl_VenueID")).Text;
            Response.Redirect("UpdateVenue.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Session["Delete"] = ((Label)GridView1.Rows[e.RowIndex].FindControl("lbl_VenueID")).Text;
            Response.Redirect("DeleteVenue.aspx");
        }

        protected void ddl_Block_SelectedIndexChanged(object sender, EventArgs e)
        {
            getVenue();
        }

        protected void ddl_Campus_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBlock();
            getVenue();
        }

        protected void ddl_Campus_DataBound(object sender, EventArgs e)
        {
            //ddl_Campus.Items.Insert(0,new ListItem("Show All"));

        }

        protected void ddl_Campus_DataBinding(object sender, EventArgs e)
        {

        }

        private void getVenue()
        {
            con.Open();
            string query = "SELECT Venue.VenueID, Stuff((Select ','+ Room.RoomCode from Room room where Room.VenueID = Venue.VenueID for XML PATH('')),1,1,' ') as \"RoomAssigned\", Venue.Capacity, Venue.Floor FROM Venue LEFT JOIN Room ON Venue.VenueID = Room.VenueID Group by Venue.VenueID, Venue.Capacity, Venue.Floor Order by(substring(Venue.VenueID, 1, 1)), case when isNumeric(substring(Venue.VenueID, 2, 1)) = 1 THEN substring(Venue.VenueID, 3, 1) when isNumeric(substring(Venue.VenueID, 2, 1)) = 0 THEN substring(Venue.VenueID, 4, 1) end, substring(Venue.VenueID, 2, 1), substring(Venue.VenueID, 3, 1)";

            if (ddl_Block.SelectedIndex == 0)
                query = "SELECT Venue.VenueID, Stuff((Select ','+ Room.RoomCode from Room room where Room.VenueID = Venue.VenueID for XML PATH('')),1,1,' ') as \"RoomAssigned\", Venue.Capacity, Venue.Floor FROM Venue LEFT JOIN Room ON Venue.VenueID = Room.VenueID CROSS JOIN BLOCK WHERE (Block.Campus = '" + ddl_Campus.SelectedValue + "') Group by Venue.VenueID, Venue.Capacity, Venue.Floor Order by(substring(Venue.VenueID, 1, 1)), case when isNumeric(substring(Venue.VenueID, 2, 1)) = 1 THEN substring(Venue.VenueID, 3, 1) when isNumeric(substring(Venue.VenueID, 2, 1)) = 0 THEN substring(Venue.VenueID, 4, 1) end, substring(Venue.VenueID, 2, 1), substring(Venue.VenueID, 3, 1)";
            else if (ddl_Block.SelectedIndex != 0)
                query = "SELECT Venue.VenueID, Stuff((Select ','+ Room.RoomCode from Room room where Room.VenueID = Venue.VenueID for XML PATH('')),1,1,' ') as \"RoomAssigned\", Venue.Capacity, Venue.Floor FROM Venue LEFT JOIN Room ON Venue.VenueID = Room.VenueID CROSS JOIN BLOCK WHERE (Location ='" + ddl_Block.SelectedValue + "') AND (Block.Campus = '" + ddl_Campus.SelectedValue + "') Group by Venue.VenueID, Venue.Capacity, Venue.Floor Order by(substring(Venue.VenueID, 1, 1)), case when isNumeric(substring(Venue.VenueID, 2, 1)) = 1 THEN substring(Venue.VenueID, 3, 1) when isNumeric(substring(Venue.VenueID, 2, 1)) = 0 THEN substring(Venue.VenueID, 4, 1) end, substring(Venue.VenueID, 2, 1), substring(Venue.VenueID, 3, 1)";

            SqlCommand sqlFunction = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(sqlFunction);
            DataTable dt = new DataTable();
            sda.Fill(dt);


            GridView1.DataSource = dt;
            GridView1.DataBind();

            con.Close();
        }

        private void getCampus()
        {
            con.Open();
            string query = "Select * from campus";
            SqlCommand sqlFunction = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(sqlFunction);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            ddl_Campus.DataSource = dt;
            ddl_Campus.DataTextField = "CampusName";
            ddl_Campus.DataValueField = "CampusName";
            ddl_Campus.DataBind();
            con.Close();
        }

        private void getBlock()
        {
            con.Open();
            string query = "Select * from block where campus = '" + ddl_Campus.SelectedValue + "'";

            SqlCommand sqlFunction = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(sqlFunction);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            ddl_Block.DataSource = dt;
            ddl_Block.DataTextField = "BlockCode";
            ddl_Block.DataValueField = "BlockCode";
            ddl_Block.DataBind();
            con.Close();
        }

        private void getVenueSource()
        {
            if (ddl_Block.SelectedIndex == 0)
                VenueDS.SelectCommand = strSelect;
        }
    }

}