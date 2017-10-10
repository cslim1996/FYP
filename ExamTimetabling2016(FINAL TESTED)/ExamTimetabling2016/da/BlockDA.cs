
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ExamTimetabling2016
{
    class BlockDA
    {
        private SqlConnection conn;
        private string connectionString = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch, cmdInsert, cmdUpdate, cmdDelete;
        private string strSelect, strSearch, strInsert, strUpdate, strDelete;

        public BlockDA()
        {
            initializeDatabase();
        }

        private void initializeDatabase()
        {
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        

        public List<Block> searchBlocksList(DateTime date, string session)
        {
            List<Block> blocksList = new List<Block>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select V.Location, V.EastOrWest From dbo.Examination E, dbo.Timeslot T, dbo.Venue V Where T.Date = @Date And T.Session = @Session And T.TimeslotID = E.TimeslotID And E.VenueID = V.VenueID Group By V.Location, V.EastOrWest";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@Date", date);
                cmdSearch.Parameters.AddWithValue("@Session", session);
                SqlDataReader dtr = cmdSearch.ExecuteReader();
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
                        Block block = new Block(dtr["Location"].ToString(), "", maintainStaffControl.searchChiefInvigilators(date, session, dtr["Location"].ToString()), new List<Venue>(), char.Parse(dtr["EastOrWest"].ToString()));
                        maintainStaffControl.shutDown();
                        MaintainVenueControl maintainVenueControl = new MaintainVenueControl();
                        block.VenuesList = maintainVenueControl.searchVenuesList(date, session, dtr["Location"].ToString());
                        maintainVenueControl.shutDown();

                        // Separate block H into 2 blocks if number of venues more than 9
                        if (block.BlockCode.Equals("Block H") && block.VenuesList.Count > 9)
                        {
                            Block blockH1To6 = new Block("Block H, H1-H6", block.Campus, block.ChiefInvigilatorsList, new List<Venue>(), block.EastOrWest);
                            Block blockH7To14 = new Block("Block H, H7-H14", block.Campus, block.ChiefInvigilatorsList, new List<Venue>(), block.EastOrWest);

                            for (int i = 0; i < block.VenuesList.Count; i++)
                            {
                                if (block.VenuesList[i].VenueID.Equals("H1") || block.VenuesList[i].VenueID.Equals("H2") || block.VenuesList[i].VenueID.Equals("H3")
                                    || block.VenuesList[i].VenueID.Equals("H4") || block.VenuesList[i].VenueID.Equals("H5") || block.VenuesList[i].VenueID.Equals("H6"))
                                {
                                    blockH1To6.VenuesList.Add(block.VenuesList[i]);
                                }
                                else
                                {
                                    blockH7To14.VenuesList.Add(block.VenuesList[i]);
                                }
                            }
                            blocksList.Add(blockH1To6);
                            blocksList.Add(blockH7To14);
                        }
                        else
                        {
                            blocksList.Add(block);
                        }
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return blocksList;
        }

        public void shutDown()
        {

            if (conn != null)
                try
                {
                    //Close SqlReader and Database connection
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }

        public static void main(string[] args)
        {
            BlockDA da = new BlockDA();
        }
    }
}
