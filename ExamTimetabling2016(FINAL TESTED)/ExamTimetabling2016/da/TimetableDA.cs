using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace ExamTimetabling2016
{
    class TimetableDA
    {
        private SqlConnection conn;
        private string connectionString = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch, cmdInsert, cmdUpdate, cmdDelete;
        private string strSelect, strSearch, strInsert, strUpdate, strDelete;

        public TimetableDA()
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

        public List<Timetable> selectTimetable()
        {
            List<Timetable> examTimetable = new List<Timetable>();
            Timetable timetable = new Timetable();
            try
            {
                //clear invigilation duty table
                strDelete = "Delete from InvigilationDuty";
                cmdDelete = new SqlCommand(strDelete, conn);
                int rows = cmdDelete.ExecuteNonQuery();

                //select timetable
                strSelect = "Select E.TimeslotID, T.Date, T.Session From dbo.Timeslot T, dbo.Examination E where T.TimeslotID = E.TimeslotID Group By E.TimeslotID, T.Date, T.Session";
                cmdSelect = new SqlCommand(strSelect, conn);
                SqlDataReader dtr = cmdSelect.ExecuteReader();

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
                        timetable = new Timetable(DateTime.Parse(dtr["Date"].ToString()), dtr["Session"].ToString(), new List<Block>(), maintainStaffControl.searchReliefInvigilators(DateTime.Parse(dtr["Date"].ToString()), dtr["Session"].ToString()));
                        maintainStaffControl.shutDown();
                        MaintainBlockControl maintainBlockControl = new MaintainBlockControl();
                        timetable.BlocksList = maintainBlockControl.searchBlocksList(DateTime.Parse(dtr["Date"].ToString()), dtr["Session"].ToString());
                        maintainBlockControl.shutDown();
                        examTimetable.Add(timetable);
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return examTimetable;
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
            TimetableDA da = new TimetableDA();
        }
    }
}
