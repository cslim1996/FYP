using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainStaffControl
    {
        private StaffDA staffDA;

        public MaintainStaffControl(){
            staffDA = new StaffDA();
        }

        public List<Staff> getInvigilatorList()
        {
            return staffDA.getInvigilatorList();
        }

        public Staff getStaffName(string staffID)
        {
            return staffDA.getStaffName(staffID);
        }

        public List<Staff> getStaffList()
        {
            return staffDA.getStaffList();
        }

        public Staff getStaffByID(string StaffID)
        {
            return staffDA.getStaffByID(StaffID);
        }

        public List<Staff> searchLecturer(string input, string criteria)
        {
            return staffDA.searchLecturer(input, criteria);
        }

        public List<Staff> searchLecturer(DateTime date, string session, string faculty, string categoryOfInvigilator, bool isFacultyCheckOnly, double totalLoadOfDutyForEach)
        {
            return staffDA.searchLecturer(date, session, faculty, categoryOfInvigilator, isFacultyCheckOnly, totalLoadOfDutyForEach);
        }

        public List<Staff> searchInvigilators(DateTime date, string time, string venueID)
        {
            return staffDA.searchInvigilators(date, time, venueID);
        }

        public List<Staff> searchChiefInvigilators(DateTime date, string time, string blockCode)
        {
            return staffDA.searchChiefInvigilators(date, time, blockCode);
        }

        public List<Faculty> getDistinctStaffFacultyCodesList()
        {
            return staffDA.getDistinctStaffFacultyCodesList();
        }

        public List<Staff> searchReliefInvigilators(DateTime date, string time)
        {
            return staffDA.searchReliefInvigilators(date, time);
        }

        public int countTotalInvigilatorsAvailable()
        {
            return staffDA.countTotalInvigilatorsAvailable();
        }

        public int countTotalChiefInvigilatorsAvailable()
        {
            return staffDA.countTotalChiefInvigilatorsAvailable();
        }

        public int getAverageNoOfExtraSession(string checkType)
        {
            return staffDA.getAverageNoOfExtraSession(checkType);
        }

        public List<string> getFacultyCodesList()
        {
            return staffDA.getFacultyCodesList();
        }

        public void shutDown()
        {
            staffDA.shutDown();
        }
    }
}