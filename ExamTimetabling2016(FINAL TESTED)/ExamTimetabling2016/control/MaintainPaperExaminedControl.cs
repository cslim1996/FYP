using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainPaperExaminedControl
    {
        private PaperExaminedDA paperExaminedDA;

        public MaintainPaperExaminedControl()
        {
            paperExaminedDA = new PaperExaminedDA();
        }

        public List<PaperExamined> getPaperExaminedList(string staffID)
        {
            return paperExaminedDA.getPaperExaminedList(staffID);
        }

        public List<string> searchPaperExamined(string location, string timeslotID)
        {
            return paperExaminedDA.searchPaperExamined(location, timeslotID);
        }

        public List<string> searchPaperExaminedByStaffID(string staffID)
        {
            return paperExaminedDA.searchPaperExaminedByStaffID(staffID);
        }

        public void shutDown()
        {
            paperExaminedDA.shutDown();
        }
    }
}