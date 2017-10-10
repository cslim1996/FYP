using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainTimeslotVenueControl
    {
        private TimeslotVenueDA timeslotVenueDA;

        public MaintainTimeslotVenueControl()
        {
            timeslotVenueDA = new TimeslotVenueDA();
        }

        public void insertNoOfInvigilatorRequired(TimeslotVenue timeslotVenue)
        {
            timeslotVenueDA.insertNoOfInvigilatorsReq(timeslotVenue);
        }

        public string getTimeslotID(DateTime date, String session)
        {
           return timeslotVenueDA.getTimeslotID(date, session);
        }

        public void shutDown()
        {
            timeslotVenueDA.shutDown();
        }
    }
}