using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamTimetabling2016
{
    public class Programme
    {
        private string programmeCode;
        private int yearStudy;
        private int sitFrom;
        private int sitTo;

        public Programme(string programmeCode, int yearStudy, int sitFrom, int sitTo)
        {
            this.programmeCode = programmeCode;
            this.yearStudy = yearStudy;
            this.sitFrom = sitFrom;
            this.sitTo = sitTo;
        }

        public Programme()
        {
            new Programme("", 0, 0, 0);
        }

        public string ProgrammeCode
        {
            get
            {
                return programmeCode;
            }

            set
            {
                programmeCode = value;
            }
        }

        public int SitFrom
        {
            get
            {
                return sitFrom;
            }

            set
            {
                sitFrom = value;
            }
        }

        public int SitTo
        {
            get
            {
                return sitTo;
            }

            set
            {
                sitTo = value;
            }
        }

        public int YearStudy
        {
            get
            {
                return yearStudy;
            }

            set
            {
                yearStudy = value;
            }
        }
    }
}
