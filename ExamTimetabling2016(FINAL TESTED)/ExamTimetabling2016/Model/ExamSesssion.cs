using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

    public class ExamSesssion 
    {
        private DateTime date;
        private String session;

        public ExamSesssion(){
            date = DateTime.Today.Date;
            session = "";
        }

        public ExamSesssion(DateTime date, String session)
        {
            this.date = date;
            this.session = session;
        }

        public DateTime Dates
        {
            get
            {
                //you may retrieve the name from the database and return it
                return date;
            }
            set
            {
                date = value;
                //you may set this name into the database table after this
            }
        }

        public string Session
        {
            get
            {
                return session;
            }
            set
            {
                session = value;
            }
        }

        public void insertToDB(DateTime date, String session, String DatePeriod)
        {
            String TimeslotID;
            if (date.Day < 10 && date.Month >= 10)
            {
                TimeslotID = session + date.Year.ToString().Substring(2, 2) + date.Month + "0" + date.Day;
            }
            else if (date.Day < 10 && date.Month < 10)
            {
                TimeslotID = session + date.Year.ToString().Substring(2, 2) + "0" + date.Month + "0" + date.Day;
            }
            else
            {
                TimeslotID = session + date.Year.ToString().Substring(2, 2) + date.Month + date.Day;
            }
            
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
            
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT INTO Timeslot (TimeslotID, Date, Session, DatePeriod) VALUES ('" + TimeslotID + "', '" + date + "', '" + session + "', '" + DatePeriod + "')";
            cmd.Connection = sqlConnection;

            sqlConnection.Open();   
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public String[,] checkHoliday(String startDates, String endDates)
        {
            String[,] holidayDate;
            int count = 0;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);

            SqlDataReader reader;
            String queryString = "SELECT * FROM Holiday WHERE (StartDate BETWEEN '" + startDates + "' AND '" + endDates + "') OR (EndDate BETWEEN '" + startDates + "' AND '" + endDates + "')";
           
            SqlCommand command = new SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    count += 1;
                }            
            }
            reader.Close();

            using (reader = command.ExecuteReader())
            {
                holidayDate = new String[count,9];
                count = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        holidayDate[count, 0] = reader["HolidayName"].ToString();
                        holidayDate[count, 1] = reader["StartDate"].ToString().Substring(0,9);
                        holidayDate[count, 2] = reader["EndDate"].ToString().Substring(0, 9);
                        holidayDate[count, 3] = reader["isKL"].ToString();
                        holidayDate[count, 4] = reader["isPenang"].ToString();
                        holidayDate[count, 5] = reader["isPerak"].ToString();
                        holidayDate[count, 6] = reader["isJohor"].ToString();
                        holidayDate[count, 7] = reader["isPahang"].ToString();
                        holidayDate[count, 8] = reader["isSabah"].ToString();
                        count += 1;
                    }
                }
            }

            sqlConnection.Close();
            return holidayDate;
        }

        public void deletePreviousSelectedDate(String sessionID)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            String queryString = "DELETE FROM Timeslot WHERE TimeslotID = '" + sessionID + "'";
            cmd.CommandText = queryString;
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public Boolean checkTimeslotIdExist(String TimeslotID)
        {
            Boolean exists = false;

            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
            SqlDataReader reader;
            String queryString = "SELECT * FROM Timeslot WHERE TimeslotID = '" + TimeslotID + "'";
           
            SqlCommand command = new SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                exists = true;
            }
            return exists;
        }

        public DataTable searchByExamePeriod(String DatePeriod)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
            SqlDataReader reader;
            DataTable dt = new DataTable();
            String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date, Session FROM Timeslot WHERE DatePeriod = " + DatePeriod + " ORDER BY Timeslot.Date, Session";

            sqlConnection.Open();
            SqlCommand command = new SqlCommand(queryString, sqlConnection);

            reader = command.ExecuteReader();

            dt.Load(reader);
            sqlConnection.Close();
            return dt;
        }

        
        public List<String> searchDayByExamePeriod(String DatePeriod)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
            SqlDataReader reader;
            List<String> dt = new List<String>();
            String queryString = "SELECT DISTINCT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date FROM Timeslot WHERE DatePeriod = " + DatePeriod + " ORDER BY Date";


            sqlConnection.Open();
        SqlCommand command = new SqlCommand(queryString, sqlConnection);

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            dt.Add(reader.GetString(0));
        }

            return dt;
        }


        public List<String> searchSessionByExamePeriod(String DatePeriod)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Examination"].ConnectionString);
            SqlDataReader reader;
            List<String> dt = new List<String>();
            String queryString = "SELECT CONVERT(VARCHAR(11), Timeslot.Date, 113) AS Date FROM Timeslot WHERE DatePeriod = " + DatePeriod + " ORDER BY Date";


            sqlConnection.Open();
            SqlCommand command = new SqlCommand(queryString, sqlConnection);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                dt.Add(reader.GetString(0));
            }

            return dt;
        }
    }

