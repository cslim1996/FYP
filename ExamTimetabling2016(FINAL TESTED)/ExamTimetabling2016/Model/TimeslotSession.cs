using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_timetabling.classes
{
    public class TimeslotSession
    {
        public string timeslot;
        public string session; 

        public TimeslotSession(){

        }

        public TimeslotSession(String timeslot, String session)
        {
            this.timeslot = timeslot;
            this.session = session;
        }

        public String getTimeslot()
        {
            return timeslot;
        }

        public void setTimeslot(String timeslot)
        {
            this.timeslot = timeslot;
        }

        public String getSession()
        {
            return session;
        }

        public void setSession(String session)
        {
            this.session = session;
        }
    }
}