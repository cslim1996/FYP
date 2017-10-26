using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class Examination
    {
        private string timeslotID;
        private string venueID;
        private string courseCode;
        private string programmeCode; 
        private char paperType;
        private char examType;     
        private int year;  
        private int sitFrom;
        private int sitTo;
        private Faculty facultyCode;

        public Examination(string timeslotID, string venueID, string courseCode, string programmeCode, char paperType, char examType, int year, int sitFrom, int sitTo)
        {
            MaintainFacultyControl mFacultyControl = new MaintainFacultyControl();   
            this.timeslotID = timeslotID;
            this.venueID = venueID;
            this.courseCode = courseCode;
            this.programmeCode = programmeCode;
            this.paperType = paperType;
            this.examType = examType;
            this.year = year;
            this.sitFrom = sitFrom;
            this.sitTo = sitTo;
            this.facultyCode = mFacultyControl.searchFacultyByCourseCode(CourseCode);
                    }
        
        public Examination()
        {
            this.timeslotID = null;
            this.venueID = null;
            this.courseCode = null;
            this.programmeCode = null;
            this.paperType = '\0';
            this.examType = '\0';
            this.year = 0;
            this.sitFrom = 0;
            this.sitTo = 0;
            this.facultyCode = null;
        }

        public string TimeslotID
        {
            get { return timeslotID; }
            set { timeslotID = value; }
        }

        public string VenueID
        {
            get { return venueID; }
            set { venueID = value; }
        }

        public string CourseCode
        {
            get { return courseCode; }
            set { courseCode = value; }
        }

        public string ProgrammeCode
        {
            get { return programmeCode; }
            set { programmeCode = value; }
        }

        public char PaperType
        {
            get { return paperType; }
            set { paperType = value; }
        }

        public char ExamType
        {
            get { return examType; }
            set { examType = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public int SitFrom
        {
            get { return sitFrom; }
            set { sitFrom = value; }
        }

        public int SitTo
        {
            get { return sitTo; }
            set { sitTo = value; }
        }



        public Faculty FacultyCode
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
    }
}