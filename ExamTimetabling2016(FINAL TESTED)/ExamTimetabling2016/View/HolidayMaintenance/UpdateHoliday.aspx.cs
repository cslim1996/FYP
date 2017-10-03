using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.Holiday_Maintenance
{
    public partial class UpdateHoliday : System.Web.UI.Page
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
            }
        }


        private void clearFields()
        {
            txt_holidayName.Text = "";
            calendar_Start.SelectedDate = calendar_Start.TodaysDate;
            calendar_End.SelectedDate = calendar_End.TodaysDate;
            cbl_States.SelectedIndex = -1;
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmdUpdate = new SqlCommand("Update Holiday Set HolidayName = @hName, StartDate = @sd, EndDate = @ed, isKL = @kl, isPenang = @penang, isPerak = @perak, isJohor = @johor, isPahang = @pahang, isSabah = @Sabah WHERE holidayID = @hid", con);
            cmdUpdate.Parameters.AddWithValue("@hName", txt_holidayName.Text);
            cmdUpdate.Parameters.AddWithValue("@sd", calendar_Start.SelectedDate);
            cmdUpdate.Parameters.AddWithValue("@ed", calendar_End.SelectedDate);

            char affected = 'Y';
            char unaffected = 'N';

            foreach (ListItem li in cbl_States.Items)
            {
                if (li.Selected == true && li.Value.Equals("Kuala Lumpur"))
                {
                    cmdUpdate.Parameters.AddWithValue("@kl", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Kuala Lumpur"))
                    cmdUpdate.Parameters.AddWithValue("@kl", unaffected);


                if (li.Selected == true && li.Value.Equals("Perak"))
                {
                    cmdUpdate.Parameters.AddWithValue("@perak", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Perak"))
                    cmdUpdate.Parameters.AddWithValue("@perak", unaffected);


                if (li.Selected == true && li.Value.Equals("Penang"))
                {
                    cmdUpdate.Parameters.AddWithValue("@penang", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Penang"))
                    cmdUpdate.Parameters.AddWithValue("@penang", unaffected);


                if (li.Selected == true && li.Value.Equals("Johor"))
                {
                    cmdUpdate.Parameters.AddWithValue("@johor", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Johor"))
                    cmdUpdate.Parameters.AddWithValue("@johor", unaffected);


                if (li.Selected == true && li.Value.Equals("Pahang"))
                {
                    cmdUpdate.Parameters.AddWithValue("@pahang", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Pahang"))
                    cmdUpdate.Parameters.AddWithValue("@pahang", unaffected);


                if (li.Selected == true && li.Value.Equals("Sabah"))
                {
                    cmdUpdate.Parameters.AddWithValue("@sabah", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Sabah"))
                    cmdUpdate.Parameters.AddWithValue("@sabah", unaffected);

            }

            cmdUpdate.Parameters.AddWithValue("@hid", Session["ID"]);

            cmdUpdate.ExecuteNonQuery();

            con.Close();
            clearFields();
            Session["ID"] = null;
            Response.Redirect("HolidayMaintenance.aspx");

        }

        protected void btn_showStart_Click(object sender, EventArgs e)
        {
            if (calendar_Start.Visible == false)
                calendar_Start.Visible = true;
            else
                calendar_Start.Visible = false;
        }

        protected void btn_showEnd_Click(object sender, EventArgs e)
        {
            if (calendar_End.Visible == false)
                calendar_End.Visible = true;
            else
                calendar_End.Visible = false;

            selectedDate = calendar_Start.SelectedDate;
        }

        protected void calendar_Start_SelectionChanged(object sender, EventArgs e)
        {
            txt_showStart.Text = calendar_Start.SelectedDate.ToShortDateString();
            selectedDate = calendar_Start.SelectedDate;
        }

        protected void calendar_End_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.CompareTo(selectedDate.Date) < 0)
            {
                e.Day.IsSelectable = false;
                e.Cell.Enabled = false;
                e.Cell.BackColor = System.Drawing.Color.DarkGray;
            }

        }

        protected void calendar_End_SelectionChanged(object sender, EventArgs e)
        {
            txt_showEnd.Text = calendar_End.SelectedDate.ToShortDateString();
            selectedDate = calendar_Start.SelectedDate;
        }

    }
}