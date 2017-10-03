using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTimetabling2016
{
    public class Block
    {
        private string blockCode;
        private string campus;
        private List<Staff> chiefInvigilatorsList;
        private List<Venue> venuesList;
        private int numberOfRooms;
        private char eastOrWest;

        public Block()
        {
            new Block("", "", new List<Staff>(), new List<Venue>(), 'W');
        }

        public Block(string blockCode, string campus, List<Staff> chiefInvigilatorsList, List<Venue> venuesList, char eastOrWest)
        {
            this.blockCode = blockCode;
            this.campus = campus;
            this.chiefInvigilatorsList = chiefInvigilatorsList;
            this.venuesList = venuesList;
            this.eastOrWest = eastOrWest;
        }

        public string BlockCode
        {
            get
            {
                return blockCode;
            }

            set
            {
                blockCode = value;
            }
        }

        public string Campus
        {
            get
            {
                return campus;
            }

            set
            {
                campus = value;
            }
        }

        internal List<Staff> ChiefInvigilatorsList
        {
            get
            {
                return chiefInvigilatorsList;
            }

            set
            {
                chiefInvigilatorsList = value;
            }
        }

        internal List<Venue> VenuesList
        {
            get
            {
                return venuesList;
            }

            set
            {
                venuesList = value;
            }
        }

        public int NumberOfRooms
        {
            get
            {
                return numberOfRooms;
            }

            set
            {
                numberOfRooms = value;
            }
        }

        public char EastOrWest
        {
            get
            {
                return eastOrWest;
            }

            set
            {
                eastOrWest = value;
            }
        }
    }
}
