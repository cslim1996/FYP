using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainExaminationControl
    {
        private ExaminationDA examinationDA;

        public MaintainExaminationControl()
        {
            examinationDA = new ExaminationDA();
        }

        public List<Examination> searchExamination(string timeslotID)
        {
            return examinationDA.searchExamination(timeslotID);
        }

        public List<string> getTimeslot()
        {
            return examinationDA.getTimeslot();
        }

        // cs addition
        public List<Examination> getExaminationList()
        {
            return examinationDA.getExaminationList();
        }

        public void shutDown()
        {
            examinationDA.shutDown();
        }
    }
}