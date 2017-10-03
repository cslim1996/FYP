using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainInvigilationDutyControl
    {
        private InvigilationDutyDA invigilationDutyDA;

        public MaintainInvigilationDutyControl()
        {
            invigilationDutyDA = new InvigilationDutyDA();
        }

        public List<InvigilationDuty> searchInvigilationDuty2(string timeslotID)
        {
            return invigilationDutyDA.searchInvigilationDuty2(timeslotID);
        }

        public List<InvigilationDuty> searchInvigilationDuty2(int staffID)
        {
            return invigilationDutyDA.searchInvigilationDuty2(staffID);
        }


        public int addInvigilationDuty(Staff staff, InvigilationDuty invigilationDuty)
        {
            return invigilationDutyDA.addInvigilationDuty(staff, invigilationDuty);
        }

        public List<InvigilationDuty> searchInvigilationDuty(string staffID)
        {
            return invigilationDutyDA.searchInvigilationDuty(staffID);
        }

        public int changeCatOfInvi(DateTime date, string session, string venueID, string staffID)
        {
            return invigilationDutyDA.changeCatOfInvi(date, session, venueID, staffID);
        }

        public int changeLocationOfReliefInvi(DateTime date, string session, string staffID, string location, bool isQuarantineInviForEastCampusAssigned)
        {
            return invigilationDutyDA.changeLocationOfReliefInvi(date, session, staffID, location, isQuarantineInviForEastCampusAssigned);
        }

        public int updateNoAsQuarantineInvi(string staffID)
        {
            return invigilationDutyDA.updateNoAsQuarantineInvi(staffID);
        }

        public int updateNoAsReliefInvi(string staffID)
        {
            return invigilationDutyDA.updateNoAsReliefInvi(staffID);
        }

        public int updateNoOfExtraSession(string staffID)
        {
            return invigilationDutyDA.updateNoOfExtraSession(staffID);
        }

        public int clearInvigilationDuty()
        {
            return invigilationDutyDA.clearInvigilationDuty();
        }

        public void shutDown()
        {
            invigilationDutyDA.shutDown();
        }
    }
}