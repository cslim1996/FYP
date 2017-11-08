using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class Constraint3
    {
        int constraintID;
        InvigilationDuty invigilationDuty;
        Staff invigilator;
        Examination examination;
        bool? isHardConstraint;
        bool? isCnblPaper;
        bool? isDoubleSeating;
        int minExperiencedInvigilator;//
        int constraintImportanceValue;
        bool? hasOtherDutyOnSameDay;
        bool? hasSpecificSessionDutyOnSameDay;
        bool? hasSpecificDurationDutyOnSameDay;
        bool? hasSpecificSessionAndDurationDutyOnSameDay;
        string hasSpecificSessionDutyOnSameDayString;
        int hasSpecificDurationDutyOnSameDayInt;
        string dayOfWeek;
        bool? assignExaminerToPaper;//
        int percentageOfInvigilatorAssignedToTheirOwnFacultyDuty;//
        string remark;

        public Constraint3()
        {
            this.invigilationDuty = new InvigilationDuty();
            this.invigilator = new Staff();
            this.examination = new Examination();
            this.isHardConstraint = false;
            this.isCnblPaper = null;
            this.IsDoubleSeating = null;
            this.hasOtherDutyOnSameDay = null;
            this.hasSpecificDurationDutyOnSameDay = null;
            this.hasSpecificDurationDutyOnSameDayInt = 0;
            this.hasSpecificSessionDutyOnSameDay = null;
            this.hasSpecificSessionDutyOnSameDayString = "";
            this.dayOfWeek = "";
            this.constraintImportanceValue = 1;
            this.assignExaminerToPaper = null;
            this.minExperiencedInvigilator = 0;
            this.hasSpecificSessionAndDurationDutyOnSameDay = null;
            this.percentageOfInvigilatorAssignedToTheirOwnFacultyDuty = 0;
            this.remark = "";
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

        public bool? AssignExaminerToPaper
        {
            get
            {
                return assignExaminerToPaper;
            }

            set
            {
                assignExaminerToPaper = value;
            }
        }

        public int MinExperiencedInvigilator
        {
            get
            {
                return minExperiencedInvigilator;
            }

            set
            {
                minExperiencedInvigilator = value;
            }
        }

        public bool? HasSpecificSessionAndDurationDutyOnSameDay
        {
            get
            {
                return hasSpecificSessionAndDurationDutyOnSameDay;
            }

            set
            {
                hasSpecificSessionAndDurationDutyOnSameDay = value;
            }
        }

        public int PercentageOfInvigilatorAssignedToTheirOwnFacultyDuty
        {
            get
            {
                return percentageOfInvigilatorAssignedToTheirOwnFacultyDuty;
            }

            set
            {
                percentageOfInvigilatorAssignedToTheirOwnFacultyDuty = value;
            }
        }

        public int ConstraintID
        {
            get
            {
                return constraintID;
            }

            set
            {
                constraintID = value;
            }
        }

        public string Remark
        {
            get
            {
                return remark;
            }

            set
            {
                remark = value;
            }
        }
    }
}