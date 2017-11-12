using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class ExemptionDA
    {
        private SqlConnection conn;
        private string connectionstring = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSearch;
        private string strSearch;

        public ExemptionDA()
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

        public List<Exemption> searchExemption(string staffID)
        {
            List<Exemption> exemptionList = new List<Exemption>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select Date, Session from dbo.Exemption where StaffID = @StaffID order by Date, Session";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Exemption exemption = new Exemption(Convert.ToDateTime(dtr["Date"].ToString()), dtr["Session"].ToString());
                        exemptionList.Add(exemption);
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return exemptionList;
        }

        public List<String> searchAllSessionAvailable()
        {
            List<string> result = new List<string>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "select distinct session from dbo.timeslot";
                cmdSearch = new SqlCommand(strSearch, conn);
                

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        result.Add(dtr["Session"].ToString());
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public List<Exemption> searchExemptionList(string staffID)
        {
            List<Exemption> exemptionList = new List<Exemption>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select Date, Session From dbo.Exemption E Where E.StaffID = @StaffID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Exemption exemption = new Exemption(DateTime.Parse(dtr["Date"].ToString()), dtr["Session"].ToString());
                        exemptionList.Add(exemption);
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return exemptionList;
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