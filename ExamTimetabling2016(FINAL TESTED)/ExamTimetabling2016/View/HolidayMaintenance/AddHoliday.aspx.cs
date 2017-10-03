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
    public partial class AddHoliday : System.Web.UI.Page
    {
        static string strCon = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
        public SqlConnection con = new SqlConnection(strCon);
        private DateTime selectedDate = DateTime.Now.Date;
        bool check = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                check = false;
                calendar_Start.Visible = false;
                calendar_End.Visible = false;
                calendar_Start.SelectedDate = calendar_Start.TodaysDate;
                txt_showStart.Text = calendar_Start.TodaysDate.ToShortDateString();
                calendar_End.SelectedDate = calendar_End.TodaysDate;
                txt_showEnd.Text = calendar_End.TodaysDate.ToShortDateString();

                btn_AddHoliday.Visible = true;

            }
        }

        protected void btn_AddHoliday_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmdInsert = new SqlCommand("Insert into Holiday Values(@holidayName,@startDate,@endDate,@Duration,@kl,@penang,@perak,@johor,@pahang,@sabah)", con);
            cmdInsert.Parameters.AddWithValue("@holidayName", txt_holidayName.Text);
            cmdInsert.Parameters.AddWithValue("@startDate", calendar_Start.SelectedDate);
            cmdInsert.Parameters.AddWithValue("@endDate", calendar_End.SelectedDate);

           // DateTime dt = Convert.ToDateTime(txt_showStart.Text);
            //DateTime dt2 = Convert.ToDateTime(txt_showEnd.Text);

            //double duration = (dt2 - dt).TotalDays;

            cmdInsert.Parameters.AddWithValue("@Duration", 0);
           

            char affected = 'Y';
            char unaffected = 'N';

            foreach (ListItem li in cbl_States.Items)
            {
                if (li.Selected == true && li.Value.Equals("Kuala Lumpur"))
                {
                    cmdInsert.Parameters.AddWithValue("@kl", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Kuala Lumpur"))
                    cmdInsert.Parameters.AddWithValue("@kl", unaffected);


                if (li.Selected == true && li.Value.Equals("Perak"))
                {
                    cmdInsert.Parameters.AddWithValue("@perak", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Perak"))
                    cmdInsert.Parameters.AddWithValue("@perak", unaffected);


                if (li.Selected == true && li.Value.Equals("Penang"))
                {
                    cmdInsert.Parameters.AddWithValue("@penang", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Penang"))
                    cmdInsert.Parameters.AddWithValue("@penang", unaffected);


                if (li.Selected == true && li.Value.Equals("Johor"))
                {
                    cmdInsert.Parameters.AddWithValue("@johor", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Johor"))
                    cmdInsert.Parameters.AddWithValue("@johor", unaffected);


                if (li.Selected == true && li.Value.Equals("Pahang"))
                {
                    cmdInsert.Parameters.AddWithValue("@pahang", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Pahang"))
                    cmdInsert.Parameters.AddWithValue("@pahang", unaffected);


                if (li.Selected == true && li.Value.Equals("Sabah"))
                {
                    cmdInsert.Parameters.AddWithValue("@sabah", affected);
                }
                else if (li.Selected == false && li.Value.Equals("Sabah"))
                    cmdInsert.Parameters.AddWithValue("@sabah", unaffected);

            }

            cmdInsert.ExecuteNonQuery();

            // Response.Write(calendar_Start.TodaysDate);

            con.Close();
            clearFields();

        }

        private void clearFields()
        {
            txt_holidayName.Text = "";
            calendar_Start.SelectedDate = calendar_Start.TodaysDate;
            calendar_End.SelectedDate = calendar_End.TodaysDate;
            cbl_States.SelectedIndex = -1;
            txt_showStart.Text = calendar_Start.TodaysDate.ToShortDateString();
            txt_showEnd.Text = calendar_End.TodaysDate.ToShortDateString();
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

        protected void btn_check_Click(object sender, EventArgs e)
        {
            bool any = false;
                foreach (ListItem li in cbl_States.Items)
                {
                if (li.Selected)
                    any = true;
                }

                if(any)
                foreach (ListItem li in cbl_States.Items)
                {
                    li.Selected = false;
                }
                else
                foreach (ListItem li in cbl_States.Items)
                {
                    li.Selected = true;
                }
        }
    }
}