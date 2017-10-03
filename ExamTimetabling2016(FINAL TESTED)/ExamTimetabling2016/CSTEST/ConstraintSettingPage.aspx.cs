using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016.View.InvigilationMaintenance
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private SqlConnection conn;
        private string connectionstring = ConfigurationManager.ConnectionStrings["ExamTimetableDBConnectionString"].ConnectionString;
        private SqlCommand cmdSearch;
        private string strSearch;

        protected void Page_Load(object sender, EventArgs e)
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }

        protected void ExamTableDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExamDDL.Items.Clear();
            ExamDDL.Items.Add(new ListItem("Select an attribute", "Select an Attribute"));
            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "Select Distinct CourseCode from ";


                //cmdSearch.Parameters.AddWithValue("@tableName", "ExamTimetabling.dbo.Course");
                strSearch = strSearch + "ExamTimetabling.dbo.Course";
                cmdSearch = new SqlCommand(strSearch, conn);
                cmdSearch.Parameters.AddWithValue("@columnName", "CourseCode");

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    { 
                     ExamDDL.Items.Add(new ListItem(dtr[0].ToString(),dtr[0].ToString()));
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }

        }
    
        protected void StaffTableDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            StaffDDL.Items.Clear();
            StaffDDL.Items.Add(new ListItem("Select an attribute", "Select an Attribute"));


            try
            {
                /*Step 2: Create Sql Search statement and Sql Search Object*/
                strSearch = "select Column_name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '";


                //cmdSearch.Parameters.AddWithValue("@tableName", "ExamTimetabling.dbo.Course");
                strSearch = strSearch + StaffTableDDL.SelectedValue + "'";
                cmdSearch = new SqlCommand(strSearch, conn);

                /*Step 3: Execute command to retrieve data*/
                SqlDataReader dtr = cmdSearch.ExecuteReader();

                /*Step 4: Get result set from the query*/
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        StaffDDL.Items.Add(new ListItem(dtr[0].ToString(), dtr[0].ToString()));
                    }
                    dtr.Close();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

    }
}