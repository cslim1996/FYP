using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class AdvancedSearch
{
    List<String> ProgrammeFAFB = new List<String>();
    List<String> ProgrammeFASC = new List<String>();
    List<String> ProgrammeFEBE = new List<String>();
    List<String> ProgrammeFSAH = new List<String>();
    List<String> ProgrammeCNBL = new List<String>();
    List<String> CourseFAFB = new List<String>();
    List<String> CourseFASC = new List<String>();
    List<String> CourseFEBE = new List<String>();
    List<String> CourseFSAH = new List<String>();
    List<String> CourseCNBL = new List<String>();
    List<String> MainPaper = new List<String>();
    String[,] ResitRepeatPaper;

    private String datePeriod;

    public AdvancedSearch()
    {
        datePeriod = "";
    }

    public AdvancedSearch(String datePeriod)
    {
        this.datePeriod = datePeriod;
    }

    public string Session
    {
        get
        {
            return datePeriod;
        }
        set
        {
            datePeriod = value;
        }
    }

    public DataTable searchByDatePeriod(String DatePeriod)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "Timeslot.Session, Examination.VenueID AS Venue, (CONVERT(varchar(3),Examination.SitFrom) + '-' + CONVERT(varchar(3),Examination.SitTo)) AS Seat, " +
                                    "('(' + Examination.PaperType + ')' + Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) AS Programme, Course.Duration " +
                             "FROM Timeslot, Examination, Course, Programme " +  
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " + 
	                               "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
	                               "Timeslot.DatePeriod = " + DatePeriod +
                             " ORDER BY Timeslot.Date, Timeslot.Session, Venue, Course, Programme";
        
        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);
        
        reader = command.ExecuteReader();

        dt.Load(reader);
        sqlConnection.Close();
        return dt;
    }

    public DataTable searchByDatePeriodWithOptionFaculty(String DatePeriod, String option)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "Timeslot.Session, Examination.VenueID AS Venue, (CONVERT(varchar(3),Examination.SitFrom) + '-' + CONVERT(varchar(3),Examination.SitTo)) AS Seat, " +
                                    "('(' + Examination.PaperType + ')' + Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) AS Programme, Course.Duration " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND Faculty.Faculty = '" + option + "' " +
                             " ORDER BY Timeslot.Date, Timeslot.Session, Venue, Course, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();

        dt.Load(reader);
        sqlConnection.Close();
        return dt;
    }

    public DataTable searchByDatePeriodWithOptionFacultyProgramme(String DatePeriod, String option, String option2)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "Timeslot.Session, Examination.VenueID AS Venue, (CONVERT(varchar(3),Examination.SitFrom) + '-' + CONVERT(varchar(3),Examination.SitTo)) AS Seat, " +
                                    "('(' + Examination.PaperType + ')' + Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) AS Programme, Course.Duration " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND " +
                                   "(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + ' - ' + Programme.ProgrammeName) = '" + option + "' AND " +
                                   "Faculty.Faculty = '" + option2 + "' " +
                             " ORDER BY Timeslot.Date, Timeslot.Session, Venue, Course, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();

        dt.Load(reader);
        sqlConnection.Close();
        return dt;
    }

    public DataTable searchByDatePeriodWithOptionFacultyCourse(String DatePeriod, String option, String option2)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "Timeslot.Session, Examination.VenueID AS Venue, (CONVERT(varchar(3),Examination.SitFrom) + '-' + CONVERT(varchar(3),Examination.SitTo)) AS Seat, " +
                                    "('(' + Examination.PaperType + ')' + Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) AS Programme, Course.Duration " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND " +
                                   "(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option + "' AND " +
                                   "Faculty.Faculty = '" + option2 + "' " +
                             " ORDER BY Timeslot.Date, Timeslot.Session, Venue, Course, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();

        dt.Load(reader);
        sqlConnection.Close();
        return dt;
    }

    public DataTable searchByDatePeriodWithOptionDate(String DatePeriod, String option)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "Timeslot.Session, Examination.VenueID AS Venue, (CONVERT(varchar(3),Examination.SitFrom) + '-' + CONVERT(varchar(3),Examination.SitTo)) AS Seat, " +
                                    "('(' + Examination.PaperType + ')' + Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) AS Programme, Course.Duration " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod + " AND Date = '" + option + "' " +
                             " ORDER BY Timeslot.Date, Timeslot.Session, Venue, Course, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();

        dt.Load(reader);
        sqlConnection.Close();
        return dt;
    }

    public DataTable searchByDatePeriodWithOptionDateProgramme(String DatePeriod, String option, String option2)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "Timeslot.Session, Examination.VenueID AS Venue, (CONVERT(varchar(3),Examination.SitFrom) + '-' + CONVERT(varchar(3),Examination.SitTo)) AS Seat, " +
                                    "('(' + Examination.PaperType + ')' + Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) AS Programme, Course.Duration " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND " +
                                   "(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + ' - ' + Programme.ProgrammeName) = '" + option + "' AND " +
                                   "Date = '" + option2 + "' " +
                             " ORDER BY Timeslot.Date, Timeslot.Session, Venue, Course, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();

        dt.Load(reader);
        sqlConnection.Close();
        return dt;
    }

    public DataTable searchByDatePeriodWithOptionDateCourse(String DatePeriod, String option, String option2)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "Timeslot.Session, Examination.VenueID AS Venue, (CONVERT(varchar(3),Examination.SitFrom) + '-' + CONVERT(varchar(3),Examination.SitTo)) AS Seat, " +
                                    "('(' + Examination.PaperType + ')' + Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) AS Programme, Course.Duration " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND " +
                                   "(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option + "' AND " +
                                   "Date = '" + option2 + "' " +
                             " ORDER BY Timeslot.Date, Timeslot.Session, Venue, Course, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();

        dt.Load(reader);
        sqlConnection.Close();
        return dt;
    }

    public DataTable searchByDatePeriodWithProgramme(String DatePeriod, String option)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "Timeslot.Session, Examination.VenueID AS Venue, (CONVERT(varchar(3),Examination.SitFrom) + '-' + CONVERT(varchar(3),Examination.SitTo)) AS Seat, " +
                                    "('(' + Examination.PaperType + ')' + Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) AS Programme, Course.Duration " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND (Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + ' - ' + Programme.ProgrammeName) = '" + option + "' " +
                             " ORDER BY Timeslot.Date, Timeslot.Session, Venue, Course, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();

        dt.Load(reader);
        sqlConnection.Close();
        return dt;
    }

    public DataTable searchByDatePeriodWithCourse(String DatePeriod, String option)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "Timeslot.Session, Examination.VenueID AS Venue, (CONVERT(varchar(3),Examination.SitFrom) + '-' + CONVERT(varchar(3),Examination.SitTo)) AS Seat, " +
                                    "('(' + Examination.PaperType + ')' + Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) AS Programme, Course.Duration " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option + "' " +
                             " ORDER BY Timeslot.Date, Timeslot.Session, Venue, Course, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();

        dt.Load(reader);
        sqlConnection.Close();
        return dt;
    }

    public DataTable searchByDatePeriodWithProgrammeCourse(String DatePeriod, String option, String option2)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "Timeslot.Session, Examination.VenueID AS Venue, (CONVERT(varchar(3),Examination.SitFrom) + '-' + CONVERT(varchar(3),Examination.SitTo)) AS Seat, " +
                                    "('(' + Examination.PaperType + ')' + Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) AS Programme, Course.Duration " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND " +
                                   "(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + ' - ' + Programme.ProgrammeName) = '" + option + "' AND " +
                                   "(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option2 + "' " +
                             " ORDER BY Timeslot.Date, Timeslot.Session, Venue, Course, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();

        dt.Load(reader);
        sqlConnection.Close();
        return dt;
    }

    public List<String> getFaculty(String DatePeriod)
    {
        List<String> faculty = new List<String>();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT DISTINCT Faculty.Faculty " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod +
                                   " ORDER BY Faculty.Faculty";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            faculty.Add(reader.GetString(0));
        }
        return faculty;
    }

    public List<String> getDate(String DatePeriod)
    {
        List<String> date = new List<String>();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT DISTINCT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod +
                                   " ORDER BY Date";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            date.Add(reader.GetString(0));
        }
        return date;
    }

    public List<String> getDateSession(String DatePeriod, String option)
    {
        List<String> date = new List<String>();
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT DISTINCT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, Timeslot.Session " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod + "AND Timeslot.Date = '" + option +  "' " +
                                   " ORDER BY Date, Timeslot.Session";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            date.Add(reader.GetString(1));
        }
        return date;
    }

    public List<String> getProgramme(String DatePeriod)
    {
        List<String> Programme = new List<String>();
        ProgrammeFAFB.Clear();
        ProgrammeFASC.Clear();
        ProgrammeFEBE.Clear();
        ProgrammeFSAH.Clear();

        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + ' - ' + Programme.ProgrammeName) AS Programme, Faculty.Faculty " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod +
                             " ORDER BY Faculty.Faculty, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            Programme.Add(reader.GetString(0));
            if (reader.GetString(1).Equals("FAFB"))
            {
                ProgrammeFAFB.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("FASC"))
            {
                ProgrammeFASC.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("FEBE"))
            {
                ProgrammeFEBE.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("FSAH"))
            {
                ProgrammeFSAH.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("CNBL"))
            {
                ProgrammeCNBL.Add(reader.GetString(0));
            }
            else
            {

            }
        }
        return Programme;
    }

    public List<String> getProgrammeFAFB(String DatePeriod)
    {
        getProgramme(DatePeriod);
        return ProgrammeFAFB;
    }

    public List<String> getProgrammeFASC(String DatePeriod)
    {
        getProgramme(DatePeriod);
        return ProgrammeFASC;
    }

    public List<String> getProgrammeFEBE(String DatePeriod)
    {
        getProgramme(DatePeriod);
        return ProgrammeFEBE;
    }

    public List<String> getProgrammeFSAH(String DatePeriod)
    {
        getProgramme(DatePeriod);
        return ProgrammeFSAH;
    }

    public List<String> getProgrammeCNBL(String DatePeriod)
    {
        getProgramme(DatePeriod);
        return ProgrammeCNBL;
    }

    public List<String> getProgrammeDate(String DatePeriod, String option)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        List<String> ProgrammeDate = new List<String>();
        String queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + ' - ' + Programme.ProgrammeName) AS Programme, Faculty.Faculty, CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod + " AND Date = '" + option + "' " +
                             "ORDER BY Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            ProgrammeDate.Add(reader.GetString(0));           
        }
        return ProgrammeDate;
    }

    public List<String> getCourseDate(String DatePeriod, String option)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        List<String> CourseDate = new List<String>();
        String queryString = "SELECT DISTINCT(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, Faculty.Faculty, CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod + " AND Date = '" + option + "' " +
                             "ORDER BY Course";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            CourseDate.Add(reader.GetString(0));
        }
        return CourseDate;
    }

    public List<String> getCourseDateByDate(String DatePeriod, String option, String option2, String option3)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        List<String> CourseDate = new List<String>();
        String queryString = "SELECT DISTINCT(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, Faculty.Faculty, CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod + " AND Date = '" + option + "' AND Faculty.Faculty = '" + option2 + "' AND Timeslot.Session = '" + option3 + "' " +
                             "ORDER BY Course";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            CourseDate.Add(reader.GetString(0));
        }
        return CourseDate;
    }

    public List<String> getCourse(String DatePeriod)
    {
        List<String> Course = new List<String>();
        CourseFAFB.Clear();
        CourseFASC.Clear();
        CourseFEBE.Clear();
        CourseFSAH.Clear();

        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT DISTINCT(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, Faculty.Faculty " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod +
                             " ORDER BY Course";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            Course.Add(reader.GetString(0));
            if (reader.GetString(1).Equals("FAFB"))
            {
                CourseFAFB.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("FASC"))
            {
                CourseFASC.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("FEBE"))
            {
                CourseFEBE.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("FSAH"))
            {
                CourseFSAH.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("CNBL"))
            {
                CourseCNBL.Add(reader.GetString(0));
            }
            else
            {

            }
        }
        return Course;
    }

    public List<String> getCourseFAFB(String DatePeriod)
    {
        getCourse(DatePeriod);
        return CourseFAFB;
    }

    public List<String> getCourseFASC(String DatePeriod)
    {
        getCourse(DatePeriod);
        return CourseFASC;
    }

    public List<String> getCourseFEBE(String DatePeriod)
    {
        getCourse(DatePeriod);
        return CourseFEBE;
    }

    public List<String> getCourseFSAH(String DatePeriod)
    {
        getCourse(DatePeriod);
        return CourseFSAH;
    }

    public List<String> getCourseCNBL(String DatePeriod)
    {
        getCourse(DatePeriod);
        return CourseCNBL;
    }

    public List<String> getCourseWithProgramme(String DatePeriod, String option)
    {
        List<String> Course = new List<String>();

        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT DISTINCT(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND (Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + ' - ' + Programme.ProgrammeName) = '" + option + "' " +
                             "ORDER BY Course";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            Course.Add(reader.GetString(0));
        }
        return Course;
    }

    public List<String> getCourseWithProgrammeOrderDate(String DatePeriod, String option)
    {
        List<String> Course = new List<String>();

        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT DISTINCT(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, Timeslot.Session " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND (Examination.ExamType + Examination.ProgrammeCode) = '" + option + "' " +
                             "ORDER BY Date, Session";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            Course.Add(reader.GetString(0));
        }
        return Course;
    }

    public List<String> getProgrammeWithCourse(String DatePeriod, String option)
    {
        List<String> Programme = new List<String>();

        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + '-' + Programme.ProgrammeName) AS Programme " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = '" + DatePeriod + "' AND (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option + "' " +
                             "ORDER BY Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            Programme.Add(reader.GetString(0));
        }
        return Programme;
    }

    public List<String> getTimetableOfProgrammeCourse_Main(String DatePeriod, String option, String option2, String option3)
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + '-' + Programme.ProgrammeName) AS Programmes, " + 
	                                "CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, Timeslot.Session, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "ProgrammeStructureCourse.Population, ProgrammeStructure.YearStudy, ProgrammeStructure.Semester, Course.Duration, Examination.VenueID AS Venue " +       
                             "FROM Timeslot, Examination, Course, Programme, Faculty, RepeatResitRegistration, ProgrammeStructureCourse, ProgrammeStructure " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " + 
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " + 
	                               "ProgrammeStructureCourse.CourseCode = Examination.CourseCode AND ProgrammeStructureCourse.PSID = ProgrammeStructure.PSID AND " +
                                   "(ProgrammeStructure.ExamType + ProgrammeStructure.ProgrammeCode) = '" + option + "' AND ProgrammeStructure.ProgrammeCode = Examination.ProgrammeCode AND " +
                                   "ProgrammeStructure.Session = " + option3 + " AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod + " AND " +
                                   "(Examination.ExamType + Examination.ProgrammeCode) = '" + option + "' AND ProgrammeStructure.YearStudy = CONVERT(varchar(1),Examination.Year) AND " +
                                   "(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option2 + "' " + 
                            "ORDER BY Programmes";           
            

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        MainPaper.Clear();
        while (reader.Read())
        {
            MainPaper.Add(reader.GetString(0));
            MainPaper.Add(reader.GetString(1));
            MainPaper.Add(reader.GetString(2));
            MainPaper.Add(reader.GetString(3));
            MainPaper.Add(reader.GetInt32(4).ToString());
            MainPaper.Add(reader.GetInt32(5).ToString());
            MainPaper.Add(reader.GetInt32(6).ToString());
            MainPaper.Add(reader.GetInt32(7).ToString());
            MainPaper.Add(reader.GetString(8).ToString());
        }
        reader.Close();
        sqlConnection.Close();
        return MainPaper;
    }

    public  String[,]  getTimetableOfProgrammeCourse_ResitRepeat(String DatePeriod, String option, String option2)
    {
        List<String> Programme = new List<String>();
        int count = 0;
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();

        String queryString1 = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + ' - ' + Programme.ProgrammeName) AS Programmes " +
                              "FROM Examination, Programme " +
                              "WHERE Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND Examination.ExamType + Examination.ProgrammeCode = '" + option + "' ";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString1, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            Programme.Add(reader.GetString(0));
        }
        sqlConnection.Close();

        for (int i = 0; i < Programme.Count; i++)
        {
            String queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + '-' + Programme.ProgrammeName) AS Programmes, " +
                                        "CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, Timeslot.Session, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                        "RepeatResitRegistration.PaperType, RepeatResitRegistration.NoOfStudRegistered, RepeatResitRegistration.YearStudy, RepeatResitRegistration.Semester, Course.Duration " +
                                 "FROM Timeslot, Examination, Course, Programme, Faculty, RepeatResitRegistration, ProgrammeStructureCourse, ProgrammeStructure " +
                                 "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                       "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                       "RepeatResitRegistration.CourseCode = Examination.CourseCode AND RepeatResitRegistration.ProgrammeCode = Examination.ProgrammeCode AND  " +
                                       "RepeatResitRegistration.CourseCode = Course.CourseCode AND RepeatResitRegistration.ProgrammeCode = Programme.ProgrammeCode AND  " +
                                       "(RepeatResitRegistration.ExamType + RepeatResitRegistration.ProgrammeCode + CONVERT(varchar(1),RepeatResitRegistration.YearStudy)) = '" + Programme[i].Substring(0,4) + "' AND  " +
                                       "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod =  " + DatePeriod + " AND " +
                                       "(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) = '" + Programme[i].Substring(0,4) + "' AND " +
                                       "(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option2 + "' " +
                                "ORDER BY Programmes";          
           


            sqlConnection.Open();
            command = new SqlCommand(queryString, sqlConnection);

            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    count += 1;
                }
            }
            ResitRepeatPaper = new String[count, 9];
            reader.Close();
            sqlConnection.Close();
        }

        count = 0;
        for (int i = 0; i < Programme.Count; i++)
        {
            String queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + '-' + Programme.ProgrammeName) AS Programmes, " +
                                        "CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, Timeslot.Session, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                        "RepeatResitRegistration.PaperType, RepeatResitRegistration.NoOfStudRegistered, RepeatResitRegistration.YearStudy, RepeatResitRegistration.Semester, Course.Duration " +
                                 "FROM Timeslot, Examination, Course, Programme, Faculty, RepeatResitRegistration, ProgrammeStructureCourse, ProgrammeStructure " +
                                 "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                       "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                       "RepeatResitRegistration.CourseCode = Examination.CourseCode AND RepeatResitRegistration.ProgrammeCode = Examination.ProgrammeCode AND  " +
                                       "RepeatResitRegistration.CourseCode = Course.CourseCode AND RepeatResitRegistration.ProgrammeCode = Programme.ProgrammeCode AND  " +
                                       "(RepeatResitRegistration.ExamType + RepeatResitRegistration.ProgrammeCode + CONVERT(varchar(1),RepeatResitRegistration.YearStudy)) = '" + Programme[i].Substring(0, 4) + "' AND  " +
                                       "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod =  " + DatePeriod + " AND " +
                                       "(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) = '" + Programme[i].Substring(0, 4) + "' AND " +
                                       "(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option2 + "' " +
                                "ORDER BY Programmes";

            sqlConnection.Open();
            command = new SqlCommand(queryString, sqlConnection);

            
            reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResitRepeatPaper[count, 0] = reader.GetString(0);
                        ResitRepeatPaper[count, 1] = reader.GetString(1);
                        ResitRepeatPaper[count, 2] = reader.GetString(2);
                        ResitRepeatPaper[count, 3] = reader.GetString(3);
                        ResitRepeatPaper[count, 4] = reader.GetString(4);
                        ResitRepeatPaper[count, 5] = reader.GetInt32(5).ToString();
                        ResitRepeatPaper[count, 6] = reader.GetInt32(6).ToString();
                        ResitRepeatPaper[count, 7] = reader.GetInt32(7).ToString();
                        ResitRepeatPaper[count, 8] = reader.GetInt32(8).ToString();
                        //ResitRepeatPaper[count, 9] = reader.GetString(9);
                        count += 1;
                    }
                }
            reader.Close();
            sqlConnection.Close();
        }
        sqlConnection.Close();
        return ResitRepeatPaper;
    }

    public String[,] getTimetableOfProgrammeCourse_MainSearch(String DatePeriod, String option2, String option3)
    {
        List<String> Programme = new List<String>();
        int count = 0;
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();

        String queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + '-' + Programme.ProgrammeName) AS Programmes, " +
                                    "CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, Timeslot.Session, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "ProgrammeStructureCourse.Population, ProgrammeStructure.YearStudy, ProgrammeStructure.Semester, Course.Duration, Examination.VenueID AS Venue " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty, RepeatResitRegistration, ProgrammeStructureCourse, ProgrammeStructure " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "ProgrammeStructureCourse.CourseCode = Examination.CourseCode AND ProgrammeStructureCourse.PSID = ProgrammeStructure.PSID AND " +
                                   "ProgrammeStructure.ProgrammeCode = Examination.ProgrammeCode AND " +
                                   "ProgrammeStructure.Session = " + option3 + " AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod + " AND " +
                                   "ProgrammeStructure.YearStudy = CONVERT(varchar(1),Examination.Year) AND " +
                                   "(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option2 + "' " +
                            "ORDER BY Programmes";           
            

            sqlConnection.Open();
            SqlCommand command = new SqlCommand(queryString, sqlConnection);

            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    count += 1;
                }
            }
            ResitRepeatPaper = new String[count, 9];
            reader.Close();
            sqlConnection.Close();

        count = 0;
        queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + '-' + Programme.ProgrammeName) AS Programmes, " +
                                "CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, Timeslot.Session, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                "ProgrammeStructureCourse.Population, ProgrammeStructure.YearStudy, ProgrammeStructure.Semester, Course.Duration, Examination.VenueID AS Venue " +
                         "FROM Timeslot, Examination, Course, Programme, Faculty, RepeatResitRegistration, ProgrammeStructureCourse, ProgrammeStructure " +
                         "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                               "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                               "ProgrammeStructureCourse.CourseCode = Examination.CourseCode AND ProgrammeStructureCourse.PSID = ProgrammeStructure.PSID AND " +
                               "ProgrammeStructure.ProgrammeCode = Examination.ProgrammeCode AND " +
                               "ProgrammeStructure.Session = " + option3 + " AND " +
                               "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod + " AND " +
                               "ProgrammeStructure.YearStudy = CONVERT(varchar(1),Examination.Year) AND " +
                               "(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option2 + "' " +
                        "ORDER BY Programmes"; 

            sqlConnection.Open();
            command = new SqlCommand(queryString, sqlConnection);


            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ResitRepeatPaper[count, 0] = reader.GetString(0);
                    ResitRepeatPaper[count, 1] = reader.GetString(1);
                    ResitRepeatPaper[count, 2] = reader.GetString(2);
                    ResitRepeatPaper[count, 3] = reader.GetString(3);
                    ResitRepeatPaper[count, 4] = reader.GetInt32(4).ToString();
                    ResitRepeatPaper[count, 5] = reader.GetInt32(5).ToString();
                    ResitRepeatPaper[count, 6] = reader.GetInt32(6).ToString();
                    ResitRepeatPaper[count, 7] = reader.GetInt32(7).ToString();
                    ResitRepeatPaper[count, 8] = reader.GetString(8);
                    //ResitRepeatPaper[count, 9] = reader.GetString(9);
                    count += 1;
                }
            }
            reader.Close();
            sqlConnection.Close();
        
        return ResitRepeatPaper;
    }

    public String[,] getTimetableOfProgrammeCourse_ResitRepeatSearch(String DatePeriod, String option2)
    {
        List<String> Programme = new List<String>();
        int count = 0;
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();

        String queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + '-' + Programme.ProgrammeName) AS Programmes, " +
                                    "CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, Timeslot.Session, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "RepeatResitRegistration.PaperType, RepeatResitRegistration.NoOfStudRegistered, RepeatResitRegistration.YearStudy, RepeatResitRegistration.Semester, Course.Duration, Examination.VenueID AS Venue  " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty, RepeatResitRegistration, ProgrammeStructureCourse, ProgrammeStructure " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "RepeatResitRegistration.CourseCode = Examination.CourseCode AND RepeatResitRegistration.ProgrammeCode = Examination.ProgrammeCode AND  " +
                                   "RepeatResitRegistration.CourseCode = Course.CourseCode AND RepeatResitRegistration.ProgrammeCode = Programme.ProgrammeCode AND  " +
            //"(RepeatResitRegistration.ExamType + RepeatResitRegistration.ProgrammeCode + CONVERT(varchar(1),RepeatResitRegistration.YearStudy)) = '" + Programme[i].Substring(0, 4) + "' AND  " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod =  " + DatePeriod + " AND " +
            //"(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) = '" + Programme[i].Substring(0, 4) + "' AND " +
                                   "(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option2 + "' " +
                            "ORDER BY Programmes";



        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                count += 1;
            }
        }
        ResitRepeatPaper = new String[count, 10];
        reader.Close();
        sqlConnection.Close();

        count = 0;
        queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + '-' + Programme.ProgrammeName) AS Programmes, " +
                                    "CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, Timeslot.Session, (Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) AS Course, " +
                                    "RepeatResitRegistration.PaperType, RepeatResitRegistration.NoOfStudRegistered, RepeatResitRegistration.YearStudy, RepeatResitRegistration.Semester, Course.Duration, Examination.VenueID AS Venue  " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty, RepeatResitRegistration, ProgrammeStructureCourse, ProgrammeStructure " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "RepeatResitRegistration.CourseCode = Examination.CourseCode AND RepeatResitRegistration.ProgrammeCode = Examination.ProgrammeCode AND  " +
                                   "RepeatResitRegistration.CourseCode = Course.CourseCode AND RepeatResitRegistration.ProgrammeCode = Programme.ProgrammeCode AND  " +
            //"(RepeatResitRegistration.ExamType + RepeatResitRegistration.ProgrammeCode + CONVERT(varchar(1),RepeatResitRegistration.YearStudy)) = '" + Programme[i].Substring(0, 4) + "' AND  " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod =  " + DatePeriod + " AND " +
            //"(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year)) = '" + Programme[i].Substring(0, 4) + "' AND " +
                                   "(Course.CourseCode + ' ' + CONVERT(varchar(100),Course.CourseTitle) COLLATE Latin1_General_CI_AS) = '" + option2 + "' " +
                            "ORDER BY Programmes";

        sqlConnection.Open();
        command = new SqlCommand(queryString, sqlConnection);


        reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                ResitRepeatPaper[count, 0] = reader.GetString(0);
                ResitRepeatPaper[count, 1] = reader.GetString(1);
                ResitRepeatPaper[count, 2] = reader.GetString(2);
                ResitRepeatPaper[count, 3] = reader.GetString(3);
                ResitRepeatPaper[count, 4] = reader.GetString(4);
                ResitRepeatPaper[count, 5] = reader.GetInt32(5).ToString();
                ResitRepeatPaper[count, 6] = reader.GetInt32(6).ToString();
                ResitRepeatPaper[count, 7] = reader.GetInt32(7).ToString();
                ResitRepeatPaper[count, 8] = reader.GetInt32(8).ToString();
                ResitRepeatPaper[count, 9] = reader.GetString(9);
                count += 1;
            }
        }
        reader.Close();
        sqlConnection.Close();

        return ResitRepeatPaper;
    }

    public List<String> getProgrammeWOYear(String DatePeriod)
    {
        List<String> Programme = new List<String>();
        ProgrammeFAFB.Clear();
        ProgrammeFASC.Clear();
        ProgrammeFEBE.Clear();
        ProgrammeFSAH.Clear();

        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
        SqlDataReader reader;
        DataTable dt = new DataTable();
        String queryString = "SELECT DISTINCT(Examination.ExamType + Examination.ProgrammeCode + CONVERT(varchar(1),Examination.Year) + ' - ' + Programme.ProgrammeName) AS Programme, Faculty.Faculty " +
                             "FROM Timeslot, Examination, Course, Programme, Faculty " +
                             "WHERE Examination.TimeslotID = Timeslot.TimeslotID AND Course.CourseCode = Examination.CourseCode AND " +
                                   "Programme.ProgrammeCode = Examination.ProgrammeCode AND Programme.ExamType = Examination.ExamType AND " +
                                   "Faculty.FacultyCode = Programme.FacultyCode AND Timeslot.DatePeriod = " + DatePeriod +
                             " ORDER BY Faculty.Faculty, Programme";

        sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            Programme.Add(reader.GetString(0));
            if (reader.GetString(1).Equals("FAFB"))
            {
                ProgrammeFAFB.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("FASC"))
            {
                ProgrammeFASC.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("FEBE"))
            {
                ProgrammeFEBE.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("FSAH"))
            {
                ProgrammeFSAH.Add(reader.GetString(0));
            }
            else if (reader.GetString(1).Equals("CNBL"))
            {
                ProgrammeCNBL.Add(reader.GetString(0));
            }
            else
            {

            }
        }
        return Programme;
    }

    public List<String> getProgrammeFAFBWOYear(String DatePeriod)
    {
        getProgrammeWOYear(DatePeriod);
        return ProgrammeFAFB;
    }

    public List<String> getProgrammeFASCWOYear(String DatePeriod)
    {
        getProgrammeWOYear(DatePeriod);
        return ProgrammeFASC;
    }

    public List<String> getProgrammeFEBEWOYear(String DatePeriod)
    {
        getProgrammeWOYear(DatePeriod);
        return ProgrammeFEBE;
    }

    public List<String> getProgrammeFSAHWOYear(String DatePeriod)
    {
        getProgrammeWOYear(DatePeriod);
        return ProgrammeFSAH;
    }
}
