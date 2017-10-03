using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class PaperExamined
    {
        private string staffID;
        private string courseCode;

        public PaperExamined(string staffID, string courseCode)
        {
            this.staffID = staffID;
            this.courseCode = courseCode;
        }

        public string StaffID
        {
            get { return staffID; }
            set { staffID = value; }
        }

        public string CourseCode
        {
            get { return courseCode; }
            set { courseCode = value; }
        }
    }
}