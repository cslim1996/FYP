using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class Constraint2
    {
        private string invigilatorQuery;
        private string examQuery;
        private string conditionQuery;
        private char isCondition;
        private List<Examination> examList;
        private List<Staff> staffList;

        public Constraint2(string invigilatorQuery, string examQuery, string conditionQuery, char isCondition)
        {
            this.invigilatorQuery = invigilatorQuery;
            this.examQuery = examQuery;
            this.conditionQuery = conditionQuery;
            this.isCondition = isCondition;
            this.staffList = null;
            this.examList = null;
        }

        public string InvigilatorQuery
        {
            get
            {
                return invigilatorQuery;
            }

            set
            {
                invigilatorQuery = value;
            }
        }

        public string ExamQuery
        {
            get
            {
                return examQuery;
            }

            set
            {
                examQuery = value;
            }
        }

        public string ConditionQuery
        {
            get
            {
                return conditionQuery;
            }

            set
            {
                conditionQuery = value;
            }
        }

        public char IsCondition
        {
            get
            {
                return isCondition;
            }

            set
            {
                isCondition = value;
            }
        }

        public List<Examination> ExamList
        {
            get
            {
                return examList;
            }

            set
            {
                examList = value;
            }
        }

        public List<Staff> StaffList
        {
            get
            {
                return staffList;
            }

            set
            {
                staffList = value;
            }
        }
    }
}