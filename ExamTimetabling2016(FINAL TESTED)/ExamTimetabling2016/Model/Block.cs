using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_timetabling
{
    public class Block
    {
        public string blockID;  
        public int blockTotalCapacity;
        public List<Venue> venue; 

        public Block(){
            
        }

        public Block(String blockID, int blockTotalCapacity)
        {
            this.blockID = blockID;
            this.blockTotalCapacity = blockTotalCapacity;
        }
        
        public Block(String blockID, int blockTotalCapacity, List<Venue> venue) {
            this.blockID = blockID;
            this.blockTotalCapacity = blockTotalCapacity;
            this.venue = venue;
        }

        public String getBlockID() {
            return blockID;
        }

        public void setBlockID(String blockID) {
            this.blockID = blockID;
        }

        public int getBlockTotalCapacity() {
            return blockTotalCapacity;
        }

        public void setBlockTotalCapacity(int blockTotalCapacity) {
            this.blockTotalCapacity = blockTotalCapacity;
        }

        public List<Venue> getVenue() {
            return venue;
        }

        public void setVenue(List<Venue> venue) {
            this.venue = venue;
        }

        public String toString() {
            return "Block{" + "blockID=" + blockID + ", blockTotalCapacity=" + blockTotalCapacity + ", venue=" + venue + '}';
        }
        
    }
}