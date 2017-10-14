using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainFacultyControl
    {
        private FacultyDA facultyDA;

        public MaintainFacultyControl()
        {
            facultyDA = new FacultyDA();
        }

        public Faculty searchFacultyByCourseCode(string courseCode)
        {
            return facultyDA.getFacultyByCourseCode(courseCode);
        }

        public List<Faculty> getFacultyList()
        {
            return facultyDA.getFacultyList();
        }

        public void shutDown()
        {
            facultyDA.shutDown();
        }
    }
}