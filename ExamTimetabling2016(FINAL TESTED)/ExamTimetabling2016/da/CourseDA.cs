using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class CourseDA
    {
        private SqlConnection conn;
        private string connectionstring = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSearch;
        private string strSearch;

        public CourseDA()
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

        public string getCourseTitle(string courseCode)
        {
            Course course = null;

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select CourseTitle from dbo.Course where CourseCode = @CourseCode";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@CourseCode", courseCode);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        course = new Course(courseCode, dtr["CourseTitle"].ToString());
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return course.CourseTitle;
        }

        public List<Course> searchCoursesList(DateTime date, string time, string venueID)
        {
            List<Course> coursesList = new List<Course>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select C.CourseCode, C.CourseTitle, C.Duration, C.DoubleSeating, C.CnblPaper From dbo.Examination E, dbo.Timeslot T, dbo.Course C Where Day(T.Date) = @Day And Month(T.Date) = @Month And Year(T.Date) = @Year And T.Session = @Session And T.TimeslotID = E.TimeslotID And E.VenueID = @VenueID And E.CourseCode = C.CourseCode Group by C.CourseCode, C.CourseTitle, C.Duration, C.DoubleSeating, C.CnblPaper";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@Day", date.Day);
                cmdSearch.Parameters.AddWithValue("@Month", date.Month);
                cmdSearch.Parameters.AddWithValue("@Year", date.Year);
                cmdSearch.Parameters.AddWithValue("@Session", time);
                cmdSearch.Parameters.AddWithValue("@VenueID", venueID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        MaintainProgrammeControl maintainProgrammeControl = new MaintainProgrammeControl();
                        bool isDoubleSeating = false;
                        if (dtr["DoubleSeating"].ToString().Equals("Y"))
                        {
                            isDoubleSeating = true;
                        }
                        else
                        {
                            isDoubleSeating = false;
                        }
                        bool isCnblPaper = false;
                        if (dtr["CnblPaper"].ToString().Equals("Y"))
                        {
                            isCnblPaper = true;
                        }
                        else
                        {
                            isCnblPaper = false;
                        }
                        Course course = new Course(dtr["CourseCode"].ToString(), dtr["CourseTitle"].ToString(), int.Parse(dtr["Duration"].ToString()), isDoubleSeating, isCnblPaper, maintainProgrammeControl.searchProgrammesList(date, time, venueID, dtr["CourseCode"].ToString()));
                        maintainProgrammeControl.shutDown();
                        coursesList.Add(course);
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return coursesList;
        }

        public List<string> searchCourseCodesExaminedList(DateTime date, string session, bool isMainPaperOnly)
        {
            List<string> courseCodesExaminedList = new List<string>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                if (isMainPaperOnly)
                {
                    strSearch = "Select E.CourseCode, SUM(SitTo - SitFrom + 1) as NumOfStud " +
                    "from Examination E, PaperExamined P, Staff S " +
                    "where P.StaffID = S.StaffID and S.isChiefInvi = 'Y' and S.isExam = 'Y' and S.TypeOfEmploy = 'F' and TimeslotID = @TimeslotID and E.PaperType = 'M' and P.PaperType = 'M' and P.CourseCode = E.CourseCode " +
                    "group by E.CourseCode order by NumOfStud desc";

                }
                else
                {
                    strSearch = "Select E.CourseCode, SUM(SitTo - SitFrom + 1) as NumOfStud " +
                    "from Examination E, PaperExamined P, Staff S " +
                    "where P.StaffID = S.StaffID and S.isChiefInvi = 'Y' and S.isExam = 'Y' and S.TypeOfEmploy = 'F' and TimeslotID = @TimeslotID and P.CourseCode = E.CourseCode " +
                    "group by E.CourseCode order by NumOfStud desc";
                }

                cmdSearch = new SqlCommand(strSearch, conn);
                cmdSearch.Parameters.AddWithValue("@TimeslotID", session + date.ToString("ddMMyy"));
                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string courseCode = dtr["CourseCode"].ToString();
                        courseCodesExaminedList.Add(courseCode);
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return courseCodesExaminedList;
        }

        public List<string> searchFacultyCodesList(List<Course> CoursesList)
        {
            List<string> facultyCodesList = new List<string>();
            for (int i = 0; i < CoursesList.Count; i++)
            {
                try
                {
                    /*Step 2: Create Sql Search statement and Sql Search Object*/
                    strSearch = "select FacultyCode from dbo.PaperExamined where CourseCode = @CourseCode";
                    cmdSearch = new SqlCommand(strSearch, conn);

                    cmdSearch.Parameters.AddWithValue("@CourseCode", CoursesList[i].CourseCode);

                    /*Step 3: Execute command to retrieve data*/
                    SqlDataReader dtr = cmdSearch.ExecuteReader();

                    /*Step 4: Get result set from the query*/
                    if (dtr.HasRows)
                    {
                        while (dtr.Read())
                        {
                            bool isDuplicated = false;
                            foreach (string faculty in facultyCodesList)
                            {
                                if (faculty.Equals(dtr["FacultyCode"].ToString()))
                                {
                                    isDuplicated = true;
                                }
                            }
                            if (!isDuplicated)
                                facultyCodesList.Add(dtr["FacultyCode"].ToString());
                        }
                    }
                    dtr.Close();
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            return facultyCodesList;
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