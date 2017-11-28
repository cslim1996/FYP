using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class ConstraintSetting
    {
        bool assignToExaminer;
        int maxExtraSession;
        int maxReliefSession;
        int maxSaturdaySession;
        int maxEveningSession;
        int dayOfExemptionForExaminer;
        int maxInvigilatorAssignToOwnFaculty;
        int maxConsecutiveDayDuty;

        public ConstraintSetting()
        {
            this.assignToExaminer = false;
            this.maxEveningSession = 1;
            this.maxExtraSession = 1;
            this.maxSaturdaySession = 1;
            this.maxReliefSession = 1;
            this.dayOfExemptionForExaminer = 0;
            this.maxInvigilatorAssignToOwnFaculty = 0;
            this.maxConsecutiveDayDuty = 0;
        }

        public ConstraintSetting(bool assignToExaminer, int maxExtraSession, int maxReliefSession, int maxSaturdaySession, int maxEveningSession)
        {
            this.assignToExaminer = assignToExaminer;
            this.maxExtraSession = maxExtraSession;
            this.maxReliefSession = maxReliefSession;
            this.maxSaturdaySession = maxSaturdaySession;
            this.maxEveningSession = maxEveningSession;
        }

        public ConstraintSetting(bool assignToExaminer, int maxExtraSession, int maxReliefSession, int maxSaturdaySession, int maxEveningSession, int dayOfExemptionForExaminer, int maxInvigilatorAssignToOwnFaculty, int maxConsecutiveDayDuty)
        {
            this.assignToExaminer = assignToExaminer;
            this.maxExtraSession = maxExtraSession;
            this.maxReliefSession = maxReliefSession;
            this.maxSaturdaySession = maxSaturdaySession;
            this.maxEveningSession = maxEveningSession;
            this.dayOfExemptionForExaminer = dayOfExemptionForExaminer;
            this.maxInvigilatorAssignToOwnFaculty = maxInvigilatorAssignToOwnFaculty;
            this.maxConsecutiveDayDuty = maxConsecutiveDayDuty;
        }

        public bool AssignToExaminer
        {
            get
            {
                return assignToExaminer;
            }

            set
            {
                assignToExaminer = value;
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

        public int MaxSaturdaySession
        {
            get
            {
                return maxSaturdaySession;
            }

            set
            {
                maxSaturdaySession = value;
            }
        }

        public int MaxEveningSession
        {
            get
            {
                return maxEveningSession;
            }

            set
            {
                maxEveningSession = value;
            }
        }

        public int DayOfExemptionForExaminer
        {
            get
            {
                return dayOfExemptionForExaminer;
            }

            set
            {
                dayOfExemptionForExaminer = value;
            }
        }

        public int MaxInvigilatorAssignToOwnFaculty
        {
            get
            {
                return maxInvigilatorAssignToOwnFaculty;
            }

            set
            {
                maxInvigilatorAssignToOwnFaculty = value;
            }
        }

        public int MaxConsecutiveDayDuty
        {
            get
            {
                return maxConsecutiveDayDuty;
            }

            set
            {
                maxConsecutiveDayDuty = value;
            }
        }
    }
}