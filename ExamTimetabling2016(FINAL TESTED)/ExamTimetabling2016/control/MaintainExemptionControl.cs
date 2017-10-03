using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainExemptionControl
    {
        private ExemptionDA exemptionDA;

        public MaintainExemptionControl()
        {
            exemptionDA = new ExemptionDA();
        }

        public List<Exemption> searchExemption(string staffID)
        {
            return exemptionDA.searchExemption(staffID);
        }

        public List<Exemption> searchExemptionList(string staffID)
        {
            return exemptionDA.searchExemptionList(staffID);
        }

        public void shutDown()
        {
            exemptionDA.shutDown();
        }

    }
}