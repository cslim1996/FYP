using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class InvigilationDutyDA
    {
        private SqlConnection conn;
        private string connectionstring = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSearch, cmdInsert, cmdUpdate;
        private string strSearch, strInsert, strUpdate, strDelete;

        public InvigilationDutyDA()
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

        public List<InvigilationDuty> searchInvigilationDuty2(string timeslotID)
        {
            List<InvigilationDuty> invDutyList = new List<InvigilationDuty>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select VenueID, Location, StaffID, CatOfInvi, Duration from dbo.InvigilationDuty where TimeslotID = @TimeslotID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@TimeslotID", timeslotID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        InvigilationDuty invDuty = new InvigilationDuty(timeslotID, dtr["VenueID"].ToString(), dtr["Location"].ToString(), dtr["StaffID"].ToString(), dtr["CatOfInvi"].ToString(), Int32.Parse(dtr["Duration"].ToString()));
                        invDutyList.Add(invDuty);
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return invDutyList;
        }

        public List<InvigilationDuty> searchInvigilationDuty2(int staffID)
        {
            List<InvigilationDuty> invDutyList = new List<InvigilationDuty>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select TimeslotID, VenueID, Location, CatOfInvi, Duration from dbo.InvigilationDuty where StaffID = @StaffID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@StaffID", staffID.ToString());

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        InvigilationDuty invDuty = new InvigilationDuty(dtr["TimeslotID"].ToString(), dtr["VenueID"].ToString(), dtr["Location"].ToString(), staffID.ToString(), dtr["CatOfInvi"].ToString(), Int32.Parse(dtr["Duration"].ToString()));
                        invDutyList.Add(invDuty);
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return invDutyList;
        }

        public List<InvigilationDuty> searchInvigilationDuty(string staffID)
        {
            List<InvigilationDuty> invigilationDutiesList = new List<InvigilationDuty>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select T.Date, T.Session, I.VenueID, I.Location, I.Duration, I.CatOfInvi From dbo.Timeslot T, dbo.InvigilationDuty I Where I.StaffID = @StaffID And I.TimeSlotID = T.TimeSlotID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string categoryOfInvigilator = dtr["CatOfInvi"].ToString();
                        string venueID = "";
                        if (categoryOfInvigilator.Equals("Chief"))
                        {
                            venueID = "";
                        }
                        else
                        {
                            venueID = dtr["VenueID"].ToString();
                        }

                        InvigilationDuty invigilationDuty = new InvigilationDuty(DateTime.Parse(dtr["Date"].ToString()), dtr["Session"].ToString(), venueID, dtr["Location"].ToString(), dtr["CatOfInvi"].ToString(), int.Parse(dtr["Duration"].ToString()));
                        invigilationDutiesList.Add(invigilationDuty);
                    }

                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return invigilationDutiesList;
        }

        public int addInvigilationDuty(Staff staff, InvigilationDuty invigilationDuty)
        {
            int n = 0;
            try
            {
                /*Step 2: Create Sql Insert statement and Sql Insert Object*/
                strInsert = "Insert Into InvigilationDuty  (TimeslotID, VenueID, Location, StaffID, CatOfInvi, Duration) Values (@TimeslotID, @VenueID, @Location, @StaffID, @CategoryOfInvigilator, @Duration)";
                cmdInsert = new SqlCommand(strInsert, conn);

                cmdInsert.Parameters.AddWithValue("@TimeslotID", invigilationDuty.Session + invigilationDuty.Date.Date.ToString("ddMMyy"));
                if (invigilationDuty.VenueID.Equals(""))
                {
                    cmdInsert.Parameters.AddWithValue("@VenueID", DBNull.Value);
                }
                else
                {

                    cmdInsert.Parameters.AddWithValue("@VenueID", invigilationDuty.VenueID);
                }
                if (invigilationDuty.Location.Equals("") && invigilationDuty.CategoryOfInvigilator.Equals("Relief"))
                {
                    cmdInsert.Parameters.AddWithValue("@Location", "West Campus");
                }
                else
                {
                    cmdInsert.Parameters.AddWithValue("@Location", invigilationDuty.Location);
                }
                cmdInsert.Parameters.AddWithValue("@StaffID", staff.StaffID);
                cmdInsert.Parameters.AddWithValue("@CategoryOfInvigilator", invigilationDuty.CategoryOfInvigilator);
                cmdInsert.Parameters.AddWithValue("@Duration", invigilationDuty.Duration);

                /*Step 3: Execute command to insert*/
                n = cmdInsert.ExecuteNonQuery();
                if (invigilationDuty.Date.DayOfWeek == DayOfWeek.Saturday)
                {
                    updateNoOfSatSession(staff.StaffID);
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return n;
        }

        public int changeCatOfInvi(DateTime date, string session, string venueID, string staffID)
        {
            int n = 0;
            string catOfInvi = "In-charge";
            try
            {
                /*Step 2: Create Sql Insert statement and Sql Insert Object*/
                strUpdate = "Update InvigilationDuty Set CatOfInvi = @CatOfInvi Where TimeslotID = @TimeslotID and VenueID = @VenueID and StaffID = @StaffID";
                cmdUpdate = new SqlCommand(strUpdate, conn);

                cmdUpdate.Parameters.AddWithValue("@CatOfInvi", catOfInvi);
                cmdUpdate.Parameters.AddWithValue("@TimeslotID", session + date.ToString("ddMMyy"));
                cmdUpdate.Parameters.AddWithValue("@VenueID", venueID);
                cmdUpdate.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to insert*/
                n = cmdUpdate.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            return n;
        }

        public int changeLocationOfReliefInvi(DateTime date, string session, string staffID, string location, bool isQuarantineInviForEastCampusAssigned)
        {
            int n = 0;
            try
            {
                if (!isQuarantineInviForEastCampusAssigned)
                {
                    strUpdate = "Update Staff Set NoAsReliefInvi = NoAsReliefInvi - 1, NoAsQuarantineInvi = NoAsQuarantineInvi + 1 Where StaffID = @StaffID";
                    cmdUpdate = new SqlCommand(strUpdate, conn);
                    cmdUpdate.Parameters.AddWithValue("@StaffID", staffID);
                    n = cmdUpdate.ExecuteNonQuery();
                }

                /*Step 2: Create Sql update statement and Sql update Object*/
                strUpdate = "Update InvigilationDuty Set Location = @Location Where TimeslotID = @TimeslotID and StaffID = @StaffID";
                cmdUpdate = new SqlCommand(strUpdate, conn);

                cmdUpdate.Parameters.AddWithValue("@TimeslotID", session + date.ToString("ddMMyy"));
                cmdUpdate.Parameters.AddWithValue("@StaffID", staffID);
                cmdUpdate.Parameters.AddWithValue("@Location", location);

                /*Step 3: Execute command to insert*/
                n = cmdUpdate.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            return n;
        }

        public int updateNoOfSatSession(string staffID)
        {
            int n = 0;
            try
            {
                /*Step 2: Create Sql Insert statement and Sql Insert Object*/
                strUpdate = "Update Staff Set NoOfSatSession = NoOfSatSession + 1 Where StaffID = @StaffID";
                cmdUpdate = new SqlCommand(strUpdate, conn);

                cmdUpdate.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to insert*/
                n = cmdUpdate.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            return n;
        }

        public int updateNoAsQuarantineInvi(string staffID)
        {
            int n = 0;
            try
            {
                /*Step 2: Create Sql Insert statement and Sql Insert Object*/
                strUpdate = "Update Staff Set NoAsQuarantineInvi = NoAsQuarantineInvi + 1 Where StaffID = @StaffID";
                cmdUpdate = new SqlCommand(strUpdate, conn);

                cmdUpdate.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to insert*/
                n = cmdUpdate.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            return n;
        }

        public int updateNoAsReliefInvi(string staffID)
        {
            int n = 0;
            try
            {
                /*Step 2: Create Sql Insert statement and Sql Insert Object*/
                strUpdate = "Update Staff Set NoAsReliefInvi = NoAsReliefInvi + 1 Where StaffID = @StaffID";
                cmdUpdate = new SqlCommand(strUpdate, conn);

                cmdUpdate.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to insert*/
                n = cmdUpdate.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            return n;
        }

        public int updateNoOfExtraSession(string staffID)
        {
            int n = 0;
            try
            {
                /*Step 2: Create Sql Insert statement and Sql Insert Object*/
                strUpdate = "Update Staff Set NoOfExtraSession = NoOfExtraSession + 1 Where StaffID = @StaffID";
                cmdUpdate = new SqlCommand(strUpdate, conn);

                cmdUpdate.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to insert*/
                n = cmdUpdate.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            return n;
        }

        public int clearInvigilationDuty()
        {
            int n = 0;

            try
            {
                strDelete = "Delete from InvigilationDuty";
                cmdUpdate = new SqlCommand(strDelete, conn);

                n = cmdUpdate.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }

            return n;
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