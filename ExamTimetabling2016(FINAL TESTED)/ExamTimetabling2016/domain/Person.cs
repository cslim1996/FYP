using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTimetabling2016
{
    public class Person
    {
        private string name;
        private char gender;
        private bool isMuslim;

        public Person(string name, char gender, bool isMuslim)
        {
            this.name = name;
            this.gender = gender;
            this.isMuslim = isMuslim;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public char Gender
        {
            get
            {
                return gender;
            }

            set
            {
                gender = value;
            }
        }

        public bool IsMuslim
        {
            get
            {
                return isMuslim;
            }

            set
            {
                isMuslim = value;
            }
        }
    }
}
