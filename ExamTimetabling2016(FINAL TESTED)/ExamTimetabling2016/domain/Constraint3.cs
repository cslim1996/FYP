using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class Constraint3
    {
        InvigilationDuty invigilationDuty;
        Staff invigilator;
        Examination examination;

        public Constraint3()
        {
            this.invigilationDuty = new InvigilationDuty();
            this.invigilator = new Staff();
            this.examination = new Examination(); 
        }

        public Constraint3(InvigilationDuty invigilationDuty, Staff invigilator, Examination examination)
        {
            this.invigilationDuty = invigilationDuty;
            this.invigilator = invigilator;
            this.examination = examination;
        }
        

        public InvigilationDuty InvigilationDuty
        {
            get
            {
                return invigilationDuty;
            }

            set
            {
                invigilationDuty = value;
            }
        }

        public Staff Invigilator
        {
            get
            {
                return invigilator;
            }

            set
            {
                invigilator = value;
            }
        }

        public Examination Examination
        {
            get
            {
                return examination;
            }

            set
            {
                examination = value;
            }
        }
    }
}