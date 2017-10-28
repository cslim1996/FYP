using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class TimeslotVenue
    {
        private string timeslotID;
        private DateTime date;
        private string session;
        private string venueID;
        private int noOfInvigilatorRequired;
        private List<Course> courseList;
        private List<Staff> invigilatorList;
        private string location;
        private int duration;

        public TimeslotVenue(string timeslotID, string venueID, DateTime date, string session, int noOfInvigilatorRequired, List<Course> courseList)
        {
            this.timeslotID = timeslotID;
            this.venueID = venueID;
            this.noOfInvigilatorRequired = noOfInvigilatorRequired;
            this.courseList = courseList;
            this.InvigilatorList = new List<Staff>();
            this.date = date;
            this.session = session;
        }
        
        //for chief
        public TimeslotVenue(string timeslotID, string location, DateTime date,string session, int noOfChiefInvigilatorRequired, int duration)
        {
            this.venueID = "";
            this.timeslotID = timeslotID;
            this.location = location;
            this.date = date;
            this.session = session;
            this.NoOfInvigilatorRequired = NoOfInvigilatorRequired;
            this.duration = duration;
        }
        
        //for relief
        public TimeslotVenue(string timeslotID, DateTime date, string session, string location)
        {
            this.timeslotID = timeslotID;
            this.date = date;
            this.venueID = "";
            this.session = session;
            this.location = location;
            this.duration = 0;
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

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public string Session
        {
            get
            {
                return session;
            }

            set
            {
                session = value;
            }
        }

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        public bool InvigilatorListHasMale()
        {
            bool result = false;

            foreach (Staff staff in this.invigilatorList)
            {
                if (staff.Gender.Equals('M'))
                {
                    result = true;
                }
            }

            return result;
        }

        public bool InvigilatorListHasFemale()
        {
            bool result = false;

            foreach (Staff staff in this.invigilatorList)
            {
                if (staff.Gender.Equals('F'))
                {
                    result = true;
                }
            }

            return result;
        }

        public int NoOfExperienceInvigilator()
        {
            int result = 0;

            foreach (Staff staff in this.invigilatorList)
            {
                if (staff.IsInviAbove2Years.Equals(true))
                    result++;
            }
            return result;
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
    }
}