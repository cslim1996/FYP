using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class ConstraintSettingDA
    {
        private SqlConnection conn;
        private string connectionstring = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch,cmdInsert,cmdUpdate;
        private string strSelect, strSearch, strInsert,strUpdate;

        public ConstraintSettingDA()
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

        public ConstraintSetting readSettingFromDatabase()
        {
            ConstraintSetting setting = new ConstraintSetting();

            /*Step 2: Create Sql Search statement and Sql Search Object*/
            strSearch = "Select * from dbo.ConstraintSetting where settingID = @SettingID";
            cmdSearch = new SqlCommand(strSearch, conn);

            cmdSearch.Parameters.AddWithValue("@SettingID", 1);
            /*Step 3: Execute command to retrieve data*/
            SqlDataReader dtr = cmdSearch.ExecuteReader();

            if (dtr.Read())
            {
                /*Step 4: Get result set from the query*/
                bool AssignToExaminerBool = convertToBool(Convert.ToChar(dtr["AssignToExaminer"]));
                setting.AssignToExaminer = AssignToExaminerBool;
                setting.MaxEveningSession = Convert.ToInt16(dtr["MaxEveningSession"]);
                setting.MaxExtraSession = Convert.ToInt16(dtr["MaxExtraSession"]);
                setting.MaxReliefSession = Convert.ToInt16(dtr["MaxReliefSession"]);
                setting.MaxSaturdaySession = Convert.ToInt16(dtr["MaxSaturdaySession"]);
                dtr.Close();
            }

            return setting;
        }

        // can be used for save and update setting
        public void saveIntoDatabase(ConstraintSetting setting)
        {
            int count = 0;
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select Count(*) from dbo.ConstraintSetting";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                count = (int)cmdSearch.ExecuteScalar();
                
            }
            catch (SqlException ex)
            {
                throw;
            }

            if (count != 0)//update if already exist
            {
                try
                {
                    strUpdate = "update ConstraintSetting set AssignToExaminer = @AssignToExaminer, MaxExtraSession = @MaxExtraSession, MaxSaturdaySession = @MaxSaturdaySession, MaxEveningSession = @MaxEveningSession where SettingID = 1";
                    cmdUpdate = new SqlCommand(strUpdate, conn);

                    cmdUpdate.Parameters.AddWithValue("@AssignToExaminer", convertToChar(setting.AssignToExaminer));
                    cmdUpdate.Parameters.AddWithValue("@MaxExtraSession", setting.MaxExtraSession);
                    cmdUpdate.Parameters.AddWithValue("@MaxReliefSession", setting.MaxReliefSession);
                    cmdUpdate.Parameters.AddWithValue("@MaxSaturdaySession", setting.MaxSaturdaySession);
                    cmdUpdate.Parameters.AddWithValue("@MaxEveningSession", setting.MaxEveningSession);

                    cmdUpdate.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw;
                }

            }
            else//insert
            {
                try
                {
                    /*Step 2: Create Sql Search statement and Sql Search Object*/
                    string strInsert = "INSERT INTO ConstraintSetting(AssignToExaminer,MaxExtraSession,MaxReliefSession,MaxSaturdaySession,MaxEveningSession) VALUES (@AssignToExaminer,@MaxExtraSession,@MaxReliefSession,@MaxSaturdaySession,@MaxEveningSession)";
                    {
                        cmdInsert = new SqlCommand(strInsert, conn);

                        cmdInsert.Parameters.AddWithValue("@AssignToExaminer", convertToChar(setting.AssignToExaminer));
                        cmdInsert.Parameters.AddWithValue("@MaxExtraSession", setting.MaxExtraSession);
                        cmdInsert.Parameters.AddWithValue("@MaxReliefSession", setting.MaxReliefSession);
                        cmdInsert.Parameters.AddWithValue("@MaxSaturdaySession", setting.MaxSaturdaySession);
                        cmdInsert.Parameters.AddWithValue("@MaxEveningSession", setting.MaxEveningSession);

                        cmdInsert.ExecuteNonQuery();
                    }
                }
                catch(SqlException)
                {
                    throw;
                }
            }

        }
        
        public char convertToChar(bool statement)
        {
            char result = '\0';
            if (statement.Equals(true))
            {
                result = 'Y';
            }
            else if (statement.Equals(false))
            {
                result = 'N';
            }
            return result;
        }

        public bool convertToBool(char character)
        {
            bool result = false;

            if (character.Equals('Y'))
            {
                result = true;
            }

            return result;
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