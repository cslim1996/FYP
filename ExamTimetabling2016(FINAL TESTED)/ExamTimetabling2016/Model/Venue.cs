using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_timetabling
{
    public class Venue
    {
        public string venueID;
        public int venueCapacity; //total course capacity in this venue (including gap)
        public List<Programme> programme; 

        public Venue() {
        }

        public Venue(String venueID, int venueCapacity)
        {
            this.venueID = venueID;
            this.venueCapacity = venueCapacity;
        }

        public Venue(String venueID, int venueCapacity, List<Programme> programme) {
            this.venueID = venueID;
            this.venueCapacity = venueCapacity;
            this.programme = programme;
        }

        public String getVenueID() {
            return venueID;
        }

        public void setVenueID(String venueID) {
            this.venueID = venueID;
        }

        public int getVenueCapacity() {
            return venueCapacity;
        }

        public void setVenueCapacity(int venueCapacity) {
            this.venueCapacity = venueCapacity;
        }

        public List<Programme> getProgramme() {
            return programme;
        }

        public void setProgramme(List<Programme> programme) {
            this.programme = programme;
        }

        public String toString() {
            return "Venue{" + "venueID=" + venueID + ", venueCapacity=" + venueCapacity + ", programme=" + programme + '}';
        }
    }
}