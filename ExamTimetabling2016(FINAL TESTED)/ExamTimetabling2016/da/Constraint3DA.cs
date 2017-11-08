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
        private SqlCommand cmdSelect, cmdSearch, cmdInsert;
        private string strSelect, strSearch, strInsert;

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
                string strInsert = "INSERT INTO Constraints(IsHardConstraint,IsCnblPaper,IsDoubleSeating,MinExperiencedInvigilatorPerVenue,ConstraintImportanceValue,HasOtherDutyOnSameDay,HasSpecificSessionDutyOnSameDay, HasSpecificDurationDutyOnSameDay,HasSpecificSessionAndDurationDutyOnSameDay,HasSpecificSessionString,HasSpecificDurationInt,DayOfWeek, StaffIsMuslim, StaffFacultyCode, StaffIsInviAbove2Years, StaffIsChiefInvi, StaffIsTakingSTSPhd, StaffTypeOfEmploy, ExamFacultyCode, ExamPaperType, CategoryOfInvigilationDuty , DurationOfInvigilationDuty , LocationOfInvigilationDuty, SessionOfInvigilationDuty, Remark) VALUES(@IsHardConstraint, @IsCnblPaper, @IsDoubleSeating,@MinExperiencedInvigilatorPerVenue, @ConstraintImportanceValue ,@HasOtherDutyOnSameDay, @HasSpecificSessionDutyOnSameDay, @HasSpecificDurationDutyOnSameDay ,@HasSpecificSessionAndDurationDutyOnSameDay, @HasSpecificSessionString,@HasSpecificDurationInt, @DayOfWeek, @StaffIsMuslim, @StaffFacultyCode,@StaffIsInviAbove2Years, @StaffIsChiefInvi, @StaffIsTakingSTSPhd, @StaffTypeOfEmploy, @ExamFacultyCode, @ExamPaperType , @CategoryOfInvigilationDuty , @DurationOfInvigilationDuty , @LocationOfInvigilationDuty, @SessionOfInvigilationDuty, @Remark)";
                {
                    cmdInsert = new SqlCommand(strInsert, conn);

                    char? isHardConstraint = convertToChar(constraint.IsHardConstraint);
                    char? isCnblPaper = convertToChar(constraint.IsCnblPaper);
                    char? isDoubleSeating = convertToChar(constraint.IsDoubleSeating);
                    char? hasOtherDutyOnSameDay = convertToChar(constraint.HasOtherDutyOnSameDay);
                    char? hasSpecificSessionDutyOnSameDay = convertToChar(constraint.HasSpecificSessionDutyOnSameDay);
                    char? hasSpecificDurationDutyOnSameDay = convertToChar(constraint.HasSpecificDurationDutyOnSameDay);
                    char? hasSpecificSessionAndDurationDutyOnSameDay = convertToChar(constraint.HasSpecificSessionAndDurationDutyOnSameDay);
                    //char? assignToExaminer = convertToChar(constraint.AssignExaminerToPaper);
                    char? isMuslim = convertToChar(constraint.Invigilator.IsMuslim);
                    char? isChiefInvi = convertToChar(constraint.Invigilator.IsChiefInvi);
                    char? isTakingSTSPhd = convertToChar(constraint.Invigilator.IsTakingSTSPhD);
                    char? isInviAbove2Years = convertToChar(constraint.Invigilator.IsInviAbove2Years);

                    if (isHardConstraint.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@IsHardConstraint", DBNull.Value);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@IsHardConstraint", isHardConstraint);
                    }

                    if (!isCnblPaper.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@IsCnblPaper", isCnblPaper);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@IsCnblPaper", DBNull.Value);
                    }

                    if (!isDoubleSeating.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@IsDoubleSeating", isDoubleSeating);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@IsDoubleSeating", DBNull.Value);
                    }

                    /*
                    if (!assignToExaminer.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@AssignExaminerToPaper", assignToExaminer);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@AssignExaminerToPaper", DBNull.Value);
                    }*/

                    cmdInsert.Parameters.AddWithValue("@ConstraintImportanceValue", constraint.ConstraintImportanceValue);
                    if (!hasOtherDutyOnSameDay.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@HasOtherDutyOnSameDay", hasOtherDutyOnSameDay);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@HasOtherDutyOnSameDay", DBNull.Value);
                    }

                    if (!hasSpecificSessionDutyOnSameDay.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@HasSpecificSessionDutyOnSameDay", hasSpecificSessionDutyOnSameDay);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@HasSpecificSessionDutyOnSameDay", DBNull.Value);
                    }

                    if (!hasSpecificDurationDutyOnSameDay.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@HasSpecificDurationDutyOnSameDay", hasSpecificDurationDutyOnSameDay);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@HasSpecificDurationDutyOnSameDay", DBNull.Value);
                    }

                    if (!hasSpecificSessionAndDurationDutyOnSameDay.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@HasSpecificSessionAndDurationDutyOnSameDay", hasSpecificSessionAndDurationDutyOnSameDay);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@HasSpecificSessionAndDurationDutyOnSameDay", DBNull.Value);
                    }

                    if (!constraint.HasSpecificSessionDutyOnSameDayString.Equals(null) && !constraint.HasSpecificSessionDutyOnSameDayString.Equals(""))
                    {
                        cmdInsert.Parameters.AddWithValue("@HasSpecificSessionString", constraint.HasSpecificSessionDutyOnSameDayString);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@HasSpecificSessionString", DBNull.Value);
                    }

                    cmdInsert.Parameters.AddWithValue("@HasSpecificDurationInt", constraint.HasSpecificDurationDutyOnSameDayInt);

                    if (!constraint.DayOfWeek.Equals(""))
                    {
                        cmdInsert.Parameters.AddWithValue("@DayOfWeek", constraint.DayOfWeek);
                    }
                    else
                    {

                        cmdInsert.Parameters.AddWithValue("@DayOfWeek", DBNull.Value);
                    }

                    if (!isMuslim.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffIsMuslim", isMuslim);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffIsMuslim", DBNull.Value);
                    }

                    if (!isChiefInvi.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffIsChiefInvi", isChiefInvi);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffIsChiefInvi", DBNull.Value);
                    }

                    if (constraint.Invigilator.FacultyCode != '\0')
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffFacultyCode", constraint.Invigilator.FacultyCode);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffFacultyCode", DBNull.Value);
                    }
                    if (!isInviAbove2Years.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffIsInviAbove2Years", isInviAbove2Years);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffIsInviAbove2Years", DBNull.Value);
                    }

                    if (!isTakingSTSPhd.Equals(null))
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffIsTakingSTSPhd", isTakingSTSPhd);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffIsTakingSTSPhd", DBNull.Value);
                    }

                    if (constraint.Invigilator.TypeOfEmploy != '\0')
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffTypeOfEmploy", constraint.Invigilator.TypeOfEmploy);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@StaffTypeOfEmploy", DBNull.Value);
                    }

                    if (!constraint.Examination.Faculty.FacultyCode.Equals('\0'))
                    {
                        cmdInsert.Parameters.AddWithValue("@ExamFacultyCode", constraint.Examination.Faculty.FacultyCode);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@ExamFacultyCode", DBNull.Value);
                    }

                    cmdInsert.Parameters.AddWithValue("@MinExperiencedInvigilatorPerVenue", constraint.MinExperiencedInvigilator);

                    if (constraint.Examination.PaperType != null && constraint.Examination.PaperType != '\0')
                    {
                        cmdInsert.Parameters.AddWithValue("@ExamPaperType", constraint.Examination.PaperType);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@ExamPaperType", DBNull.Value);
                    }

                    if (constraint.InvigilationDuty.CategoryOfInvigilator != null && constraint.InvigilationDuty.CategoryOfInvigilator != "")
                    {
                        cmdInsert.Parameters.AddWithValue("@CategoryOfInvigilationDuty", constraint.InvigilationDuty.CategoryOfInvigilator);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@CategoryOfInvigilationDuty", DBNull.Value);
                    }

                    cmdInsert.Parameters.AddWithValue("@DurationOfInvigilationDuty", constraint.InvigilationDuty.Duration);

                    if (constraint.InvigilationDuty.Location != null && constraint.InvigilationDuty.Location != "")
                    {
                        cmdInsert.Parameters.AddWithValue("@LocationOfInvigilationDuty", constraint.InvigilationDuty.Location);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@LocationOfInvigilationDuty", DBNull.Value);
                    }
                    if (constraint.InvigilationDuty.Session != null && constraint.InvigilationDuty.Session != "")
                    {
                        cmdInsert.Parameters.AddWithValue("@SessionOfInvigilationDuty", constraint.InvigilationDuty.Session);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@SessionOfInvigilationDuty", DBNull.Value);
                    }
                    if(constraint.Remark!= "" && constraint.Remark!= null)
                    {
                        cmdInsert.Parameters.AddWithValue("@Remark", constraint.Remark);
                    }
                    else
                    {
                        cmdInsert.Parameters.AddWithValue("@Remark", DBNull.Value);
                    }

                    cmdInsert.ExecuteNonQuery();

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Constraint3> loadFullConstraintList()
        {
            List<Constraint3> constraintList = new List<Constraint3>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select * from dbo.constraints";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Constraint3 dbConstraint = new Constraint3();
                        dbConstraint.ConstraintID = Convert.ToInt16(dtr["constraintID"]);
                        if (!(dtr["IsHardConstraint"]).Equals(DBNull.Value))
                        {
                            dbConstraint.IsHardConstraint = convertToBool(Convert.ToChar(dtr["IsHardConstraint"]));
                        }
                        if (!(dtr["IsCnblPaper"]).Equals(DBNull.Value))
                        {
                            dbConstraint.IsCnblPaper = convertToBool(Convert.ToChar(dtr["IsCnblPaper"]));
                        }
                        if (!(dtr["IsDoubleSeating"]).Equals(DBNull.Value))
                        {
                            dbConstraint.IsDoubleSeating = convertToBool(Convert.ToChar(dtr["IsDoubleSeating"]));
                        }
                        if (!(dtr["MinExperiencedInvigilatorPerVenue"]).Equals(DBNull.Value))
                        {
                            dbConstraint.MinExperiencedInvigilator = Convert.ToInt16(dtr["MinExperiencedInvigilatorPerVenue"]);
                        }
                        if (!(dtr["ConstraintImportanceValue"]).Equals(DBNull.Value))
                        {
                            dbConstraint.ConstraintImportanceValue = Convert.ToInt16(dtr["ConstraintImportanceValue"]);
                        }
                        if (!(dtr["HasOtherDutyOnSameDay"]).Equals(DBNull.Value))
                        {
                            dbConstraint.HasOtherDutyOnSameDay = convertToBool(Convert.ToChar(dtr["HasOtherDutyOnSameDay"]));
                        }
                        if (!(dtr["HasSpecificSessionDutyOnSameDay"]).Equals(DBNull.Value))
                        {
                            dbConstraint.HasSpecificSessionDutyOnSameDay = convertToBool(Convert.ToChar(dtr["HasSpecificSessionDutyOnSameDay"]));
                        }
                        if (!(dtr["HasSpecificDurationDutyOnSameDay"]).Equals(DBNull.Value))
                        {
                            dbConstraint.HasSpecificDurationDutyOnSameDay = convertToBool(Convert.ToChar(dtr["HasSpecificDurationDutyOnSameDay"]));
                        }
                        if (!(dtr["HasSpecificSessionAndDurationDutyOnSameDay"]).Equals(DBNull.Value))
                        {
                            dbConstraint.HasSpecificSessionAndDurationDutyOnSameDay = convertToBool(Convert.ToChar(dtr["HasSpecificSessionAndDurationDutyOnSameDay"]));
                        }
                        if (!(dtr["HasSpecificSessionString"]).Equals(DBNull.Value))
                        {
                            dbConstraint.HasSpecificSessionDutyOnSameDayString = dtr["HasSpecificSessionString"].ToString();
                        }
                        if (!(dtr["HasSpecificDurationInt"]).Equals(DBNull.Value))
                        {
                            dbConstraint.HasSpecificDurationDutyOnSameDayInt = Convert.ToInt16(dtr["HasSpecificDurationInt"]);
                        }
                        if (!(dtr["DayOfWeek"]).Equals(DBNull.Value))
                        {
                            dbConstraint.DayOfWeek = dtr["DayOfWeek"].ToString();
                        }
                        if (!(dtr["StaffIsMuslim"]).Equals(DBNull.Value))
                        {
                            dbConstraint.Invigilator.IsMuslim = convertToBool(Convert.ToChar(dtr["StaffIsMuslim"]));
                        }
                        if (!(dtr["StaffFacultyCode"]).Equals(DBNull.Value))
                        {
                            dbConstraint.Invigilator.FacultyCode = Convert.ToChar(dtr["StaffFacultyCode"]);
                        }
                        if (!(dtr["StaffIsInviAbove2Years"]).Equals(DBNull.Value))
                        {
                            dbConstraint.Invigilator.IsInviAbove2Years = convertToBool(Convert.ToChar(dtr["StaffIsInviAbove2Years"]));
                        }
                        if (!(dtr["StaffIsChiefInvi"]).Equals(DBNull.Value))
                        {
                            dbConstraint.Invigilator.IsChiefInvi = convertToBool(Convert.ToChar(dtr["StaffIsChiefInvi"]));
                        }
                        if (!(dtr["StaffIsTakingSTSPhd"]).Equals(DBNull.Value))
                        {
                            dbConstraint.Invigilator.IsTakingSTSPhD = convertToBool(Convert.ToChar(dtr["StaffIsTakingSTSPhd"]));
                        }
                        if (!(dtr["StaffTypeOfEmploy"]).Equals(DBNull.Value))
                        {
                            dbConstraint.Invigilator.TypeOfEmploy = Convert.ToChar(dtr["StaffTypeOfEmploy"]);
                        }
                        if (!(dtr["ExamFacultyCode"]).Equals(DBNull.Value))
                        {
                            dbConstraint.Examination.Faculty.FacultyCode = Convert.ToChar(dtr["ExamFacultyCode"]);
                        }
                        if (!(dtr["ExamPaperType"]).Equals(DBNull.Value))
                        {
                            dbConstraint.Examination.PaperType = Convert.ToChar(dtr["ExamPaperType"]);
                        }
                        if (!(dtr["CategoryOfInvigilationDuty"]).Equals(DBNull.Value))
                        {
                            dbConstraint.InvigilationDuty.CategoryOfInvigilator = Convert.ToString(dtr["CategoryOfInvigilationDuty"]);
                        }
                        if (!(dtr["DurationOfInvigilationDuty"]).Equals(DBNull.Value))
                        {
                            dbConstraint.InvigilationDuty.Duration = Convert.ToInt16(dtr["DurationOfInvigilationDuty"]);
                        }
                        if (!(dtr["LocationOfInvigilationDuty"]).Equals(DBNull.Value))
                        {
                            dbConstraint.InvigilationDuty.Location = Convert.ToString(dtr["LocationOfInvigilationDuty"]);
                        }
                        if (!(dtr["SessionOfInvigilationDuty"]).Equals(DBNull.Value))
                        {
                            dbConstraint.InvigilationDuty.Session = Convert.ToString(dtr["SessionOfInvigilationDuty"]);
                        }
                        if (!(dtr["Remark"]).Equals(DBNull.Value))
                        {
                            dbConstraint.Remark = Convert.ToString(dtr["Remark"]);
                        }

                        constraintList.Add(dbConstraint);
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return constraintList;
        }


        public char? convertToChar(bool? statement)
        {
            char? result = null;
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

        public bool? convertToBool(char character)
        {
            bool? result = null;

            if (character.Equals('Y'))
            {
                result = true;
            }
            else if (character.Equals('N'))
            {
                result = false;
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