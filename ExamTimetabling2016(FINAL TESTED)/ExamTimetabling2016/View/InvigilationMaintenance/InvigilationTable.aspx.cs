using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace ExamTimetabling2016
{
    public partial class InvigilatationTable : System.Web.UI.Page
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

            MaintainTimetableControl maintainTimetableControl = new MaintainTimetableControl();
            List<Timetable> examTimetable = maintainTimetableControl.selectTimetable();
            maintainTimetableControl.shutDown();

            //This part will be unchanged (CS)
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            double totalLoadOfDutyForEachInvigilator = calculateTotalLoadOfDutyForEachInvigilator(calculateTotalInvigilatorsRequired(examTimetable), maintainStaffControl.countTotalInvigilatorsAvailable());
            double totalLoadOfDutyForEachChiefInvigilator = calculateTotalLoadOfDutyForEachChiefInvigilator(calculateTotalChiefInvigilatorsRequired(examTimetable), maintainStaffControl.countTotalChiefInvigilatorsAvailable());
            maintainStaffControl.shutDown();

            //This part will be unchanged (CS)
            double minTotalLoadOfDutyForEachInvigilator = (int)totalLoadOfDutyForEachInvigilator;
            double minTotalLoadOfDutyForEachChiefInvigilator = (int)totalLoadOfDutyForEachChiefInvigilator;
            
            examTimetable = assignExaminersAsInvigilators(minTotalLoadOfDutyForEachInvigilator, examTimetable);
            examTimetable = assignExaminersAsChiefInvigilators(minTotalLoadOfDutyForEachChiefInvigilator, examTimetable);
            examTimetable = assignChiefInvigilators(getChiefInvigilatorsList(), minTotalLoadOfDutyForEachChiefInvigilator, examTimetable);
            examTimetable = assignInvigilatorsToPrioritizedVenue(getExperiencedInvigilatorsList(), minTotalLoadOfDutyForEachInvigilator, examTimetable, true);
            examTimetable = assignInvigilatorsInCharge(minTotalLoadOfDutyForEachInvigilator, examTimetable);
            examTimetable = assignInvigilatorsToPrioritizedVenue(getInvigilatorsList(), minTotalLoadOfDutyForEachInvigilator, examTimetable, false);
            examTimetable = assignMuslimReliefInvigilatorsToEVSession(getInvigilatorsList(), getMuslimInvigilatorsList(), minTotalLoadOfDutyForEachInvigilator, examTimetable);
            examTimetable = assignExperiencedReliefInvigilators(getExperiencedInvigilatorsList(), minTotalLoadOfDutyForEachInvigilator, examTimetable, true);
            examTimetable = assignExperiencedReliefInvigilators(getExperiencedInvigilatorsList(), minTotalLoadOfDutyForEachInvigilator, examTimetable, false);
            examTimetable = assignExperiencedInvigilators(getExperiencedInvigilatorsList(), minTotalLoadOfDutyForEachInvigilator, examTimetable);
            examTimetable = assignReliefInvigilators(getInvigilatorsList(), minTotalLoadOfDutyForEachInvigilator, examTimetable, true);
            examTimetable = assignReliefInvigilators(getInvigilatorsList(), minTotalLoadOfDutyForEachInvigilator, examTimetable, false);
            examTimetable = assignInvigilators(getInvigilatorsList(), minTotalLoadOfDutyForEachInvigilator, examTimetable);

            //fill in all unassigned duty
            examTimetable = assignExaminersAsInvigilators(totalLoadOfDutyForEachInvigilator, examTimetable);
            examTimetable = assignExaminersAsChiefInvigilators(totalLoadOfDutyForEachChiefInvigilator, examTimetable);
            examTimetable = assignChiefInvigilators(getChiefInvigilatorsList(), totalLoadOfDutyForEachChiefInvigilator, examTimetable);
            examTimetable = assignInvigilatorsToPrioritizedVenue(getExperiencedInvigilatorsList(), totalLoadOfDutyForEachInvigilator, examTimetable, true);
            examTimetable = assignInvigilatorsInCharge(totalLoadOfDutyForEachInvigilator, examTimetable);
            examTimetable = assignInvigilatorsToPrioritizedVenue(getInvigilatorsList(), totalLoadOfDutyForEachInvigilator, examTimetable, false);
            examTimetable = assignMuslimReliefInvigilatorsToEVSession(getInvigilatorsList(), getMuslimInvigilatorsList(), totalLoadOfDutyForEachInvigilator, examTimetable);
            examTimetable = assignExperiencedReliefInvigilators(getExperiencedInvigilatorsList(), totalLoadOfDutyForEachInvigilator, examTimetable, true);
            examTimetable = assignExperiencedReliefInvigilators(getExperiencedInvigilatorsList(), totalLoadOfDutyForEachInvigilator, examTimetable, false);
            examTimetable = assignExperiencedInvigilators(getExperiencedInvigilatorsList(), totalLoadOfDutyForEachInvigilator, examTimetable);
            examTimetable = assignReliefInvigilators(getInvigilatorsList(), totalLoadOfDutyForEachInvigilator, examTimetable, true);
            examTimetable = assignReliefInvigilators(getInvigilatorsList(), totalLoadOfDutyForEachInvigilator, examTimetable, false);
            examTimetable = assignInvigilators(getInvigilatorsList(), totalLoadOfDutyForEachInvigilator, examTimetable);

            examTimetable = changeLocationOfRelief(examTimetable);

            lblMsg.Text = "Planning Completed Successfully! <br /><br />Please proceed to generate <a href=\"../Report/ReportingInvigilationTimetable.aspx\">Invigilation Timetable</a>.";
        }

        //get list of invigilators
        public static List<Staff> getInvigilatorsList()
        {
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            List<Staff> invigilatorsList = maintainStaffControl.searchLecturer("Invigilator", "categoryOfInvigilator");
            maintainStaffControl.shutDown();
            return invigilatorsList;
        }

        //get list of chief invigilators
        public static List<Staff> getChiefInvigilatorsList()
        {
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            List<Staff> chiefInvigilatorsList = maintainStaffControl.searchLecturer("Chief", "categoryOfInvigilator");
            maintainStaffControl.shutDown();
            return chiefInvigilatorsList;
        }

        //get list of experienced invigilators
        public static List<Staff> getExperiencedInvigilatorsList()
        {
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            List<Staff> experiencedInvigilatorsList = maintainStaffControl.searchLecturer("InviAbove2Years", "categoryOfInvigilator");
            maintainStaffControl.shutDown();
            return experiencedInvigilatorsList;
        }

        //get list of Muslim invigilators
        public static List<Staff> getMuslimInvigilatorsList()
        {
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            List<Staff> experiencedInvigilatorsList = maintainStaffControl.searchLecturer("Muslim", "isMuslim");
            maintainStaffControl.shutDown();
            return experiencedInvigilatorsList;
        }

        //assign examiners
        public static List<Timetable> assignExaminersAsInvigilators(double totalLoadOfDutyForEachInvigilator, List<Timetable> examTimetable)
        {
            string checkType = "Examiner";
            List<string> paperCodeList = new List<string>();
            List<Staff> examinersList = new List<Staff>();
            List<Block> blockList = new List<Block>();
            List<Venue> venueList = new List<Venue>();
            double totalLoadOfDutyForEachInvigilatorAfterRound = (int)Math.Round(totalLoadOfDutyForEachInvigilator, MidpointRounding.AwayFromZero);

            int countForTimetable = 0;
            int countForBlock = 0;
            foreach (Timetable examTimetableInSameDayAndSession in examTimetable.ToList())
            {
                //get a list of paper code in same day and session (assume same paper only examined in same day)
                paperCodeList.Clear();
                foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                {
                    foreach (Venue venue in block.VenuesList)
                    {
                        foreach (Course paper in venue.CoursesList)
                        {
                            if (paperCodeList.Count == 0)
                            {
                                paperCodeList.Add(paper.CourseCode);
                            }
                            bool isRepeated = false;
                            foreach (string paperCode in paperCodeList)
                            {
                                if (paperCode.Equals(paper.CourseCode))
                                {
                                    isRepeated = true;
                                    break;
                                }
                            }
                            if (!isRepeated)
                            {
                                paperCodeList.Add(paper.CourseCode);
                            }
                        }
                    }
                    countForBlock++;
                }

                foreach (string paperCode in paperCodeList)
                {
                    //get a list of examiners with same paper code
                    MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
                    examinersList = maintainStaffControl.searchLecturer(paperCode, "examAsInvi");
                    maintainStaffControl.shutDown();

                    //remove the unavailable examiner from list
                    List<Staff> tempExaminersList = new List<Staff>();
                    for (int i = 0; i < examinersList.Count; i++)
                    {
                        if (isAvailable(examinersList[i], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, checkType, new Venue()))
                        {
                            tempExaminersList.Add(examinersList[i]);
                        }
                    }
                    examinersList = tempExaminersList;
                    examinersList.Shuffle();

                    //get a list of venues with same paper code
                    bool isContainsVenuesWithSameCourseCode;
                    blockList.Clear();
                    venueList.Clear();
                    for (int x = 0; x < examTimetableInSameDayAndSession.BlocksList.Count; x++)
                    {
                        Block block = examTimetableInSameDayAndSession.BlocksList[x];
                        isContainsVenuesWithSameCourseCode = false;
                        for (int y = 0; y < block.VenuesList.Count; y++)
                        {
                            bool isAvailableVenue = true;
                            if (examTimetableInSameDayAndSession.BlocksList[x].VenuesList[y].InvigilatorsList.Count == getNumberOfInvigilatorsRequired(examTimetableInSameDayAndSession.BlocksList[x].VenuesList[y]) - 1)
                            {
                                isAvailableVenue = false;
                                foreach (Staff invigilator in examTimetableInSameDayAndSession.BlocksList[x].VenuesList[y].InvigilatorsList)
                                {
                                    if (invigilator.IsInviAbove2Years)
                                    {
                                        isAvailableVenue = true;
                                        break;
                                    }

                                }
                            }
                            foreach (Course paper in examTimetableInSameDayAndSession.BlocksList[x].VenuesList[y].CoursesList)
                            {
                                if (paper.CourseCode.Equals(paperCode) && isAvailableVenue)
                                {
                                    isContainsVenuesWithSameCourseCode = true;
                                    venueList.Add(examTimetableInSameDayAndSession.BlocksList[x].VenuesList[y]);
                                    break;
                                }
                            }
                        }
                        if (isContainsVenuesWithSameCourseCode) //if venue in the block has the paper with same paper code
                        {
                            blockList.Add(new Block(block.BlockCode, "", null, venueList, block.EastOrWest));
                        }
                    }

                    MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
                    string catOfInvi = "Invigilator";
                    /**
                     * assign one examiner to each venue if the number of invigilators in the venue have not full and
                     * there are examiner available
                     **/
                    for (int x = 0; x < blockList.Count && examinersList.Count > 0; x++)
                    {
                        Block block = blockList[x];
                        for (int y = 0; y < block.VenuesList.Count && examinersList.Count > 0; y++)
                        {
                            if (blockList[x].VenuesList[y].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(blockList[x].VenuesList[y]))
                            {
                                blockList[x].VenuesList[y].InvigilatorsList.Add(examinersList[0]);
                                maintainInvigilationDutyControl.addInvigilationDuty(examinersList[0], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, blockList[x].VenuesList[y].VenueID, blockList[x].BlockCode, catOfInvi, getLongestCourseDuration(blockList[x].VenuesList[y])));
                                if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && examinersList[0].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                {
                                    examinersList[0].NoOfExtraSession++;
                                    maintainInvigilationDutyControl.updateNoOfExtraSession(examinersList[0].StaffID);
                                }
                                examinersList.RemoveAt(0);
                            }
                        }
                    }

                    for (int x = 0; x < blockList.Count && examinersList.Count > 0; x++)
                    {
                        /**
                         * only one paper or subject examined in the venue and 
                         * the number of invigilators in the venue have not full and
                         * venue is neither DU, KS1 nor KS2
                         * */
                        Block block = blockList[x];
                        for (int y = 0; y < block.VenuesList.Count && examinersList.Count > 0; y++)
                        {
                            if (blockList[x].VenuesList[y].CoursesList.Count == 1 &&
                            blockList[x].VenuesList[y].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(blockList[x].VenuesList[y]) &&
                            !blockList[x].VenuesList[y].VenueID.Equals("DU") && !blockList[x].VenuesList[y].VenueID.Equals("KS1") &&
                            !blockList[x].VenuesList[y].VenueID.Equals("KS2"))
                            {
                                while (blockList[x].VenuesList[y].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(blockList[x].VenuesList[y]) &&
                                    examinersList.Count > 0)
                                {
                                    blockList[x].VenuesList[y].InvigilatorsList.Add(examinersList[0]);
                                    maintainInvigilationDutyControl.addInvigilationDuty(examinersList[0], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, blockList[x].VenuesList[y].VenueID, blockList[x].BlockCode, catOfInvi, getLongestCourseDuration(blockList[x].VenuesList[y])));
                                    if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && examinersList[0].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                    {
                                        examinersList[0].NoOfExtraSession++;
                                        maintainInvigilationDutyControl.updateNoOfExtraSession(examinersList[0].StaffID);
                                    }
                                    examinersList.RemoveAt(0);
                                }
                            }
                        }
                    }

                    for (int x = 0; x < blockList.Count && examinersList.Count > 0; x++)
                    {
                        /**
                         * 
                         **/
                        Block block = blockList[x];
                        for (int y = 0; y < block.VenuesList.Count && examinersList.Count > 0; y++)
                        {
                            if (blockList[x].VenuesList[y].CoursesList.Count == 1 &&
                            blockList[x].VenuesList[y].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(blockList[x].VenuesList[y]) &&
                            (blockList[x].VenuesList[y].VenueID.Equals("DU") || blockList[x].VenuesList[y].VenueID.Equals("KS1") || blockList[x].VenuesList[y].VenueID.Equals("KS2")))
                            {
                                while (blockList[x].VenuesList[y].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(blockList[x].VenuesList[y]) &&
                                examinersList.Count > 0)
                                {
                                    blockList[x].VenuesList[y].InvigilatorsList.Add(examinersList[0]);
                                    maintainInvigilationDutyControl.addInvigilationDuty(examinersList[0], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, blockList[x].VenuesList[y].VenueID, blockList[x].BlockCode, catOfInvi, getLongestCourseDuration(blockList[x].VenuesList[y])));
                                    if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && examinersList[0].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                    {
                                        examinersList[0].NoOfExtraSession++;
                                        maintainInvigilationDutyControl.updateNoOfExtraSession(examinersList[0].StaffID);
                                    }
                                    examinersList.RemoveAt(0);
                                }
                            }
                        }
                    }

                    for (int x = 0; x < blockList.Count && examinersList.Count > 0; x++)
                    {
                        /**
                         * assign examiner to the timetable if paper examined in the venue is 
                         * less than the number of invigilators required
                         **/
                        Block block = blockList[x];
                        for (int y = 0; y < block.VenuesList.Count && examinersList.Count > 0; y++)
                        {
                            if (blockList[x].VenuesList[y].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(blockList[x].VenuesList[y]))
                            {
                                if (blockList[x].VenuesList[y].CoursesList.Count < getNumberOfInvigilatorsRequired(blockList[x].VenuesList[y]))
                                {
                                    int numberOfExaminersWithSameCourseCode = 0;
                                    foreach (Staff invigilator in blockList[x].VenuesList[y].InvigilatorsList)
                                    {
                                        foreach (string courseCode in invigilator.PaperCodeExamined)
                                        {
                                            if (courseCode.Equals(paperCode))
                                            {
                                                numberOfExaminersWithSameCourseCode++;
                                                break;
                                            }
                                        }
                                    }
                                    while (examinersList.Count > 0 &&
                                          blockList[x].VenuesList[y].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(blockList[x].VenuesList[y]) &&
                                          numberOfExaminersWithSameCourseCode <
                                          getNumberOfInvigilatorsRequired(blockList[x].VenuesList[y]) / blockList[x].VenuesList[y].CoursesList.Count)
                                    {
                                        blockList[x].VenuesList[y].InvigilatorsList.Add(examinersList[0]);
                                        maintainInvigilationDutyControl.addInvigilationDuty(examinersList[0], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, blockList[x].VenuesList[y].VenueID, blockList[x].BlockCode, catOfInvi, getLongestCourseDuration(blockList[x].VenuesList[y])));
                                        if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && examinersList[0].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                        {
                                            examinersList[0].NoOfExtraSession++;
                                            maintainInvigilationDutyControl.updateNoOfExtraSession(examinersList[0].StaffID);
                                        }
                                        examinersList.RemoveAt(0);
                                        numberOfExaminersWithSameCourseCode++;
                                    }
                                }
                            }
                        }
                    }


                    //assign examiner to venue which the number of invigilators required have not full
                    for (int x = 0; x < blockList.Count && examinersList.Count > 0; x++)
                    {
                        Block block = blockList[x];
                        for (int y = 0; y < block.VenuesList.Count && examinersList.Count > 0; y++)
                        {
                            while (examinersList.Count > 0 &&
                               blockList[x].VenuesList[y].InvigilatorsList.Count <
                               getNumberOfInvigilatorsRequired(blockList[x].VenuesList[y]))
                            {
                                blockList[x].VenuesList[y].InvigilatorsList.Add(examinersList[0]);
                                maintainInvigilationDutyControl.addInvigilationDuty(examinersList[0], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, blockList[x].VenuesList[y].VenueID, blockList[x].BlockCode, catOfInvi, getLongestCourseDuration(blockList[x].VenuesList[y])));
                                if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && examinersList[0].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                {
                                    examinersList[0].NoOfExtraSession++;
                                    maintainInvigilationDutyControl.updateNoOfExtraSession(examinersList[0].StaffID);
                                }
                                examinersList.RemoveAt(0);
                            }
                        }
                    }

                    //assign examiner to nearby venue                    
                    for (int x = 0; x < blockList.Count && examinersList.Count > 0; x++)
                    {
                        foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                        {
                            if (block.Equals(blockList[x]))
                            {
                                foreach (Venue venue in block.VenuesList)
                                {
                                    while (!blockList[x].VenuesList.Contains(venue) && venue.InvigilatorsList.Count < getNumberOfInvigilatorsRequired(venue)
                                        && examinersList.Count > 0)
                                    {
                                        bool isAvailableVenue = true;
                                        if (venue.InvigilatorsList.Count == getNumberOfInvigilatorsRequired(venue) - 1)
                                        {
                                            isAvailableVenue = false;
                                            foreach (Staff invigilator in venue.InvigilatorsList)
                                            {
                                                if (invigilator.IsInviAbove2Years)
                                                {
                                                    isAvailableVenue = true;
                                                    break;
                                                }

                                            }
                                        }
                                        if (isAvailableVenue)
                                        {
                                            venue.InvigilatorsList.Add(examinersList[0]);
                                            maintainInvigilationDutyControl.addInvigilationDuty(examinersList[0], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                            if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && examinersList[0].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                            {
                                                examinersList[0].NoOfExtraSession++;
                                                maintainInvigilationDutyControl.updateNoOfExtraSession(examinersList[0].StaffID);
                                            }
                                            examinersList.RemoveAt(0);
                                        }
                                    }
                                    blockList[x].VenuesList.Add(venue);
                                }
                            }
                        }
                    }
                    maintainInvigilationDutyControl.shutDown();
                }

                //assign the venues with invigilators back into the timetable
                for (int x = 0; x < examTimetableInSameDayAndSession.BlocksList.Count; x++)
                {
                    foreach (Block block in blockList.ToList())
                    {
                        if (examTimetableInSameDayAndSession.BlocksList[x].BlockCode.Equals(block.BlockCode))
                        {
                            for (int y = 0; y < examTimetableInSameDayAndSession.BlocksList[x].VenuesList.Count; y++)
                            {
                                int countForVenue = 0;
                                countForVenue = 0;
                                List<Venue> venuesList = block.VenuesList;
                                foreach (Venue venue in venuesList.ToList())
                                {
                                    if (examTimetableInSameDayAndSession.BlocksList[x].VenuesList[y].VenueID.Equals(venue.VenueID))
                                    {
                                        examTimetableInSameDayAndSession.BlocksList[x].VenuesList[y].InvigilatorsList = venue.InvigilatorsList;
                                        block.VenuesList.RemoveAt(block.VenuesList.IndexOf(venue));
                                    }
                                    countForVenue++;
                                }
                            }
                        }
                        blockList.Remove(block);
                    }
                }
                examTimetable[countForTimetable] = examTimetableInSameDayAndSession;
                countForTimetable++;
            }
            return examTimetable;
        }

        //assign examiner as chief invigilators
        public static List<Timetable> assignExaminersAsChiefInvigilators(double totalLoadOfDutyForEachChiefInvigilator, List<Timetable> examTimetable)
        {
            string checkType = "Examiner";
            List<string> paperCodeList = new List<string>();
            List<Staff> examinersList = new List<Staff>();
            List<Block> blockList = new List<Block>();
            List<Venue> venueList = new List<Venue>();
            double totalLoadOfDutyForEachChiefInvigilatorAfterRound = (int)Math.Round(totalLoadOfDutyForEachChiefInvigilator, MidpointRounding.AwayFromZero);

            /**count = 0 - assign examiner(main paper) to the block which have paper examined
             * count = 1 - assign examiner(main paper) to the nearby block
             * count = 2 - assign examiner(main/resit/repeat paper) to the block which have paper examined
             * count = 3 - assign examiner(main/resit/repeat paper) to the nearby block
             **/
            for (int count = 0; count < 4; count++)
            {
                int countForTimetable = 0;
                foreach (Timetable examTimetableInSameDayAndSession in examTimetable.ToList())
                {
                    bool isMainPaperOnly = true;
                    //get a list of paper code in same day and session (assume same paper only examined in same day)
                    if (count == 0 || count == 1)
                        isMainPaperOnly = true;
                    else
                        isMainPaperOnly = false;
                    paperCodeList.Clear();
                    MaintainCourseControl maintainCourseControl = new MaintainCourseControl();
                    paperCodeList = maintainCourseControl.searchCourseCodesExaminedList(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, isMainPaperOnly);
                    maintainCourseControl.shutDown();

                    foreach (string paperCode in paperCodeList)
                    {
                        //get a list of examiners with same paper code
                        MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
                        examinersList = maintainStaffControl.searchLecturer(paperCode, "examAsChief");
                        maintainStaffControl.shutDown();

                        //remove the unavailable examiner from list
                        List<Staff> tempExaminersList = new List<Staff>();
                        for (int i = 0; i < examinersList.Count; i++)
                        {
                            if (isAvailable(examinersList[i], examTimetableInSameDayAndSession, totalLoadOfDutyForEachChiefInvigilator, checkType, new Venue()))
                            {
                                tempExaminersList.Add(examinersList[i]);
                            }
                        }
                        examinersList = tempExaminersList;
                        examinersList.Shuffle();

                        //get a list of blocks with same paper code
                        bool isContainsVenuesWithSameCourseCode;
                        blockList.Clear();
                        for (int x = 0; x < examTimetableInSameDayAndSession.BlocksList.Count; x++)
                        {
                            Block block = examTimetableInSameDayAndSession.BlocksList[x];
                            isContainsVenuesWithSameCourseCode = false;
                            for (int y = 0; y < block.VenuesList.Count; y++)
                            {
                                foreach (Course paper in examTimetableInSameDayAndSession.BlocksList[x].VenuesList[y].CoursesList)
                                {
                                    if (paper.CourseCode.Equals(paperCode))
                                    {
                                        isContainsVenuesWithSameCourseCode = true;
                                        break;
                                    }
                                }
                                if (isContainsVenuesWithSameCourseCode)
                                {
                                    break;
                                }
                            }
                            if (isContainsVenuesWithSameCourseCode) //if venue in the block has the paper with same paper code
                            {
                                blockList.Add(block);
                            }
                        }

                        MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
                        string catOfInvi = "Chief";
                        /**
                         * assign one examiner to each block if the number of chief required to the block have not achieve and
                         * there are examiner available
                         **/
                        for (int x = 0; x < blockList.Count && examinersList.Count > 0 && (count == 0 || count == 2); x++)
                        {
                            Block block = blockList[x];
                            if (blockList[x].ChiefInvigilatorsList.Count < getNumberOfChiefInvigilatorsRequired(block))
                            {
                                blockList[x].ChiefInvigilatorsList.Add(examinersList[0]);
                                maintainInvigilationDutyControl.addInvigilationDuty(examinersList[0], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, "", blockList[x].BlockCode, catOfInvi, getLongestCourseDuration(block)));
                                if (totalLoadOfDutyForEachChiefInvigilatorAfterRound < totalLoadOfDutyForEachChiefInvigilator && examinersList[0].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachChiefInvigilatorAfterRound)
                                {
                                    examinersList[0].NoOfExtraSession++;
                                    maintainInvigilationDutyControl.updateNoOfExtraSession(examinersList[0].StaffID);
                                }
                                examinersList.RemoveAt(0);
                            }
                        }

                        //assign examiner to nearby block                    
                        for (int x = 0; x < blockList.Count && examinersList.Count > 0 && (count == 1 || count == 3); x++)
                        {
                            foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                            {
                                while (!blockList.Contains(block) && block.ChiefInvigilatorsList.Count < getNumberOfChiefInvigilatorsRequired(block)
                                            && examinersList.Count > 0)
                                {
                                    block.ChiefInvigilatorsList.Add(examinersList[0]);
                                    maintainInvigilationDutyControl.addInvigilationDuty(examinersList[0], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, "", block.BlockCode, catOfInvi, getLongestCourseDuration(block)));
                                    examinersList.RemoveAt(0);
                                }
                                blockList.Add(block);
                            }
                        }
                        maintainInvigilationDutyControl.shutDown();
                    }

                    //assign the blocks with chief invigilators back into the timetable
                    for (int x = 0; x < examTimetableInSameDayAndSession.BlocksList.Count; x++)
                    {
                        foreach (Block block in blockList.ToList())
                        {
                            if (examTimetableInSameDayAndSession.BlocksList[x].BlockCode.Equals(block.BlockCode))
                            {
                                examTimetableInSameDayAndSession.BlocksList[x].ChiefInvigilatorsList = block.ChiefInvigilatorsList;
                                blockList.Remove(block);
                            }
                        }
                    }
                    examTimetable[countForTimetable] = examTimetableInSameDayAndSession;
                    countForTimetable++;
                }
            }
            return examTimetable;
        }

        //assign chief invigilators
        public static List<Timetable> assignChiefInvigilators(List<Staff> chiefInvigilatorsList, double totalLoadOfDutyForEachChiefInvigilator, List<Timetable> examTimetable)
        {
            string checkType = "Chief";
            List<int> fafbChiefInvigilatorsIndexList = new List<int>();
            List<int> febeChiefInvigilatorsIndexList = new List<int>();
            double totalLoadOfDutyForEachChiefInvigilatorAfterRound = (int)Math.Round(totalLoadOfDutyForEachChiefInvigilator, MidpointRounding.AwayFromZero);
            MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();

            for (int x = 0; x < chiefInvigilatorsList.Count; x++)
            {
                //get a list of index number of FAFB Chief Invigilator
                if (chiefInvigilatorsList[x].Faculty.Contains("FAFB"))
                {
                    fafbChiefInvigilatorsIndexList.Add(x);
                }
                //get a list of index number of FEBE Chief Invigilator
                if (chiefInvigilatorsList[x].Faculty.Contains("FEBE"))
                {
                    febeChiefInvigilatorsIndexList.Add(x);
                }
            }

            int countForTimetable = 0;
            int countForBlock = 0;
            foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
            {
                countForBlock = 0;
                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                {
                    chiefInvigilatorsList = chiefInvigilatorsList.OrderBy(chiefInvigilators => chiefInvigilators.NoOfSatSession).ToList();
                    for (int x = 0; x < chiefInvigilatorsList.Count; x++)
                    {
                        //get a list of index number of FAFB Chief Invigilator
                        if (chiefInvigilatorsList[x].Faculty.Contains("FAFB"))
                        {
                            fafbChiefInvigilatorsIndexList.Add(x);
                        }
                        //get a list of index number of FEBE Chief Invigilator
                        if (chiefInvigilatorsList[x].Faculty.Contains("FEBE"))
                        {
                            febeChiefInvigilatorsIndexList.Add(x);
                        }
                    }
                }
                else
                {
                    fafbChiefInvigilatorsIndexList.Shuffle();
                    febeChiefInvigilatorsIndexList.Shuffle();
                }
                foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                {
                    if (examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count < getNumberOfChiefInvigilatorsRequired(block))
                    {
                        foreach (Venue venue in block.VenuesList)
                        {
                            foreach (Course paper in venue.CoursesList)
                            {
                                //assign FAFB 3 hours/3 hours 15 mins session (FAFB paper) to FAFB Chief Invigilator
                                if (paper.Duration >= 3 && examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count < getNumberOfChiefInvigilatorsRequired(block))
                                {
                                    foreach (int index in fafbChiefInvigilatorsIndexList)
                                    {
                                        if (isAvailable(chiefInvigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachChiefInvigilator, checkType, new Venue())
                                            && chiefInvigilatorsList[index].InvigilationDuty.Count + 1 <= (totalLoadOfDutyForEachChiefInvigilator / 3 * 2)
                                            && examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count < getNumberOfChiefInvigilatorsRequired(block))
                                        {
                                            examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Add(chiefInvigilatorsList[index]);
                                            chiefInvigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, "", block.BlockCode, checkType, getLongestCourseDuration(block)));
                                            maintainInvigilationDutyControl.addInvigilationDuty(chiefInvigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, "", block.BlockCode, checkType, getLongestCourseDuration(block)));
                                            if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                            {
                                                chiefInvigilatorsList[index].NoOfSatSession++;
                                            }
                                            if (totalLoadOfDutyForEachChiefInvigilatorAfterRound < totalLoadOfDutyForEachChiefInvigilator && chiefInvigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachChiefInvigilatorAfterRound)
                                            {
                                                chiefInvigilatorsList[index].NoOfExtraSession++;
                                                maintainInvigilationDutyControl.updateNoOfExtraSession(chiefInvigilatorsList[index].StaffID);
                                            }
                                        }
                                        if (examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count >= getNumberOfChiefInvigilatorsRequired(block))
                                        {
                                            break;
                                        }
                                    }
                                }

                                //assign double seating (FEBE paper) to FEBE Chief Invigilator
                                if (paper.IsDoubleSeating && examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count < getNumberOfChiefInvigilatorsRequired(block))
                                {
                                    foreach (int index in febeChiefInvigilatorsIndexList)
                                    {
                                        if (isAvailable(chiefInvigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachChiefInvigilator, checkType, new Venue())
                                            && examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count < getNumberOfChiefInvigilatorsRequired(block))
                                        {
                                            examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Add(chiefInvigilatorsList[index]);
                                            chiefInvigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, "", block.BlockCode, checkType, getLongestCourseDuration(block)));
                                            maintainInvigilationDutyControl.addInvigilationDuty(chiefInvigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, "", block.BlockCode, checkType, getLongestCourseDuration(block)));
                                            if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                            {
                                                chiefInvigilatorsList[index].NoOfSatSession++;
                                            }
                                            if (totalLoadOfDutyForEachChiefInvigilatorAfterRound < totalLoadOfDutyForEachChiefInvigilator && chiefInvigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachChiefInvigilatorAfterRound)
                                            {
                                                chiefInvigilatorsList[index].NoOfExtraSession++;
                                                maintainInvigilationDutyControl.updateNoOfExtraSession(chiefInvigilatorsList[index].StaffID);
                                            }
                                        }
                                        if (examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count >= getNumberOfChiefInvigilatorsRequired(block))
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count >= getNumberOfChiefInvigilatorsRequired(block))
                                {
                                    break;
                                }
                            }
                            if (examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count >= getNumberOfChiefInvigilatorsRequired(block))
                            {
                                break;
                            }
                        }
                    }
                    countForBlock++;
                }
                countForTimetable++;
            }

            //assign duty to chief invigilators
            countForBlock = 0;
            bool isLoaded = false;
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            List<string> allFacultyCodesList = maintainStaffControl.getFacultyCodesList();
            maintainStaffControl.shutDown();
            for (int count = 0; count < 3; count++)
            {
                if (count == 2 && !isLoaded)
                {
                    chiefInvigilatorsList = getChiefInvigilatorsList();
                    isLoaded = true;
                }
                countForTimetable = 0;
                foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
                {
                    countForBlock = 0;
                    foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                    {
                        if (examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count < getNumberOfChiefInvigilatorsRequired(block))
                        {
                            if (count < 2)
                            {
                                //get a list of the faculty codes of the courses(papers) in the block
                                MaintainCourseControl maintainCourseControl = new MaintainCourseControl();
                                List<string> facultyCodeList = new List<string>();
                                foreach (Venue venue in block.VenuesList)
                                {
                                    facultyCodeList.AddRange(maintainCourseControl.searchFacultyCodesList(venue.CoursesList));
                                }
                                maintainCourseControl.shutDown();

                                maintainStaffControl = new MaintainStaffControl();
                                chiefInvigilatorsList.Clear();
                                foreach (string faculty in facultyCodeList)
                                {
                                    if (count == 0) //get a list of chief invigilators which same faculty with the courses in the venue and have duty in the same day
                                    {
                                        chiefInvigilatorsList.AddRange(maintainStaffControl.searchLecturer(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, faculty, "Chief", false, totalLoadOfDutyForEachChiefInvigilator));
                                    }
                                    else if (count == 1) //get a list of chief invigilators which same faculty with the courses in the venue
                                    {
                                        chiefInvigilatorsList.AddRange(maintainStaffControl.searchLecturer(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, faculty, "Chief", true, totalLoadOfDutyForEachChiefInvigilator));
                                    }
                                }
                                maintainStaffControl.shutDown();
                            }
                            if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                chiefInvigilatorsList = chiefInvigilatorsList.OrderBy(chiefInvigilator => chiefInvigilator.NoOfSatSession).ToList();
                            else
                                chiefInvigilatorsList.Shuffle();
                            List<int> freeChiefInvigilatorIndexNumbersList = new List<int>();
                            if (count == 2)
                                freeChiefInvigilatorIndexNumbersList = getFreeInvigilatorIndexNumbersList(chiefInvigilatorsList, examTimetableInSameDayAndSession, allFacultyCodesList);

                            int index = 0;
                            foreach (Staff chiefInvigilator in chiefInvigilatorsList)
                            {
                                if (isAvailable(chiefInvigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachChiefInvigilator, checkType, new Venue())
                                    && !freeChiefInvigilatorIndexNumbersList.Contains(index)
                                    && examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count < getNumberOfChiefInvigilatorsRequired(block))
                                {
                                    examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Add(chiefInvigilatorsList[index]);
                                    chiefInvigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, "", block.BlockCode, checkType, getLongestCourseDuration(block)));
                                    maintainInvigilationDutyControl.addInvigilationDuty(chiefInvigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, "", block.BlockCode, checkType, getLongestCourseDuration(block)));
                                    if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                    {
                                        chiefInvigilatorsList[index].NoOfSatSession++;
                                    }
                                    if (totalLoadOfDutyForEachChiefInvigilatorAfterRound < totalLoadOfDutyForEachChiefInvigilator && chiefInvigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachChiefInvigilatorAfterRound)
                                    {
                                        chiefInvigilatorsList[index].NoOfExtraSession++;
                                        maintainInvigilationDutyControl.updateNoOfExtraSession(chiefInvigilatorsList[index].StaffID);
                                    }
                                }
                                index++;
                                if (examTimetable[countForTimetable].BlocksList[countForBlock].ChiefInvigilatorsList.Count >= getNumberOfChiefInvigilatorsRequired(block))
                                {
                                    break;
                                }
                            }
                        }
                        countForBlock++;
                    }
                    countForTimetable++;
                }
            }
            maintainInvigilationDutyControl.shutDown();
            return examTimetable;
        }

        //assign invigilators to venue with FAFB 3 hours paper, double seating paper and cnbl paper
        public static List<Timetable> assignInvigilatorsToPrioritizedVenue(List<Staff> invigilatorsList, double totalLoadOfDutyForEachInvigilator, List<Timetable> examTimetable, bool isAssignmentOfExperiencedInviOnly)
        {
            string checkType = "Invigilator";
            string catOfInvi = "Invigilator";
            List<int> fafbInvigilatorsIndexList = new List<int>();
            List<int> febeQsAndBdInvigilatorsIndexList = new List<int>();
            List<int> cnblInvigilatorsIndexList = new List<int>();
            double totalLoadOfDutyForEachInvigilatorAfterRound = (int)Math.Round(totalLoadOfDutyForEachInvigilator, MidpointRounding.AwayFromZero);

            int countForTimetable = 0;
            int countForBlock = 0;
            MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();

            for (int x = 0; x < invigilatorsList.Count; x++)
            {
                //get a list of index number of FAFB Invigilator
                if (invigilatorsList[x].Faculty.Contains("B"))
                {
                    fafbInvigilatorsIndexList.Add(x);
                }
                //get a list of index number of FEBE QS and BD Invigilator
                if (invigilatorsList[x].Faculty.Contains("T") && (invigilatorsList[x].Department.Contains("QS") || invigilatorsList[x].Department.Contains("BD")))
                {
                    febeQsAndBdInvigilatorsIndexList.Add(x);
                }
                //get a list of index number of CNBL Invigilator
                if (invigilatorsList[x].Faculty.Contains("C"))
                {
                    cnblInvigilatorsIndexList.Add(x);
                }
            }

            //assign FAFB papers to FAFB invigilators first, FEBE DS papers to FEBE QS and BD invigilators
            //CNBL papers to CNBL invigilators
            foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
            {
                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                {
                    invigilatorsList = invigilatorsList.OrderBy(invigilators => invigilators.NoOfSatSession).ToList();
                    for (int x = 0; x < invigilatorsList.Count; x++)
                    {
                        //get a list of index number of FAFB Invigilator
                        if (invigilatorsList[x].Faculty.Contains("B"))
                        {
                            fafbInvigilatorsIndexList.Add(x);
                        }
                        //get a list of index number of FEBE QS and BD Invigilator
                        if (invigilatorsList[x].Faculty.Contains("T") && (invigilatorsList[x].Department.Contains("QS") || invigilatorsList[x].Department.Contains("BD")))
                        {
                            febeQsAndBdInvigilatorsIndexList.Add(x);
                        }
                        //get a list of index number of CNBL Invigilator
                        if (invigilatorsList[x].Faculty.Contains("C"))
                        {
                            cnblInvigilatorsIndexList.Add(x);
                        }
                    }
                }
                else
                {
                    fafbInvigilatorsIndexList.Shuffle();
                    febeQsAndBdInvigilatorsIndexList.Shuffle();
                    cnblInvigilatorsIndexList.Shuffle();
                }
                countForBlock = 0;
                foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                {
                    int countForVenue = 0;
                    foreach (Venue venue in block.VenuesList)
                    {
                        if ((isAssignmentOfExperiencedInviOnly
                            && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Where(staff => staff.IsInviAbove2Years == true).ToList().Count == 0
                            && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(venue))
                            || (!isAssignmentOfExperiencedInviOnly
                            && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(venue)))
                        {
                            foreach (Course paper in venue.CoursesList)
                            {
                                if (examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(venue))
                                {
                                    //assign FAFB 3 hours/3 hours 15 mins session (FAFB paper) to FAFB Invigilator up to 2/3 work load
                                    if (paper.Duration >= 3)
                                    {
                                        foreach (int index in fafbInvigilatorsIndexList)
                                        {
                                            int numberOf3HoursDuty = invigilatorsList[index].InvigilationDuty.Where(duty => duty.Duration >= 3 && !duty.CategoryOfInvigilator.Equals("Relief")).ToList().Count;
                                            
                                            if (isAvailable(invigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, checkType, examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue]) &&
                                                numberOf3HoursDuty + 1 <= (totalLoadOfDutyForEachInvigilator / 3 * 2))
                                            {
                                                examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Add(invigilatorsList[index]);
                                                invigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                                maintainInvigilationDutyControl.addInvigilationDuty(invigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                                {
                                                    invigilatorsList[index].NoOfSatSession++;
                                                }
                                                if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && invigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                                {
                                                    invigilatorsList[index].NoOfExtraSession++;
                                                    maintainInvigilationDutyControl.updateNoOfExtraSession(invigilatorsList[index].StaffID);
                                                }
                                                if (isAssignmentOfExperiencedInviOnly && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Where(staff => staff.IsInviAbove2Years == true).ToList().Count > 0)
                                                {
                                                    break;
                                                }
                                            }
                                            if (examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count == getNumberOfInvigilatorsRequired(venue))
                                            {
                                                break;
                                            }
                                        }
                                        if (isAssignmentOfExperiencedInviOnly && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Where(staff => staff.IsInviAbove2Years == true).ToList().Count > 0)
                                        {
                                            break;
                                        }
                                    }

                                    //assign FEBE DS papers to FEBE QS and BD invigilators
                                    if (paper.IsDoubleSeating)
                                    {
                                        foreach (int index in febeQsAndBdInvigilatorsIndexList)
                                        {
                                            if (isAvailable(invigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, checkType, examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue]) && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(venue))
                                            {
                                                examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Add(invigilatorsList[index]);
                                                invigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                                maintainInvigilationDutyControl.addInvigilationDuty(invigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                                {
                                                    invigilatorsList[index].NoOfSatSession++;
                                                }
                                                if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && invigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                                {
                                                    invigilatorsList[index].NoOfExtraSession++;
                                                    maintainInvigilationDutyControl.updateNoOfExtraSession(invigilatorsList[index].StaffID);
                                                }
                                                if (isAssignmentOfExperiencedInviOnly && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Where(staff => staff.IsInviAbove2Years == true).ToList().Count > 0)
                                                {
                                                    break;
                                                }
                                            }
                                            if (examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count == getNumberOfInvigilatorsRequired(venue))
                                            {
                                                break;
                                            }
                                        }
                                        if (isAssignmentOfExperiencedInviOnly && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Where(staff => staff.IsInviAbove2Years == true).ToList().Count > 0)
                                        {
                                            break;
                                        }
                                    }

                                    //assign CNBL papers to CNBL invigilators
                                    if (paper.IsCnblPaper)
                                    {
                                        foreach (int index in cnblInvigilatorsIndexList)
                                        {
                                            if (isAvailable(invigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, checkType, examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue]) && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(venue))
                                            {
                                                examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Add(invigilatorsList[index]);
                                                invigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                                maintainInvigilationDutyControl.addInvigilationDuty(invigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                                {
                                                    invigilatorsList[index].NoOfSatSession++;
                                                }
                                                if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && invigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                                {
                                                    invigilatorsList[index].NoOfExtraSession++;
                                                    maintainInvigilationDutyControl.updateNoOfExtraSession(invigilatorsList[index].StaffID);
                                                }
                                                if (isAssignmentOfExperiencedInviOnly && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Where(staff => staff.IsInviAbove2Years == true).ToList().Count > 0)
                                                {
                                                    break;
                                                }
                                            }
                                            if (examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count == getNumberOfInvigilatorsRequired(venue))
                                            {
                                                break;
                                            }
                                        }
                                        if (isAssignmentOfExperiencedInviOnly && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Where(staff => staff.IsInviAbove2Years == true).ToList().Count > 0)
                                        {
                                            break;
                                        }
                                    }
                                    if (examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count == getNumberOfInvigilatorsRequired(venue))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        countForVenue++;
                    }
                    countForBlock++;
                }
                countForTimetable++;
            }
            maintainInvigilationDutyControl.shutDown();
            return examTimetable;
        }

        //assign Muslim relief invigilators for evening session
        public static List<Timetable> assignMuslimReliefInvigilatorsToEVSession(List<Staff> invigilatorsList, List<Staff> muslimInvigilatorsList, double totalLoadOfDutyForEachInvigilator, List<Timetable> examTimetable)
        {
            string catOfInvi = "Relief";
            string venueID = "";
            string location = "";
            int duration = 0;
            int countForTimetable = 0;
            int numberOfQuarantineInviRequired = 1;
            int numberOfMuslimReliefInviRequired = 1;
            int numOfMuslimReliefInvi = 0;
            double totalLoadOfDutyForEachInvigilatorAfterRound = (int)Math.Round(totalLoadOfDutyForEachInvigilator, MidpointRounding.AwayFromZero);

            MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
            foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
            {
                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                {
                    invigilatorsList = invigilatorsList.OrderBy(invigilators => invigilators.NoOfSatSession).ToList();
                    muslimInvigilatorsList = muslimInvigilatorsList.OrderBy(muslimInvigilators => muslimInvigilators.NoOfSatSession).ToList();
                }
                else
                {
                    invigilatorsList.Shuffle();
                    muslimInvigilatorsList.Shuffle();
                }
                if (examTimetableInSameDayAndSession.Session.Equals("EV"))
                {
                    numOfMuslimReliefInvi = 0;
                    for (int index = 0; index < invigilatorsList.Count && examTimetable[countForTimetable].ReliefInvigilatorsList.Count == 0; index++)
                    {
                        if (isAvailable(invigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, catOfInvi, new Venue()))
                        {
                            invigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                            examTimetable[countForTimetable].ReliefInvigilatorsList.Add(invigilatorsList[index]);
                            maintainInvigilationDutyControl.addInvigilationDuty(invigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                            if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                invigilatorsList[index].NoOfSatSession++;
                            }
                            if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && invigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                            {
                                invigilatorsList[index].NoOfExtraSession++;
                                maintainInvigilationDutyControl.updateNoOfExtraSession(invigilatorsList[index].StaffID);
                            }
                            if (examTimetable[countForTimetable].ReliefInvigilatorsList.Count == 1)
                                maintainInvigilationDutyControl.updateNoAsQuarantineInvi(invigilatorsList[index].StaffID);

                            else
                                maintainInvigilationDutyControl.updateNoAsReliefInvi(invigilatorsList[index].StaffID);
                        }
                    }

                    for (int index = 0; numOfMuslimReliefInvi == 0 && index < muslimInvigilatorsList.Count && examTimetable[countForTimetable].ReliefInvigilatorsList.Count < examTimetableInSameDayAndSession.BlocksList.Count + numberOfQuarantineInviRequired + numberOfMuslimReliefInviRequired; index++)
                    {
                        if (isAvailable(muslimInvigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, catOfInvi, new Venue()))
                        {
                            muslimInvigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                            examTimetable[countForTimetable].ReliefInvigilatorsList.Add(muslimInvigilatorsList[index]);
                            maintainInvigilationDutyControl.addInvigilationDuty(muslimInvigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                            if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                invigilatorsList[index].NoOfSatSession++;
                            }
                            if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && invigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                            {
                                invigilatorsList[index].NoOfExtraSession++;
                                maintainInvigilationDutyControl.updateNoOfExtraSession(invigilatorsList[index].StaffID);
                            }
                            if (examTimetable[countForTimetable].ReliefInvigilatorsList.Count == 1)
                                maintainInvigilationDutyControl.updateNoAsQuarantineInvi(muslimInvigilatorsList[index].StaffID);

                            else
                                maintainInvigilationDutyControl.updateNoAsReliefInvi(muslimInvigilatorsList[index].StaffID);
                            numOfMuslimReliefInvi++;
                        }
                    }
                }
                countForTimetable++;
            }
            maintainInvigilationDutyControl.shutDown();
            return examTimetable;
        }

        //assign experienced invigilators as relief invigilators
        public static List<Timetable> assignExperiencedReliefInvigilators(List<Staff> experiencedInvigilatorsList, double totalLoadOfDutyForEachInvigilator, List<Timetable> examTimetable, bool isConstraintsCheckRequired)
        {
            string checkType = "Relief";
            string catOfInvi = checkType;
            string venueID = "";
            string location = "";
            int duration = 0;
            int countForTimetable = 0;
            int numOfMale = 0;
            int numOfFemale = 0;
            double totalLoadOfDutyForEachInvigilatorAfterRound = (int)Math.Round(totalLoadOfDutyForEachInvigilator, MidpointRounding.AwayFromZero);
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            List<string> allFacultyCodesList = maintainStaffControl.getFacultyCodesList();
            maintainStaffControl.shutDown();
            MaintainConstraintControl maintainConstraintControl = new MaintainConstraintControl();
            MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
            foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
            {
                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                    experiencedInvigilatorsList = experiencedInvigilatorsList.OrderBy(experiencedInvigilators => experiencedInvigilators.NoOfSatSession).ToList();
                else
                    experiencedInvigilatorsList.Shuffle();
                List<int> freeInvigilatorIndexNumbersList = getFreeInvigilatorIndexNumbersList(experiencedInvigilatorsList, examTimetableInSameDayAndSession, allFacultyCodesList);
                double totalReliefInvigilatorsRequiredPerSession = (double)calculateTotalReliefInvigilatorsRequiredPerSession(examTimetableInSameDayAndSession);
                double percentageOfReliefInvigilator = (double)examTimetable[countForTimetable].ReliefInvigilatorsList.Count / totalReliefInvigilatorsRequiredPerSession;
                for (int index = 0; index < experiencedInvigilatorsList.Count && !maintainConstraintControl.constraintValidation(new string[] { "PercentageOfExperienceReliefInvigilator" }, new double[] { percentageOfReliefInvigilator }); index++)
                {
                    if (isConstraintsCheckRequired)
                    {
                        numOfMale = 0;
                        numOfFemale = 0;
                        foreach (Staff reliefInvigilator in examTimetable[countForTimetable].ReliefInvigilatorsList)
                        {
                            if (reliefInvigilator.Gender.Equals("M"))
                            {
                                numOfMale++;
                            }
                            else
                            {
                                numOfFemale++;
                            }
                        }
                    }

                    if (isAvailable(experiencedInvigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, checkType, new Venue()) &&
                        !freeInvigilatorIndexNumbersList.Contains(index) &&
                        !maintainConstraintControl.constraintValidation(new string[] { "PercentageOfExperienceReliefInvigilator" }, new double[] { percentageOfReliefInvigilator }))
                    {
                        if (isConstraintsCheckRequired)
                        {
                            if (maintainConstraintControl.constraintValidation(new string[] { "numOfMale" }, new double[] { Convert.ToDouble(numOfMale) }) || maintainConstraintControl.constraintValidation(new string[] { "numOfFemale" }, new double[] { Convert.ToDouble(numOfFemale) }))
                            {
                                if ((maintainConstraintControl.constraintValidation(new string[] { "numOfMale" }, new double[] { Convert.ToDouble(numOfMale) }) && experiencedInvigilatorsList[index].Gender.Equals("M")) || (maintainConstraintControl.constraintValidation(new string[] { "numOfFemale" }, new double[] { Convert.ToDouble(numOfFemale) }) && experiencedInvigilatorsList[index].Gender.Equals("F")))
                                {

                                    experiencedInvigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                                    examTimetable[countForTimetable].ReliefInvigilatorsList.Add(experiencedInvigilatorsList[index]);
                                    maintainInvigilationDutyControl.addInvigilationDuty(experiencedInvigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                                    if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                    {
                                        experiencedInvigilatorsList[index].NoOfSatSession++;
                                    }
                                    if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && experiencedInvigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                    {
                                        experiencedInvigilatorsList[index].NoOfExtraSession++;
                                        maintainInvigilationDutyControl.updateNoOfExtraSession(experiencedInvigilatorsList[index].StaffID);
                                    }
                                    if (examTimetable[countForTimetable].ReliefInvigilatorsList.Count == 1)
                                        maintainInvigilationDutyControl.updateNoAsQuarantineInvi(experiencedInvigilatorsList[index].StaffID);

                                    else
                                        maintainInvigilationDutyControl.updateNoAsReliefInvi(experiencedInvigilatorsList[index].StaffID);
                                    percentageOfReliefInvigilator = (double)examTimetable[countForTimetable].ReliefInvigilatorsList.Count / totalReliefInvigilatorsRequiredPerSession;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            experiencedInvigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                            examTimetable[countForTimetable].ReliefInvigilatorsList.Add(experiencedInvigilatorsList[index]);
                            maintainInvigilationDutyControl.addInvigilationDuty(experiencedInvigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                            if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                experiencedInvigilatorsList[index].NoOfSatSession++;
                            }
                            if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && experiencedInvigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                            {
                                experiencedInvigilatorsList[index].NoOfExtraSession++;
                                maintainInvigilationDutyControl.updateNoOfExtraSession(experiencedInvigilatorsList[index].StaffID);
                            }
                            if (examTimetable[countForTimetable].ReliefInvigilatorsList.Count == 1)
                                maintainInvigilationDutyControl.updateNoAsQuarantineInvi(experiencedInvigilatorsList[index].StaffID);

                            else
                                maintainInvigilationDutyControl.updateNoAsReliefInvi(experiencedInvigilatorsList[index].StaffID);
                            percentageOfReliefInvigilator = (double)examTimetable[countForTimetable].ReliefInvigilatorsList.Count / totalReliefInvigilatorsRequiredPerSession;
                        }
                    }
                }
                countForTimetable++;
            }
            maintainInvigilationDutyControl.shutDown();
            return examTimetable;
        }

        //assign invigilators-in-charge
        public static List<Timetable> assignInvigilatorsInCharge(double totalLoadOfDutyForEachInvigilator, List<Timetable> examTimetable)
        {
            string catOfInvi = "In-charge";
            int countForTimetable = 0;
            int countForBlock = 0;
            double totalLoadOfDutyForEachInvigilatorAfterRound = (int)Math.Round(totalLoadOfDutyForEachInvigilator, MidpointRounding.AwayFromZero);
            bool isLoaded = false;
            MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
            List<Staff> experiencedInvigilatorsList = new List<Staff>();
            for (int count = 0; count < 3; count++)
            {
                countForTimetable = 0;
                if (count == 2 && !isLoaded)//get a list of experienced invigilators
                {
                    experiencedInvigilatorsList = getExperiencedInvigilatorsList();
                    isLoaded = true;
                }
                foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
                {
                    countForBlock = 0;
                    foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                    {
                        int countForVenue = 0;
                        foreach (Venue venue in block.VenuesList)
                        {
                            bool isInviInChargeAssigned = false;
                            foreach (Staff invigilator in venue.InvigilatorsList)
                            {
                                if (invigilator.IsInviAbove2Years && invigilator.InvigilationDuty.Where(duty => duty.Date.Equals(examTimetableInSameDayAndSession.Date) && duty.Session.Equals(examTimetableInSameDayAndSession.Session) && duty.CategoryOfInvigilator.Equals("In-charge")).ToList().Count > 0)
                                {
                                    isInviInChargeAssigned = true;
                                    break;
                                }
                            }
                            if (!isInviInChargeAssigned)
                            {
                                foreach (Staff invigilator in venue.InvigilatorsList)
                                {
                                    if (invigilator.IsInviAbove2Years)
                                    {
                                        maintainInvigilationDutyControl.changeCatOfInvi(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, invigilator.StaffID);
                                        isInviInChargeAssigned = true;
                                        break;
                                    }
                                }
                            }
                            if (!isInviInChargeAssigned)
                            {
                                if (count < 2)
                                {
                                    //get a list of the faculty codes of the courses(papers) in the venue
                                    MaintainCourseControl maintainCourseControl = new MaintainCourseControl();
                                    List<string> facultyCodeList = maintainCourseControl.searchFacultyCodesList(venue.CoursesList);
                                    maintainCourseControl.shutDown();

                                    MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
                                    experiencedInvigilatorsList.Clear();
                                    foreach (string faculty in facultyCodeList)
                                    {
                                        if (count == 0) //get a list of experienced invigilators which same faculty with the courses in the venue and have duty in the same day
                                        {
                                            experiencedInvigilatorsList.AddRange(maintainStaffControl.searchLecturer(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, faculty, "InviAbove2Years", false, totalLoadOfDutyForEachInvigilator));
                                        }
                                        else if (count == 1) //get a list of experienced invigilators which same faculty with the courses in the venue
                                        {
                                            experiencedInvigilatorsList.AddRange(maintainStaffControl.searchLecturer(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, faculty, "InviAbove2Years", true, totalLoadOfDutyForEachInvigilator));
                                        }
                                    }
                                    maintainStaffControl.shutDown();
                                }
                                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                    experiencedInvigilatorsList = experiencedInvigilatorsList.OrderBy(experiencedInvigilators => experiencedInvigilators.NoOfSatSession).ToList();
                                else
                                    experiencedInvigilatorsList.Shuffle();
                                for (int index = 0; index < experiencedInvigilatorsList.Count; index++)
                                {
                                    MaintainVenueControl maintainVenueControl = new MaintainVenueControl();
                                    if (isAvailable(experiencedInvigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, catOfInvi, examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue])
                                        && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(venue))
                                    {
                                        examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Add(experiencedInvigilatorsList[index]);
                                        experiencedInvigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                        maintainInvigilationDutyControl.addInvigilationDuty(experiencedInvigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                        if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            experiencedInvigilatorsList[index].NoOfSatSession++;
                                        }
                                        if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && experiencedInvigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                        {
                                            experiencedInvigilatorsList[index].NoOfExtraSession++;
                                            maintainInvigilationDutyControl.updateNoOfExtraSession(experiencedInvigilatorsList[index].StaffID);
                                        }
                                        isInviInChargeAssigned = true;
                                        break;
                                    }
                                    maintainVenueControl.shutDown();
                                }
                            }
                            countForVenue++;
                        }
                        countForBlock++;
                    }
                    countForTimetable++;
                }
            }
            maintainInvigilationDutyControl.shutDown();
            return examTimetable;
        }

        //assign experienced invigilators to DU, KS1, KS2
        public static List<Timetable> assignExperiencedInvigilators(List<Staff> experiencedInvigilatorsList, double totalLoadOfDutyForEachInvigilator, List<Timetable> examTimetable)
        {
            string catOfInvi = "Invigilator";
            int countForTimetable = 0;
            int countForBlock = 0;
            double totalLoadOfDutyForEachInvigilatorAfterRound = (int)Math.Round(totalLoadOfDutyForEachInvigilator, MidpointRounding.AwayFromZero);
            MaintainConstraintControl maintainConstraintControl = new MaintainConstraintControl();
            MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            List<string> allFacultyCodesList = maintainStaffControl.getFacultyCodesList();
            maintainStaffControl.shutDown();
            foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
            {
                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                    experiencedInvigilatorsList = experiencedInvigilatorsList.OrderBy(experiencedInvigilators => experiencedInvigilators.NoOfSatSession).ToList();
                else
                    experiencedInvigilatorsList.Shuffle();
                List<int> freeInvigilatorIndexNumbersList = getFreeInvigilatorIndexNumbersList(experiencedInvigilatorsList, examTimetableInSameDayAndSession, allFacultyCodesList);
                countForBlock = 0;
                foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                {
                    int countForVenue = 0;
                    if (block.BlockCode.Contains("DU") || block.BlockCode.Contains("KS"))
                    {
                        foreach (Venue venue in block.VenuesList)
                        {
                            if (venue.VenueID.Contains("DU") || venue.VenueID.Contains("KS1") || venue.VenueID.Contains("KS2"))
                            {
                                int numberOfExperiencedInvigilators = 0;
                                foreach (Staff invigilator in venue.InvigilatorsList)
                                {
                                    if (invigilator.IsInviAbove2Years)
                                    {
                                        numberOfExperiencedInvigilators++;
                                    }
                                }
                                double numberOfInvigilatorsRequired = (double)getNumberOfInvigilatorsRequired(venue);
                                double percentageOfInvigilator = (double)numberOfExperiencedInvigilators / numberOfInvigilatorsRequired;
                                for (int index = 0; index < experiencedInvigilatorsList.Count && !maintainConstraintControl.constraintValidation(new string[] { "PercentageOfExperienceInvigilator" }, new double[] { percentageOfInvigilator }); index++)
                                {                                    
                                    MaintainVenueControl maintainVenueControl = new MaintainVenueControl();
                                    if (isAvailable(experiencedInvigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, catOfInvi, examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue])
                                        && !freeInvigilatorIndexNumbersList.Contains(index)
                                        && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(venue))
                                    {
                                        examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Add(experiencedInvigilatorsList[index]);
                                        experiencedInvigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                        maintainInvigilationDutyControl.addInvigilationDuty(experiencedInvigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, catOfInvi, getLongestCourseDuration(venue)));
                                        numberOfExperiencedInvigilators++;
                                        if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            experiencedInvigilatorsList[index].NoOfSatSession++;
                                        }
                                        if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && experiencedInvigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                        {
                                            experiencedInvigilatorsList[index].NoOfExtraSession++;
                                            maintainInvigilationDutyControl.updateNoOfExtraSession(experiencedInvigilatorsList[index].StaffID);
                                        }
                                        numberOfExperiencedInvigilators++;
                                        percentageOfInvigilator = (double)numberOfExperiencedInvigilators / numberOfInvigilatorsRequired;
                                    }
                                    maintainVenueControl.shutDown();
                                }
                            }
                            countForVenue++;
                        }
                    }
                    countForBlock++;
                }
                countForTimetable++;
            }
            maintainInvigilationDutyControl.shutDown();
            return examTimetable;
        }

        //assign relief invigilators
        public static List<Timetable> assignReliefInvigilators(List<Staff> invigilatorsList, double totalLoadOfDutyForEachInvigilator, List<Timetable> examTimetable, bool isConstraintsCheckRequired)
        {
            string catOfInvi = "Relief";
            string venueID = "";
            string location = "";
            int duration = 0;
            int countForTimetable = 0;
            int numOfMale = 0;
            int numOfFemale = 0;
            double totalLoadOfDutyForEachInvigilatorAfterRound = (int)Math.Round(totalLoadOfDutyForEachInvigilator, MidpointRounding.AwayFromZero);
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            List<string> allFacultyCodesList = maintainStaffControl.getFacultyCodesList();
            maintainStaffControl.shutDown();
            MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
            MaintainConstraintControl maintainConstraintControl = new MaintainConstraintControl();
            foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
            {
                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                    invigilatorsList = invigilatorsList.OrderBy(invigilators => invigilators.NoOfSatSession).ToList();
                else
                    invigilatorsList.Shuffle();
                List<int> freeInvigilatorIndexNumbersList = getFreeInvigilatorIndexNumbersList(invigilatorsList, examTimetableInSameDayAndSession, allFacultyCodesList);
                for (int index = 0; index < invigilatorsList.Count && examTimetable[countForTimetable].ReliefInvigilatorsList.Count < calculateTotalReliefInvigilatorsRequiredPerSession(examTimetableInSameDayAndSession); index++)
                {
                    if (isConstraintsCheckRequired)
                    {
                        numOfMale = 0;
                        numOfFemale = 0;
                        foreach (Staff reliefInvigilator in examTimetable[countForTimetable].ReliefInvigilatorsList)
                        {
                            if (reliefInvigilator.Gender.Equals("M"))
                            {
                                numOfMale++;
                            }
                            else
                            {
                                numOfFemale++;
                            }
                        }
                    }

                    if (isAvailable(invigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, catOfInvi, new Venue()) &&
                        !freeInvigilatorIndexNumbersList.Contains(index))
                    {
                        if (isConstraintsCheckRequired)
                        {
                            if (maintainConstraintControl.constraintValidation(new string[] { "numOfMale" }, new double[] { Convert.ToDouble(numOfMale) }) || maintainConstraintControl.constraintValidation(new string[] { "numOfFemale" }, new double[] { Convert.ToDouble(numOfFemale) }))
                            {
                                if ((maintainConstraintControl.constraintValidation(new string[] { "numOfMale" }, new double[] { Convert.ToDouble(numOfMale) }) && invigilatorsList[index].Gender.Equals("M")) || (maintainConstraintControl.constraintValidation(new string[] { "numOfFemale" }, new double[] { Convert.ToDouble(numOfFemale) }) && invigilatorsList[index].Gender.Equals("F")))
                                {
                                    invigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                                    examTimetable[countForTimetable].ReliefInvigilatorsList.Add(invigilatorsList[index]);
                                    maintainInvigilationDutyControl.addInvigilationDuty(invigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                                    if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                    {
                                        invigilatorsList[index].NoOfSatSession++;
                                    }
                                    if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && invigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                    {
                                        invigilatorsList[index].NoOfExtraSession++;
                                        maintainInvigilationDutyControl.updateNoOfExtraSession(invigilatorsList[index].StaffID);
                                    }
                                    if (examTimetable[countForTimetable].ReliefInvigilatorsList.Count == 1)
                                        maintainInvigilationDutyControl.updateNoAsQuarantineInvi(invigilatorsList[index].StaffID);

                                    else
                                        maintainInvigilationDutyControl.updateNoAsReliefInvi(invigilatorsList[index].StaffID);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            invigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                            examTimetable[countForTimetable].ReliefInvigilatorsList.Add(invigilatorsList[index]);
                            maintainInvigilationDutyControl.addInvigilationDuty(invigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venueID, location, catOfInvi, duration));
                            if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                invigilatorsList[index].NoOfSatSession++;
                            }
                            if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && invigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                            {
                                invigilatorsList[index].NoOfExtraSession++;
                                maintainInvigilationDutyControl.updateNoOfExtraSession(invigilatorsList[index].StaffID);
                            }
                            if (examTimetable[countForTimetable].ReliefInvigilatorsList.Count == 1)
                                maintainInvigilationDutyControl.updateNoAsQuarantineInvi(invigilatorsList[index].StaffID);

                            else
                                maintainInvigilationDutyControl.updateNoAsReliefInvi(invigilatorsList[index].StaffID);
                        }
                    }
                }
                countForTimetable++;
            }
            maintainInvigilationDutyControl.shutDown();
            return examTimetable;
        }

        //assign invigilators
        public static List<Timetable> assignInvigilators(List<Staff> invigilatorsList, double totalLoadOfDutyForEachInvigilator, List<Timetable> examTimetable)
        {
            string checkType = "Invigilator";

            int countForTimetable = 0;
            int countForBlock = 0;
            MaintainStaffControl maintainStaffControl = new MaintainStaffControl();
            List<string> allFacultyCodesList = maintainStaffControl.getFacultyCodesList();
            maintainStaffControl.shutDown();
            double totalLoadOfDutyForEachInvigilatorAfterRound = (int)Math.Round(totalLoadOfDutyForEachInvigilator, MidpointRounding.AwayFromZero);
            bool isLoaded = false;
            MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
            for (int count = 0; count < 3; count++)
            {
                countForTimetable = 0;
                //get a list of invigilators
                if (count == 2 && !isLoaded)
                {
                    invigilatorsList = getInvigilatorsList();
                    isLoaded = true;
                }

                foreach (Timetable examTimetableInSameDayAndSession in examTimetable)
                {
                    countForBlock = 0;
                    foreach (Block block in examTimetableInSameDayAndSession.BlocksList)
                    {
                        int countForVenue = 0;
                        foreach (Venue venue in block.VenuesList)
                        {
                            if (examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(venue))
                            {
                                if (count < 2)
                                {
                                    //get a list of the faculty codes of the courses(papers) in the venue
                                    MaintainCourseControl maintainCourseControl = new MaintainCourseControl();
                                    List<string> facultyCodeList = maintainCourseControl.searchFacultyCodesList(venue.CoursesList);
                                    maintainCourseControl.shutDown();

                                    maintainStaffControl = new MaintainStaffControl();
                                    invigilatorsList.Clear();
                                    foreach (string faculty in facultyCodeList)
                                    {
                                        if (count == 0) //get a list of invigilators which same faculty with the courses in the venue and have duty in the same day
                                        {
                                            invigilatorsList.AddRange(maintainStaffControl.searchLecturer(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, faculty, "Invigilators", false, totalLoadOfDutyForEachInvigilator));
                                        }
                                        else if (count == 1) //get a list of invigilators which same faculty with the courses in the venue
                                        {
                                            invigilatorsList.AddRange(maintainStaffControl.searchLecturer(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, faculty, "Invigilators", true, totalLoadOfDutyForEachInvigilator));
                                        }
                                    }
                                    maintainStaffControl.shutDown();
                                }

                                if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                    invigilatorsList = invigilatorsList.OrderBy(experiencedInvigilators => experiencedInvigilators.NoOfSatSession).ToList();
                                else
                                    invigilatorsList.Shuffle();
                                List<int> freeInvigilatorIndexNumbersList = new List<int>();
                                if (count == 2)
                                {
                                    freeInvigilatorIndexNumbersList = getFreeInvigilatorIndexNumbersList(invigilatorsList, examTimetableInSameDayAndSession, allFacultyCodesList);
                                }

                                for (int index = 0; index < invigilatorsList.Count && examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Count < getNumberOfInvigilatorsRequired(venue); index++)
                                {
                                    if (isAvailable(invigilatorsList[index], examTimetableInSameDayAndSession, totalLoadOfDutyForEachInvigilator, checkType, examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue])
                                        && !freeInvigilatorIndexNumbersList.Contains(index))
                                    {
                                        examTimetable[countForTimetable].BlocksList[countForBlock].VenuesList[countForVenue].InvigilatorsList.Add(invigilatorsList[index]);
                                        invigilatorsList[index].InvigilationDuty.Add(new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, checkType, getLongestCourseDuration(venue)));
                                        maintainInvigilationDutyControl.addInvigilationDuty(invigilatorsList[index], new InvigilationDuty(examTimetableInSameDayAndSession.Date, examTimetableInSameDayAndSession.Session, venue.VenueID, block.BlockCode, checkType, getLongestCourseDuration(venue)));
                                        if (examTimetableInSameDayAndSession.Date.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            invigilatorsList[index].NoOfSatSession++;
                                        }
                                        if (totalLoadOfDutyForEachInvigilatorAfterRound < totalLoadOfDutyForEachInvigilator && invigilatorsList[index].InvigilationDuty.Count + 1 > totalLoadOfDutyForEachInvigilatorAfterRound)
                                        {
                                            invigilatorsList[index].NoOfExtraSession++;
                                            maintainInvigilationDutyControl.updateNoOfExtraSession(invigilatorsList[index].StaffID);
                                        }
                                    }
                                }
                            }
                            countForVenue++;
                        }
                        countForBlock++;
                    }
                    countForTimetable++;
                }
            }
            maintainInvigilationDutyControl.shutDown();
            return examTimetable;
        }

        //assign relief invi to east campus
        public static List<Timetable> changeLocationOfRelief(List<Timetable> examTimetable)
        {
            string location = "East Campus";
            MaintainInvigilationDutyControl maintainInvigilationDutyControl = new MaintainInvigilationDutyControl();
            for (int countForTimetable = 0; countForTimetable < examTimetable.Count; countForTimetable++)
            {
                int numOfBlocksInEastCampus = examTimetable[countForTimetable].BlocksList.Where(block => block.EastOrWest.Equals('E')).ToList().Count;
                if (numOfBlocksInEastCampus > 0)
                {
                    //include the quarantine invi for east campus
                    int numOfReliefInviRequiredForEastCampus = numOfBlocksInEastCampus + 1;
                    int numOfExperiencedReliefInEastCampus = 0;
                    int numOfUnExperiencedReliefInEastCampus = 0;
                    bool isQuarantineInviForEastCampusAssigned = false;
                    int numOfExperiencedReliefInviRequiredForEastCampus = (int)Math.Round(numOfReliefInviRequiredForEastCampus * 0.6, MidpointRounding.AwayFromZero);
                    for (int i = 1; i < examTimetable[countForTimetable].ReliefInvigilatorsList.Count; i++)
                    {
                        if ((examTimetable[countForTimetable].ReliefInvigilatorsList[i].IsInviAbove2Years
                            && numOfExperiencedReliefInEastCampus < numOfExperiencedReliefInviRequiredForEastCampus)
                            || (examTimetable[countForTimetable].ReliefInvigilatorsList.Where(relief => !relief.IsInviAbove2Years).ToList().Count < numOfReliefInviRequiredForEastCampus - numOfExperiencedReliefInviRequiredForEastCampus
                            && numOfExperiencedReliefInEastCampus + numOfUnExperiencedReliefInEastCampus < numOfReliefInviRequiredForEastCampus))
                        {
                            maintainInvigilationDutyControl.changeLocationOfReliefInvi(examTimetable[countForTimetable].Date, examTimetable[countForTimetable].Session, examTimetable[countForTimetable].ReliefInvigilatorsList[i].StaffID, location, isQuarantineInviForEastCampusAssigned);
                            numOfExperiencedReliefInEastCampus++;
                            //change the flag to true to indicate the quarantine invigilator for wast campus is assigned
                            if (!isQuarantineInviForEastCampusAssigned)
                            {
                                isQuarantineInviForEastCampusAssigned = true;
                            }
                        }

                        if ((!examTimetable[countForTimetable].ReliefInvigilatorsList[i].IsInviAbove2Years
                            && numOfUnExperiencedReliefInEastCampus < numOfReliefInviRequiredForEastCampus - numOfExperiencedReliefInviRequiredForEastCampus)
                            || (examTimetable[countForTimetable].ReliefInvigilatorsList.Where(relief => relief.IsInviAbove2Years).ToList().Count < numOfExperiencedReliefInviRequiredForEastCampus
                            && numOfExperiencedReliefInEastCampus + numOfUnExperiencedReliefInEastCampus < numOfReliefInviRequiredForEastCampus))
                        {
                            maintainInvigilationDutyControl.changeLocationOfReliefInvi(examTimetable[countForTimetable].Date, examTimetable[countForTimetable].Session, examTimetable[countForTimetable].ReliefInvigilatorsList[i].StaffID, location, isQuarantineInviForEastCampusAssigned);
                            numOfUnExperiencedReliefInEastCampus++;
                            //change the flag to true to indicate the quarantine invigilator for wast campus is assigned
                            if (!isQuarantineInviForEastCampusAssigned)
                            {
                                isQuarantineInviForEastCampusAssigned = true;
                            }
                        }
                    }
                }
            }
            maintainInvigilationDutyControl.shutDown();
            return examTimetable;
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