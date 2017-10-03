using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainCourseControl
    {
        private CourseDA courseDA;

        public MaintainCourseControl()
        {
            courseDA = new CourseDA();
        }

        public string getCourseTitle(string courseCode)
        {
            return courseDA.getCourseTitle(courseCode);
        }

        public List<Course> searchCoursesList(DateTime date, string time, string venueID)
        {
            return courseDA.searchCoursesList(date, time, venueID);
        }

        public List<string> searchCourseCodesExaminedList(DateTime date, string session, bool isMainPaperOnly)
        {
            return courseDA.searchCourseCodesExaminedList(date, session, isMainPaperOnly);
        }

        public List<string> searchFacultyCodesList(List<Course> CoursesList)
        {
            return courseDA.searchFacultyCodesList(CoursesList);
        }

        public void shutDown()
        {
            courseDA.shutDown();
        }
    }
}