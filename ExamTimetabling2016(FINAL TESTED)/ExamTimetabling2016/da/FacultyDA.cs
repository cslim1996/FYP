using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class FacultyDA
    {
        private SqlConnection conn;
        private string connectionstring = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSearch;
        private string strSearch;

        public FacultyDA()
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

        public List<Faculty> getFacultyList()
        {
            List<Faculty> facultyList = new List<Faculty>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select * from dbo.Faculty";
                cmdSearch = new SqlCommand(strSearch, conn);
                

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Faculty faculty = new Faculty();
                        faculty = new Faculty(Convert.ToChar(dtr["FacultyCode"]), dtr["Faculty"].ToString(), dtr["FacultyName"].ToString());
                        facultyList.Add(faculty);
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return facultyList;


        }

        public Faculty getFacultyByCourseCode(string courseCode)
        {
            Faculty faculty = new Faculty();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select * from dbo.PaperExamined a inner join dbo.Faculty b on a.FacultyCode = b.FacultyCode  where a.CourseCode = @CourseCode";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@CourseCode", courseCode);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        faculty = new Faculty(Convert.ToChar(dtr["FacultyCode"]), dtr["Faculty"].ToString(), dtr["FacultyName"].ToString());
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            
            return faculty;
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