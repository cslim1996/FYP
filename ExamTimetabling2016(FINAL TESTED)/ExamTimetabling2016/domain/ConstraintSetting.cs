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

        public ConstraintSetting()
        {
            this.assignToExaminer = false;
            this.maxEveningSession = 1;
            this.maxExtraSession = 1;
            this.maxSaturdaySession = 1;
            this.maxReliefSession = 1;
        }

        public ConstraintSetting(bool assignToExaminer, int maxExtraSession, int maxReliefSession, int maxSaturdaySession, int maxEveningSession)
        {
            this.assignToExaminer = assignToExaminer;
            this.maxExtraSession = maxExtraSession;
            this.maxReliefSession = maxReliefSession;
            this.maxSaturdaySession = maxSaturdaySession;
            this.maxEveningSession = maxEveningSession;
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
    }
}