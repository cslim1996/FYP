using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace ExamTimetabling2016
{
    public class PaperExaminedDA
    {
        private SqlConnection conn;
        private string connectionString = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch, cmdInsert, cmdUpdate, cmdDelete;
        private string strSelect, strSearch, strInsert, strUpdate, strDelete;

        public PaperExaminedDA()
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

        public List<PaperExamined> getPaperExaminedList(string staffID)
        {
            List<PaperExamined> paperExaminedList = new List<PaperExamined>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select CourseCode from PaperExamined where StaffID = @StaffID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        PaperExamined pExamined = new PaperExamined(staffID, dtr["CourseCode"].ToString());
                        paperExaminedList.Add(pExamined);
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return paperExaminedList;
        }

        public List<string> searchPaperExamined(string location, string timeslotID)
        {
            List<string> paperExaminedList = new List<string>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select distinct e.CourseCode from Examination e, Venue v where e.VenueID = v.VenueID and v.Location = @Location and e.TimeslotID = @TimeslotID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@Location", location);
                cmdSearch.Parameters.AddWithValue("@TimeslotID", timeslotID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        paperExaminedList.Add(dtr["CourseCode"].ToString());
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return paperExaminedList;
        }
        public List<string> searchPaperExaminedByStaffID(string staffID)
        {
            List<string> paperExaminedList = new List<string>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select * from dbo.PaperExamined where staffID = @StaffID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        paperExaminedList.Add(dtr["CourseCode"].ToString());
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return paperExaminedList;
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
    }
}