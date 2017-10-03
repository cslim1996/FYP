using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP.Venue_Maintenance
{
    public partial class AddBlock : System.Web.UI.Page
    {
        static string strCon = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
        public SqlConnection con = new SqlConnection(strCon);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmdInsert = new SqlCommand("Insert into Block values(@blockCode,@totalFloor,@campus)", con);
            cmdInsert.Parameters.AddWithValue("@blockCode", txt_BlockCode.Text);
            cmdInsert.Parameters.AddWithValue("@totalFloor", txt_Floor.Text);
            cmdInsert.Parameters.AddWithValue("@campus", ddl_Campus.SelectedValue);

            cmdInsert.ExecuteNonQuery();
            con.Close();


            clearFields();
        }

        private void clearFields()
        {
            txt_Floor.Text = "";
            txt_BlockCode.Text = "";
            ddl_Campus.ClearSelection();
        }
    }
}