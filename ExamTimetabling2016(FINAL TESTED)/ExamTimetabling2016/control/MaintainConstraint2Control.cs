using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainConstraint2Control
    {
        private Constraint2DA constraint2DA;

        public MaintainConstraint2Control()
        {
            constraint2DA = new Constraint2DA();
        }

        public List<Constraint2> getConstraintList()
        {
            return constraint2DA.getConstraintList();
        }

        public void shutDown()
        {
            constraint2DA.shutDown();
        }
    }
}