using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class ExaminationDA
    {
        private SqlConnection conn;
        private string connectionstring = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch;
        private string strSelect, strSearch;

        public ExaminationDA()
        {
            initializeDatabase();
        }

        private void initializeDatabase()
        {
            try
            {
                conn = new SqlConnection(connectionstring);
                conn.Open();
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Examination> searchExamination(string timeslotID)
        {
            List<Examination> examList = new List<Examination>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select * from dbo.Examination where TimeslotID = @TimeslotID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@TimeslotID", timeslotID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Examination exam = new Examination(timeslotID, dtr["VenueID"].ToString(), dtr["CourseCode"].ToString(), dtr["ProgrammeCode"].ToString(), Convert.ToChar(dtr["PaperType"].ToString()), Convert.ToChar(dtr["ExamType"].ToString()), Int32.Parse(dtr["Year"].ToString()), Int32.Parse(dtr["SitFrom"].ToString()), Int32.Parse(dtr["SitTo"].ToString()));
                        examList.Add(exam);
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return examList;
        }

        public List<string> getTimeslot()
        {
            List<string> timeslotList = new List<string>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select * from dbo.Timeslot order by Date, TimeslotID";
                cmdSearch = new SqlCommand(strSearch, conn);       

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        timeslotList.Add(dtr["TimeslotID"].ToString());
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return timeslotList;
        }
        
        public List<Examination> getExaminationList()
        {
            List<Examination> result = new List<Examination>();

            try
            {

                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "select * from dbo.Examination";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Examination exam = new Examination(dtr["TimeslotID"].ToString(), dtr["VenueID"].ToString(), dtr["CourseCode"].ToString(), dtr["ProgrammeCode"].ToString(), Convert.ToChar(dtr["PaperType"].ToString()), Convert.ToChar(dtr["ExamType"].ToString()), Int32.Parse(dtr["Year"].ToString()), Int32.Parse(dtr["SitFrom"].ToString()), Int32.Parse(dtr["SitTo"].ToString()));
                        result.Add(exam);
                    }
                    dtr.Close();
                }

            }
            catch (SqlException)
            {
                throw;
            }
            return result;
        }

        public List<Examination> searchExaminationByTimeslotAndVenue(string timeslotID, string venueID)
        {
            List<Examination> examList = new List<Examination>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select * from dbo.Examination where TimeslotID = @TimeslotID AND VenueID = @VenueID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@TimeslotID", timeslotID);
                cmdSearch.Parameters.AddWithValue("@VenueID", venueID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Examination exam = new Examination(timeslotID, venueID, dtr["CourseCode"].ToString(), dtr["ProgrammeCode"].ToString(), Convert.ToChar(dtr["PaperType"].ToString()), Convert.ToChar(dtr["ExamType"].ToString()), Int32.Parse(dtr["Year"].ToString()), Int32.Parse(dtr["SitFrom"].ToString()), Int32.Parse(dtr["SitTo"].ToString()));
                        examList.Add(exam);
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return examList;
        }

        public List<Examination> searchExamByQuery(string examQuery)
        {
            List<Examination> result = new List<Examination>();

            try
            {

                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = examQuery;
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Examination exam = new Examination(dtr["TimeslotID"].ToString(), dtr["VenueID"].ToString(), dtr["CourseCode"].ToString(), dtr["ProgrammeCode"].ToString(), Convert.ToChar(dtr["PaperType"].ToString()), Convert.ToChar(dtr["ExamType"].ToString()), Int32.Parse(dtr["Year"].ToString()), Int32.Parse(dtr["SitFrom"].ToString()), Int32.Parse(dtr["SitTo"].ToString()));
                        result.Add(exam);
                    }
                    dtr.Close();
                }

            }
            catch (SqlException)
            {
                throw;
            }
                return result;
    }

        public List<char> getDistinctExamPaperType()
        {

            List<char> paperTypeList = new List<char>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "select distinct Examination.PaperType from Examination";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        paperTypeList.Add(Convert.ToChar(dtr["PaperType"]));
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return paperTypeList;
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
    }
}