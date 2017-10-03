using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class InvigilationDuty
    {
        private DateTime date;
        private string session;
        private string timeslotID;
        private string venueID;
        private string location;
        private string staffID;
        private string categoryOfInvigilator;
        private int duration;

        public InvigilationDuty()
        {
            new InvigilationDuty(new DateTime(), "", "", "", "", 0);
        }

        public InvigilationDuty(DateTime date, string session, string venueID, string location, string categoryOfInvigilator, int duration)
        {
            this.date = date;
            this.session = session;
            this.venueID = venueID;
            this.location = location;
            this.categoryOfInvigilator = categoryOfInvigilator;
            this.duration = duration;
        }

        public InvigilationDuty(string timeslotID, string venueID, string location, string staffID, string categoryOfInvigilator, int duration)
        {
            this.timeslotID = timeslotID;
            this.venueID = venueID;
            this.location = location;
            this.staffID = staffID;
            this.categoryOfInvigilator = categoryOfInvigilator;
            this.duration = duration;
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

        public string StaffID
        {
            get
            {
                return staffID;
            }

            set
            {
                staffID = value;
            }
        }

        public string CategoryOfInvigilator
        {
            get
            {
                return categoryOfInvigilator;
            }

            set
            {
                categoryOfInvigilator = value;
            }
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