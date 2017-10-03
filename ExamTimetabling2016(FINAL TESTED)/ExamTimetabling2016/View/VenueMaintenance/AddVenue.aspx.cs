using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace FYP.Venue_Maintenance
{
    public partial class AddVenue : System.Web.UI.Page
    {
        static string strCon = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
        public SqlConnection con = new SqlConnection(strCon);

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                preprocessXML();
            }

        }

        protected void dd_block_SelectedIndexChanged(object sender, EventArgs e)
        {
            dd_floor.Items.Clear();
            con.Open();
            SqlCommand cmdSelect = new SqlCommand("Select TotalFloor from Block where blockcode = @blockcode", con);
            cmdSelect.Parameters.AddWithValue("@blockcode", dd_block.SelectedValue);
            int totalFloor = 0;
            object floor = cmdSelect.ExecuteScalar();
            if (!floor.ToString().Equals(""))
                totalFloor = (int)cmdSelect.ExecuteScalar();

            for (int i = 0; i < totalFloor; i++)
            {
                dd_floor.Items.Add("" + (i + 1));
            }
            con.Close();

        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                if (validateID(txt_Venue.Text))
                {
                    con.Open();
                    SqlCommand cmdInsert = new SqlCommand("Insert into Venue values (@venueID, @location, @capacity, @floor, @maxProg, @prefPaper, @eastWest, @exitLoc, @maxc, @maxr)", con);
                    cmdInsert.Parameters.AddWithValue("@venueID", txt_Venue.Text);
                    cmdInsert.Parameters.AddWithValue("@location", dd_block.SelectedItem.Text);
                    cmdInsert.Parameters.AddWithValue("@capacity", txt_Cap.Text);
                    cmdInsert.Parameters.AddWithValue("@floor", dd_floor.SelectedItem.Text);
                    cmdInsert.Parameters.AddWithValue("@maxProg", txt_maxProg.Text);
                    cmdInsert.Parameters.AddWithValue("@prefPaper", txt_prefPaper.Text);
                    cmdInsert.Parameters.AddWithValue("@eastWest", rbl_eastWest.SelectedItem.Text[0].ToString().ToUpper());
                    cmdInsert.Parameters.AddWithValue("@exitLoc", txt_exitLoc.Text);
                    cmdInsert.Parameters.AddWithValue("@maxc", txt_maxCol.Text);
                    cmdInsert.Parameters.AddWithValue("@maxr", txt_maxRow.Text);
                    int n = cmdInsert.ExecuteNonQuery();


                    con.Close();

                    preprocessXML();

                    success_Alert.Visible = true;
                    alert.Visible = false;
                    lbl_success.Text = txt_Venue.Text + " successfully added!";
                    clearAdd();

                }
                else
                {
                    alert.Visible = true;
                    success_Alert.Visible = false;
                    lbl_alert.Text = "Cannot insert duplicated id";
                }
            }

        }

        private void clearAdd()
        {
            txt_Venue.Text = "";
            dd_block.SelectedIndex = 0;
            txt_Cap.Text = "";
            dd_floor.SelectedIndex = 0;
            txt_maxProg.Text = "";
            txt_prefPaper.Text = "";
            txt_maxRow.Text = "";
            txt_maxCol.Text = "";
            rbl_eastWest.ClearSelection();
            txt_exitLoc.Text = "";
        }


        private void preProcess()
        {
            con.Open();
            string strLine = "";
            string strMain = "";
            int mainCounter = 0;
            string delimiter = "|";
            SqlCommand readBlock = new SqlCommand("Select * from Block", con);
            SqlDataReader blockReader = readBlock.ExecuteReader();
            StreamWriter VenueCapacityFile = new StreamWriter("D:\\FYP\\FYP\\VenueCapacity.txt");
            StreamWriter VenueFile = new StreamWriter("D:\\FYP\\FYP\\Venue.txt");

            while (blockReader.Read())
            {
                strMain += mainCounter + delimiter + blockReader["BlockCode"];
                strMain += System.Environment.NewLine;
                mainCounter++;

                strLine += checkGroupID(blockReader["BlockCode"].ToString()) + delimiter;
                strLine += blockReader["TotalFloor"] + delimiter;




                int totalFloor = int.Parse(blockReader["TotalFloor"].ToString());

                for (int i = 1; i <= totalFloor; i++)
                {
                    strLine += "" + checkVenuesPerFloor(i, blockReader["BlockCode"].ToString()) + " ";
                }

                strLine += delimiter;


                for (int i = 1; i <= totalFloor; i++)
                {
                    SqlCommand readVenueID = new SqlCommand("Select * from Venue where location = @location and floor = @floor", con);
                    readVenueID.Parameters.AddWithValue("@location", blockReader["BlockCode"]);
                    readVenueID.Parameters.AddWithValue("@floor", i);
                    SqlDataReader venueIDReader = readVenueID.ExecuteReader();
                    string venuesID = "";
                    string capacity = "";

                    while (venueIDReader.Read())
                    {
                        venuesID += venueIDReader["VenueID"] + " ";
                        capacity += venueIDReader["Capacity"] + " ";
                    }

                    strLine += venuesID + delimiter + capacity + delimiter;

                }

                strLine += System.Environment.NewLine;

            }
            VenueCapacityFile.WriteLine(strLine);
            VenueCapacityFile.Close();
            VenueFile.Write(strMain);
            VenueFile.Close();

        }

        private bool validateID(string id)
        {
            con.Open();

            string query = "Select Count(*) from venue where venueid = '" + id + "'";
            SqlCommand sqlFunction = new SqlCommand(query, con);
            int validate = (int)sqlFunction.ExecuteScalar();

            con.Close();

            if (validate == 0)
                return true;
            else
                return false;

        }


        private string checkGroupID(string blockcode)
        {
            string blockID = "";

            if (blockcode.Equals("PA") || blockcode.Equals("Q") || blockcode.Equals("R"))
                blockID = "1";
            else if (blockcode.Equals("L") || blockcode.Equals("M"))
                blockID = "2";
            else if (blockcode.Equals("DU") || blockcode.Equals("V"))
                blockID = "3";
            else if (blockcode.Equals("KS"))
                blockID = "4";
            else if (blockcode.Equals("H"))
                blockID = "5";
            else if (blockcode.Equals("SB") || blockcode.Equals("SD") || blockcode.Equals("SE"))
                blockID = "6";

            return blockID;
        }

        private int checkVenuesPerFloor(int floor, string blockcode)
        {
            SqlCommand readVenue = new SqlCommand("Select COUNT(*) from Venue WHERE location = @location and floor = @floor", con);
            readVenue.Parameters.AddWithValue("@location", blockcode);
            readVenue.Parameters.AddWithValue("@floor", floor);
            int venuesNo = (int)readVenue.ExecuteScalar();
            return venuesNo;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            preProcess();
        }

        private void initiliazeFloor()
        {
            try
            {
                con.Open();
                SqlCommand cmdSelect = new SqlCommand("Select TotalFloor from Block where blockcode = @blockcode", con);
                cmdSelect.Parameters.AddWithValue("@blockcode", dd_block.SelectedValue);
                int totalFloor = 0;
                object floor = cmdSelect.ExecuteScalar();
                if (!floor.ToString().Equals(""))
                    totalFloor = (int)cmdSelect.ExecuteScalar();

                for (int i = 0; i < totalFloor; i++)
                {
                    dd_floor.Items.Add("" + (i + 1));
                }
                con.Close();
            }

            catch (Exception ex)
            {
                alert.Visible = true;
                lbl_alert.Text = "You need to add a block first!";
                btn_Add.Enabled = false;
            }
        }

        protected void dd_block_DataBound(object sender, EventArgs e)
        {

            dd_block.SelectedIndex = 0;
            initiliazeFloor();

        }


        private void preprocessXML()
        {
            con.Open();

            // String sqlQuery = "SELECT * FROM Venue Order by(substring(VenueID, 1, 1)), case when isNumeric(substring(VenueID, 2, 1)) = 1 THEN substring(VenueID, 3, 1) when isNumeric(substring(VenueID, 2, 1)) = 0 THEN substring(VenueID, 4, 1) end, substring(VenueID, 2, 1), substring(VenueID, 3, 1) ";
            SqlCommand readBlock = new SqlCommand("Select * from Block", con);
            SqlDataReader blockReader = readBlock.ExecuteReader();



            XmlTextWriter xmlWriter = new XmlTextWriter(Server.MapPath("..\\VenueCapacity.xml"), System.Text.Encoding.UTF8);
            xmlWriter.WriteStartElement("blocks");
            int blockCount = 0;


            while (blockReader.Read())
            {
                xmlWriter.WriteStartElement("block");
                xmlWriter.WriteAttributeString("id", "" + blockCount);
                xmlWriter.WriteStartElement("group");
                xmlWriter.WriteString(checkGroupID(blockReader["BlockCode"].ToString()));
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("name");
                xmlWriter.WriteString(blockReader["BlockCode"].ToString());
                xmlWriter.WriteEndElement();

                SqlCommand getDirection = new SqlCommand("Select * from venue where location = @location",con);
                getDirection.Parameters.AddWithValue("@location",blockReader["BlockCode"]);
                SqlDataReader sda = getDirection.ExecuteReader();

                string direction = "";
                while(sda.Read())
                {
                    direction = sda["EastORWest"].ToString();
                }

                if (direction.Equals("E"))
                    direction = "East";
                else
                    direction = "West";

                xmlWriter.WriteStartElement("location");
                xmlWriter.WriteString(direction);
                xmlWriter.WriteEndElement();


                SqlCommand getTotalCapacity = new SqlCommand("Select SUM(Capacity) from Venue where location = @location", con);
                getTotalCapacity.Parameters.AddWithValue("@location", blockReader["BlockCode"]);

                // Response.Write(blockReader["BlockCode"] + " Total" + getTotalCapacity.ExecuteScalar() + "\n");

                int totalCap = 0;

                if (!getTotalCapacity.ExecuteScalar().ToString().Equals(""))
                    totalCap = (int)getTotalCapacity.ExecuteScalar();

                xmlWriter.WriteStartElement("capacity");
                xmlWriter.WriteString("" + totalCap);
                xmlWriter.WriteEndElement();


                //SqlCommand readVenueID = new SqlCommand("Select * from Venue where location = @location", con);
                SqlCommand readVenueID = new SqlCommand("Select * from Venue where location = @location  Order by(substring(VenueID, 1, 1)), case when isNumeric(substring(VenueID, 2, 1)) = 1 THEN substring(VenueID, 3, 1) when isNumeric(substring(VenueID, 2, 1)) = 0 THEN substring(VenueID, 4, 1) end, substring(VenueID, 2, 1), substring(VenueID, 3, 1) ", con);
                readVenueID.Parameters.AddWithValue("@location", blockReader["BlockCode"]);
                SqlDataReader venueIDReader = readVenueID.ExecuteReader();

                int venueCount = 0;

                while (venueIDReader.Read())
                {
                    xmlWriter.WriteStartElement("venue");
                    xmlWriter.WriteAttributeString("id", "" + venueCount);
                    xmlWriter.WriteStartElement("name");
                    xmlWriter.WriteString(venueIDReader["VenueID"].ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("floor");
                    xmlWriter.WriteString(venueIDReader["Floor"].ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteStartElement("capacity");
                    xmlWriter.WriteString(venueIDReader["Capacity"].ToString());
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    venueCount++;
                }
                blockCount++;
                xmlWriter.WriteEndElement();
            }


            xmlWriter.WriteEndElement();
            xmlWriter.Close();
            con.Close();

        }

        protected void txt_maxCol_TextChanged(object sender, EventArgs e)
        {
           // txt_maxRow.Text = "" + Math.Ceiling((decimal)(int.Parse(txt_Cap.Text) / int.Parse(txt_maxCol.Text)));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            clearAdd();
        }
    }
}