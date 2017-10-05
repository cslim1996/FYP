using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTimetabling2016
{
    class MaintainVenueControl
    {
        private VenueDA venueDA;

        public MaintainVenueControl()
        {
            venueDA = new VenueDA();
        }

        public List<Venue> searchVenuesList(DateTime date, string session, string blockCode)
        {
            return venueDA.searchVenuesList(date, session, blockCode);
        }

        public int getNumberOfInvigilatorsInChargeAssinged(DateTime date, string session, string venueID)
        {
            return venueDA.getNumberOfInvigilatorsInChargeAssinged(date, session, venueID);
        }

        public List<String> getListOfAllVenue()
        {
            return venueDA.getListOfAllVenue();
        }

        public void shutDown()
        {
            venueDA.shutDown();
        }
    }
}