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
            this.noOfInvigilatorRequired = 0;
            this.invigilatorList = new List<Staff>();
            this.courseList = new List<Course>();
        }
        
        public TimeslotVenue()
        {
            this.timeslotID = null;
            this.date = new DateTime();
            this.venueID = null;
            this.session = null;
            this.location = null;
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

        public bool InvigilatorListHasMale(List<Staff> invigilatorList)
        {
            bool result = false;

            foreach (Staff staff in invigilatorList)
            {
                if (staff.Gender.Equals('M'))
                {
                    result = true;
                }
            }

            return result;
        }

        public bool InvigilatorListHasFemale(List<Staff> invigilatorList)
        {
            bool result = false;

            foreach (Staff staff in invigilatorList)
            {
                if (staff.Gender.Equals('F'))
                {
                    result = true;
                }
            }

            return result;
        }

        public int NoOfExperienceInvigilator(List<Staff> invigilatorList)
        {
            int result = 0;

            foreach (Staff staff in invigilatorList)
            {
                if (staff.IsInviAbove2Years.Equals(true))
                    result++;
            }
            return result;
        }

        public int percentageOfExperiencedInvigilator(List<Staff> invigilatorList,int noOfInvigilatorRequired)
        {
            double result = 0;
            double experiencedInvigilatorCount = 0;
            foreach (Staff invigilator in invigilatorList)
            {
                if (invigilator.IsInviAbove2Years.Equals(true))
                    experiencedInvigilatorCount++;
            }

            result = (experiencedInvigilatorCount / noOfInvigilatorRequired) * 100;

            return (int)result;
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