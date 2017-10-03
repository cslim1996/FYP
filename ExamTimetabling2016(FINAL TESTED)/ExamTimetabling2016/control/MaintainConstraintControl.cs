using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTimetabling2016
{
    class MaintainConstraintControl
    {
        private ConstraintDA constraintDA;
        public MaintainConstraintControl()
        {
            constraintDA = new ConstraintDA();
        }
        public bool constraintValidation(string[] variable, double[] input)
        {
            return constraintDA.constraintValidation(variable, input);
        }
        public double invigilatorRequiredConstraint(string[] variable, double[] input)
        {
            return constraintDA.invigilatorRequiredConstraint(variable, input);
        }
        public double convertDayOfWeek(DateTime dayOfWeek)
        {
            return constraintDA.convertDayOfWeek(dayOfWeek);
        }
        public double convertMuslim(bool muslim)
        {
            return constraintDA.convertMuslim(muslim);
        }
        public double convertPeriod(string period)
        {
            return constraintDA.convertPeriod(period);
        }
        public double convertGender(char gender)
        {
            return constraintDA.convertGender(gender);
        }
    }
}
