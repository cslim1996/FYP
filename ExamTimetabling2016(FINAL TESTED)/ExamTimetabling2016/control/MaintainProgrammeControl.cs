using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTimetabling2016
{
    class MaintainProgrammeControl
    {
        private ProgrammeDA programmeDA;

        public MaintainProgrammeControl()
        {
            programmeDA = new ProgrammeDA();
        }

        public List<Programme> searchProgrammesList(DateTime date, string time, string venueID, string courseCode)
        {
            return programmeDA.searchProgrammesList(date, time, venueID, courseCode);
        }
        
        public void shutDown()
        {
            programmeDA.shutDown();
        }
    }
}
