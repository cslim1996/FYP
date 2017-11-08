using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainConstraint3Control
    {
        private Constraint3DA constraint;

        public MaintainConstraint3Control()
        {
            constraint = new Constraint3DA();
        }

        public void insertConstraintIntoDatabase(Constraint3 constraints)
        {
            constraint.insertConstraintIntoDatabase(constraints);
        }

        public List<Constraint3> loadFullConstraintList()
        {
            return constraint.loadFullConstraintList();
        }

        public void shutDown()
        {
            constraint.shutDown();
        }
    }

}