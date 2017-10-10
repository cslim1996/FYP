using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class TimeslotVenue
    {
        private string timeslotID;
        private string venueID;
        private int noOfInvigilatorRequired;
        private List<Course> courseList;
        private List<Staff> invigilatorList;

        public TimeslotVenue(string timeslotID, string venueID, int noOfInvigilatorRequired, List<Course> courseList)
        {
            this.timeslotID = timeslotID;
            this.venueID = venueID;
            this.noOfInvigilatorRequired = noOfInvigilatorRequired;
            this.courseList = courseList;
            this.InvigilatorList = new List<Staff>();
        }

        public string TimeslotID
        {
            get
            {
                return timeslotID;
            }

            set
            {
                timeslotID = value;
            }
        }

        public string VenueID
        {
            get
            {
                return venueID;
            }

            set
            {
                venueID = value;
            }
        }

        public int NoOfInvigilatorRequired
        {
            get
            {
                return noOfInvigilatorRequired;
            }

            set
            {
                noOfInvigilatorRequired = value;
            }
        }
        
        public List<Course> CourseList
        {
            get
            {
                return courseList;
            }

            set
            {
                courseList = value;
            }
        }

        public List<Staff> InvigilatorList
        {
            get
            {
                return invigilatorList;
            }

            set
            {
                invigilatorList = value;
            }
        }
    }
}