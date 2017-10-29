using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016.CSTEST
{
    public partial class CsInvigilationTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {/*
            MaintainFacultyControl mfc = new MaintainFacultyControl();
            Label1.Text = mfc.getFacultyList()[0].FacultyFullName.ToString();*/
           

        }

        protected void btnPlan_Click(object sender, EventArgs e)
        {

            //clear invigilationDuty table
            MaintainInvigilationDutyControl maintainInvDutyControl = new MaintainInvigilationDutyControl();
            maintainInvDutyControl.clearInvigilationDuty();
            maintainInvDutyControl.shutDown();

            MaintainTimetableControl maintainTimetableControl = new MaintainTimetableControl();
            //Different session of examtimetable
            List<Timetable> examTimetable = maintainTimetableControl.selectTimetable();
            maintainTimetableControl.shutDown();


            MaintainExaminationControl maintainExamControl = new MaintainExaminationControl();
            List<Examination> fullExamList = maintainExamControl.getExaminationList();
            maintainExamControl.shutDown();

            //This part will be unchanged (CS)
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            double totalLoadOfDutyForEachInvigilator = calculateTotalLoadOfDutyForEachInvigilator(calculateTotalInvigilatorsRequired(examTimetable), maintainStaffControl.countTotalInvigilatorsAvailable());
            double totalLoadOfDutyForEachChiefInvigilator = calculateTotalLoadOfDutyForEachChiefInvigilator(calculateTotalChiefInvigilatorsRequired(examTimetable), maintainStaffControl.countTotalChiefInvigilatorsAvailable());


            //This part will be unchanged (CS)
            double minTotalLoadOfDutyForEachInvigilator = (int)totalLoadOfDutyForEachInvigilator;
            double minTotalLoadOfDutyForEachChiefInvigilator = (int)totalLoadOfDutyForEachChiefInvigilator;
            MaintainVenueControl venueControl = new MaintainVenueControl();
            MaintainConstraint2Control examConstraintControl = new MaintainConstraint2Control();
            List<Constraint2> examConstraintList = examConstraintControl.getConstraintList();
            List<String> venueID = venueControl.getListOfAllVenue();

            //used for assignation
            List<TimeslotVenue> timeslotVenueForInvigilator = calculateInvigilatorForEachVenue(examTimetable);
            List<TimeslotVenue> timeslotVenueForRelief = calculateReliefForEachVenue(examTimetable);
            List<TimeslotVenue> timeslotVenueForChief = calculateChiefInvigilatorForEachVenue(examTimetable);
            
            //List of invigilation Duty
            List<InvigilationDuty> inviDutyList = createInvigilationDutyList(timeslotVenueForInvigilator, timeslotVenueForChief, timeslotVenueForRelief);
            List<Staff> invigilatorList = maintainStaffControl.getInvigilatorList();

            maintainStaffControl.shutDown();
            
            /*
            List<Constraint3> constraintList = new List<Constraint3>();
            Staff staff = new Staff();
            Examination exam = new Examination();
            InvigilationDuty inviDuty = new InvigilationDuty();
            staff.FacultyCode = 'A';
            exam.Faculty.FacultyCode = 'B';
            Constraint3 constraint = new Constraint3(inviDuty,staff,exam,false);
            constraintList.Add(constraint);
            //initialize domain for heuristic calculation

            List<InvigilatorHeuristic> inviHeuristicList = new List<InvigilatorHeuristic>();

            //input
            foreach (Staff invigilator in invigilatorList)
            {
                inviHeuristicList.Add(new InvigilatorHeuristic(invigilator));
            }
            
            getCanditateList(inviHeuristicList,inviDutyList[1],constraintList,(int)minTotalLoadOfDutyForEachInvigilator, (int)minTotalLoadOfDutyForEachInvigilator);


            /*  during assignation update staff id on invigilation duty list
                update staff to timeslot venue
                update duty into staff*/


        }

        //load constraint list
        public void assignInvigilator(List<InvigilationDuty> inviDutyList, List<Constraint2> constraintList)
        {
            MaintainCourseControl mCourseControl = new MaintainCourseControl();
            foreach (InvigilationDuty invigilationDuty in inviDutyList)
            {
                foreach (Constraint2 constraint in constraintList)
                {

                }
            }
            mCourseControl.shutDown();
        }

        public List<InvigilationDuty> createInvigilationDutyList(List<TimeslotVenue> tsVenueForInvigilator, List<TimeslotVenue> tsVenueForChief, List<TimeslotVenue> tsVenueForRelief)
        {
            List<InvigilationDuty> invigilationDutyList = new List<InvigilationDuty>();
            
            MaintainInvigilationDutyControl mInvigilatorControl = new MaintainInvigilationDutyControl();
            MaintainExaminationControl mExamControl = new MaintainExaminationControl();
            MaintainVenueControl mVenueControl = new MaintainVenueControl();


            //create invigilation duty for normal invigilator and invigilator in charge
            foreach (TimeslotVenue tv in tsVenueForInvigilator)
            {
                for (int x = 0; x < tv.NoOfInvigilatorRequired; x++)
                {
                    if (x == 0)
                    {
                        InvigilationDuty inviDuty = new InvigilationDuty(tv.Date, tv.Session, tv.VenueID, mVenueControl.getLocationByVenueID(tv.VenueID), "In-Charge", tv.CourseList[0].Duration);
                        invigilationDutyList.Add(inviDuty);
                    }
                    else
                    {
                        InvigilationDuty inviDuty = new InvigilationDuty(tv.Date, tv.Session, tv.VenueID, mVenueControl.getLocationByVenueID(tv.VenueID), "Invigilator", tv.CourseList[0].Duration);
                        invigilationDutyList.Add(inviDuty);
                    }
                }
            }
            //create invigilator list for Chief
            foreach (TimeslotVenue tsVenue in tsVenueForChief)
            {
                for (int x = 0; x < tsVenue.NoOfInvigilatorRequired; x++)
                {
                    InvigilationDuty inviDuty = new InvigilationDuty(tsVenue.Date, tsVenue.Session, tsVenue.VenueID, tsVenue.Location, "Chief", tsVenue.Duration);
                    invigilationDutyList.Add(inviDuty);
                }
            }

            //relief
            foreach (TimeslotVenue tsVenue in tsVenueForRelief)
            {
                InvigilationDuty inviDuty = new InvigilationDuty(tsVenue.Date, tsVenue.Session, tsVenue.VenueID, tsVenue.Location, "Relief", tsVenue.Duration);
                invigilationDutyList.Add(inviDuty);
            }

            mInvigilatorControl.shutDown();
            mVenueControl.shutDown();
            mExamControl.shutDown();

            return invigilationDutyList;
        }
        
        //calculate number of programme different venue for every session
        public List<TimeslotVenue> calculateInvigilatorForEachVenue(List<Timetable> examTimetableInSameDayAndSession)
        {
            List<TimeslotVenue> result = new List<TimeslotVenue>();

            MaintainVenueControl venueControl = new MaintainVenueControl();
            MaintainTimeslotVenueControl timeslotVenueControl = new MaintainTimeslotVenueControl();
            MaintainCourseControl courseControl = new MaintainCourseControl();

            List<Course> courseList = new List<Course>();
            List<String> venueIDList = venueControl.getListOfAllVenue();

            foreach (Timetable tt in examTimetableInSameDayAndSession)
            {
                foreach (String venueID in venueIDList)
                {
                    courseList = courseControl.searchCoursesList(tt.Date, tt.Session, venueID);
                    Venue venue = new Venue();
                    venue.VenueID = venueID;
                    venue.CoursesList = courseList;
                    int noOfInvigilator = getNumberOfInvigilatorsRequired(venue);

                    if (venue.CoursesList.Count != 0 && noOfInvigilator != 0)
                    {
                        TimeslotVenue timeslotVenue = new TimeslotVenue((timeslotVenueControl.getTimeslotID(tt.Date, tt.Session)), venue.VenueID, tt.Date, tt.Session, noOfInvigilator, venue.CoursesList);
                        result.Add(timeslotVenue);
                    }
                }
            }
            courseControl.shutDown();
            timeslotVenueControl.shutDown();
            venueControl.shutDown();
            return result;
        }

        public List<TimeslotVenue> calculateChiefInvigilatorForEachVenue(List<Timetable> examTimetableInSameDayAndSession)
        {
            MaintainTimeslotVenueControl tsVenueControl = new MaintainTimeslotVenueControl();
            MaintainVenueControl venueControl = new MaintainVenueControl();

            List<TimeslotVenue> tsVenueChiefList = new List<TimeslotVenue>();
            foreach (Timetable tt in examTimetableInSameDayAndSession)
            {
                foreach (Block block in tt.BlocksList)
                {
                    int duration = getLongestCourseDuration(block);
                    int noOfChiefInvi = 0;

                    if (block.VenuesList.Count >= 9)
                    {
                        noOfChiefInvi += 2;
                    }
                    else
                    {
                        noOfChiefInvi++;
                    }

                    TimeslotVenue timeslotVenueForChief = new TimeslotVenue(tsVenueControl.getTimeslotID(tt.Date, tt.Session), block.BlockCode, tt.Date, tt.Session, noOfChiefInvi, duration);
                    tsVenueChiefList.Add(timeslotVenueForChief);
                    //date,session,location,chief,duration

                }
            }
            venueControl.shutDown();
            tsVenueControl.shutDown();
            return tsVenueChiefList;
        }

        public List<TimeslotVenue> calculateReliefForEachVenue(List<Timetable> examTimetableInSameDayAndSession)
        {
            List<TimeslotVenue> reliefList = new List<TimeslotVenue>();
            MaintainTimeslotVenueControl tsVenueControl = new MaintainTimeslotVenueControl();
            String location = "";

            foreach (Timetable tt in examTimetableInSameDayAndSession)
            {
                //add 1 quarantine invi for east campus
                if (tt.BlocksList.Where(block => block.EastOrWest.Equals('E')).ToList().Count > 0)
                {
                    TimeslotVenue tsVenueRelief = new TimeslotVenue(tsVenueControl.getTimeslotID(tt.Date, tt.Session), tt.Date, tt.Session, "East Campus");
                    reliefList.Add(tsVenueRelief);
                }

                //add 1 quarantine invi for west campus
                if (tt.BlocksList.Where(block => block.EastOrWest.Equals('W')).ToList().Count > 0)
                {
                    TimeslotVenue tsVenueRelief = new TimeslotVenue(tsVenueControl.getTimeslotID(tt.Date, tt.Session), tt.Date, tt.Session, "West Campus");
                    reliefList.Add(tsVenueRelief);
                }

                foreach (Block block in tt.BlocksList)
                {
                    if (block.EastOrWest.Equals('E'))
                    {
                        location = "East Campus";
                    }
                    else if (block.EastOrWest.Equals('W'))
                    {
                        location = "West Campus";
                    }
                    TimeslotVenue tsVenueRelief = new TimeslotVenue(tsVenueControl.getTimeslotID(tt.Date, tt.Session), tt.Date, tt.Session, location);
                    reliefList.Add(tsVenueRelief);
                }

            }
            tsVenueControl.shutDown();
            return reliefList;
        }

        //get longest paper duration in the block
        public static int getLongestCourseDuration(Block block)
        {
            int duration = 0;
            foreach (Venue venue in block.VenuesList)
            {
                if (duration < getLongestCourseDuration(venue))
                {
                    duration = getLongestCourseDuration(venue);
                }
            }
            return duration;
        }

        //get longest paper duration in the venue
        public static int getLongestCourseDuration(Venue venue)
        {
            int duration = 0;
            foreach (Course paper in venue.CoursesList)
            {
                if (duration < paper.Duration)
                {
                    duration = paper.Duration;
                }
            }
            return duration;
        }

        //calculate total invigilators required
        public static int calculateTotalInvigilatorsRequired(List<Timetable> examTimetable)
        {
            int totalInvigilatorsRequired = 0;
            //add number of invigilators required in whole exam period
            foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
            {
                foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                {
                    foreach (Venue venue in block.VenuesList)
                    {
                        totalInvigilatorsRequired += getNumberOfInvigilatorsRequired(venue);
                    }
                }
            }
            totalInvigilatorsRequired += calculateTotalReliefInvigilatorsRequired(examTimetable);
            return totalInvigilatorsRequired;
        }

        //calculate total relief invigilators required
        public static int calculateTotalReliefInvigilatorsRequired(List<Timetable> examTimetable)
        {
            int totalReliefInvigilatorsRequired = 0;

            foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
            {
                //add number of relief invigilators required(one session by one session)
                totalReliefInvigilatorsRequired += calculateTotalReliefInvigilatorsRequiredPerSession(examTimetableInSameDayAndSession);
            }
            return totalReliefInvigilatorsRequired;
        }

        //calculate total relief invigilators required for that session
        public static int calculateTotalReliefInvigilatorsRequiredPerSession(Timetable examTimetableInSameDayAndSession)
        {
            int totalReliefInvigilatorsRequiredPerSession = 0;
            int numberOfMuslimReliefInviRequired = 1;

            //add number of relief invigilators required(one for each block)
            totalReliefInvigilatorsRequiredPerSession += examTimetableInSameDayAndSession.BlocksList.Count;

            //add 1 quarantine invi for east campus
            if (examTimetableInSameDayAndSession.BlocksList.Where(block => block.EastOrWest.Equals('E')).ToList().Count > 0)
                totalReliefInvigilatorsRequiredPerSession++;

            //add 1 quarantine invi for west campus
            if (examTimetableInSameDayAndSession.BlocksList.Where(block => block.EastOrWest.Equals('W')).ToList().Count > 0)
                totalReliefInvigilatorsRequiredPerSession++;

            if (examTimetableInSameDayAndSession.Session.Equals("EV"))
            {
                totalReliefInvigilatorsRequiredPerSession += numberOfMuslimReliefInviRequired;
            }
            return totalReliefInvigilatorsRequiredPerSession;
        }

        //get number of invigilators required based on venue, duration and number of programmes
        public static int getNumberOfInvigilatorsRequired(Venue venue)
        {
            string venueID = venue.VenueID;
            int numberOfProgrammes = 0;
            int duration = 0;


            double[] arrayInput;
            arrayInput = new double[3];
            MaintainConstraintControl maintainConstraintControl = new MaintainConstraintControl();

            for (int i = 0; i < venue.CoursesList.Count; i++)
            {
                numberOfProgrammes += venue.CoursesList[i].ProgrammeList.Count;
                if (venue.CoursesList[i].Duration > duration)
                {
                    duration = venue.CoursesList[i].Duration;
                }
            }
            arrayInput[1] = Convert.ToDouble(numberOfProgrammes);

            if (venueID.StartsWith("H") || venueID.StartsWith("M") || venueID.StartsWith("PA") || venueID.StartsWith("Q") || venueID.StartsWith("R") || venueID.StartsWith("V") || venueID.Equals("L1") || venueID.Equals("L3") || venueID.StartsWith("SD") || venueID.StartsWith("SE"))
            {
                arrayInput[0] = 0;
                if ((int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "invigilatorRequired" }, arrayInput)) != 0)
                {
                    return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "invigilatorRequired" }, arrayInput));
                }
                else
                {
                    arrayInput[2] = Convert.ToDouble(numberOfProgrammes);
                    return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "numberOfPaper", "invigilatorRequired" }, arrayInput));
                }
            }
            else if (venueID.Equals("L2"))
            {
                arrayInput[0] = 1;
                return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "invigilatorRequired" }, arrayInput));

            }
            else if (venueID.Equals("SB1") || venueID.Equals("SB3"))
            {
                arrayInput[0] = 2;
                return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "invigilatorRequired" }, arrayInput));
            }
            else if (venueID.Equals("SB2") || venueID.Equals("SB4"))
            {
                arrayInput[0] = 3;
                return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "numberOfPaper", "invigilatorRequired" }, arrayInput));
            }
            else if (venueID.Equals("DU"))
            {
                arrayInput[0] = 4;
                arrayInput[1] = Convert.ToDouble(duration);
                return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "duration", "invigilatorRequired" }, arrayInput));
            }
            else if (venueID.Equals("KS1") || venueID.Equals("KS2"))
            {
                arrayInput[0] = 5;
                arrayInput[1] = Convert.ToDouble(duration);
                return (int)(maintainConstraintControl.invigilatorRequiredConstraint(new string[] { "VenueSize", "duration", "invigilatorRequired" }, arrayInput));
            }
            else
                return 0;
        }

        //calculate total load of duty for each invigilator
        public static double calculateTotalLoadOfDutyForEachInvigilator(int totalInvigilatorsRequired, int totalInvigilatorsAvailable)
        {
            return (double)totalInvigilatorsRequired / (double)totalInvigilatorsAvailable;
        }

        //calculate total chief invigilators required in whole exam period
        public static int calculateTotalChiefInvigilatorsRequired(List<Timetable> examTimetable)
        {
            int totalChiefInvigilatorsRequired = 0;

            foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
            {
                //add number of chief invigilators required in same day and session
                totalChiefInvigilatorsRequired += examTimetableInSameDayAndSession.BlocksList.Count;
            }
            return totalChiefInvigilatorsRequired;
        }

        //get number of chief invigilators needed in the block
        public static int getNumberOfChiefInvigilatorsRequired(Block block)
        {
            return 1;
        }

        //calculate total load of duty for each chief invigilator
        public static double calculateTotalLoadOfDutyForEachChiefInvigilator(int totalChiefInvigilatorsRequired, int totalChiefInvigilatorsAvailable)
        {
            return (double)totalChiefInvigilatorsRequired / (double)totalChiefInvigilatorsAvailable;
        }

        //get the index number of invigilator free for particular session
        public static List<int> getFreeInvigilatorIndexNumbersList(List<Staff> invigilatorsList, Timetable examTimetableInSameDayAndSession, List<string> allFacultyCodesList)
        {
            List<int> freeInvigilatorsIndexList = new List<int>();
            for (int j = 0; j < allFacultyCodesList.Count; j++)
            {
                List<Staff> tempInvigilatorsList = invigilatorsList.Where(staff => staff.Faculty.Equals(allFacultyCodesList[j])
                    && staff.InvigilationDuty.Where(duty => duty.Date.Equals(examTimetableInSameDayAndSession.Date)
                    && duty.Session.Equals(examTimetableInSameDayAndSession.Session)).ToList().Count < 1).ToList();
                if (tempInvigilatorsList.Count > 0 && invigilatorsList.Where(staff => staff.Faculty.Equals(allFacultyCodesList[j])).ToList().Count > 1)
                {
                    freeInvigilatorsIndexList.Add(invigilatorsList.IndexOf(tempInvigilatorsList[tempInvigilatorsList.Count - 1]));
                }

            }
            return freeInvigilatorsIndexList;
        }
        /*
        //check whether invigilator is available
        public static bool isAvailable(Staff invigilator, Timetable examTimetableInSameDayAndSession, double totalLoadOfDutyForEach, string checkType, Venue venue)
        {
            MaintainConstraintControl maintainConstraintControl = new MaintainConstraintControl();
            invigilator.InvigilationDuty = invigilator.InvigilationDuty.OrderBy(duty => duty.Date).ToList();

            //check whether invigilator is Muslim Male staff and the duty is on Friday PM session
            if (!maintainConstraintControl.constraintValidation(new string[] { "Date", "Period", "IsMuslim", "gender" }, new double[] { maintainConstraintControl.convertDayOfWeek(examTimetableInSameDayAndSession.Date), maintainConstraintControl.convertPeriod(examTimetableInSameDayAndSession.Session),
                maintainConstraintControl.convertMuslim(invigilator.IsMuslim), maintainConstraintControl.convertGender(invigilator.Gender) }))
            {
                return false;
            }
            //check whether 2/3 load of duty is achieved if invigilator is doing PHD 
            else if (invigilator.IsTakingSTSPhD && invigilator.InvigilationDuty.Count >= (totalLoadOfDutyForEach / 3 * 2))
            {
                return false;
            }
            else if (checkType.Equals("Relief"))
            {
                foreach (InvigilationDuty invigilationDuty in invigilator.InvigilationDuty)
                {
                    if (invigilationDuty.CategoryOfInvigilator.Equals("Relief"))
                    {
                        return false;
                    }
                }
            }
            //keep 1 free slot for in-charge if the venue only need 1 more invigilator and there is no experienced invigilators assigned in the venue
            else if ((checkType.Equals("Invigilator") || checkType.Equals("Examiner")) && !invigilator.IsInviAbove2Years && venue.InvigilatorsList.Count == getNumberOfInvigilatorsRequired(venue) - 1)
            {
                bool isAssignedWithExperiencedInvi = false;
                foreach (Staff invi in venue.InvigilatorsList)
                {
                    if (invi.IsInviAbove2Years)
                    {
                        isAssignedWithExperiencedInvi = true;
                        break;
                    }
                }
                if (!isAssignedWithExperiencedInvi)
                {
                    return false;
                }
            }

            //check whether load of duty is full
            if (Math.Round(totalLoadOfDutyForEach, MidpointRounding.AwayFromZero) == totalLoadOfDutyForEach)
            {
                if (invigilator.InvigilationDuty.Count >= totalLoadOfDutyForEach)
                {
                    return false;
                }
            }
            else
            {
                MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
                int avgNoOfExtraSession = maintainStaffControl.getAverageNoOfExtraSession(checkType);
                maintainStaffControl.shutDown();
                if (invigilator.InvigilationDuty.Count >= totalLoadOfDutyForEach - (invigilator.NoOfExtraSession - avgNoOfExtraSession))
                {
                    return false;
                }
            }

            //check whether new duty will lead to allocation of duties for more than 3 consecutive days at a stretch
            if (invigilator.InvigilationDuty.Count >= 2)
            {
                TimeSpan currentTimeSpan, previousTimeSpan;
                List<InvigilationDuty> tempInviDutyList = invigilator.InvigilationDuty;
                tempInviDutyList.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, "", "", "", "", 0));
                tempInviDutyList = tempInviDutyList.OrderBy(duty => duty.Date).ToList();
                for (int i = 2; i < tempInviDutyList.Count; i++)
                {
                    currentTimeSpan = tempInviDutyList[i].Date.Subtract(tempInviDutyList[i - 1].Date);
                    previousTimeSpan = tempInviDutyList[i - 1].Date.Subtract(tempInviDutyList[i - 2].Date);
                    if (currentTimeSpan == TimeSpan.FromDays(1) && previousTimeSpan == TimeSpan.FromDays(1))
                    {
                        return false;
                    }
                }
            }

            //check whether invigilator is available for that session
            foreach (InvigilationDuty invigilationDuty in invigilator.InvigilationDuty)
            {
                foreach (Exemption exemption in invigilator.ExemptionList)
                {
                    if (exemption.Date.Equals(examTimetableInSameDayAndSession.Date) &&
                    exemption.Session.Equals(examTimetableInSameDayAndSession.Session))
                    {
                        return false;
                    }
                }
                //return false if same date same session and prevent AM EV session
                if ((invigilationDuty.Date.Equals(examTimetableInSameDayAndSession.Date) &&
                    invigilationDuty.Session.Equals(examTimetableInSameDayAndSession.Session)) ||
                    (invigilationDuty.Date.Equals(examTimetableInSameDayAndSession.Date) &&
                    (invigilationDuty.Session.Equals("PM") && examTimetableInSameDayAndSession.Session.Equals("EV")) ||
                    (invigilationDuty.Session.Equals("EV") && examTimetableInSameDayAndSession.Session.Equals("PM"))))
                {
                    return false;
                }
            }
            return true;
        }
        */
        //process constraint
        public void processConstraint(List<Staff> invigilatorList, List<InvigilationDuty> invigilationDutyList, List<Constraint2> constraintList)
        {
         
            //initialize domain for heuristic calculation
            List<InvigilatorHeuristic> inviHeuristicList = new List<InvigilatorHeuristic>();
            
            //input
            foreach(Staff invigilator in invigilatorList)
            {
                inviHeuristicList.Add(new InvigilatorHeuristic(invigilator));
            }
            
            //for every invigilation duty available check the constraint stored in database (normal invigilator)
            // if exam is correct                                
            foreach(InvigilationDuty inviDuty in invigilationDutyList)
            {
                // process contraints here and get highest heuristic staff and update the staff
                
            }                                      
            
        }
        

        public void getCanditateList(List<InvigilatorHeuristic> invigilators, InvigilationDuty invigilationDuty, List<Constraint3> constraintList, int minInvigilationDuty, int minInvigilationDutyForChief)
        {
            MaintainFacultyControl mFacultyControl = new MaintainFacultyControl();
            
            foreach (InvigilatorHeuristic invigilator in invigilators)
            {
                
                foreach (Constraint3 constraint in constraintList)
                {
                    int maxHeuristic = 0;
                    int score = 0;
                    //if exempted invigilator will not be assigned as invigilator
                    if (invigilator.Staff.ExemptionList != null)
                    {
                        foreach (Exemption exemption in invigilator.Staff.ExemptionList)
                            if (invigilationDuty.Date.Equals(exemption))
                            {
                                invigilator.PossibleCanditate = false;
                            }
                    }

                    //staff variable
                    if (!constraint.Invigilator.hasOtherDutyOnSameDay(invigilationDuty.Date,invigilationDuty.Session).Equals(null))
                    {
                        maxHeuristic++;
                        if (invigilator.Staff.hasOtherDutyOnSameDay(invigilationDuty.Date, invigilationDuty.Session).Equals(constraint.HasMorningDutyOnSameDay)){
                            score++;
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true)
                                invigilator.PossibleCanditate = false;
                        }

                    }

                    if (!constraint.Invigilator.FacultyCode.Equals('\0'))
                    {
                        maxHeuristic++;
                        if (invigilator.Staff.FacultyCode.Equals(constraint.Invigilator.FacultyCode))
                        {
                            score++;
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true && !constraint.Invigilator.FacultyCode.Equals('\0'))
                                invigilator.PossibleCanditate = false;
                        }
                    }

                    if (constraint.Invigilator.IsMuslim != null)
                    {
                        maxHeuristic++;
                        if (invigilator.Staff.IsMuslim.Equals(constraint.Invigilator.IsMuslim))
                        {
                            score++;
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true && constraint.Invigilator.IsMuslim != null)
                                invigilator.PossibleCanditate = false;
                        }
                    }

                    if (!constraint.Invigilator.IsInviAbove2Years.Equals(null))
                    {
                        maxHeuristic++;
                        if (invigilator.Staff.IsInviAbove2Years.Equals(constraint.Invigilator.IsInviAbove2Years))
                        {
                            score++;
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true && !constraint.Invigilator.IsInviAbove2Years.Equals(null))
                                invigilator.PossibleCanditate = false;
                        }
                    }

                    if (constraint.Invigilator.IsChiefInvi.Equals(null))
                    {
                        maxHeuristic++;
                        if (invigilator.Staff.IsChief.Equals(constraint.Invigilator.IsChief))
                        {
                            score++;
                            
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true && !constraint.Invigilator.IsChiefInvi.Equals(null))
                                invigilator.PossibleCanditate = false;
                        }
                    }

                    if (constraint.Invigilator.IsInviAbove2Years != null)
                    {
                        maxHeuristic++;
                        if (invigilator.Staff.IsInviAbove2Years.Equals(constraint.Invigilator.IsInviAbove2Years))
                        {
                            score++;
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true && constraint.Invigilator.IsInviAbove2Years != null)
                                invigilator.PossibleCanditate = false;
                        }
                    }

                    if (!invigilator.Staff.IsTakingSTSPhD.Equals(null))
                    {
                        maxHeuristic++;
                        if (invigilator.Staff.IsTakingSTSPhD.Equals(constraint.Invigilator.IsTakingSTSPhD))
                        {
                            score++;
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true && constraint.Invigilator.IsTakingSTSPhD != null)
                                invigilator.PossibleCanditate = false;
                        }
                    }

                    if (constraint.Invigilator.TypeOfEmploy != '\0')
                    {
                        maxHeuristic++;
                        if (invigilator.Staff.TypeOfEmploy.Equals(constraint.Invigilator.TypeOfEmploy))
                        {
                            score++;
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true && constraint.Invigilator.TypeOfEmploy != '\0')
                                invigilator.PossibleCanditate = false;
                        }
                    }

                    //invigilation duty
                    if ((constraint.InvigilationDuty.CategoryOfInvigilator != null || constraint.InvigilationDuty.CategoryOfInvigilator != ""))
                    {
                        maxHeuristic++;
                        if (invigilationDuty.CategoryOfInvigilator.Equals(constraint.InvigilationDuty.CategoryOfInvigilator))
                        {
                            score++;
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true && (constraint.InvigilationDuty.CategoryOfInvigilator != null || constraint.InvigilationDuty.CategoryOfInvigilator != ""))
                                invigilator.PossibleCanditate = false;
                        }
                    }

                    if (constraint.InvigilationDuty.Duration != 0)
                    {
                        maxHeuristic++;
                        if (invigilationDuty.Duration.Equals(constraint.InvigilationDuty.Duration))
                        {
                            score++; 
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true && constraint.InvigilationDuty.Duration != 0)
                                invigilator.PossibleCanditate = false;
                        }
                    }

                    if (constraint.InvigilationDuty.Location != null || constraint.InvigilationDuty.CategoryOfInvigilator != "")
                    {
                        maxHeuristic++;
                        if (invigilationDuty.Location.Equals(constraint.InvigilationDuty.Location))
                        {
                            score++;
                        }
                        else
                        {
                            if (constraint.IsHardConstraint == true && (constraint.InvigilationDuty.Location != null || constraint.InvigilationDuty.CategoryOfInvigilator != ""))
                                invigilator.PossibleCanditate = false;
                        }
                    }

                    //exam
                    foreach (Examination exam in invigilationDuty.ExamList)
                    {
                        if (!constraint.Examination.Faculty.FacultyCode.Equals('\0'))
                        {
                            maxHeuristic++;
                            if (mFacultyControl.searchFacultyByCourseCode(exam.CourseCode).FacultyCode.Equals(constraint.Examination.Faculty.FacultyCode))
                            {
                                score++;
                            }
                            else
                            {
                                if (constraint.IsHardConstraint == true && (!constraint.Examination.Faculty.FacultyCode.Equals('\0')))
                                    invigilator.PossibleCanditate = false;
                            }
                        }

                        if (!constraint.Examination.ExamType.Equals('\0') || constraint.Examination.ExamType != '\0')
                        {
                            maxHeuristic++;
                            if (exam.ExamType.Equals(constraint.Examination.ExamType))
                            {
                                score++;
                            }
                            else
                            {
                                if (constraint.IsHardConstraint == true && constraint.Examination.ExamType != '\0')
                                    invigilator.PossibleCanditate = false;
                            }
                        }

                        if (constraint.Examination.PaperType != '\0')
                        {
                            maxHeuristic++;
                            if (exam.PaperType.Equals(constraint.Examination.PaperType))
                            {
                                score++;
                            }
                            else
                            {
                                if (constraint.IsHardConstraint == true && constraint.Examination.ExamType != '\0')
                                    invigilator.PossibleCanditate = false;
                            }
                        }

                        if (constraint.Examination.Year != 0)
                        {
                            maxHeuristic++;
                            if (exam.Year.Equals(constraint.Examination.Year))
                            {
                                score++;
                            }
                            else
                            {
                                if (constraint.IsHardConstraint == true && constraint.Examination.Year != 0)
                                    invigilator.PossibleCanditate = false;
                            }
                        }

                        MaintainCourseControl mCourseControl = new MaintainCourseControl();
                        Course course = mCourseControl.searchCourseByCourseCode(exam.CourseCode);
                        mCourseControl.shutDown();

                        if (!constraint.IsCnblPaper.Equals(null))
                        {
                            maxHeuristic++;
                            if (course.IsCnblPaper.Equals(constraint.IsCnblPaper))
                            {
                                score++;
                            }
                            else
                            {
                                if (constraint.IsHardConstraint == true && constraint.IsCnblPaper != null)
                                    invigilator.PossibleCanditate = false;
                            }
                        }

                        if (!constraint.IsDoubleSeating.Equals(null))
                        {
                            maxHeuristic++;
                            if (course.IsDoubleSeating.Equals(constraint.IsDoubleSeating))
                            {
                                score++;
                            }
                            else
                            {
                                if (constraint.IsHardConstraint == true && constraint.IsDoubleSeating != null)
                                    invigilator.PossibleCanditate = false;
                            }
                        }

                        
                        //got problem (is examiner)
                        //add assign examiner = true
                        foreach (string paperExamined in invigilator.Staff.PaperCodeExamined)
                        {
                            //questionable assignation of examiner no constraint involvement
                            if (paperExamined.Equals(exam.CourseCode) && (paperExamined != "" || paperExamined != null))
                            {
                                invigilator.Heuristic++;
                                maxHeuristic++;
                            }

                        }

                    }

                if(score == maxHeuristic)
                    {
                        invigilator.Heuristic++;
                    }    
                }
                
            }

            mFacultyControl.shutDown();
        }
         
        public List<Staff> removeExemptedAndNotAvailableInvigilator(List<Staff> fullInviList, InvigilationDuty invigilatonDuty)
        {
            List<Staff> possibleStaffList = new List<Staff>();
            MaintainExemptionControl exemptionControl = new MaintainExemptionControl();
            foreach(Staff staff in fullInviList)
            {
                bool? isAvailable = true;
                foreach(Exemption exemption in staff.ExemptionList)
                {
                    if(exemption.Date.Equals(invigilatonDuty.Date) && exemption.Session.Equals(invigilatonDuty.Date)){
                        isAvailable = false;
                    }
                }
                
               foreach(InvigilationDuty inviDuty in staff.InvigilationDuty)
                {
                    if(inviDuty.Date.Equals(invigilatonDuty.Date) && inviDuty.Session.Equals(invigilatonDuty.Session))
                    {
                        isAvailable = false;
                    }
                }
                  if(isAvailable == true)
                {
                    possibleStaffList.Add(staff);
                }
            }

            exemptionControl.shutDown();
            return possibleStaffList;
        }

        //maybe wrong
        public List<Staff> getInvigilatorCandidate()
        {
            List<Staff> staffList = new List<Staff>();

            return staffList;
        }
    }    
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }



    static class Extensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
