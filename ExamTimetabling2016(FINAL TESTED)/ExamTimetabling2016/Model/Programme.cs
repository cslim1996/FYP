using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_timetabling
{
    public class Programme
    {
        public string programmeCode;
        public int populationAssigned; //total course capacity in this venue (including gap)

        public Programme() {
        }

        public Programme(String programmeCode, int populationAssigned) {
            this.programmeCode = programmeCode;
            this.populationAssigned = populationAssigned;
        }

        public String getProgrammeCode() {
            return programmeCode;
        }

        public void setProgrammeCode(String programmeCode) {
            this.programmeCode = programmeCode;
        }

        public int getPopulationAssigned() {
            return populationAssigned;
        }

        public void setPopulationAssigned(int populationAssigned) {
            this.populationAssigned = populationAssigned;
        }

        public String toString() {
            return "Programme{" + "programmeCode=" + programmeCode + ", populationAssigned=" + populationAssigned + '}';
        }
    }
}