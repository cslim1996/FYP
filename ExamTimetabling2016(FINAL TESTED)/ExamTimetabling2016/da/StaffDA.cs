using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class StaffDA
    {
        private SqlConnection conn;
        private string connectionstring = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch;
        private string strSelect, strSearch;

        public StaffDA()
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

        public Staff getStaffName(string staffID)
        {
            Staff staff = null;

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select Title, Name from dbo.Staff where StaffID = @StaffID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        staff = new Staff(dtr["Title"].ToString(), dtr["Name"].ToString());
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return staff;
        }

        public List<Staff> getStaffList()
        {
            List<Staff> staffList = new List<Staff>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select StaffID, Title, Name, FacultyCode, isChiefInvi, isInvi from dbo.Staff order by Name";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Staff staff = new Staff(dtr["StaffID"].ToString(), dtr["Title"].ToString(), dtr["Name"].ToString(), Convert.ToChar(dtr["FacultyCode"].ToString()), Convert.ToChar(dtr["isChiefInvi"].ToString()), Convert.ToChar(dtr["isInvi"].ToString()));
                        staffList.Add(staff);
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return staffList;
        }

        //unused for now
        public List<Staff> getInvigilatorList()
        {
            List<Staff> staffList = new List<Staff>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select * from dbo.Staff where isInvi = 'Y'";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        bool? isTakingSTSPhD = null;
                        bool? isMuslim= null;
                        bool? isChiefInvi = null;
                        bool? isInviAbove2years = null;
                        //convert isMuslim
                        if (Convert.ToChar(dtr["isMuslim"]).Equals('Y'))
                            isMuslim = true;
                        else if (Convert.ToChar(dtr["isMuslim"]).Equals('N'))
                            isMuslim = false;

                        if (Convert.ToChar(dtr["isTakingSTSPhD"]).Equals('Y'))
                            isTakingSTSPhD = true;
                        else if (Convert.ToChar(dtr["isTakingSTSPhD"]).Equals('N'))
                            isTakingSTSPhD = false;

                        if (Convert.ToChar(dtr["isChiefInvi"]).Equals('Y'))
                            isChiefInvi = true;
                        else if (Convert.ToChar(dtr["isChiefInvi"]).Equals('N'))
                            isChiefInvi = false;

                        if (Convert.ToChar(dtr["isInviAbove2Years"]).Equals('Y'))
                            isInviAbove2years = true;
                        else if (Convert.ToChar(dtr["isInviAbove2Years"]).Equals('N'))
                            isInviAbove2years = false;

                        Staff staff = new Staff(dtr["StaffID"].ToString(),isMuslim,isTakingSTSPhD,
                            Convert.ToChar(dtr["typeOfEmploy"]),Convert.ToInt16(dtr["NoOfSatSession"]),
                            Convert.ToInt16(dtr["noAsReliefInvi"]),Convert.ToInt16(dtr["NoOfExtraSession"]),
                            isChiefInvi,isInviAbove2years,Convert.ToChar(dtr["Gender"]));

                        staff.IsChief = Convert.ToChar(dtr["isChiefInvi"]);


                        staffList.Add(staff);
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return staffList;
        }

        public List<Staff> searchLecturer(string input, string criteria)
        {
            List<Staff> staffList = new List<Staff>();
            List<InvigilationDuty> invigilationDutiesList = new List<InvigilationDuty>();
            string strSearch;
            SqlCommand cmdSearch;

            try
            {
                if (criteria.Equals("faculty"))
                {
                    /*Step 2: Create Sql Search statement and Sql Search Object*/
                        strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S Where S.typeOfEmploy = 'F' and S.typeOfEmploy = 'F' and S.faculty = @Faculty";
                    cmdSearch = new SqlCommand(strSearch, conn);

                    cmdSearch.Parameters.AddWithValue("@Faculty", input);
                }
                else if (criteria.Equals("examAsInvi"))
                {
                    strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, dbo.PaperExamined P Where S.typeOfEmploy = 'F' and P.CourseCode = @CourseCode And S.StaffID = P.StaffID And S.isExam = 'Y' And (S.isInvi = 'Y' or S.isInviAbove2Years = 'Y') And isChiefInvi = 'N'";
                    cmdSearch = new SqlCommand(strSearch, conn);

                    cmdSearch.Parameters.AddWithValue("@CourseCode", input);
                }
                else if (criteria.Equals("examAsChief"))
                {
                    strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, dbo.PaperExamined P Where S.typeOfEmploy = 'F' and P.CourseCode = @CourseCode And S.StaffID = P.StaffID And S.isExam = 'Y' And isChiefInvi = 'Y'";
                    cmdSearch = new SqlCommand(strSearch, conn);

                    cmdSearch.Parameters.AddWithValue("@CourseCode", input);
                }
                else if (criteria.Equals("categoryOfInvigilator") && input.Equals("Chief"))
                {
                    strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S Where S.typeOfEmploy = 'F' and S.isChiefInvi = 'Y'";
                    cmdSearch = new SqlCommand(strSearch, conn);
                }
                else if (criteria.Equals("categoryOfInvigilator") && input.Equals("InviAbove2Years"))
                {
                    strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S Where S.typeOfEmploy = 'F' and S.isInviAbove2Years = 'Y' and isChiefInvi = 'N'";
                    cmdSearch = new SqlCommand(strSearch, conn);
                }
                else if (criteria.Equals("categoryOfInvigilator") && input.Equals("Invigilator"))
                {
                    strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S Where S.typeOfEmploy = 'F' and (isInvi = 'Y' or S.isInviAbove2Years = 'Y') and isChiefInvi = 'N'";
                    cmdSearch = new SqlCommand(strSearch, conn);
                }
                else if (criteria.Equals("isMuslim") && input.Equals("Muslim"))
                {
                    strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S Where S.typeOfEmploy = 'F' and (isInvi = 'Y' or S.isInviAbove2Years = 'Y') and isChiefInvi = 'N' and S.isMuslim = 'Y'";
                    cmdSearch = new SqlCommand(strSearch, conn);
                }
                else
                {
                    strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S Where S.typeOfEmploy = 'F'";
                    cmdSearch = new SqlCommand(strSearch, conn);
                }

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string staffID = dtr["StaffID"].ToString();
                        MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
                        bool isMuslim = false;
                        bool isTakingSTSPhD = false;
                        bool isChiefInvi = false;
                        bool isInviAbove2Years = false;
                        if (dtr["isMuslim"].ToString().Equals("Y"))
                        {
                            isMuslim = true;
                        }
                        else
                        {
                            isMuslim = false;
                        }
                        if (dtr["isTakingSTSPhD"].ToString().Equals("Y"))
                        {
                            isTakingSTSPhD = true;
                        }
                        else
                        {
                            isTakingSTSPhD = false;
                        }
                        if (dtr["isChiefInvi"].ToString().Equals("Y"))
                        {
                            isChiefInvi = true;
                        }
                        else
                        {
                            isChiefInvi = false;
                        }
                        if (dtr["isInviAbove2Years"].ToString().Equals("Y"))
                        {
                            isInviAbove2Years = true;
                        }
                        else
                        {
                            isInviAbove2Years = false;
                        }
                        Staff staff = new Staff(dtr["name"].ToString(), char.Parse(dtr["gender"].ToString()), isMuslim, dtr["StaffID"].ToString(),
                                 dtr["title"].ToString(), dtr["position"].ToString(), dtr["facultyCode"].ToString(), dtr["department"].ToString(),
                                 isTakingSTSPhD, char.Parse(dtr["typeOfEmploy"].ToString()), int.Parse(dtr["NoOfSatSession"].ToString()),
                                 int.Parse(dtr["NoAsQuarantineInvi"].ToString()), int.Parse(dtr["NoAsReliefInvi"].ToString()), int.Parse(dtr["NoOfExtraSession"].ToString()),
                                 isChiefInvi, isInviAbove2Years, new List<string>(), new List<Exemption>(), maintainInvigilationDutyControl.searchInvigilationDuty(staffID));
                        maintainInvigilationDutyControl.shutDown();

                        MaintainExemptionControl maintainExemptionControl = new MaintainExemptionControl();
                        staff.ExemptionList = maintainExemptionControl.searchExemptionList(staffID);
                        maintainExemptionControl.shutDown();
                        staffList.Add(staff);
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return staffList;
        }

        public List<Staff> searchLecturer(DateTime date, string session, string faculty, string categoryOfInvigilator, bool isFacultyCheckOnly, double totalLoadOfDutyForEach)
        {
            List<Staff> staffList = new List<Staff>();
            List<InvigilationDuty> invigilationDutiesList = new List<InvigilationDuty>();
            string strSearch = "";
            SqlCommand cmdSearch;

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                if (!isFacultyCheckOnly)
                {
                    if (categoryOfInvigilator.Equals("InviAbove2Years"))
                    {
                        strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, dbo.InvigilationDuty I, dbo.Timeslot T Where S.typeOfEmploy = 'F' and S.isInviAbove2Years = 'Y' and isChiefInvi = 'N' and S.FacultyCode = @Faculty and T.Date = @Date And T.Session != @Session And T.TimeslotID = I.TimeslotID And I.StaffID = S.StaffID Group by S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years Having COUNT(InvigilationDutyID) < @TotalLoad";
                    }
                    else if (categoryOfInvigilator.Equals("Invigilators"))
                    {
                        strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, dbo.InvigilationDuty I, dbo.Timeslot T Where S.typeOfEmploy = 'F' and (isInvi = 'Y' or S.isInviAbove2Years = 'Y') and isChiefInvi = 'N' and S.FacultyCode = @Faculty and T.Date = @Date And T.Session != @Session And T.TimeslotID = I.TimeslotID And I.StaffID = S.StaffID Group by S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years Having COUNT(InvigilationDutyID) < @TotalLoad";
                    }
                    else if (categoryOfInvigilator.Equals("Chief"))
                    {
                        strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, dbo.InvigilationDuty I, dbo.Timeslot T Where S.typeOfEmploy = 'F' and S.isChiefInvi = 'Y' and S.FacultyCode = @Faculty and T.Date = @Date And T.Session != @Session And T.TimeslotID = I.TimeslotID And I.StaffID = S.StaffID Group by S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years Having COUNT(InvigilationDutyID) < @TotalLoad";
                    }
                }
                else
                {
                    if (categoryOfInvigilator.Equals("InviAbove2Years"))
                    {
                        strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, dbo.InvigilationDuty I, dbo.Timeslot T Where S.typeOfEmploy = 'F' and S.isInviAbove2Years = 'Y' and isChiefInvi = 'N' and S.FacultyCode = @Faculty and T.Date = @Date And T.Session = @Session And T.TimeslotID != I.TimeslotID And I.StaffID = S.StaffID Group by S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years Having COUNT(InvigilationDutyID) < @TotalLoad";
                    }
                    else if (categoryOfInvigilator.Equals("Invigilators"))
                    {
                        strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, dbo.InvigilationDuty I, dbo.Timeslot T Where S.typeOfEmploy = 'F' and (isInvi = 'Y' or S.isInviAbove2Years = 'Y') and isChiefInvi = 'N' and S.FacultyCode = @Faculty and T.Date = @Date And T.Session = @Session And T.TimeslotID != I.TimeslotID And I.StaffID = S.StaffID Group by S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years Having COUNT(InvigilationDutyID) < @TotalLoad";
                    }
                    else if (categoryOfInvigilator.Equals("Chief"))
                    {
                        strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, dbo.InvigilationDuty I, dbo.Timeslot T Where S.typeOfEmploy = 'F' and S.isChiefInvi = 'Y' and S.FacultyCode = @Faculty and T.Date = @Date And T.Session = @Session And T.TimeslotID != I.TimeslotID And I.StaffID = S.StaffID Group by S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years Having COUNT(InvigilationDutyID) < @TotalLoad";
                    }
                }

                cmdSearch = new SqlCommand(strSearch, conn);
                cmdSearch.Parameters.AddWithValue("@Faculty", faculty);
                cmdSearch.Parameters.AddWithValue("@Date", date);
                cmdSearch.Parameters.AddWithValue("@Session", session);
                cmdSearch.Parameters.AddWithValue("@TotalLoad", totalLoadOfDutyForEach);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string staffID = dtr["StaffID"].ToString();
                        MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
                        bool isMuslim = false;
                        bool isTakingSTSPhD = false;
                        bool isChiefInvi = false;
                        bool isInviAbove2Years = false;
                        if (dtr["isMuslim"].ToString().Equals("Y"))
                        {
                            isMuslim = true;
                        }
                        else
                        {
                            isMuslim = false;
                        }
                        if (dtr["isTakingSTSPhD"].ToString().Equals("Y"))
                        {
                            isTakingSTSPhD = true;
                        }
                        else
                        {
                            isTakingSTSPhD = false;
                        }
                        if (dtr["isChiefInvi"].ToString().Equals("Y"))
                        {
                            isChiefInvi = true;
                        }
                        else
                        {
                            isChiefInvi = false;
                        }
                        if (dtr["isInviAbove2Years"].ToString().Equals("Y"))
                        {
                            isInviAbove2Years = true;
                        }
                        else
                        {
                            isInviAbove2Years = false;
                        }
                        Staff staff = new Staff(dtr["name"].ToString(), char.Parse(dtr["gender"].ToString()), isMuslim, dtr["StaffID"].ToString(),
                                 dtr["title"].ToString(), dtr["position"].ToString(), dtr["facultyCode"].ToString(), dtr["department"].ToString(),
                                 isTakingSTSPhD, char.Parse(dtr["typeOfEmploy"].ToString()), int.Parse(dtr["NoOfSatSession"].ToString()),
                                 int.Parse(dtr["NoAsQuarantineInvi"].ToString()), int.Parse(dtr["NoAsReliefInvi"].ToString()), int.Parse(dtr["NoOfExtraSession"].ToString()),
                                 isChiefInvi, isInviAbove2Years, new List<string>(), new List<Exemption>(), maintainInvigilationDutyControl.searchInvigilationDuty(staffID));
                        maintainInvigilationDutyControl.shutDown();

                        MaintainExemptionControl maintainExemptionControl = new MaintainExemptionControl();
                        staff.ExemptionList = maintainExemptionControl.searchExemptionList(staffID);
                        maintainExemptionControl.shutDown();
                        staffList.Add(staff);
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return staffList;
        }

        public List<Staff> searchInvigilators(DateTime date, string session, string venueID)
        {
            List<Staff> invigilatorsList = new List<Staff>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, dbo.PaperExamined P, Timeslot T, dbo.InvigilationDuty I Where S.typeOfEmploy = 'F' and T.Date = @Date And T.Session = @Session And T.TimeslotID = I.TimeslotID And I.VenueID = @VenueID And I.StaffID = S.StaffID Group by S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@Date", date);
                cmdSearch.Parameters.AddWithValue("@Session", session);
                cmdSearch.Parameters.AddWithValue("@VenueID", venueID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string staffID = dtr["StaffID"].ToString();
                        MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
                        bool isMuslim = false;
                        bool isTakingSTSPhD = false;
                        bool isChiefInvi = false;
                        bool isInviAbove2Years = false;
                        if (dtr["isMuslim"].ToString().Equals("Y"))
                        {
                            isMuslim = true;
                        }
                        else
                        {
                            isMuslim = false;
                        }
                        if (dtr["isTakingSTSPhD"].ToString().Equals("Y"))
                        {
                            isTakingSTSPhD = true;
                        }
                        else
                        {
                            isTakingSTSPhD = false;
                        }
                        if (dtr["isChiefInvi"].ToString().Equals("Y"))
                        {
                            isChiefInvi = true;
                        }
                        else
                        {
                            isChiefInvi = false;
                        }
                        if (dtr["isInviAbove2Years"].ToString().Equals("Y"))
                        {
                            isInviAbove2Years = true;
                        }
                        else
                        {
                            isInviAbove2Years = false;
                        }
                        Staff invigilator = new Staff(dtr["name"].ToString(), char.Parse(dtr["gender"].ToString()), isMuslim, dtr["StaffID"].ToString(),
                                 dtr["title"].ToString(), dtr["position"].ToString(), dtr["facultyCode"].ToString(), dtr["department"].ToString(),
                                 isTakingSTSPhD, char.Parse(dtr["typeOfEmploy"].ToString()), int.Parse(dtr["NoOfSatSession"].ToString()),
                                 int.Parse(dtr["NoAsQuarantineInvi"].ToString()), int.Parse(dtr["NoAsReliefInvi"].ToString()), int.Parse(dtr["NoOfExtraSession"].ToString()),
                                 isChiefInvi, isInviAbove2Years, new List<string>(), new List<Exemption>(), maintainInvigilationDutyControl.searchInvigilationDuty(staffID));
                        maintainInvigilationDutyControl.shutDown();

                        MaintainExemptionControl maintainExemptionControl = new MaintainExemptionControl();
                        invigilator.ExemptionList = maintainExemptionControl.searchExemptionList(staffID);
                        maintainExemptionControl.shutDown();
                        invigilatorsList.Add(invigilator);
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            for (int i = 0; i < invigilatorsList.Count; i++)
            {
                invigilatorsList[i].PaperCodeExamined = searchPaperExamined(invigilatorsList[i].StaffID);
            }
            return invigilatorsList;
        }

        public List<Staff> searchChiefInvigilators(DateTime date, string session, string blockCode)
        {
            List<Staff> chiefInvigilatorsList = new List<Staff>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, dbo.Timeslot T, dbo.InvigilationDuty I Where S.typeOfEmploy = 'F' and T.Date = @Date And T.Session = @Session And T.TimeslotID = I.TimeslotID And I.CatOfInvi = 'Chief' And I.Location = @Location And I.StaffID = S.StaffID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@Date", date);
                cmdSearch.Parameters.AddWithValue("@Session", session);
                cmdSearch.Parameters.AddWithValue("@Location", blockCode);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string staffID = dtr["StaffID"].ToString();
                        MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
                        bool isMuslim = false;
                        bool isTakingSTSPhD = false;
                        bool isChiefInvi = false;
                        bool isInviAbove2Years = false;
                        if (dtr["isMuslim"].ToString().Equals("Y"))
                        {
                            isMuslim = true;
                        }
                        else
                        {
                            isMuslim = false;
                        }
                        if (dtr["isTakingSTSPhD"].ToString().Equals("Y"))
                        {
                            isTakingSTSPhD = true;
                        }
                        else
                        {
                            isTakingSTSPhD = false;
                        }
                        if (dtr["isChiefInvi"].ToString().Equals("Y"))
                        {
                            isChiefInvi = true;
                        }
                        else
                        {
                            isChiefInvi = false;
                        }
                        if (dtr["isInviAbove2Years"].ToString().Equals("Y"))
                        {
                            isInviAbove2Years = true;
                        }
                        else
                        {
                            isInviAbove2Years = false;
                        }
                        Staff chiefInvigilator = new Staff(dtr["name"].ToString(), char.Parse(dtr["gender"].ToString()), isMuslim, dtr["StaffID"].ToString(),
                                 dtr["title"].ToString(), dtr["position"].ToString(), dtr["facultyCode"].ToString(), dtr["department"].ToString(),
                                 isTakingSTSPhD, char.Parse(dtr["typeOfEmploy"].ToString()), int.Parse(dtr["NoOfSatSession"].ToString()),
                                 int.Parse(dtr["NoAsQuarantineInvi"].ToString()), int.Parse(dtr["NoAsReliefInvi"].ToString()), int.Parse(dtr["NoOfExtraSession"].ToString()),
                                 isChiefInvi, isInviAbove2Years, new List<string>(), new List<Exemption>(), maintainInvigilationDutyControl.searchInvigilationDuty(staffID));
                        maintainInvigilationDutyControl.shutDown();

                        MaintainExemptionControl maintainExemptionControl = new MaintainExemptionControl();
                        chiefInvigilator.ExemptionList = maintainExemptionControl.searchExemptionList(staffID);
                        maintainExemptionControl.shutDown();
                        chiefInvigilatorsList.Add(chiefInvigilator);
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            for (int i = 0; i < chiefInvigilatorsList.Count; i++)
            {
                chiefInvigilatorsList[i].PaperCodeExamined = searchPaperExamined(chiefInvigilatorsList[i].StaffID);
            }
            return chiefInvigilatorsList;
        }

        public List<Staff> searchReliefInvigilators(DateTime date, string session)
        {
            List<Staff> reliefInvigilatorsList = new List<Staff>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select S.name, S.gender, S.isMuslim, S.StaffID, S.title, S.position, S.facultyCode, S.department, S.isTakingSTSPhD, S.typeOfEmploy, S.NoOfSatSession, S.NoAsQuarantineInvi, S.NoAsReliefInvi, S.NoOfExtraSession, S.isChiefInvi, S.isInviAbove2Years From dbo.Staff S, Timeslot T, dbo.InvigilationDuty I Where S.typeOfEmploy = 'F' and T.Date = @Date And T.Session = @Session And T.TimeslotID = I.TimeslotID And I.StaffID = S.StaffID And I.CatOfInvi = 'Relief' Or I.CatOfInvi = 'Quarantine'";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@Date", date);
                cmdSearch.Parameters.AddWithValue("@Session", session);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        string staffID = dtr["StaffID"].ToString();
                        MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
                        bool isMuslim = false;
                        bool isTakingSTSPhD = false;
                        bool isChiefInvi = false;
                        bool isInviAbove2Years = false;
                        if (dtr["isMuslim"].ToString().Equals("Y"))
                        {
                            isMuslim = true;
                        }
                        else
                        {
                            isMuslim = false;
                        }
                        if (dtr["isTakingSTSPhD"].ToString().Equals("Y"))
                        {
                            isTakingSTSPhD = true;
                        }
                        else
                        {
                            isTakingSTSPhD = false;
                        }
                        if (dtr["isChiefInvi"].ToString().Equals("Y"))
                        {
                            isChiefInvi = true;
                        }
                        else
                        {
                            isChiefInvi = false;
                        }
                        if (dtr["isInviAbove2Years"].ToString().Equals("Y"))
                        {
                            isInviAbove2Years = true;
                        }
                        else
                        {
                            isInviAbove2Years = false;
                        }
                        Staff reliefInvigilator = new Staff(dtr["name"].ToString(), char.Parse(dtr["gender"].ToString()), isMuslim, dtr["StaffID"].ToString(),
                                 dtr["title"].ToString(), dtr["position"].ToString(), dtr["facultyCode"].ToString(), dtr["department"].ToString(),
                                 isTakingSTSPhD, char.Parse(dtr["typeOfEmploy"].ToString()), int.Parse(dtr["NoOfSatSession"].ToString()),
                                 int.Parse(dtr["NoAsQuarantineInvi"].ToString()), int.Parse(dtr["NoAsReliefInvi"].ToString()), int.Parse(dtr["NoOfExtraSession"].ToString()),
                                 isChiefInvi, isInviAbove2Years, new List<string>(), new List<Exemption>(), maintainInvigilationDutyControl.searchInvigilationDuty(staffID));
                        maintainInvigilationDutyControl.shutDown();

                        MaintainExemptionControl maintainExemptionControl = new MaintainExemptionControl();
                        reliefInvigilator.ExemptionList = maintainExemptionControl.searchExemptionList(staffID);
                        maintainExemptionControl.shutDown();
                        reliefInvigilatorsList.Add(reliefInvigilator);
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            for (int i = 0; i < reliefInvigilatorsList.Count; i++)
            {
                reliefInvigilatorsList[i].PaperCodeExamined = searchPaperExamined(reliefInvigilatorsList[i].StaffID);
            }
            return reliefInvigilatorsList;
        }

        public List<string> searchPaperExamined(string staffID)
        {
            List<string> paperCodeExamined = new List<string>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select P.CourseCode From dbo.PaperExamined P Where P.StaffID = @StaffID";
                cmdSearch = new SqlCommand(strSearch, conn);

                cmdSearch.Parameters.AddWithValue("@StaffID", staffID);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        paperCodeExamined.Add(dtr["CourseCode"].ToString());
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return paperCodeExamined;
        }

        public int countTotalInvigilatorsAvailable()
        {
            int totalInvigilatorsAvailable = 0;

            try
            {
                strSelect = "Select * From dbo.Staff Where typeOfEmploy = 'F' and (isInvi = 'Y' or isInviAbove2Years = 'Y') and isChiefInvi = 'N'";
                cmdSelect = new SqlCommand(strSelect, conn);
                SqlDataReader dtr = cmdSelect.ExecuteReader();
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        totalInvigilatorsAvailable++;
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return totalInvigilatorsAvailable;
        }

        public int countTotalChiefInvigilatorsAvailable()
        {
            int totalChiefInvigilatorsAvailable = 0;

            try
            {
                strSelect = "Select * From dbo.Staff Where typeOfEmploy = 'F' and isChiefInvi = 'Y'";
                cmdSelect = new SqlCommand(strSelect, conn);
                SqlDataReader dtr = cmdSelect.ExecuteReader();
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        totalChiefInvigilatorsAvailable++;
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return totalChiefInvigilatorsAvailable;
        }

        public int getAverageNoOfExtraSession(string checkType)
        {
            int averageNoOfExtraSession = 0;

            try
            {
                if (checkType.Equals("Chief"))
                {
                    strSelect = "Select ROUND(AVG(CAST(NoOfExtraSession AS FLOAT)), 1) as avgExtraSession From dbo.Staff Where typeOfEmploy = 'F' and isChiefInvi = 'Y'";
                }
                else
                {
                    strSelect = "Select ROUND(AVG(CAST(NoOfExtraSession AS FLOAT)), 1) as avgExtraSession From dbo.Staff Where typeOfEmploy = 'F' and (isInvi = 'Y' or isInviAbove2Years = 'Y') and isChiefInvi = 'N'";
                }
                cmdSelect = new SqlCommand(strSelect, conn);
                SqlDataReader dtr = cmdSelect.ExecuteReader();
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        averageNoOfExtraSession = (int)Math.Round(double.Parse(dtr["avgExtraSession"].ToString()), MidpointRounding.AwayFromZero);
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            return averageNoOfExtraSession;
        }

        public List<string> getFacultyCodesList()
        {
            List<string> facultyCodesList = new List<string>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select FacultyCode from Faculty";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        facultyCodesList.Add(dtr["FacultyCode"].ToString());
                    }
                }
                dtr.Close();
            }
            catch (SqlException)
            {
                throw;
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

        //need to be moved into staffDA(start of my edit)
        public List<Staff> searchLecturerByQuery(string invigilatorQuery)
        {
            StaffDA staffs = new StaffDA();
            List<Staff> result = new List<Staff>();

            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch =  invigilatorQuery;
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        Staff staff = new Staff(dtr["StaffID"].ToString(), dtr["Title"].ToString(), dtr["Name"].ToString(), Convert.ToChar(dtr["FacultyCode"].ToString()), Convert.ToChar(dtr["isChiefInvi"].ToString()), Convert.ToChar(dtr["isInvi"].ToString()));
                        result.Add(staff);
                    }
                    dtr.Close();
                }

            }
            catch (SqlException)
            {
                throw;
            }

            return result;
        }
    }

    
  
}