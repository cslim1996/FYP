using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class Staff : Person
    {
        private string name;
        private char facultyCode;
        private char isChief;
        private char isInvi;
        private string staffID;
        private string title;
        private string position;
        private string faculty;
        private string department;
        private bool? isTakingSTSPhD;
        private char typeOfEmploy;
        private int noOfSatSession;
        private int noAsQuarantineInvi;
        private int noAsReliefInvi;
        private int noOfExtraSession;
        private bool? isChiefInvi;
        private bool? isInviAbove2Years;
        private List<string> paperCodeExamined;
        private List<Exemption> exemptionList;
        private List<InvigilationDuty> invigilationDuty;
        
        

        public Staff(string name, char gender, bool? isMuslim, string staffID, string title, string position,
            string faculty, string department, bool? isTakingSTSPhD, char typeOfEmploy, int noOfSatSession,
            int noAsQuarantineInvi, int noAsReliefInvi, int noOfExtraSession, bool? isChiefInvi, bool? isInviAbove2Years,
            List<string> paperCodeExamined, List<Exemption> exemptionList, List<InvigilationDuty> invigilationDuty)
            : base(name, gender, isMuslim)
        {
            this.staffID = staffID;
            this.title = title;
            this.position = position;
            this.faculty = faculty;
            this.department = department;
            this.isTakingSTSPhD = isTakingSTSPhD;
            this.typeOfEmploy = typeOfEmploy;
            this.noOfSatSession = noOfSatSession;
            this.noAsQuarantineInvi = noAsQuarantineInvi;
            this.noAsReliefInvi = noAsReliefInvi;
            this.noOfExtraSession = noOfExtraSession;
            this.isChiefInvi = isChiefInvi;
            this.isInviAbove2Years = isInviAbove2Years;
            this.paperCodeExamined = paperCodeExamined;
            this.exemptionList = exemptionList;
            this.invigilationDuty = invigilationDuty;
        }


        public Staff(string staffID,bool? isMuslim,bool? isTakingSTSPhD,char typeOfEmploy, int noOfSatSession,
            int noAsReliefInvi, int noOfExtraSession, bool? isChiefInvi, bool? isInviAbove2Years, char gender)
            : base("",gender, isMuslim)
        {
            MaintainExemptionControl mExemptioncontrol = new MaintainExemptionControl();
            MaintainPaperExaminedControl mPaperExaminedControl = new MaintainPaperExaminedControl();
            this.staffID = staffID;
            this.isTakingSTSPhD = isTakingSTSPhD;
            this.typeOfEmploy = typeOfEmploy;
            this.noOfExtraSession = noOfExtraSession;
            this.noAsReliefInvi = noAsReliefInvi;
            this.noOfExtraSession = noOfExtraSession;
            this.isInviAbove2Years = isInviAbove2Years;
            this.isChiefInvi = isChiefInvi;
            this.exemptionList = mExemptioncontrol.searchExemptionList(staffID);
            this.paperCodeExamined = mPaperExaminedControl.searchPaperExaminedByStaffID(staffID);
            this.invigilationDuty = new List<InvigilationDuty>();
            mExemptioncontrol.shutDown();
            mPaperExaminedControl.shutDown();
        }

        public Staff(string staffID, string title, string name, char facultyCode, char isChief, char isInvi)
            : base("", '\0', null)
        {
            this.staffID = staffID;
            this.title = title;
            this.name = name;
            this.facultyCode = facultyCode;
            this.isChief = isChief;
            this.isInvi = isInvi;
        }

        public Staff(string title, string name)
            : base("", '\0', null)
        {
            this.title = title;
            this.name = name;
            facultyCode = '\0';
            isChief = '\0';
        }

        public Staff()
            : base("", 'M', false)
        {
            new Staff("", 'M', false, "", "", "", "", "", null, 'F', 0, 0, 0, 0, null, null,
                new List<string>(), new List<Exemption>(), new List<InvigilationDuty>());
        }

        public string StaffID
        {
            get { return staffID; }
            set { staffID = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public char FacultyCode
        {
            get { return facultyCode; }
            set { facultyCode = value; }
        }

        public char IsChief
        {
            get { return isChief; }
            set { isChief = value; }
        }

        public char IsInvi
        {
            get { return isInvi; }
            set { isInvi = value; }
        }


        public string Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public string Faculty
        {
            get
            {
                return faculty;
            }

            set
            {
                faculty = value;
            }
        }

        public string Department
        {
            get
            {
                return department;
            }

            set
            {
                department = value;
            }
        }

        public bool? IsTakingSTSPhD
        {
            get
            {
                return isTakingSTSPhD;
            }

            set
            {
                isTakingSTSPhD = value;
            }
        }

        public char TypeOfEmploy
        {
            get
            {
                return typeOfEmploy;
            }

            set
            {
                typeOfEmploy = value;
            }
        }

        public int NoOfSatSession
        {
            get
            {
                return noOfSatSession;
            }

            set
            {
                noOfSatSession = value;
            }
        }

        public int NoAsQuarantineInvi
        {
            get
            {
                return noAsQuarantineInvi;
            }

            set
            {
                noAsQuarantineInvi = value;
            }
        }

        public int NoAsReliefInvi
        {
            get
            {
                return noAsReliefInvi;
            }

            set
            {
                noAsReliefInvi = value;
            }
        }

        public int NoOfExtraSession
        {
            get
            {
                return noOfExtraSession;
            }

            set
            {
                noOfExtraSession = value;
            }
        }

        public bool? IsChiefInvi
        {
            get
            {
                return isChiefInvi;
            }

            set
            {
                isChiefInvi = value;
            }
        }

        public bool? IsInviAbove2Years
        {
            get
            {
                return isInviAbove2Years;
            }

            set
            {
                isInviAbove2Years = value;
            }
        }

        public List<string> PaperCodeExamined
        {
            get
            {
                return paperCodeExamined;
            }

            set
            {
                paperCodeExamined = value;
            }
        }

        internal List<Exemption> ExemptionList
        {
            get
            {
                return exemptionList;
            }

            set
            {
                exemptionList = value;
            }
        }

        internal List<InvigilationDuty> InvigilationDuty
        {
            get
            {
                return invigilationDuty;
            }

            set
            {
                invigilationDuty = value;
            }
        }

        public bool? hasOtherDutyOnSameDay(DateTime date, string session)
        {
            bool? result = null;

            foreach(InvigilationDuty inviDuty in this.invigilationDuty)
            {
                if (inviDuty.Date.Equals(date) && !inviDuty.Session.Equals(session))
                {
                    result = true;
                }
                else
                    result = false;
            }
            return result;
        }

        public bool? hasMorningDutySameDay(DateTime date)
        {
            bool? result = null;

            foreach (InvigilationDuty inviDuty in this.invigilationDuty)
            {
                if (inviDuty.Date.Equals(date) && inviDuty.Session.Equals("AM"))
                {
                    result = true;
                }
                else
                    result = false;

            }
            return result;
        }

        
        
    }
}