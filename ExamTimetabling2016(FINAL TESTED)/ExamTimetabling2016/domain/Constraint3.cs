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
        bool? isCnblPaper;
        bool? isDoubleSeating;
        int minSatSession;
        int maxSatSesssion;
        int minReliefSession;
        int maxReliefSession;
        int minExtraSession;
        int maxExtraSession;
        bool? hasOtherDutyOnSameDay;
        bool? hasMorningDutyOnSameDay;

        public Constraint3()
        {
            this.invigilationDuty = new InvigilationDuty();
            this.invigilator = new Staff();
            this.examination = new Examination();
            this.isHardConstraint = false;
            this.isCnblPaper = null;
            this.IsDoubleSeating = null;
            this.minExtraSession = 0;
            this.maxExtraSession = 0;
            this.minReliefSession = 0;
            this.maxReliefSession = 0;
            this.MinExtraSession = 0;
            this.MaxExtraSession = 0;
            this.hasOtherDutyOnSameDay = null;
            this.hasMorningDutyOnSameDay = null;
            
        }

        public Constraint3(InvigilationDuty invigilationDuty, Staff invigilator, Examination examination, bool? isHardConstraint , bool? isCnblPaper, bool? isDoubleSeating)
        {
            this.invigilationDuty = invigilationDuty;
            this.invigilator = invigilator;
            this.examination = examination;
            this.isHardConstraint = isHardConstraint;
            this.isCnblPaper = isCnblPaper;
            this.isDoubleSeating = isDoubleSeating;
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

        public bool? IsCnblPaper
        {
            get
            {
                return isCnblPaper;
            }

            set
            {
                isCnblPaper = value;
            }
        }

        public bool? IsDoubleSeating
        {
            get
            {
                return isDoubleSeating;
            }

            set
            {
                isDoubleSeating = value;
            }
        }

        public int MinSatSession
        {
            get
            {
                return minSatSession;
            }

            set
            {
                minSatSession = value;
            }
        }

        public int MaxSatSesssion
        {
            get
            {
                return maxSatSesssion;
            }

            set
            {
                maxSatSesssion = value;
            }
        }

        public int MinReliefSession
        {
            get
            {
                return minReliefSession;
            }

            set
            {
                minReliefSession = value;
            }
        }

        public int MaxReliefSession
        {
            get
            {
                return maxReliefSession;
            }

            set
            {
                maxReliefSession = value;
            }
        }

        public int MinExtraSession
        {
            get
            {
                return minExtraSession;
            }

            set
            {
                minExtraSession = value;
            }
        }

        public int MaxExtraSession
        {
            get
            {
                return maxExtraSession;
            }

            set
            {
                maxExtraSession = value;
            }
        }

        public bool? HasOtherDutyOnSameDay
        {
            get
            {
                return hasDutyOnSameDay;
            }

            set
            {
                hasDutyOnSameDay = value;
            }
        }

        public bool? HasMorningDutyOnSameDay
        {
            get
            {
                return hasMorningDutyOnSameDay;
            }

            set
            {
                hasMorningDutyOnSameDay = value;
            }
        }
    }
}