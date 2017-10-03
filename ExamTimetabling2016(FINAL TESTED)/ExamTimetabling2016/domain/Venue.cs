using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTimetabling2016
{
    public class Venue
    {
        private string venueID;
        private List<Course> coursesList;
        private List<Staff> invigilatorsList;

        public Venue()
        {
            new Venue("", new List<Course>(), new List<Staff>());
        }

        public Venue(string venueID, List<Course> coursesList, List<Staff> invigilatorsList)
        {
            this.venueID = venueID;
            this.coursesList = coursesList;
            this.invigilatorsList = invigilatorsList;
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

        internal List<Course> CoursesList
        {
            get
            {
                return coursesList;
            }

            set
            {
                coursesList = value;
            }
        }

        internal List<Staff> InvigilatorsList
        {
            get
            {
                return invigilatorsList;
            }

            set
            {
                invigilatorsList = value;
            }
        }
    }
}
