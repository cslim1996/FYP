﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016
{
    public partial class CsInvigilationTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnPlan_Click(object sender, EventArgs e)
        {

            //clear invigilationDuty table
            MaintainInvigilationDutyControl maintainInvDutyControl = new MaintainInvigilationDutyControl();
            maintainInvDutyControl.clearInvigilationDuty();
            maintainInvDutyControl.shutDown();

            //get examination list
            MaintainExaminationControl maintainExamControl = new MaintainExaminationControl();
            List<Examination> fullExamList = maintainExamControl.getExaminationList();
            maintainExamControl.shutDown();

            //get different session of examtimetable
            MaintainTimetableControl maintainTimetableControl = new MaintainTimetableControl();
            List<Timetable> examTimetable = maintainTimetableControl.selectTimetable();
            maintainTimetableControl.shutDown();

            //Invigilation Duty Load calculation
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            double totalLoadOfDutyForEachInvigilator = calculateTotalLoadOfDutyForEachInvigilator(calculateTotalInvigilatorsRequired(examTimetable), maintainStaffControl.countTotalInvigilatorsAvailable());
            double totalLoadOfDutyForEachChiefInvigilator = calculateTotalLoadOfDutyForEachChiefInvigilator(calculateTotalChiefInvigilatorsRequired(examTimetable), maintainStaffControl.countTotalChiefInvigilatorsAvailable());
            if (totalLoadOfDutyForEachInvigilator < 1)
            {
                totalLoadOfDutyForEachInvigilator = 1;
            }

            //calculate min duty load for differeten invigilators
            double minTotalLoadOfDutyForEachInvigilator = (int)totalLoadOfDutyForEachInvigilator;
            double minTotalLoadOfDutyForEachChiefInvigilator = (int)totalLoadOfDutyForEachChiefInvigilator;
            
            MaintainVenueControl venueControl = new MaintainVenueControl();
            //get list of venue IDs
            List<String> venueID = venueControl.getListOfAllVenue();

            //used for assignation
            List<TimeslotVenue> timeslotVenueForInvigilator = calculateInvigilatorForEachVenue(examTimetable);
            List<TimeslotVenue> timeslotVenueForRelief = calculateReliefForEachVenue(examTimetable);
            List<TimeslotVenue> timeslotVenueForChief = calculateChiefInvigilatorForEachVenue(examTimetable);

            //Get constraint list from database;
            MaintainConstraint3Control mConstraintControl = new MaintainConstraint3Control();
            List<Constraint3> fullConstraintList = mConstraintControl.loadFullConstraintList();
            mConstraintControl.shutDown();

            //combine timeslotvenue list
            List<TimeslotVenue> CombinedTimeslotVenueList = new List<TimeslotVenue>();
            
            foreach(TimeslotVenue tsVenue in timeslotVenueForInvigilator)
            {
                CombinedTimeslotVenueList.Add(tsVenue);
            }
            foreach (TimeslotVenue tsVenue in timeslotVenueForRelief)
            {
                CombinedTimeslotVenueList.Add(tsVenue);
            }
            foreach (TimeslotVenue tsVenue in timeslotVenueForChief)
            {
                CombinedTimeslotVenueList.Add(tsVenue);
            }
            

            //create list of invigilation Duty
            List<InvigilationDuty> inviDutyList = createInvigilationDutyList(timeslotVenueForInvigilator, timeslotVenueForChief, timeslotVenueForRelief);
            MaintainFacultyControl mFacultyControl = new MaintainFacultyControl();
            List<Faculty> fullFacultyList = mFacultyControl.getFacultyList();

            //calculate total duty count for each faculty
            int TotalInvigilationDutyCount = inviDutyList.Count;
            
            foreach(InvigilationDuty inviDuty in inviDutyList)
            {
                foreach(Examination exam in inviDuty.ExamList)
                {
                    foreach(Faculty faculty in fullFacultyList)
                    {
                        if (exam.Faculty.FacultyCode.Equals(faculty.FacultyCode))
                        {
                            faculty.FacultyDutyCount++;
                        }
                    }
                }
            }
            //get invigilator list from database where isInvi = Y
            List<Staff> invigilatorList = maintainStaffControl.getInvigilatorList();
            maintainStaffControl.shutDown();


            //load constraint setting
            MaintainConstraintSettingControl mSettingControl = new MaintainConstraintSettingControl();
            ConstraintSetting setting = mSettingControl.readSettingFromDatabase();
            mSettingControl.shutDown();

            //setting for exemption for examiner
            int exemptionForExaminerInDay = 2;

            //load exemption list for each invigilator
            MaintainExemptionControl mExemptionControl = new MaintainExemptionControl();
            foreach(Staff invigilator in invigilatorList)
            {
                invigilator.ExemptionList = mExemptionControl.searchExemption(invigilator.StaffID);
            }
            List<string> distinctSession = mExemptionControl.searchAllSessionAvailable();
            mExemptionControl.shutDown();

            //add exemption for examiner
            foreach (InvigilationDuty inviDuty in inviDutyList)
            {
                foreach (Examination exam in inviDuty.ExamList)
                {
                    foreach (Staff invigilator in invigilatorList)
                    {
                        foreach (String paperExamined in invigilator.PaperCodeExamined)
                        {
                            if(paperExamined == exam.CourseCode)
                            {
                                for (int day = 0; day < exemptionForExaminerInDay; day++) {

                                    foreach(string session in distinctSession)
                                    {
                                        invigilator.ExemptionList.Add(new Exemption(inviDuty.Date.AddDays(day + 1), session));
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }   
            //assign invigilator
            assignInvigilator(invigilatorList, inviDutyList, fullConstraintList, (int)minTotalLoadOfDutyForEachInvigilator, (int)minTotalLoadOfDutyForEachChiefInvigilator, CombinedTimeslotVenueList, fullFacultyList,setting);
            lblMsg.Text = "Planning Completed Successfully! <br /><br />Please proceed to generate <a href=\"../Report/ReportingInvigilationTimetable.aspx\">Invigilation Timetable</a>.";

        }

        //create List Of invigilation duty list without staff assigned
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
                        MaintainVenueControl mVenueControl = new MaintainVenueControl();
                        string location = mVenueControl.getLocationByVenueID(venue.VenueID);
                        mVenueControl.shutDown();
                        TimeslotVenue timeslotVenue = new TimeslotVenue((timeslotVenueControl.getTimeslotID(tt.Date, tt.Session)), venue.VenueID,location, tt.Date, tt.Session, noOfInvigilator, venue.CoursesList);
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
                    else if(!(block.VenuesList.Count<=0))
                    {
                        noOfChiefInvi++;
                    }

                    TimeslotVenue timeslotVenueForChief = new TimeslotVenue(tsVenueControl.getTimeslotID(tt.Date, tt.Session), block.BlockCode, tt.Date, tt.Session, noOfChiefInvi, duration);
                    tsVenueChiefList.Add(timeslotVenueForChief);
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
        
        //get candidate list 
        public Tuple<List<InvigilatorHeuristic>,int> getCanditateList(List<InvigilatorHeuristic> invigilators, InvigilationDuty invigilationDuty, List<Constraint3> constraintList, int minInvigilationDuty, int minInvigilationDutyForChief, List<TimeslotVenue> fullTimeslotVenueList, List<Faculty> facultyList,ConstraintSetting setting)
        {
            MaintainFacultyControl mFacultyControl = new MaintainFacultyControl();
            List<InvigilatorHeuristic> CandidateList = new List<InvigilatorHeuristic>();
            int constraintCount = 0;
            
            //assign to examiner
            if(setting.AssignToExaminer == true)
            {
                foreach(Examination exam in invigilationDuty.ExamList)
                {
                    if(exam.ExamType == 'M')
                    {
                        invigilationDuty.ConstraintInvolved++;
                        invigilationDuty.MaxScore += 20;
                        foreach (InvigilatorHeuristic invi in invigilators)
                        {
                            if (invi.Staff.PaperCodeExamined.Equals(exam.CourseCode))
                            {
                                invi.Heuristic += 20;
                                
                            }
                        }
                    }
                }
            }

                foreach (Constraint3 constraint in constraintList)
                {
                    int maxScoreForInviDutyAndExam = 0;
                    int scoreForInviDutyAndExam = 0;
                    // matches the constraint exam and invigilation duty to see if it matches with the invigilation duty
                    // Day of Week of Exam
                    if (constraint.DayOfWeek != null && constraint.DayOfWeek != "")
                    {
                        maxScoreForInviDutyAndExam++;
                        if (invigilationDuty.Date.DayOfWeek.Equals(constraint.InvigilationDuty.Date.DayOfWeek))
                        {
                            scoreForInviDutyAndExam++;
                        }
                    }
                    
                    //category of invigilation
                    if (constraint.InvigilationDuty.CategoryOfInvigilator != "" && constraint.InvigilationDuty.CategoryOfInvigilator != null)
                    {
                        maxScoreForInviDutyAndExam++;
                        if (invigilationDuty.CategoryOfInvigilator.Equals(constraint.InvigilationDuty.CategoryOfInvigilator))
                        {
                            scoreForInviDutyAndExam++;
                        }
                    }

                    //duration of invigilation  
                    if (constraint.InvigilationDuty.Duration != 0)
                    {
                        maxScoreForInviDutyAndExam++;
                        if (invigilationDuty.Duration.Equals(constraint.InvigilationDuty.Duration))
                        {
                            scoreForInviDutyAndExam++;
                        }
                    }

                    //session of invigilationDuty
                    if (constraint.InvigilationDuty.Session!=null &&!constraint.InvigilationDuty.Session.Equals(""))
                    {
                        maxScoreForInviDutyAndExam++;
                        if (invigilationDuty.Session.Equals(constraint.InvigilationDuty.Session))
                        {
                            scoreForInviDutyAndExam++;
                        }
                    }

                    //location of invigilation Duty
                    if (constraint.InvigilationDuty.Location != "" && constraint.InvigilationDuty.Location != null)
                    {
                        maxScoreForInviDutyAndExam++;
                        if (invigilationDuty.Location.Equals(constraint.InvigilationDuty.Location))
                        {
                            scoreForInviDutyAndExam++;
                        }
                    }

                    //exam facultycode
                    if (!constraint.Examination.Faculty.FacultyCode.Equals('\0') && !constraint.Examination.Faculty.FacultyCode.Equals(null))
                    {
                        maxScoreForInviDutyAndExam++;
                        bool examHasFaculty = false;
                        foreach (Examination exam in invigilationDuty.ExamList)
                        {
                            if (mFacultyControl.searchFacultyByCourseCode(exam.CourseCode).FacultyCode.Equals(constraint.Examination.Faculty.FacultyCode))
                            {
                                    examHasFaculty = true;
                            }
                        }
                        if(examHasFaculty == true)
                    {
                        scoreForInviDutyAndExam++;
                    }
                    }

                    //exam examtype
                    if (!constraint.Examination.ExamType.Equals('\0') && !constraint.Examination.ExamType.Equals(null))
                    {
                        maxScoreForInviDutyAndExam++;
                        bool hasExamType = false;
                        foreach (Examination exam in invigilationDuty.ExamList)
                        {
                            if (exam.ExamType.Equals(constraint.Examination.ExamType))
                            {
                            hasExamType = true;
                            }
                        }
                            if(hasExamType == true)
                    {

                        scoreForInviDutyAndExam++;
                    }

                    }

                    //exam papertype
                    if (!constraint.Examination.PaperType.Equals('\0') && !constraint.Examination.PaperType.Equals(null))
                    {
                        maxScoreForInviDutyAndExam++;
                        bool isPaperType = false;
                        foreach (Examination exam in invigilationDuty.ExamList)
                        {
                            if (exam.PaperType.Equals(constraint.Examination.PaperType))
                            {
                                isPaperType = true;
                            }
                        }
                        if(isPaperType == true)
                    {
                        scoreForInviDutyAndExam++;
                    }
                    }

                    //exam year
                    if (constraint.Examination.Year != 0)
                    {
                        maxScoreForInviDutyAndExam++;
                    bool isExamYear = false;
                        foreach (Examination exam in invigilationDuty.ExamList)
                        {
                            if (exam.Year.Equals(constraint.Examination.Year))
                            {
                            isExamYear = true;
                            }
                        }
                        if(isExamYear == true)
                    {
                        scoreForInviDutyAndExam++;
                    }
                    }


                    //exam iscnbl paper
                    if (!constraint.IsCnblPaper.Equals(null))
                    {
                        maxScoreForInviDutyAndExam++;
                        bool isCnblPaper = false;
                        foreach (Examination exam in invigilationDuty.ExamList)
                        {
                            MaintainCourseControl mCourseControl = new MaintainCourseControl();
                            Course course = mCourseControl.searchCourseByCourseCode(exam.CourseCode);
                            mCourseControl.shutDown();
                            if (course.IsCnblPaper.Equals(constraint.IsCnblPaper))
                            {
                                isCnblPaper = true;
                        }
                    }
                    if (isCnblPaper == true)
                    {
                        scoreForInviDutyAndExam++;
                    }
                }

                if (invigilationDuty.CategoryOfInvigilator != "Chief")
                {
                    //exam is double seating
                    if (!constraint.IsDoubleSeating.Equals(null))
                    {
                        maxScoreForInviDutyAndExam++;
                        bool containDoubleSeating = false;
                        foreach (Examination exam in invigilationDuty.ExamList)
                        {
                            MaintainCourseControl mCourseControl = new MaintainCourseControl();
                            Course course = mCourseControl.searchCourseByCourseCode(exam.CourseCode);
                            mCourseControl.shutDown();
                            if (course.IsDoubleSeating.Equals(constraint.IsDoubleSeating))
                            {
                                containDoubleSeating = true;
                            }
                        }
                        if (containDoubleSeating == true)
                        {
                            scoreForInviDutyAndExam++;
                        }
                    }
                }else 
                {
                    //exam is double seating
                    if (!constraint.IsDoubleSeating.Equals(null))
                    {
                        maxScoreForInviDutyAndExam++;
                        bool containDoubleSeating = false;
                        //examination from database containing location
                        foreach (TimeslotVenue tsVenue in fullTimeslotVenueList)
                        {
                            if (tsVenue.TimeslotID.Equals(invigilationDuty.TimeslotID) && tsVenue.Location.Equals(invigilationDuty.Location))
                            {
                                MaintainExaminationControl mExamControl = new MaintainExaminationControl();
                                List<Examination> examList = new List<Examination>();
                                examList = mExamControl.searchExaminationByTimeslotAndLocation(tsVenue.VenueID, tsVenue.Location);

                                foreach (Examination exam in examList)
                                {
                                    MaintainCourseControl mCourseControl = new MaintainCourseControl();
                                    Course course = mCourseControl.searchCourseByCourseCode(exam.CourseCode);
                                    mCourseControl.shutDown();
                                    if (course.IsDoubleSeating.Equals(constraint.IsDoubleSeating))
                                    {
                                        containDoubleSeating = true;
                                    }
                                }
                            }
                        }
                        if (containDoubleSeating == true)
                        {
                            scoreForInviDutyAndExam++;
                        }
                    }
                }
                
                

                //finish checking for duty and exam
                if (maxScoreForInviDutyAndExam == scoreForInviDutyAndExam)
                {
                    constraintCount++;
                    invigilationDuty.MaxScore += constraint.ConstraintImportanceValue;

                    foreach (InvigilatorHeuristic invigilator in invigilators)
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
                        //hasDutyOnSameDay = true 
                        if (!constraint.HasOtherDutyOnSameDay.Equals(null))
                        {
                            maxHeuristic++;
                            if (invigilator.Staff.hasOtherDutyOnSameDay(invigilator.Staff.InvigilationDuty, invigilationDuty.Date, invigilationDuty.Session).Equals(constraint.HasOtherDutyOnSameDay))
                            {
                                score++;
                            }
                            else
                            {
                                if (constraint.IsHardConstraint == true)
                                    invigilator.PossibleCanditate = false;
                            }

                        }

                        //has durationDutyOnSameDay
                        if (!constraint.HasSpecificDurationDutyOnSameDay.Equals(null) && !constraint.HasSpecificDurationDutyOnSameDayInt.Equals(0))
                        {
                            maxHeuristic++;
                            if (invigilator.Staff.hasSpecificDurationOfADutyOnSameDay(invigilator.Staff.InvigilationDuty, invigilationDuty.Date, constraint.HasSpecificDurationDutyOnSameDayInt).Equals(constraint.HasSpecificDurationDutyOnSameDay))
                            {
                                score++;
                            }
                            else if (constraint.IsHardConstraint == true)
                            {
                                invigilator.PossibleCanditate = false;
                            }
                        }

                        //has sessionDutyonSameDay
                        if (!constraint.HasSpecificSessionDutyOnSameDay.Equals(null) && (!constraint.HasSpecificSessionDutyOnSameDayString.Equals("") && !constraint.HasSpecificSessionDutyOnSameDayString.Equals("")))
                        {
                            maxHeuristic++;
                            if (invigilator.Staff.hasSpecificSessionDutyOnSameDay(invigilator.Staff.InvigilationDuty, invigilationDuty.Date, constraint.HasSpecificSessionDutyOnSameDayString).Equals(constraint.HasSpecificSessionDutyOnSameDay))
                            {
                                score++;

                            }
                            else if (constraint.IsHardConstraint == true)
                            {
                                invigilator.PossibleCanditate = false;
                            }

                        }

                        //has specific Session and duration on the same day
                        if (!constraint.HasSpecificSessionAndDurationDutyOnSameDay.Equals(null) && !constraint.HasSpecificDurationDutyOnSameDayInt.Equals(0) && (!constraint.HasSpecificSessionDutyOnSameDayString.Equals("") && !constraint.HasSpecificSessionDutyOnSameDayString.Equals(null)))
                        {
                            maxHeuristic++;
                            if (invigilator.Staff.hasSpecificSessionAndDurationDutyOnSameDay(invigilator.Staff.InvigilationDuty, invigilationDuty.Date, constraint.HasSpecificSessionDutyOnSameDayString, constraint.HasSpecificDurationDutyOnSameDayInt).Equals(constraint.HasSpecificSessionAndDurationDutyOnSameDay))
                            {
                                score++;
                            }
                            else if (constraint.IsHardConstraint == true)
                            {
                                invigilator.PossibleCanditate = false;
                            }
                        }

                        //invigilator's facultycode
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

                        //Muslim
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

                        //isExperiencedInvigilator
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

                        //isChiefInvgilator
                        if (!constraint.Invigilator.IsChiefInvi.Equals(null))
                        {
                            maxHeuristic++;
                            if (invigilator.Staff.IsChiefInvi.Equals(constraint.Invigilator.IsChiefInvi))
                            {
                                score++;
                            }
                            else
                            {
                                if (constraint.IsHardConstraint == true && !constraint.Invigilator.IsChiefInvi.Equals(null))
                                    invigilator.PossibleCanditate = false;
                            }
                        }

                        //isTakingSTSPHD
                        if (!constraint.Invigilator.IsTakingSTSPhD.Equals(null))
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

                        //TypeOfEmploy
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

                        TimeslotVenue tsVenue = getTimeslotVenue(fullTimeslotVenueList, invigilationDuty.Date, invigilationDuty.Session, invigilationDuty.VenueID, invigilationDuty.Location);
                        
                        //min experienced invigilator in a venue
                        if (constraint.MinExperiencedInvigilator != 0)
                        {
                            int percentageOfExperiencedInvigilator = 0;
                            if (!tsVenue.InvigilatorList.Equals(null))
                            {
                                percentageOfExperiencedInvigilator = tsVenue.percentageOfExperiencedInvigilator(tsVenue.InvigilatorList, tsVenue.NoOfInvigilatorRequired);
                                if (percentageOfExperiencedInvigilator <= constraint.MinExperiencedInvigilator)
                                {
                                    maxHeuristic++;
                                    if (invigilator.Staff.IsInviAbove2Years.Equals(true))
                                    {
                                        score++;
                                    }
                                }
                            }
                            else
                            {
                                if (percentageOfExperiencedInvigilator <= constraint.MinExperiencedInvigilator)
                                {
                                    maxHeuristic++;
                                    if (invigilator.Staff.IsInviAbove2Years.Equals(true))
                                    {
                                        score++;
                                    }
                                }
                            }
                        }
                        

                        if (score == maxHeuristic)
                        {
                            invigilator.Heuristic += constraint.ConstraintImportanceValue;
                        }
                    }//max
                    }//invi
                }//contraint


                //doesnt affected by other
                foreach (InvigilatorHeuristic invigilator in invigilators)
                {
                    if (invigilator.PossibleCanditate != false)
                    {
                        CandidateList.Add(invigilator);
                    }
                }

                mFacultyControl.shutDown();
                return new Tuple<List<InvigilatorHeuristic>,int>(CandidateList,constraintCount);
        }

        //assign invigilator
        public void assignInvigilator(List<Staff> staffs, List<InvigilationDuty> invigilationDuties, List<Constraint3> constraintList, int minInvigilationDuty, int minInvigilationDutyForChief, List<TimeslotVenue> fullTimeslotVenueList, List<Faculty> facultyList,ConstraintSetting setting)
        {
            List<Tuple<InvigilationDuty, List<InvigilatorHeuristic>>> otherPossibleCandidate = new List<Tuple<InvigilationDuty, List<InvigilatorHeuristic>>>();
            
            //get candidate list and calculate constraint involved
            foreach (InvigilationDuty invigilationDuty in invigilationDuties)
            {
                List<InvigilatorHeuristic> emptyInvi = new List<InvigilatorHeuristic>();
                foreach (Staff staff in staffs)
                {
                    emptyInvi.Add(new InvigilatorHeuristic(staff));
                }

                //get possible candidate for each duty
                Tuple<List<InvigilatorHeuristic>, int> tuple = getCanditateList(emptyInvi, invigilationDuty, constraintList, minInvigilationDuty, minInvigilationDutyForChief, fullTimeslotVenueList, facultyList,setting);
                List<InvigilatorHeuristic> candidateList = new List<InvigilatorHeuristic>(tuple.Item1);
                invigilationDuty.PossibleCandidate = new List<InvigilatorHeuristic>(candidateList);
                if (tuple.Item2 != 0)
                {
                    invigilationDuty.ConstraintInvolved = tuple.Item2;
                }
            }

            //sort invigilation duty according to constraint involved
            var newInviDutyList = invigilationDuties.OrderByDescending(x => x.MaxScore).ThenBy(y => y.ConstraintInvolved).ToList();

            //process assignaton of invigilator
            foreach (InvigilationDuty invigilationDuty in newInviDutyList) { 

                //remove exempted or not available candidate
                List<InvigilatorHeuristic> processedInvigilatorList = removeExemptedAndNotAvailableInvigilator(invigilationDuty.PossibleCandidate, invigilationDuty, minInvigilationDuty, minInvigilationDutyForChief,setting, staffs,facultyList);

                //field for the highest scoring candidate to be keep in
                List<InvigilatorHeuristic> finalCandidateList = new List<InvigilatorHeuristic>();

                //max score for each duty 
                int maxScore = 0;
                
                //find the highest score of candidate of invigilators
                foreach (InvigilatorHeuristic possibleCanditate in processedInvigilatorList)
                {
                    if (possibleCanditate.Heuristic>maxScore)
                    {
                        maxScore = possibleCanditate.Heuristic;
                    }       
                }

                //find the invigilator with highest score and add them into a list
                foreach (InvigilatorHeuristic invigilator in processedInvigilatorList)
                {
                    if (invigilator.Heuristic.Equals(maxScore))
                    {
                        finalCandidateList.Add(invigilator);
                    }
                }

                //shuffle the final list before assignation
                if (finalCandidateList.Count > 1) {
                    finalCandidateList.Shuffle();
                }
                
                 
                    InvigilatorHeuristic finalInvigilatorCandidate = new InvigilatorHeuristic();
                    finalInvigilatorCandidate = finalCandidateList[0];
                    List<InvigilatorHeuristic> otherCandidate = new List<InvigilatorHeuristic>();
                    //add other candidate to other candidatelist
                    for (int x= 1; x < finalCandidateList.Count; x++)
                    {
                        otherCandidate.Add(finalCandidateList[x]);
                    }

                    //add into tuple to store each other candidate possible for each duty
                    otherPossibleCandidate.Add(Tuple.Create(invigilationDuty, otherCandidate));
                
                    //updating from here on

                    //update invigilator assigned to own faculty count;
                    List<char> repeatedFaculty = new List<char>();

                    foreach (Examination exam in invigilationDuty.ExamList)
                    {
                        //for preventing multiple exam that has similar faculty code by checking  with is repeated faculty list
                        bool isRepeated = false;
                        foreach (char facultyCode in repeatedFaculty)
                        {
                            if (exam.Faculty.FacultyCode.Equals(facultyCode))
                            {
                                isRepeated = true;
                            }
                        }

                        // if the faculty code matches with the staff faculty code and the faculty code is not repeated then increase the number of invigilator assigned to their own faculty
                        if (exam.Faculty.FacultyCode.Equals(finalInvigilatorCandidate.Staff.FacultyCode) && isRepeated.Equals(false))
                        {
                            foreach (Faculty faculty in facultyList)
                            {
                                if (faculty.FacultyCode.Equals(finalInvigilatorCandidate.Staff.FacultyCode))
                                {
                                    faculty.InvigilatorAssignedToOwnFacultyDutyCount++;
                                }
                            }
                        }

                        //if the faculty code is a new faculty code then add it into the list of existed faculty code
                        if (isRepeated.Equals(false) && exam.Faculty.FacultyCode.Equals('\0') && exam.Faculty.Equals(null))
                        {
                            repeatedFaculty.Add(exam.Faculty.FacultyCode);
                        }
                    }

                    //update invigilation duty
                    invigilationDuty.StaffID = finalInvigilatorCandidate.Staff.StaffID;
                    //lblMsg.Text += invigilationDuty.TimeslotID + " , " + finalInvigilatorCandidate.Staff.StaffID + " , " + finalInvigilatorCandidate.Heuristic + " , " + invigilationDuty.ConstraintInvolved + ", " + invigilationDuty.MaxScore + "<br/>";

                    //update staff duty
                    foreach (Staff invigilator in staffs)
                    {
                        if (invigilator.StaffID.Equals(finalInvigilatorCandidate.Staff.StaffID))
                        {
                            int minInviDuty = 0;

                            invigilator.InvigilationDuty.Add(invigilationDuty);
                            if (invigilationDuty.Date.DayOfWeek.Equals("Saturday"))
                            {
                                invigilator.NoOfSatSession++;
                            }

                            //no of evening session update
                            if (invigilationDuty.Session.Equals("VM"))
                            {
                                invigilator.NoOfEveningSession++;
                            }

                            //update extra session
                            if (invigilator.IsChiefInvi.Equals(true))
                            {
                                minInviDuty = minInvigilationDutyForChief;
                            }
                            else
                            {
                                minInviDuty = minInvigilationDuty;
                            }

                            if (invigilator.IsTakingSTSPhD.Equals(true))
                            {
                                minInviDuty = (int)(((double)minInviDuty * 2) / 3);
                            }

                            if (invigilator.InvigilationDuty.Count >= minInviDuty)
                            {
                                invigilator.NoOfExtraSession++;
                            }
                        }
                    }

                    //update timeslotvenue
                    foreach (TimeslotVenue timeslotVenue in fullTimeslotVenueList)
                    {
                        if (timeslotVenue.VenueID == null)
                        {
                            if (timeslotVenue.Date.Equals(invigilationDuty.Date) && timeslotVenue.Session.Equals(invigilationDuty.Session) && timeslotVenue.Location.Equals(invigilationDuty.Location))
                            {
                                timeslotVenue.InvigilatorList.Add(finalInvigilatorCandidate.Staff);
                            }
                        }
                        else
                        {
                            if (timeslotVenue.Date.Equals(invigilationDuty.Date) && timeslotVenue.Session.Equals(invigilationDuty.Session) && timeslotVenue.Location.Equals(invigilationDuty.Location) && timeslotVenue.VenueID.Equals(timeslotVenue.VenueID))
                            {
                                timeslotVenue.InvigilatorList.Add(finalInvigilatorCandidate.Staff);
                            }
                        }
                    }
                }

            foreach(InvigilationDuty assignedInviDuties in newInviDutyList)
            {
                Staff staff = new Staff();
                MaintainStaffControl mStaffControl = new MaintainStaffControl();
                staff = mStaffControl.getStaffByID(assignedInviDuties.StaffID);
                MaintainInvigilationDutyControl mInvigilationDutyControl = new MaintainInvigilationDutyControl();
                mInvigilationDutyControl.addInvigilationDuty(staff, assignedInviDuties);
                mInvigilationDutyControl.shutDown();
            }
           
        }
        
        //get timeslotvenue with the similar duty session and date 
        public TimeslotVenue getTimeslotVenue(List<TimeslotVenue> fullTimeslotVenueList,DateTime date, string session, string venueID, string location)
        {
            TimeslotVenue tsVenue = new TimeslotVenue();
            foreach(TimeslotVenue timeslotVenue in fullTimeslotVenueList)
            {

                if (timeslotVenue.VenueID == null)
                {
                    if (timeslotVenue.Date.Equals(timeslotVenue.Date.Equals(date) && timeslotVenue.Equals(session) && timeslotVenue.Location.Equals(location)))
                    {
                        tsVenue = timeslotVenue;
                    }
                }
                else
                {
                    if (timeslotVenue.Date.Equals(timeslotVenue.Date.Equals(date) && timeslotVenue.Equals(session) && timeslotVenue.Location.Equals(location) && timeslotVenue.VenueID.Equals(venueID)))
                    {
                        tsVenue = timeslotVenue;
                    }
                }
            }
            return tsVenue;
        }
        
        // remove exempted and unavailable invigilator before assignation of invigilator
        public List<InvigilatorHeuristic> removeExemptedAndNotAvailableInvigilator(List<InvigilatorHeuristic> candidateInvigilatorList, InvigilationDuty invigilatonDuty, int minInvigilationDuty, int minInvigilationDutyForChief, ConstraintSetting setting, List<Staff> staffs, List<Faculty> facultyList)
        {
            List<InvigilatorHeuristic> finalInvigilatorCandidateList = new List<InvigilatorHeuristic>();
            MaintainExemptionControl exemptionControl = new MaintainExemptionControl();
            
            foreach (InvigilatorHeuristic invigilator in candidateInvigilatorList)
            {
                foreach (Staff staff in staffs)
                {
                    if (invigilator.Staff.StaffID.Equals(staff.StaffID))
                    {
                        bool isAvailable = true;
                        foreach (Exemption exemption in staff.ExemptionList)
                        {   //remove exempted invigilators
                            if (exemption.Date.Equals(invigilatonDuty.Date) && exemption.Session.Equals(invigilatonDuty.Date))
                            {
                                isAvailable = false;
                            }
                        }
                        //remove staff from list if already has relief session if relief session is already 1
                        if (staff.NoAsReliefInvi.Equals(setting.MaxReliefSession) && invigilatonDuty.CategoryOfInvigilator.Equals("Relief"))
                        {
                            isAvailable = false;
                        }

                        int maxConsecutiveDay = 3;
                        bool hasConsecutiveDuty = false;
                        int currentConsecutiveDay = 0;
                        //remove staff when staff already have how many consecutive session
                        maxConsecutiveDay -= 1;
                        for (int x = 0 - maxConsecutiveDay; x < maxConsecutiveDay; x++) {
                            DateTime currentDay = invigilatonDuty.Date.AddDays(x);
                            foreach (InvigilationDuty invigilationDuty in staff.InvigilationDuty)
                            {
                                if (invigilationDuty.Date.Equals(currentDay))
                                {
                                    currentConsecutiveDay++;
                                }
                            }
                            if(currentConsecutiveDay == maxConsecutiveDay)
                            {
                                hasConsecutiveDuty = true;
                            }
                        }

                        if(hasConsecutiveDuty == true)
                        {
                            isAvailable = false;
                        }

                        //when staff already has an extra session then they are not eligible for the duty anymore
                        if (staff.NoOfExtraSession.Equals(setting.MaxExtraSession))
                        {
                            isAvailable = false;

                            //remove staf from list if already has saturday session
                            if (staff.NoOfSatSession.Equals(setting.MaxSaturdaySession) && invigilatonDuty.Date.DayOfWeek.Equals("Saturday"))
                            {
                                isAvailable = false;
                            }

                            foreach (InvigilationDuty inviDuty in staff.InvigilationDuty)
                            {
                                if (inviDuty.Date.Equals(invigilatonDuty.Date) && inviDuty.Session.Equals(invigilatonDuty.Session))
                                {
                                    isAvailable = false;
                                }
                            }
                        }

                        if (isAvailable == true)
                        {
                            finalInvigilatorCandidateList.Add(invigilator);
                        }
                    }
                }

            }
                exemptionControl.shutDown();
                return finalInvigilatorCandidateList;
}

        //pass in invigilation duty list to calculate how many invigilators are assigned to their own faculty
       /* public int calculatePercentageOfInvigilatorAssignedToOwnFaculty(List<InvigilationDuty> invigilationDutyList, Faculty faculty , char facultyCodeFromDuty)
        {
            double result = 0;
            foreach(InvigilationDuty inviDuty in invigilationDutyList)
            {
                if()
            }
            return (int)result;
        }*/
        
        
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
