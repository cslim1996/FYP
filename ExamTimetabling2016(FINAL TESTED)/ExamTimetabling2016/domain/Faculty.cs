using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class Faculty
    {
        private string facultyName;
        private char facultyCode;
        private string facultyFullName;
        private int facultyDutyCount;

        public Faculty()
        {
            this.facultyFullName = "";
            this.facultyCode = '\0';
            this.facultyName = "";
            this.facultyDutyCount = 0;
        }


        public Faculty(char facultyCode, string facultyName, string facultyFullName)
        {
            this.facultyName = facultyName;
            this.facultyCode = facultyCode;
            this.facultyFullName = facultyFullName;
            this.facultyDutyCount = 0;
        }

        public string FacultyName
        {
            get
            {
                return facultyName;
            }

            set
            {
                facultyName = value;
            }
        }

        public char FacultyCode
        {
            get
            {
                return facultyCode;
            }

            set
            {
                facultyCode = value;
            }
        }

        public string FacultyFullName
        {
            get
            {
                return facultyFullName;
            }

            set
            {
                facultyFullName = value;
            }
        }

        public int FacultyDutyCount
        {
            get
            {
                return facultyDutyCount;
            }

            set
            {
                facultyDutyCount = value;
            }
        }
    }

}