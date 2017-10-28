using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ExamTimetabling2016.CSTEST
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string invigilatorQuery = "Select * from ";
        string timetableQuery = "";

        StaffDA staffs = new StaffDA();
        TimetableDA timetables = new TimetableDA();
        ExaminationDA exams = new ExaminationDA();

        protected void Page_Load(object sender, EventArgs e)
        {

            string x = "";
            if(x == "")
            Label1.Text = "wtf";

        }

        //handle overlapping
        public void method1(string invigilatorQuery, string timetableQuery)
        {
            //initialize staff and timetable list
            List<Staff> staffList = new List<Staff>();
            List<Examination> examList = new List<Examination>();
            List<Timetable> timetableList = new List<Timetable>();

            // DA for staff and timetable

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string query = "Select StaffID, Title, Name, FacultyCode, isChiefInvi, isInvi from dbo.Staff order by Name";
            string result= "";
            string queryx = "Select * from dbo.Examination where venueid= 'M2'";
            /*
            List<Staff> staffList = staffs.searchLecturerByQuery(query);
            foreach(Staff stf in staffList)
            {
                result = result + stf.Name +"\n";
            }*/
            
           List<Examination> exam = exams.searchExamByQuery(queryx);
            //List<Examination> exam = exams.getExaminationList();
            foreach (Examination exm in exam)
            {
                result = result + exm.TimeslotID + "\n";
            }
            
            Label1.Text = result;
        }
    }
}