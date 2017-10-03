using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ExamTimetabling2016
{
    class ProgrammeDA
    {
        private SqlConnection conn;
        private string connectionString = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch, cmdInsert, cmdUpdate, cmdDelete;
        private string strSelect, strSearch, strInsert, strUpdate, strDelete;

        public ProgrammeDA()
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
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Programme> searchProgrammesList(DateTime date, string time, string venueID, string courseCode)
        {
            List<Programme> programmesList = new List<Programme>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select ExamType, ProgrammeCode, Year, E.SitFrom, E.SitTo From dbo.Examination E, dbo.Timeslot T Where Day(T.Date) = @Day And Month(T.Date) = @Month And Year(T.Date) = @Year And T.Session = @Session And T.TimeslotID = E.TimeslotID And E.VenueID = @VenueID And E.CourseCode = @CourseCode";

                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@Day", date.Day);
                cmdSearch.Parameters.AddWithValue("@Month", date.Month);
                cmdSearch.Parameters.AddWithValue("@Year", date.Year);
                cmdSearch.Parameters.AddWithValue("@Session", time);
                cmdSearch.Parameters.AddWithValue("@VenueID", venueID);
                cmdSearch.Parameters.AddWithValue("@CourseCode", courseCode);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Programme programme = new Programme(dtr["ExamType"].ToString()+ dtr["ProgrammeCode"].ToString()+ dtr["Year"].ToString(), int.Parse(dtr["Year"].ToString()), int.Parse(dtr["SitFrom"].ToString()), int.Parse(dtr["SitTo"].ToString()));
                        programmesList.Add(programme);
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return programmesList;
        }

        public void shutDown()
        {
            if (conn != null)
                try
                {
                    //Close Database connection
                    conn.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
        }

        public static void main(string[] args)
        {
            ProgrammeDA da = new ProgrammeDA();
        }
    }
}
