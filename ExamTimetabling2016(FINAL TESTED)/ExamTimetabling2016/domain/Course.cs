using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class Course
    {
        private string courseCode;
        private string courseTitle;
        private int duration;
        private bool isDoubleSeating;
        private bool isCnblPaper;
        private List<Programme> programmeList;

        public Course()
        {
            new Course("", "", 0, false, false, new List<Programme>());
        }

        public Course(string courseCode, string courseTitle, int duration, bool isDoubleSeating, bool isCnblPaper, List<Programme> programmeList)
        {
            this.courseCode = courseCode;
            this.courseTitle = courseTitle;
            this.duration = duration;
            this.isDoubleSeating = isDoubleSeating;
            this.isCnblPaper = isCnblPaper;
            this.programmeList = programmeList;
        }

        public Course(string courseCode, string courseTitle)
        {
            this.courseCode = courseCode;
            this.courseTitle = courseTitle;
        }

        public string CourseTitle
        {
            get { return courseTitle; }
            set { courseTitle = value; }
        }

        public string CourseCode
        {
            get { return courseCode; }
            set { courseCode = value; }
        }


        public int Duration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
            }
        }

        public bool IsDoubleSeating
        {
            get
            {
                return isDoubleSeating;
            }

            set
            {
                isDoubleSeating = value;
            }
        }

        public bool IsCnblPaper
        {
            get
            {
                return isCnblPaper;
            }

            set
            {
                isCnblPaper = value;
            }
        }

        internal List<Programme> ProgrammeList
        {
            get
            {
                return programmeList;
            }

            set
            {
                programmeList = value;
            }
        }
    }
}