using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class StaffHeuristic
    {
        Staff staff;
        int heuristic;
        bool possibleCanditate;

        public StaffHeuristic(Staff staff)
        {
            this.staff = staff; 
            this.heuristic = 0;
            this.possibleCanditate = true;
        }

        public Staff Staff
        {
            get
            {
                return staff;
            }

            set
            {
                staff = value;
            }
        }

        public int Heuristic
        {
            get
            {
                return heuristic;
            }

            set
            {
                heuristic = value;
            }
        }

        public bool PossibleCanditate
        {
            get
            {
                return possibleCanditate;
            }

            set
            {
                possibleCanditate = value;
            }
        }
    }
}