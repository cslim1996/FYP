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
        int constraintImportanceValue;
        bool? hasOtherDutyOnSameDay;
        bool? hasSpecificSessionDutyOnSameDay;
        bool? hasSpecificDurationDutyOnSameDay;
        string hasSpecificSessionDutyOnSameDayString;
        int hasSpecificDurationDutyOnSameDayInt;
        string dayOfWeek;

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
            this.minExtraSession = 0;
            this.maxExtraSession = 0;
            this.hasOtherDutyOnSameDay = null;
            this.hasSpecificDurationDutyOnSameDay = null;
            this.hasSpecificDurationDutyOnSameDayInt = 0;
            this.hasSpecificSessionDutyOnSameDay = null;
            this.hasSpecificSessionDutyOnSameDayString = null;
            this.dayOfWeek = "";
            this.constraintImportanceValue = 1;
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
                return hasOtherDutyOnSameDay;
            }

            set
            {
                hasOtherDutyOnSameDay = value;
            }
        }

        public int ConstraintImportanceValue
        {
            get
            {
                return constraintImportanceValue;
            }

            set
            {
                constraintImportanceValue = value;
            }
        }

        public bool? HasSpecificSessionDutyOnSameDay
        {
            get
            {
                return hasSpecificSessionDutyOnSameDay;
            }

            set
            {
                hasSpecificSessionDutyOnSameDay = value;
            }
        }

        public bool? HasSpecificDurationDutyOnSameDay
        {
            get
            {
                return hasSpecificDurationDutyOnSameDay;
            }

            set
            {
                hasSpecificDurationDutyOnSameDay = value;
            }
        }

        public string HasSpecificSessionDutyOnSameDayString
        {
            get
            {
                return hasSpecificSessionDutyOnSameDayString;
            }

            set
            {
                hasSpecificSessionDutyOnSameDayString = value;
            }
        }

        public int HasSpecificDurationDutyOnSameDayInt
        {
            get
            {
                return hasSpecificDurationDutyOnSameDayInt;
            }

            set
            {
                hasSpecificDurationDutyOnSameDayInt = value;
            }
        }

        public string DayOfWeek
        {
            get
            {
                return dayOfWeek;
            }

            set
            {
                dayOfWeek = value;
            }
        }
    }
}