﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class InvigilatorHeuristic
    {
        Staff staff;
        int heuristic;
        bool? possibleCanditate;

        public InvigilatorHeuristic()
        {
            this.staff = new Staff();
            this.heuristic = 0;
            this.possibleCanditate = null;
        }

        public InvigilatorHeuristic(Staff staff)
        {
            this.staff = staff; 
            this.heuristic = 0;
            this.possibleCanditate = null;
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

        public bool? PossibleCanditate
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

        public string toString()
        {
            return this.staff.StaffID +", " + this.heuristic + ", " + this.possibleCanditate + "\n";
        }
    }
}