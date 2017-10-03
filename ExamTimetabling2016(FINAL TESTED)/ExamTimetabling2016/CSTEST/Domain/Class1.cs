using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace ExamTimetabling2016.CSTEST.Domain
{  
    public class ConstraintReader
    {
        private SqlConnection conn;
        private string connectionString = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch;
        private string strSelect, strSearch;

        public ConstraintReader()
        {
            initializeDatabase();
        }

        //database initialization
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

        public List<Staff> getStaffByConstraints(string constraint)
        {
            List<Staff> staffList = new List<Staff>(); ;




            return staffList;
        }

        public string lecturerSQLConstructor(string constraint)
        {

            //step 1: initialization
            string[] variableName = { "Gender", "" };
            string sqlStatement = "";
            string[] relativeTableName = { "dbo.Staff", "" };
            string fromSqlStatement = " from dbo.Staff a, dbo.InvigilationDuty b, dbo.PaperExamined c, dbo.faculty d WHERE a.StaffID = b.StaffID AND a.FacultyCode = d.FacultyCode AND a.StaffID = c.StaffID";
            

            sqlStatement = sqlStatement + "Select *";


            if (calculateConstraintVar(constraint) == 1)
            {

                sqlStatement = sqlStatement + fromSqlStatement + " AND " + variableName[0]; //" " + connector + tableName ;
            }
            /* Major thoughts on the part where we need to handle the process of handling table name 
            and variable name*/

            else
            {
                

            }
            //return sqlStatement;
            return sqlStatement;
        }

        public int calculateConstraintVar(string constraint)
        {
            int noOfConstraintObject = 0;
            string[] word;

            word = constraint.Split(' ');

            foreach (string x in word)
            {
                noOfConstraintObject++;
            }

            /*by nature each word is seperated by an operator making each constraint with odd 
              number of words making this equation possible to find the number of variables
             */
            noOfConstraintObject = ((noOfConstraintObject + 1) / 2) / 2;
            return noOfConstraintObject;
        }

        //function to build sqlstatement
        public string buildStatement(string constraint)
        {
            ConstraintVarDA constraintVar = new ConstraintVarDA();
            string sqlStatement = "";//for final result
            /**check number of object in the constraint to determine how many condition is there 
             * Sample statement: var1 >= 3 || var2 == 1 
             * in the structure of the statement we can found out that every 4th object is an connector between a statement
             * while the first and the first after an connector will be a variable name 
             * **/
            char[] letters = { 'a', 'b', 'c', 'd' };//for letter in database
            int noOfConstraintConnector = 0;// example: || >= <= < >
            int noOfConstraintCondition = 0;   // example: conditin1 || condition2 
            int wordCount = 1;
            string[] temp = { };
            string[] tableList = { };//used to store distinct table obtained from constraint
            string[] wordList = { };//used to store words in the table

            string variablePart = "";// select a.var1,b.var2 
            string tablePart = ""; // from table a 

            int noOfConstraintObject = 0;
            string[] word;

            word = constraint.Split(' ');

            foreach (string x in word)
            {
                noOfConstraintObject++;
            }

            /**number of object modulo 3 can find the number of connector between the condition of constraint
             * while the total number of object - number of connector , then divide by 3 will find the number of 
             * constraint condition
             **/
            noOfConstraintConnector = noOfConstraintObject % 3;
            noOfConstraintCondition = noOfConstraintObject - noOfConstraintConnector;

            //check every first variable
            int tempNo = 0;
            string tableName = "";
            string variableNameInTable = "";
            Boolean tableIsRepeated = false;
            for (int x = 1; x < noOfConstraintObject; x += 4)
            {
                //obtain table name
                tableName = constraintVar.getTableName(word[x - 1]);

                //obtain actual name in table
                variableNameInTable = constraintVar.getVariableNameInTable(word[x - 1]);

                //initiliaze repeating statement for table
                tableIsRepeated = false;

                //belows are all for handling repeating table name
                if (tableList == null)
                {
                    tableList[0] = tableName;
                }
                else // compare table with each of the previous table name to find out if there is any repeatance
                {
                    for (int y = 0; y > tableList.Count(); y++)
                    {
                        if (tableList[y].Equals(tableName))// might need to be changed into ==
                        {
                            tableIsRepeated = true;
                        }

                    }

                    //table is not repeated, input into the last slot of the list
                    if (tableIsRepeated == false)
                    {
                        tableList[tableList.Count()] = tableName;
                    }


                    // check table 1 and table 2 for key variable join them, then check table 2/ 3 or 1/3 for another variable
                        

                    //assign letter for table
                }

                //check every 2nd operator


                //check ever connector


            }

            return sqlStatement;
        }

        public class ConstraintVarDA
        {
            private SqlConnection conn;
            private string connectionstring = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
            private SqlCommand cmdSearch;
            private string strSearch;

            public ConstraintVarDA()
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

            public string getVariableNameInTable(string constraintWord)
            {
                string result = "";

                try
                {
                    /*Step 2: Create Sql Search statement and Sql Search Object*/
                    strSearch = "Select VariableNameInTable from dbo.Constraint where VariableName = @VarName";
                    cmdSearch = new SqlCommand(strSearch, conn);

                    cmdSearch.Parameters.AddWithValue("@VarName", constraintWord);

                    /*Step 3: Execute command to retrieve data*/
                    SqlDataReader dtr = cmdSearch.ExecuteReader();

                    /*Step 4: Get result set from the query*/
                    if (dtr.HasRows)
                    {
                        while (dtr.Read())
                        {
                            result = dtr["CourseTitle"].ToString();
                            //course = new Course(courseCode, dtr["CourseTitle"].ToString());    
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


            public string getTableName(string constraintWord)
            {
                string result = "";
                try
                { 
                    /*Step 2: Create Sql Search statement and Sql Search Object*/
                    strSearch = "Select Table from dbo.Constraint where VariableName = @VarName";
                    cmdSearch = new SqlCommand(strSearch, conn);

                    cmdSearch.Parameters.AddWithValue("@VarName", constraintWord);

                    /*Step 3: Execute command to retrieve data*/
                    SqlDataReader dtr = cmdSearch.ExecuteReader();

                    /*Step 4: Get result set from the query*/
                    if (dtr.HasRows)
                    {
                        while (dtr.Read())
                        {
                            result = dtr["TableName"].ToString();
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

            //new part

            public void Assign(List<Examination>[] ExaminationList, List<Staff>[] StaffList)
            {
                //clear invigilationDuty table
                MaintainInvigilationDutyControl maintainInvDutyControl = new MaintainInvigilationDutyControl();
                maintainInvDutyControl.clearInvigilationDuty();
                maintainInvDutyControl.shutDown();

                MaintainTimetableControl maintainTimetableControl = new MaintainTimetableControl();
                List<Timetable> examTimetable = maintainTimetableControl.selectTimetable();
                maintainTimetableControl.shutDown();

                //This part will be unchanged (CS)
                MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
                double totalLoadOfDutyForEachInvigilator = calculateTotalLoadOfDutyForEachInvigilator(calculateTotalInvigilatorsRequired(examTimetable), maintainStaffControl.countTotalInvigilatorsAvailable());
                double totalLoadOfDutyForEachChiefInvigilator = calculateTotalLoadOfDutyForEachChiefInvigilator(calculateTotalChiefInvigilatorsRequired(examTimetable), maintainStaffControl.countTotalChiefInvigilatorsAvailable());
                maintainStaffControl.shutDown();

                //This part will be unchanged (CS)
                double minTotalLoadOfDutyForEachInvigilator = (int)totalLoadOfDutyForEachInvigilator;
                double minTotalLoadOfDutyForEachChiefInvigilator = (int)totalLoadOfDutyForEachChiefInvigilator;




            }
            public static double calculateTotalLoadOfDutyForEachInvigilator(int totalInvigilatorsRequired, int totalInvigilatorsAvailable)
            {
                return (double)totalInvigilatorsRequired / (double)totalInvigilatorsAvailable;
            }

            //calculate total invigilators required
            public static int calculateTotalInvigilatorsRequired(List<Timetable> examTimetable)
            {
                int totalInvigilatorsRequired = 0;
                //add number of invigilators required in whole exam period
                foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
                {
                    foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                    {
                        foreach (Venue venue in block.VenuesList)
                        {
                            totalInvigilatorsRequired += getNumberOfInvigilatorsRequired(venue);
                        }
                    }
                }
                totalInvigilatorsRequired += calculateTotalReliefInvigilatorsRequired(examTimetable);
                return totalInvigilatorsRequired;
            }

            //calculate total relief invigilators required
            public static int calculateTotalReliefInvigilatorsRequired(List<Timetable> examTimetable)
            {
                int totalReliefInvigilatorsRequired = 0;

                foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
                {
                    //add number of relief invigilators required(one session by one session)
                    totalReliefInvigilatorsRequired += calculateTotalReliefInvigilatorsRequiredPerSession(examTimetableInSameDayAndSession);
                }
                return totalReliefInvigilatorsRequired;
            }
            //calculate total relief invigilators required for that session
            public static int calculateTotalReliefInvigilatorsRequiredPerSession(Timetable examTimetableInSameDayAndSession)
            {
                int totalReliefInvigilatorsRequiredPerSession = 0;
                int numberOfMuslimReliefInviRequired = 1;

                //add number of relief invigilators required(one for each block)
                totalReliefInvigilatorsRequiredPerSession += examTimetableInSameDayAndSession.BlocksList.Count;

                //add 1 quarantine invi for east campus
                if (examTimetableInSameDayAndSession.BlocksList.Where(block => block.EastOrWest.Equals('E')).ToList().Count > 0)
                    totalReliefInvigilatorsRequiredPerSession++;

                //add 1 quarantine invi for west campus
                if (examTimetableInSameDayAndSession.BlocksList.Where(block => block.EastOrWest.Equals('W')).ToList().Count > 0)
                    totalReliefInvigilatorsRequiredPerSession++;

                if (examTimetableInSameDayAndSession.Session.Equals("EV"))
                {
                    totalReliefInvigilatorsRequiredPerSession += numberOfMuslimReliefInviRequired;
                }
                return totalReliefInvigilatorsRequiredPerSession;
            }

            public static int getNumberOfInvigilatorsRequired(Venue venue)
            {
                string venueID = venue.VenueID;
                int numberOfProgrammes = 0;
                int duration = 0;

                double[] arrayInput;
                arrayInput = new double[3];
                MaintainConstraintControl maintainConstraintControl = new MaintainConstraintControl();

                for (int i = 0; i < venue.CoursesList.Count; i++)
                {
                    numberOfProgrammes += venue.CoursesList[i].ProgrammeList.Count;
                    if (venue.CoursesList[i].Duration > duration)
                    {
                        duration = venue.CoursesList[i].Duration;
                    }
                }
                arrayInput[1] = Convert.ToDouble(numberOfProgrammes);

                if (venueID.StartsWith("H") || venueID.StartsWith("M") || venueID.StartsWith("PA") || venueID.StartsWith("Q") || venueID.StartsWith("R") || venueID.StartsWith("V") || venueID.Equals("L1") || venueID.Equals("L3") || venueID.StartsWith("SD") || venueID.StartsWith("SE"))
                {
                    arrayInput[0] = 0;
                    if ((int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "invigilatorRequired" }, arrayInput)) != 0)
                    {
                        return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "invigilatorRequired" }, arrayInput));
                    }
                    else
                    {
                        arrayInput[2] = Convert.ToDouble(numberOfProgrammes);
                        return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "numberOfPaper", "invigilatorRequired" }, arrayInput));
                    }
                }
                else if (venueID.Equals("L2"))
                {
                    arrayInput[0] = 1;
                    return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "invigilatorRequired" }, arrayInput));

                }
                else if (venueID.Equals("SB1") || venueID.Equals("SB3"))
                {
                    arrayInput[0] = 2;
                    return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "invigilatorRequired" }, arrayInput));
                }
                else if (venueID.Equals("SB2") || venueID.Equals("SB4"))
                {
                    arrayInput[0] = 3;
                    return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "invigilatorRequired" }, arrayInput));
                }
                else if (venueID.Equals("DU"))
                {
                    arrayInput[0] = 4;
                    arrayInput[1] = Convert.ToDouble(duration);
                    return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "duration", "invigilatorRequired" }, arrayInput));
                }
                else if (venueID.Equals("KS1") || venueID.Equals("KS2"))
                {
                    arrayInput[0] = 5;
                    arrayInput[1] = Convert.ToDouble(duration);
                    return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "duration", "invigilatorRequired" }, arrayInput));
                }
                else
                    return 0;
            }

            //calculate total chief invigilators required in whole exam period
            public static int calculateTotalChiefInvigilatorsRequired(List<Timetable> examTimetable)
            {
                int totalChiefInvigilatorsRequired = 0;

                foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
                {
                    //add number of chief invigilators required in same day and session
                    totalChiefInvigilatorsRequired += examTimetableInSameDayAndSession.BlocksList.Count;
                }
                return totalChiefInvigilatorsRequired;
            }

            public static double calculateTotalLoadOfDutyForEachChiefInvigilator(int totalChiefInvigilatorsRequired, int totalChiefInvigilatorsAvailable)
            {
                return (double)totalChiefInvigilatorsRequired / (double)totalChiefInvigilatorsAvailable;
            }


            public static List<Staff> getInvigilatorsList()
            {
                MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
                List<Staff> invigilatorsList = maintainStaffControl.searchLecturer("Invigilator", "categoryOfInvigilator");
                maintainStaffControl.shutDown();
                return invigilatorsList;
            }

        }

    }
}