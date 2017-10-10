using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class Constraint2DA
    {

        private SqlConnection conn;
        private string connectionString = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSelect, cmdSearch, cmdInsert, cmdUpdate, cmdDelete;
        private string strSelect, strSearch, strInsert, strUpdate, strDelete;

        public Constraint2DA()
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

        public List<Constraint2> getConstraintList() {
            List<Constraint2> constraintList = new List<Constraint2>();
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select * from dbo.ExamConstraint";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {

                        Constraint2 constraint = new Constraint2(dtr["InvigilatorQuery"].ToString(), dtr["ExamQuery"].ToString(), dtr["ConditionQuery"].ToString(), Convert.ToChar(dtr["IsCond"]));
                        constraintList.Add(constraint);
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
            Constraint2DA da = new Constraint2DA();
        }
    }
}