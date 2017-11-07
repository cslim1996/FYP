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
        private int invigilatorAssignedToOwnFacultyDutyCount;

        public Faculty()
        {
            this.facultyFullName = null;
            this.facultyCode = '\0';
            this.facultyName = null;
            this.facultyDutyCount = 0;
            this.invigilatorAssignedToOwnFacultyDutyCount = 0;
        }


        public Faculty(char facultyCode, string facultyName, string facultyFullName)
        {
            this.facultyName = facultyName;
            this.facultyCode = facultyCode;
            this.facultyFullName = facultyFullName;
            this.facultyDutyCount = 0;
            this.invigilatorAssignedToOwnFacultyDutyCount = 0;
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

        public int InvigilatorAssignedToOwnFacultyDutyCount
        {
            get
            {
                return invigilatorAssignedToOwnFacultyDutyCount;
            }

            set
            {
                invigilatorAssignedToOwnFacultyDutyCount = value;
            }
        }

        public int PercentageOfInvigilatorAssignedToOwnFacultyDuty()
        {
            double result =0;

            result = (invigilatorAssignedToOwnFacultyDutyCount / facultyDutyCount) * 100;

            return (int) result;
        }
    }

}