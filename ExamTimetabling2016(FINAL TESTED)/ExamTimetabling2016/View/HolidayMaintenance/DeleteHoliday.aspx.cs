using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016.View.HolidayMaintenance
{
    public partial class DeleteHoliday : System.Web.UI.Page
    {
        static string strCon = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
        public SqlConnection con = new SqlConnection(strCon);
        private DateTime selectedDate = DateTime.Now.Date;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calendar_Start.Visible = false;
                calendar_End.Visible = false;

                if (Session["ID"] != null)
                {

                    con.Open();
                    SqlCommand cmdSelect = new SqlCommand("Select * from Holiday where HolidayID = @hid", con);
                    cmdSelect.Parameters.AddWithValue("@hid", Session["ID"].ToString());
                    SqlDataReader dr = cmdSelect.ExecuteReader();

                    while (dr.Read())
                    {
                        txt_holidayName.Text = "" + dr["HolidayName"];
                        calendar_Start.SelectedDate = (DateTime)dr["StartDate"];
                        txt_showStart.Text = calendar_Start.SelectedDate.ToShortDateString();
                        calendar_End.SelectedDate = (DateTime)dr["EndDate"];
                        txt_showEnd.Text = calendar_End.SelectedDate.ToShortDateString();

                        foreach (ListItem li in cbl_States.Items)
                        {
                            if (dr["isKL"].Equals("Y") && li.Value.Equals("Kuala Lumpur"))
                                li.Selected = true;
                            else if (dr["isPenang"].Equals("Y") && li.Value.Equals("Penang"))
                                li.Selected = true;
                            else if (dr["isPerak"].Equals("Y") && li.Value.Equals("Perak"))
                                li.Selected = true;
                            else if (dr["isJohor"].Equals("Y") && li.Value.Equals("Johor"))
                                li.Selected = true;
                            else if (dr["isPahang"].Equals("Y") && li.Value.Equals("Pahang"))
                                li.Selected = true;
                            else if (dr["isSabah"].Equals("Y") && li.Value.Equals("Sabah"))
                                li.Selected = true;
                            else
                                li.Selected = false;

                        }
                    }

                    con.Close();

                }

                calendar_End.Enabled = false;
                calendar_Start.Enabled = false;
                txt_holidayName.Enabled = false;
                cbl_States.Enabled = false;
            }
        }

        protected void btn_Delete_Click(object sender, EventArgs e)
        {
            con.Open();


            SqlCommand sqlDelete = new SqlCommand("Delete from Holiday where HolidayName = @hn", con);
            sqlDelete.Parameters.AddWithValue("@hn", txt_holidayName.Text);

            sqlDelete.ExecuteNonQuery();

            con.Close();

            Response.Redirect("HolidayMaintenance.aspx");
        }
    }
}