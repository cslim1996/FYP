using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTimetabling2016
{
    public class Timetable
    {
        private DateTime date;
        private string session;
        private List<Block> blocksList;
        private List<Staff> reliefInvigilatorsList;

        public Timetable()
        {
            new Timetable(new DateTime(), "", new List<Block>(), new List<Staff>());
        }

        public Timetable(DateTime date, string session, List<Block> blocksList, List<Staff> reliefInvigilatorsList)
        {
            this.date = date;
            this.session = session;
            this.blocksList = blocksList;
            this.reliefInvigilatorsList = reliefInvigilatorsList;
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

        internal List<Block> BlocksList
        {
            get
            {
                return blocksList;
            }

            set
            {
                blocksList = value;
            }
        }

        internal List<Staff> ReliefInvigilatorsList
        {
            get
            {
                return reliefInvigilatorsList;
            }

            set
            {
                reliefInvigilatorsList = value;
            }
        }

        override
        public string ToString()
        {
            return this.date + "\n" + this.session;
        }
    }
}
