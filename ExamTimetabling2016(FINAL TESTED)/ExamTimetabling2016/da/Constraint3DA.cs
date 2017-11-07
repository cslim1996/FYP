using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class Constraint3DA
    {

        private SqlConnection conn;
        private string connectionstring = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch,cmdInsert;
        private string strSelect, strSearch,strInsert;

        public Constraint3DA()
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

        public void insertConstraintIntoDatabase(Constraint3 constraint)
        {
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                string strInsert = "INSERT INTO constraint (IsHardConstraint,IsCnblPaper,IsDoubleSeating,MinExtraSession,MaxExtraSession,MinReliefSession,MaxReliefSession,MinExperiencedInvigilatorPerVenue,ConstraintImportanceValue,HasOtherDutyOnSameDay,HasSpecificSessionDutyOnSameDay, HasSpecificDurationDutyOnSameDay,HasSpecificSessionAndDurationDutyOnSameDay,HasSpecificSessionString,HasSpecificDurationInt,DayOfWeek, AssignExaminerToPaper, StaffIsMuslim, StaffFacultyCode, StaffIsInviAbove2Years, StaffIsTakingSTSPhd, StaffTypeOfEmploy, ExamFacultyCode, ExamPaperType, CategoryOfInvigilationDuty , DurationOfInvigilationDuty , LocationOfInvigilationDuty, SessionOfInvigilationDuty) VALUES (@IsHardConstraint, @IsCnblPaper, @IsDoubleSeating, @MinExtraSession,@MaxExtraSession, @MinReliefSession,@MaxReliefSession, @MinExperiencedInvigilatorPerVenue, @ConstraintImportanceValue ,@HasOtherDutyOnSameDay, @HasSpecificSessionDutyOnSameDay, @HasSpecificDurationDutyOnSameDay ,@HasSpecificSessionAndDurationDutyOnSameDay, @HasSpecificSessionString,@HasSpecificDurationInt, @DayOfWeek , @AssignExaminerToPaper , @StaffIsMuslim, @StaffFacultyCode,@StaffIsInviAbove2Years, @StaffIsTakingSTSPhd, @StaffTypeOfEmploy, @ExamFacultyCode, @ExamPaperType , @CategoryOfInvigilationDuty , @DurationOfInvigilationDuty , @LocationOfInvigilationDuty, @SessionOfInvigilationDuty)";
                {
                    cmdInsert = new SqlCommand(strInsert, conn);
                    cmdInsert.Parameters.AddWithValue("@IsHardConstraint", constraint.IsHardConstraint);//
                    cmdInsert.Parameters.AddWithValue("@IsCnblPaper",constraint.IsCnblPaper);//
                    cmdInsert.Parameters.AddWithValue("@IsDoubleSeating",constraint.IsDoubleSeating);//
                    cmdInsert.Parameters.AddWithValue("@MinExtraSession",constraint.MinExtraSession );
                    cmdInsert.Parameters.AddWithValue("@MaxExtraSession",constraint.MaxExtraSession );
                    cmdInsert.Parameters.AddWithValue("@MinReliefSession",constraint.MinReliefSession );
                    cmdInsert.Parameters.AddWithValue("@MaxReliefSession",constraint.MinReliefSession );
                    cmdInsert.Parameters.AddWithValue("@MinExperiencedInvigilatorPerVenue", constraint.MinExperiencedInvigilator);
                    cmdInsert.Parameters.AddWithValue("@ConstraintImportanceValue", constraint.ConstraintImportanceValue );
                    cmdInsert.Parameters.AddWithValue("@HasOtherDutyOnSameDay", constraint.HasOtherDutyOnSameDay);//
                    cmdInsert.Parameters.AddWithValue("@HasSpecificSessionDutyOnSameDay", constraint.HasSpecificSessionDutyOnSameDay);//
                    cmdInsert.Parameters.AddWithValue("@HasSpecificDurationDutyOnSameDay", constraint.HasSpecificDurationDutyOnSameDay);//
                    cmdInsert.Parameters.AddWithValue("@HasSpecificSessionAndDurationDutyOnSameDay",constraint.HasSpecificSessionAndDurationDutyOnSameDay);//
                    cmdInsert.Parameters.AddWithValue("@HasSpecificSessionString",constraint.HasSpecificSessionDutyOnSameDayString);
                    cmdInsert.Parameters.AddWithValue("@HasSpecificDurationInt",constraint.HasSpecificDurationDutyOnSameDayInt);
                    cmdInsert.Parameters.AddWithValue("@DayOfWeek", constraint.DayOfWeek);
                    cmdInsert.Parameters.AddWithValue("@AssignExaminerToPaper",constraint.AssignExaminerToPaper);//
                    cmdInsert.Parameters.AddWithValue("@StaffIsMuslim", constraint.Invigilator.IsMuslim );
                    cmdInsert.Parameters.AddWithValue("@StaffFacultyCode",constraint.Invigilator.FacultyCode );
                    cmdInsert.Parameters.AddWithValue("@StaffIsInviAbove2Years", constraint.Invigilator.IsInviAbove2Years);
                    cmdInsert.Parameters.AddWithValue("@StaffIsTakingSTSPhd", constraint.Invigilator.IsTakingSTSPhD );
                    cmdInsert.Parameters.AddWithValue("@StaffTypeOfEmploy", constraint.Invigilator.TypeOfEmploy );
                    cmdInsert.Parameters.AddWithValue("@ExamFacultyCode", constraint.Examination.Faculty.FacultyCode );
                    cmdInsert.Parameters.AddWithValue("@ExamPaperType", constraint.Examination.PaperType);
                    cmdInsert.Parameters.AddWithValue("@CategoryOfInvigilationDuty",constraint.InvigilationDuty.CategoryOfInvigilator );
                    cmdInsert.Parameters.AddWithValue("@DurationOfInvigilationDuty", constraint.InvigilationDuty.Duration);
                    cmdInsert.Parameters.AddWithValue("@LocationOfInvigilationDuty", constraint.InvigilationDuty.Location);
                    cmdInsert.Parameters.AddWithValue("@SessionOfInvigilationDuty", constraint.InvigilationDuty.Session);
                    cmdInsert.ExecuteNonQuery();

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Constraint3> getAllConstraint()
        {
            List<Constraint3> constraintList = new List<Constraint3>();
            try
            {

                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "select * from constraint";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        if (!dtr[""].Equals(DBNull.Value)); 
                    }
                }
                dtr.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return constraintList;
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