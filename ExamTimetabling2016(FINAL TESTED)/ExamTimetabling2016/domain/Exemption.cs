using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class Exemption
    {
        private DateTime date;
        private string session;

        public Exemption(DateTime date, string session)
        {
            this.date = date;
            this.session = session;
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Session
        {
            get { return session; }
            set { session = value; }
        }
    }
}