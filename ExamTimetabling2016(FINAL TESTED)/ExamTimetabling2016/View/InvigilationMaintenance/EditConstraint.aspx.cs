using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016.View.InvigilationMaintenance
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        bool validate;
        Constraint3 originalConstraint = new Constraint3();

        protected void Page_Load(object sender, EventArgs e)
        {
            validate = false;
            if (!IsPostBack)
            {
                tbSession.Enabled = false;
                tbDuration.Enabled = false;
                MaintainTimeslotVenueControl mTimeslotControl = new MaintainTimeslotVenueControl();
                MaintainExaminationControl mExamControl = new MaintainExaminationControl();
                MaintainCourseControl mCourseControl = new MaintainCourseControl();
                MaintainFacultyControl mFacultyControl = new MaintainFacultyControl();
                MaintainStaffControl mStaffControl = new MaintainStaffControl();
                MaintainConstraint3Control mConstraintControl = new MaintainConstraint3Control();

                List<Faculty> staffFacultyList = mStaffControl.getDistinctStaffFacultyCodesList();
                List<int> durationList = mCourseControl.selectDistinctDuration();
                List<string> sessionList = mTimeslotControl.selectAllSession();
                List<char> paperTypeList = mExamControl.getDistinctExamPaperType();
                List<Faculty> facultyList = mFacultyControl.getFacultyList();

                //session ddl
                foreach (string session in sessionList)
                {
                    DropDownListSession.Items.Add(new ListItem(session, session));
                }

                //duration ddl
                foreach (int duration in durationList)
                {
                    ddlDuration.Items.Add(new ListItem(duration.ToString(), duration.ToString()));
                }

                //paper type ddl
                foreach (char paperType in paperTypeList)
                {
                    ddlPaperType.Items.Add(new ListItem(paperType.ToString(), paperType.ToString()));
                }

                //faculty ddl
                foreach (Faculty faculty in facultyList)
                {
                    ddlFacultyExam.Items.Add(new ListItem(faculty.FacultyName, faculty.FacultyCode.ToString()));
                }

                foreach (Faculty faculty in staffFacultyList)
                {
                    ddlStaffFaculty.Items.Add(new ListItem(faculty.FacultyName, faculty.FacultyCode.ToString()));
                }
                //staff faculty ddl

                //load constraint from db by id
                int id = (int)Session["id"];
                originalConstraint = mConstraintControl.getConstraintById(id);

                if (originalConstraint.InvigilationDuty.Session!= null && originalConstraint.InvigilationDuty.Session!= "")
                {
                    DropDownListSession.ClearSelection();
                    DropDownListSession.SelectedIndex = DropDownListSession.Items.IndexOf(DropDownListSession.Items.FindByText("AM"));
                }

                if (originalConstraint.InvigilationDuty.Duration!= null && originalConstraint.InvigilationDuty.Duration != 0)
                {
                    ddlDuration.ClearSelection();
                    ddlDuration.SelectedIndex = ddlDuration.Items.IndexOf(ddlDuration.Items.FindByText(originalConstraint.InvigilationDuty.Duration.ToString()));
                }

                if(originalConstraint.InvigilationDuty.CategoryOfInvigilator!="" && originalConstraint.InvigilationDuty.CategoryOfInvigilator!= null)
                {
                    ddlCategoryOfInvigilation.ClearSelection();
                    ddlCategoryOfInvigilation.SelectedIndex = ddlCategoryOfInvigilation.Items.IndexOf(ddlCategoryOfInvigilation.Items.FindByText(originalConstraint.InvigilationDuty.CategoryOfInvigilator));
                }

                if(originalConstraint.Examination.Faculty.FacultyCode!='\0' && originalConstraint.Examination.Faculty.FacultyCode != null)
                {
                    ddlFacultyExam.ClearSelection();
                    ddlFacultyExam.SelectedIndex = ddlFacultyExam.Items.IndexOf(ddlFacultyExam.Items.FindByValue(originalConstraint.Examination.Faculty.FacultyCode.ToString()));
                }

                if(originalConstraint.Examination.PaperType !='\0' && originalConstraint.Examination.PaperType!= null)
                {
                    ddlPaperType.ClearSelection();
                    ddlPaperType.SelectedIndex = ddlPaperType.Items.IndexOf(ddlPaperType.Items.FindByText(originalConstraint.Examination.PaperType.ToString()));
                }

                if (originalConstraint.IsCnblPaper != null && originalConstraint.IsCnblPaper ==true)
                {
                    CheckBoxCnbl.Checked = true;
                }
                if(originalConstraint.IsDoubleSeating == true)
                {
                    CheckBoxDoubleSeating.Checked = true;
                }
                if(originalConstraint.DayOfWeek!=null && originalConstraint.DayOfWeek != "")
                {
                    ddlDayOfExam.SelectedIndex = ddlDayOfExam.Items.IndexOf(ddlDayOfExam.Items.FindByText(originalConstraint.DayOfWeek));
                }

                if (originalConstraint.MinExperiencedInvigilator != 0 && originalConstraint.MinExperiencedInvigilator!= null)
                {
                    tbMinPercentageOfExpInvi.Text = originalConstraint.MinExperiencedInvigilator.ToString();
                }

                mConstraintControl.shutDown();
                mFacultyControl.shutDown();
                mExamControl.shutDown();
                mTimeslotControl.shutDown();
                mCourseControl.shutDown();
            }
        }

        protected void btnResetExam_Click(object sender, EventArgs e)
        {
            resetExam();
            validate = false;
        }

        protected void btnResetStaff_Click(object sender, EventArgs e)
        {
            resetStaff();
            validate = false;
        }

        protected void ddlDurationDuty_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
            if (ddlDurationDuty.SelectedIndex != 0)
            {
                ddlSessionAndDurationDuty.ClearSelection();
                ddlSessionAndDurationDuty.SelectedIndex = 0;
                ddlSessionDuty.ClearSelection();
                ddlSessionDuty.SelectedIndex = 0;
                tbDuration.Enabled = true;
            }
            else
            {
                tbSession.Enabled = false;
                tbDuration.Enabled = false;
            }
        }

        protected void ddlSessionDuty_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
            if (ddlSessionDuty.SelectedIndex != 0)
            {
                ddlSessionAndDurationDuty.ClearSelection();
                ddlSessionAndDurationDuty.SelectedIndex = 0;
                ddlDurationDuty.ClearSelection();
                ddlDurationDuty.SelectedIndex = 0;

                tbSession.Enabled = true;
            }
            else
            {
                tbSession.Enabled = false;
                tbDuration.Enabled = false;
            }
        }

        protected void ddlSessionAndDurationDuty_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
            if (ddlSessionAndDurationDuty.SelectedIndex != 0)
            {
                ddlDurationDuty.ClearSelection();
                ddlDurationDuty.SelectedIndex = 0;

                ddlSessionDuty.ClearSelection();
                ddlSessionDuty.SelectedIndex = 0;
                tbSession.Enabled = true;
                tbDuration.Enabled = true;
            }
            else
            {
                tbSession.Enabled = false;
                tbDuration.Enabled = false;
            }
        }

        protected void btnResetConstraint_Click(object sender, EventArgs e)
        {
            resetConstraint();
            validate = false;
        }

        public void resetStaff()
        {

            //ddl muslim
            ddlMuslim.ClearSelection();
            ddlMuslim.SelectedIndex = 0;

            //ddl experienced invigilator
            ddlExperiencedInvigilator.ClearSelection();
            ddlExperiencedInvigilator.SelectedIndex = 0;

            //ddl chief invigilator
            ddlChief.ClearSelection();
            ddlChief.SelectedIndex = 0;

            //ddl stsphd
            ddlSTSPhd.ClearSelection();
            ddlSTSPhd.SelectedIndex = 0;

            //ddl typeofemploy
            ddlEmployType.ClearSelection();
            ddlSTSPhd.SelectedIndex = 0;

            //ddl other duty
            ddlOtherDuty.ClearSelection();
            ddlOtherDuty.SelectedIndex = 0;

            //ddl sesion duty
            ddlSessionDuty.ClearSelection();
            ddlSessionDuty.SelectedIndex = 0;

            //ddl duration duty
            ddlDurationDuty.ClearSelection();
            ddlDurationDuty.SelectedIndex = 0;

            //ddl session and duration duty
            ddlSessionAndDurationDuty.ClearSelection();
            ddlSessionAndDurationDuty.SelectedIndex = 0;

            //disable text box
            tbSession.Enabled = false;
            tbDuration.Enabled = false;
        }

        public void resetExam()
        { //session ddl
            DropDownListSession.ClearSelection();
            DropDownListSession.Items.FindByText("Not Specified").Selected = true;

            //duration ddl
            ddlDuration.ClearSelection();
            ddlDuration.Items.FindByText("Not Specified").Selected = true;

            //catofInvi ddl
            ddlCategoryOfInvigilation.ClearSelection();
            ddlCategoryOfInvigilation.SelectedIndex = 0;

            //paper type ddl
            ddlPaperType.ClearSelection();
            ddlPaperType.SelectedIndex = 0;

            //faculty ddl
            ddlFacultyExam.ClearSelection();
            ddlFacultyExam.SelectedIndex = 0;

            ///day of exam
            ddlDayOfExam.ClearSelection();
            ddlDayOfExam.SelectedIndex = 0;

            //checkboxes
            CheckBoxCnbl.Checked = false;
            CheckBoxDoubleSeating.Checked = false;
        }

        public void resetConstraint()
        {
            tbRemark.Text = "";
            ddlHardConstraint.ClearSelection();
            ddlHardConstraint.SelectedIndex = 0;
            ddlConstraintImportance.ClearSelection();
            ddlConstraintImportance.SelectedIndex = 0;
        }

        protected void btnResetAll_Click(object sender, EventArgs e)
        {
            resetStaff();
            resetExam();
            resetConstraint();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Constraint3 constraint = new Constraint3();
            constraint = loadInput();
            if (validate == true)
            {
                MaintainConstraint3Control mConstraintControl = new MaintainConstraint3Control();
                mConstraintControl.insertConstraintIntoDatabase(constraint);
                mConstraintControl.shutDown();
                Response.Redirect("SuccessAddConstraint.aspx");
            }
            else
            {
                lblValidate.Text = "Validation is Required";
            }
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            bool constraintIsCorrect = true;
            Constraint3 constraint = new Constraint3();
            constraint = loadInput();
            MaintainStaffControl mStaffControl = new MaintainStaffControl();
            List<Staff> invigilatorList = mStaffControl.getInvigilatorList();

            mStaffControl.shutDown();
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

            //create list of invigilation Duty
            List<InvigilationDuty> inviDutyList = createInvigilationDutyList(timeslotVenueForInvigilator, timeslotVenueForChief, timeslotVenueForRelief);

            //load constraint setting
            MaintainConstraintSettingControl mSettingControl = new MaintainConstraintSettingControl();
            ConstraintSetting setting = new ConstraintSetting();
            setting = mSettingControl.readSettingFromDatabase();
            mSettingControl.shutDown();

            //load constraint list with new constraint
            MaintainConstraint3Control mConstraintControl = new MaintainConstraint3Control();
            List<Constraint3> constraintList = new List<Constraint3>();
            constraintList = mConstraintControl.loadFullConstraintList();
            constraintList.Add(constraint);
            mConstraintControl.shutDown();

            //get faculty list
            MaintainFacultyControl mFacultyControl = new MaintainFacultyControl();
            List<Faculty> facultyList = new List<Faculty>();
            facultyList = mFacultyControl.getFacultyList();
            mFacultyControl.shutDown();

            //combine timeslotvenue list
            List<TimeslotVenue> CombinedTimeslotVenueList = new List<TimeslotVenue>();

            foreach (TimeslotVenue tsVenue in timeslotVenueForInvigilator)
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

            foreach (InvigilationDuty invigilationDuty in inviDutyList)
            {
                List<InvigilatorHeuristic> emptyInvi = new List<InvigilatorHeuristic>();
                foreach (Staff staff in invigilatorList)
                {
                    emptyInvi.Add(new InvigilatorHeuristic(staff));
                }

                Tuple<List<InvigilatorHeuristic>, int> tuple = getCanditateList(emptyInvi, invigilationDuty, constraintList, (int)minTotalLoadOfDutyForEachInvigilator, (int)minTotalLoadOfDutyForEachChiefInvigilator, CombinedTimeslotVenueList, facultyList, setting);
                List<InvigilatorHeuristic> candidateList = new List<InvigilatorHeuristic>(tuple.Item1);
                if (candidateList.Count <= 0)
                {
                    constraintIsCorrect = false;
                }
            }

            if (constraintIsCorrect == true)
            {
                validate = true;
                lblValidate.Text = "Validation completed, constraint is valid";
            }
            else
            {
                validate = false;
                lblValidate.Text = "Constraint will result in optionless for certain duty";
            }
        }

        protected Constraint3 loadInput()
        {
            Constraint3 constraint = new Constraint3();

            if (DropDownListSession.SelectedIndex != 0)
            {
                constraint.InvigilationDuty.Session = DropDownListSession.SelectedItem.Value;
            }

            if (ddlDuration.SelectedIndex != 0)
            {
                constraint.InvigilationDuty.Duration = Convert.ToInt16(ddlDuration.SelectedItem.Text);
            }

            if (ddlCategoryOfInvigilation.SelectedIndex != 0)
            {
                constraint.InvigilationDuty.CategoryOfInvigilator = ddlCategoryOfInvigilation.SelectedItem.Value;
            }

            if (ddlFacultyExam.SelectedIndex != 0)
            {
                constraint.Examination.Faculty.FacultyCode = Convert.ToChar(ddlFacultyExam.SelectedItem.Value);
            }

            if (ddlPaperType.SelectedIndex != 0)
            {
                constraint.Examination.PaperType = Convert.ToChar(ddlPaperType.SelectedItem.Value);
            }

            if (CheckBoxDoubleSeating.Checked == true)
            {
                constraint.IsDoubleSeating = true;
            }

            if (CheckBoxCnbl.Checked == true)
            {
                constraint.IsCnblPaper = true;
            }

            //staff
            if (ddlMuslim.SelectedIndex != 0)
            {
                if (ddlMuslim.SelectedValue == "Yes")
                {
                    constraint.Invigilator.IsMuslim = true;
                }
                else if (ddlMuslim.SelectedValue == "No")
                {
                    constraint.Invigilator.IsMuslim = false;
                }
            }

            //faculty code for staff

            if (ddlStaffFaculty.SelectedIndex != 0)
            {
                constraint.Invigilator.FacultyCode = Convert.ToChar(ddlStaffFaculty.SelectedValue);
            }

            //chief
            if (ddlChief.SelectedIndex != 0)
            {
                if (ddlChief.SelectedValue == "Yes")
                {
                    constraint.Invigilator.IsChiefInvi = true;
                }
                else if (ddlChief.SelectedValue == "No")
                {
                    constraint.Invigilator.IsChiefInvi = false;
                }
            }

            if (ddlExperiencedInvigilator.SelectedIndex != 0)
            {
                if (ddlExperiencedInvigilator.SelectedValue == "Yes")
                {
                    constraint.Invigilator.IsInviAbove2Years = true;
                }
                else if (ddlExperiencedInvigilator.SelectedValue == "No")
                {
                    constraint.Invigilator.IsInviAbove2Years = false;
                }
            }

            if (ddlSTSPhd.SelectedIndex != 0)
            {
                if (ddlSTSPhd.SelectedValue == "Yes")
                {
                    constraint.Invigilator.IsTakingSTSPhD = true;

                }
                else if (ddlSTSPhd.SelectedValue == "No")
                {
                    constraint.Invigilator.IsTakingSTSPhD = false;
                }
            }

            if (ddlEmployType.SelectedIndex != 0)
            {
                if (ddlEmployType.SelectedValue == "F")
                {
                    constraint.Invigilator.TypeOfEmploy = Convert.ToChar(ddlEmployType.SelectedValue);
                }
                if (ddlEmployType.SelectedValue == "P")
                {
                    constraint.Invigilator.TypeOfEmploy = Convert.ToChar(ddlEmployType.SelectedValue);
                }
            }

            if (ddlOtherDuty.SelectedIndex != 0)
            {
                if (ddlOtherDuty.SelectedValue == "Yes")
                {
                    constraint.HasOtherDutyOnSameDay = true;
                }
                else if (ddlOtherDuty.SelectedValue == "No")
                {
                    constraint.HasOtherDutyOnSameDay = false;
                }
            }

            if (ddlSessionAndDurationDuty.SelectedIndex != 0)
            {
                if (ddlSessionAndDurationDuty.SelectedValue == "Yes")
                {
                    constraint.HasSpecificSessionAndDurationDutyOnSameDay = true;
                }
                else if (ddlSessionAndDurationDuty.SelectedValue == "No")
                {
                    constraint.HasSpecificSessionAndDurationDutyOnSameDay = true;
                }
                constraint.HasSpecificSessionDutyOnSameDayString = tbSession.Text;
                constraint.HasSpecificDurationDutyOnSameDayInt = Convert.ToInt16(tbDuration.Text);
            }

            if (ddlSessionDuty.SelectedIndex != 0)
            {
                if (ddlSessionDuty.SelectedValue == "Yes")
                {
                    constraint.HasSpecificSessionDutyOnSameDay = true;
                }
                else if (ddlSessionDuty.SelectedValue == "No")
                {
                    constraint.HasSpecificSessionDutyOnSameDay = false;
                }
                constraint.HasSpecificSessionDutyOnSameDayString = tbSession.Text;
            }

            if (ddlDurationDuty.SelectedIndex != 0)
            {
                if (ddlDurationDuty.SelectedValue == "Yes")
                {
                    constraint.HasSpecificDurationDutyOnSameDay = false;
                }
                else if (ddlDurationDuty.SelectedValue == "No")
                {
                    constraint.HasSpecificDurationDutyOnSameDay = false;
                }
                constraint.HasSpecificDurationDutyOnSameDayInt = Convert.ToInt16(tbDuration.Text);
            }

            if (ddlConstraintImportance.SelectedValue == "Low")
            {
                constraint.ConstraintImportanceValue = 1;
            }
            else if (ddlConstraintImportance.SelectedValue == "Medium")
            {
                constraint.ConstraintImportanceValue = 10;
            }
            else if (ddlConstraintImportance.SelectedValue == "High")
            {
                constraint.ConstraintImportanceValue = 100;
            }

            if (ddlHardConstraint.SelectedIndex == 1)
            {
                constraint.IsHardConstraint = true;
            }
            else if (ddlHardConstraint.SelectedIndex == 0)
            {
                constraint.IsHardConstraint = false;
            }

            if (tbSession.Text != "" && tbRemark.Text != null)
            {
                constraint.Remark = tbRemark.Text;
            }

            return constraint;
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
                        TimeslotVenue timeslotVenue = new TimeslotVenue((timeslotVenueControl.getTimeslotID(tt.Date, tt.Session)), venue.VenueID, location, tt.Date, tt.Session, noOfInvigilator, venue.CoursesList);
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
                    else if (!(block.VenuesList.Count <= 0))
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

        protected void CheckBoxDoubleSeating_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxCnbl.Checked = false;
            validate = false;
        }

        protected void CheckBoxCnbl_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxDoubleSeating.Checked = false;
            validate = false;
        }

        //get candidate list 
        public Tuple<List<InvigilatorHeuristic>, int> getCanditateList(List<InvigilatorHeuristic> invigilators, InvigilationDuty invigilationDuty, List<Constraint3> constraintList, int minInvigilationDuty, int minInvigilationDutyForChief, List<TimeslotVenue> fullTimeslotVenueList, List<Faculty> facultyList, ConstraintSetting setting)
        {
            MaintainFacultyControl mFacultyControl = new MaintainFacultyControl();
            List<InvigilatorHeuristic> CandidateList = new List<InvigilatorHeuristic>();
            int constraintCount = 0;

            //assign to examiner
            if (setting.AssignToExaminer == true)
            {
                foreach (Examination exam in invigilationDuty.ExamList)
                {
                    if (exam.ExamType == 'M')
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
                if (constraint.InvigilationDuty.Session != null && !constraint.InvigilationDuty.Session.Equals(""))
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
                    if (examHasFaculty == true)
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
                    if (hasExamType == true)
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
                    if (isPaperType == true)
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
                    if (isExamYear == true)
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
            return new Tuple<List<InvigilatorHeuristic>, int>(CandidateList, constraintCount);
        }

        //get timeslotvenue with the similar duty session and date 
        public TimeslotVenue getTimeslotVenue(List<TimeslotVenue> fullTimeslotVenueList, DateTime date, string session, string venueID, string location)
        {
            TimeslotVenue tsVenue = new TimeslotVenue();
            foreach (TimeslotVenue timeslotVenue in fullTimeslotVenueList)
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

        protected void tbMinPercentageOfExpInvi_TextChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlDayOfExam_SelectedIndexChanged(object sender, EventArgs e)
        {

            validate = false;
        }

        protected void ddlPaperType_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlFacultyExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlCategoryOfInvigilation_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void DropDownListSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlMuslim_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlStaffFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlExperiencedInvigilator_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlChief_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlSTSPhd_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlEmployType_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlOtherDuty_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void tbSession_TextChanged(object sender, EventArgs e)
        {

            validate = false;
        }

        protected void ddlConstraintImportance_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void ddlHardConstraint_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void tbRemark_TextChanged(object sender, EventArgs e)
        {
            validate = false;
        }

        protected void tbDuration_TextChanged(object sender, EventArgs e)
        {
            validate = false;
        }
    }


}