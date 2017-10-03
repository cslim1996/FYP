using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_timetabling
{
    public class AssignedSubject
    {
        public string courseID;
        public string mainOrDualAward;
        public string timeslot;  
        public string session; 
        public List<Block> block; 

        public AssignedSubject(){

        }

        public AssignedSubject(String courseID, String timeslot, String session)
        {
            this.courseID = courseID;
            this.timeslot = timeslot;
            this.session = session;
        }

        public AssignedSubject(String courseID, String mainOrDualAward, String timeslot, String session, List<Block> block)
        {
            this.courseID = courseID;
            this.mainOrDualAward = mainOrDualAward;
            this.timeslot = timeslot;
            this.session = session;
            this.block = block;
        }

        public String getCourseID(){
            return courseID;
        }

        public void setCourseID(String courseID){
            this.courseID = courseID;
        }

        public String getMainOrDualAward(){
            return mainOrDualAward;
        }

        public void setMainOrDualAward(String mainOrDualAward)        {
            this.mainOrDualAward = mainOrDualAward;
        }

        public String getTimeslot() {
            return timeslot;
        }

        public void setTimeslot(String timeslot) {
            this.timeslot = timeslot;
        }

        public String getSession() {
            return session;
        }

        public void setSession(String session) {
            this.session = session;
        }

        public List<Block> getBlock()
        {
            return block;
        }

        public void setBlock(List<Block> block)
        {
            this.block = block;
        }

        public String toString() {
            return "AssignedSubject{" + "courseID=" + courseID + ", timeslot=" + timeslot + ", session=" + session + ", blockName=" + block + '}';
        }

    }
}