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
        private int invigilatorRequired;

        public Examination(string timeslotID, string venueID, string courseCode, string programmeCode, char paperType, char examType, int year, int sitFrom, int sitTo)
        {
            this.timeslotID = timeslotID;
            this.venueID = venueID;
            this.courseCode = courseCode;
            this.programmeCode = programmeCode;
            this.paperType = paperType;
            this.examType = examType;
            this.year = year;
            this.sitFrom = sitFrom;
            this.sitTo = sitTo;
            this.invigilatorRequired = 0; 
        }

        public int InvigilatorRequired
        {
            get { return invigilatorRequired; }
            set { invigilatorRequired = value; }
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
    }
}