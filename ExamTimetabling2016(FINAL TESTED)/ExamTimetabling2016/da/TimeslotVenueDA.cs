using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class TimeslotVenueDA
    {
        private SqlConnection conn;
        private string connectionString = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch, cmdInsert, cmdUpdate, cmdDelete;
        private string strSelect, strSearch, strInsert, strUpdate, strDelete;

        public TimeslotVenueDA()
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

        public string getTimeslotID(DateTime date, string session)
        {
            string timeslotID = "";
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select timeslotid From dbo.Timeslot Where Day(Date) = @Day And Month(Date) = @Month And Year(Date) = @Year And Session = @Session";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@Day", date.Day);
                cmdSearch.Parameters.AddWithValue("@Month", date.Month);
                cmdSearch.Parameters.AddWithValue("@Year", date.Year);
                cmdSearch.Parameters.AddWithValue("@Session", session);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        timeslotID = dtr["timeslotid"].ToString();
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
            }

            return timeslotID;
        }
        
        public List<string> selectAllSession()
        {
            List<string> sessionList = new List<string>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select distinct session from dbo.timeslot";
                cmdSearch = new SqlCommand(strSearch, conn);
                

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        sessionList.Add(dtr["session"].ToString());
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
            }


            return sessionList;
        }

        public void insertNoOfInvigilatorsReq(TimeslotVenue timeslotVenue)
        {
            try
            {
                /*Step 2: Create Sql Insert statement and Sql Insert Object*/
                strInsert = "Insert Into InvigilationDuty  (VenueID, TimeslotID, NoOfInvigilators Values (@VenueID, @TimeslotID, @NoOfInvigilators)";
                cmdInsert = new SqlCommand(strInsert, conn);

                cmdInsert.Parameters.AddWithValue("@TimeslotID", timeslotVenue.TimeslotID);
                cmdInsert.Parameters.AddWithValue("@VenueID", timeslotVenue.VenueID);
                cmdInsert.Parameters.AddWithValue("@Location", timeslotVenue.NoOfInvigilatorRequired);
                cmdInsert.ExecuteNonQuery();
               
            }
            catch (SqlException)
            {
                throw;
            }
        }
        
        //untested
        public int countNoOfFacultyInvolved(String timeslotID, String venueID)
        {
            int count = 0;
            try
            {/*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "select count(distinct b.FacultyCode) as FacultyCount from examination a inner join Course c on a.CourseCode = c.CourseCode inner join PaperExamined b on b.CourseCode = c.CourseCode group by TimeslotID,VenueID where a.timeslotid =@timeslotID and a.venueid =@venueID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@timeslotID", timeslotID);
                cmdSearch.Parameters.AddWithValue("@venueID", venueID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();
                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        count = Convert.ToInt32(dtr["FacultyCount"]);
}
                    dtr.Close();
                }

            }
            catch(SqlException ex)
            {
                throw;
            }

            
            return count;

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
            TimeslotVenueDA da = new TimeslotVenueDA();
        }
    }
}
//