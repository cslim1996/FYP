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
        bool? isHardConstraint;

        public Constraint3()
        {
            this.invigilationDuty = new InvigilationDuty();
            this.invigilator = new Staff();
            this.examination = new Examination();
            this.isHardConstraint = false;
            
        }

        public Constraint3(InvigilationDuty invigilationDuty, Staff invigilator, Examination examination, bool? isHardConstraint)
        {
            this.invigilationDuty = invigilationDuty;
            this.invigilator = invigilator;
            this.examination = examination;
            this.isHardConstraint = isHardConstraint;
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

        public bool? IsHardConstraint
        {
            get
            {
                return isHardConstraint;
            }

            set
            {
                isHardConstraint = value;
            }
        }
    }
}