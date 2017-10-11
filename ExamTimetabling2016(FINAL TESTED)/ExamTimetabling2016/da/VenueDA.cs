using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ExamTimetabling2016
{
    class VenueDA
    {
        private SqlConnection conn;
        private string connectionString = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch, cmdInsert, cmdUpdate, cmdDelete;
        private string strSelect, strSearch, strInsert, strUpdate, strDelete;

        public VenueDA()
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

        public List<Venue> searchVenuesList(DateTime date, string session, string blockCode)
        {
            List<Venue> venuesList = new List<Venue>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select V.VenueID From dbo.Examination E, dbo.Timeslot T, dbo.Venue V Where Day(T.Date) = @Day And Month(T.Date) = @Month And Year(T.Date) = @Year And T.Session = @Session And T.TimeslotID = E.TimeslotID And E.VenueID = V.VenueID And V.Location = @BlockCode Group By V.VenueID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@Day", date.Day);
                cmdSearch.Parameters.AddWithValue("@Month", date.Month);
                cmdSearch.Parameters.AddWithValue("@Year", date.Year);
                cmdSearch.Parameters.AddWithValue("@Session", session);
                cmdSearch.Parameters.AddWithValue("@BlockCode", blockCode);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        MaintainCourseControl maintainPaperControl = new MaintainCourseControl();
                        Venue venue = new Venue(dtr["VenueID"].ToString(), maintainPaperControl.searchCoursesList(date, session, dtr["VenueID"].ToString()), new List<Staff>());
                        maintainPaperControl.shutDown();
                        MaintainStaffControl maintainLecturerControl = new MaintainStaffControl();
                        venue.InvigilatorsList = maintainLecturerControl.searchInvigilators(date, session, dtr["VenueID"].ToString());
                        maintainLecturerControl.shutDown();
                        venuesList.Add(venue);
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return venuesList;
        }


        public string getLocationByVenueID(string venueID)
        {
            string result = "";
            try
            {

                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select location from dbo.venue where venueid = @venueID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@venueID", venueID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        result = dtr["location"].ToString();
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
                throw;
            }

            return result;
        }


        //get number of invigilator-in-charge assigned in the venue
        public int getNumberOfInvigilatorsInChargeAssinged(DateTime date, string session, string venueID)
        {
            int numberOfInvigilatorsInChargeAssinged = 0;
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select I.StaffID From dbo.InvigilationDuty I, dbo.Timeslot T Where T.Date = @Date And T.Session = @Session And T.TimeslotID = I.TimeslotID And I.VenueID = @VenueID And I.CatOfInvi = 'In-charge'";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@Date", date);
                cmdSearch.Parameters.AddWithValue("@Session", session);
                cmdSearch.Parameters.AddWithValue("@VenueID", venueID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        numberOfInvigilatorsInChargeAssinged++;
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return numberOfInvigilatorsInChargeAssinged;
        }

        public List<String> getListOfAllVenue()
        {
            List<String> venueList = new List<string>();
            try
            {

                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "select * from dbo.Venue";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        venueList.Add(dtr["VenueID"].ToString());
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return venueList;
        }

        public void shutDown()
        {
            if (conn != null)
                try
                {
                    //Close Database connection
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
        }

        public static void main(string[] args)
        {
            VenueDA da = new VenueDA();
        }
    }
}
