using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Diagnostics;
using System.Drawing;
using System.Configuration;
using System.IO;
using OfficeOpenXml.Style;

namespace ExamTimetabling2016
{
    public partial class ReportingInvigilationTimetable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnInvTimetable_Click(object sender, EventArgs e)
        {
            //Create folder if it does not exist
            if (!Directory.Exists(@"C:\ExamTimetablingFile\Report"))
                Directory.CreateDirectory(@"C:\ExamTimetablingFile\Report");

            //Assign file name
            string fileName = "InvTT_ " + DateTime.Now.ToString("ddMMyy_hhmmss") + ".xlsx";

            //Create a new workbook
            FileInfo newFile = new FileInfo(@"C:\ExamTimetablingFile\Report\" + fileName);

            writeToExcelTT(newFile);

            lblMsg.Text = "Invigilation Timetable Generated Successfully! <br /> Generated file stored at: <br /> " +
                @"C:\ExamTimetablingFile\Report\" + fileName + "<br /><br />" +
                "Please proceed to generate <a href=\"ReportingInvigilatorDutiesMaster.aspx\">Invigilation Duty Master</a>.";
        }


        private void writeToExcelTT(FileInfo newFile)
        {
            // EPPlus library is required
            ExcelPackage package = new ExcelPackage(newFile);

            // Add a worksheet to the empty workbook
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inv.TT");

            // Set column width
            worksheet.Column(1).Width = 4;
            worksheet.Column(2).Width = 12;
            worksheet.Column(3).Width = 0.8;
            worksheet.Column(4).Width = 19;
            worksheet.Column(5).Width = 0.8;
            worksheet.Column(6).Width = 19;
            worksheet.Column(7).Width = 11;
            worksheet.Column(8).Width = 12;
            worksheet.Column(9).Width = 0.8;
            worksheet.Column(10).Width = 25;

            MaintainExaminationControl examControl = new MaintainExaminationControl();
            MaintainInvigilationDutyControl invControl = new MaintainInvigilationDutyControl();
            MaintainStaffControl staffControl = new MaintainStaffControl();
            MaintainCourseControl courseControl = new MaintainCourseControl();

            //Get lists of examination, timeslot and invigilationDuty
            List<string> timeslotList = examControl.getTimeslot();
            List<Examination> examList = null;
            List<InvigilationDuty> invDutyList = null;

            //To keep track of the column number for A, B, D, F, G, H, J
            int[] columnNo = new int[7] { 2, 2, 2, 2, 2, 2, 2 };

            //Loop through examList and invDutyList with each timeslotID in timeslotList
            for (int y = 0; y < timeslotList.Count; y++)
            {
                //Get new list for every iteration with different timeslotID passed in
                examList = examControl.searchExamination(timeslotList[y]);
                invDutyList = invControl.searchInvigilationDuty2(timeslotList[y]);

                //Get date in string
                int year = Convert.ToInt32("20" + timeslotList[y].Substring(6, 2));
                int month = Convert.ToInt32(timeslotList[y].Substring(4, 2));
                int day = Convert.ToInt32(timeslotList[y].Substring(2, 2));
                DateTime dt = new DateTime(year, month, day);

                //Get session in string
                string session = timeslotList[y].Substring(0, 2);
                if (session.Equals("AM"))
                    session = "MORNING SESSION";
                else if (session.Equals("PM"))
                    session = "AFTERNOON SESSION";
                else
                    session = "EVENING SESSION";

                // Add data into cells, Date and Sessiom
                worksheet.Cells["A" + columnNo[0].ToString()].Value = dt.DayOfWeek.ToString().ToUpper() + ", " + dt.Day + " " + dt.ToString("MMMM").ToUpper() + " " + dt.Year;
                worksheet.Cells["A" + columnNo[0].ToString() + ":" + "J" + columnNo[0].ToString()].Merge = true;
                worksheet.Cells["A" + columnNo[0].ToString() + ":" + "J" + columnNo[0]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A" + columnNo[0].ToString()].Value = session;
                worksheet.Cells["A" + columnNo[0].ToString() + ":" + "J" + columnNo[0].ToString()].Merge = true;
                worksheet.Cells["A" + columnNo[0].ToString() + ":" + "J" + columnNo[0]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // New row
                for (int a = 0; a < columnNo.Length; a++)
                {
                    if (a != 0)
                        columnNo[a] = columnNo[0] + 1;
                }
                columnNo[0]++;

                // Add data into cells, Chief and Relief Invigilator for West Campus
                worksheet.Cells["B" + columnNo[1].ToString()].Value = "West Campus";
                worksheet.Cells["B" + columnNo[1]++.ToString()].Style.Font.UnderLine = true;
                worksheet.Cells["D" + columnNo[2]++.ToString()].Value = "";
                worksheet.Cells["F" + columnNo[3]++.ToString()].Value = "";
                worksheet.Cells["G" + columnNo[4]++.ToString()].Value = "";
                worksheet.Cells["H" + columnNo[5].ToString()].Value = "West Campus";
                worksheet.Cells["H" + columnNo[5]++.ToString()].Style.Font.UnderLine = true;
                worksheet.Cells["J" + columnNo[6]++.ToString()].Value = "";

                // New row
                for (int a = 0; a < columnNo.Length; a++)
                    columnNo[a] += 1;

                //print Chief and Relief title
                worksheet.Cells["B" + columnNo[1]++.ToString()].Value = "Chief";
                worksheet.Cells["B" + columnNo[1]++.ToString()].Value = "Invigilators";
                worksheet.Cells["H" + columnNo[5]++.ToString()].Value = "Relief";
                worksheet.Cells["H" + columnNo[5]++.ToString()].Value = "Invigilators";

                //print Chief and Relief invigilators for West Campus
                for (int i = 0; i < invDutyList.Count; i++)
                {
                    Staff staff = staffControl.getStaffName(invDutyList[i].StaffID);
                    string staffName = staff.Title + " " + staff.Name;

                    if (invDutyList[i].CategoryOfInvigilator.Equals("Chief") && !invDutyList[i].Location.Equals("Block SA") && !invDutyList[i].Location.Equals("Block SB") && !invDutyList[i].Location.Equals("Block SC") && !invDutyList[i].Location.Equals("Block SD") && !invDutyList[i].Location.Equals("Block SE") && !invDutyList[i].Location.Equals("Block SF") && !invDutyList[i].Location.Equals("Block SG"))
                    {
                        worksheet.Cells["D" + columnNo[2].ToString()].Value = "(" + invDutyList[i].Location + ")";
                        worksheet.Cells["E" + columnNo[2]++.ToString()].Value = "-";
                        worksheet.Cells["F" + columnNo[3]++.ToString()].Value = staffName;
                    }
                    else if (invDutyList[i].CategoryOfInvigilator.Equals("Relief") && invDutyList[i].Location.Equals("West Campus"))
                    {
                        worksheet.Cells["I" + columnNo[6].ToString()].Value = "-";
                        worksheet.Cells["J" + columnNo[6]++.ToString()].Value = staffName;
                    }
                }

                //new row
                for (int a = 0; a < columnNo.Length; a++)
                {
                    if (a != 6)
                        columnNo[a] = columnNo[6] + 2;
                }
                columnNo[6] += 2;

                // Add data into cells, Chief and Relief Invigilator for East Campus
                worksheet.Cells["B" + columnNo[1].ToString()].Value = "East Campus";
                worksheet.Cells["B" + columnNo[1]++.ToString()].Style.Font.UnderLine = true;
                worksheet.Cells["D" + columnNo[2]++.ToString()].Value = "";
                worksheet.Cells["F" + columnNo[3]++.ToString()].Value = "";
                worksheet.Cells["G" + columnNo[4]++.ToString()].Value = "";
                worksheet.Cells["H" + columnNo[5].ToString()].Value = "East Campus";
                worksheet.Cells["H" + columnNo[5]++.ToString()].Style.Font.UnderLine = true;
                worksheet.Cells["J" + columnNo[6]++.ToString()].Value = "";

                // New row
                for (int a = 0; a < columnNo.Length; a++)
                    columnNo[a] += 1;

                //print Chief and Relief title
                worksheet.Cells["B" + columnNo[1]++.ToString()].Value = "Chief";
                worksheet.Cells["B" + columnNo[1]++.ToString()].Value = "Invigilators";
                worksheet.Cells["H" + columnNo[5]++.ToString()].Value = "Relief";
                worksheet.Cells["H" + columnNo[5]++.ToString()].Value = "Invigilators";

                //print Chief and Relief invigilators for East Campus
                for (int i = 0; i < invDutyList.Count; i++)
                {
                    Staff staff = staffControl.getStaffName(invDutyList[i].StaffID);
                    string staffName = staff.Title + " " + staff.Name;

                    if (invDutyList[i].CategoryOfInvigilator.Equals("Chief") && (invDutyList[i].Location.Equals("Block SA") || invDutyList[i].Location.Equals("Block SB") || invDutyList[i].Location.Equals("Block SC") || invDutyList[i].Location.Equals("Block SD") || invDutyList[i].Location.Equals("Block SE") || invDutyList[i].Location.Equals("Block SF") || invDutyList[i].Location.Equals("Block SG")))
                    {
                        worksheet.Cells["D" + columnNo[2].ToString()].Value = "(" + invDutyList[i].Location + ")";
                        worksheet.Cells["E" + columnNo[2]++.ToString()].Value = "-";
                        worksheet.Cells["F" + columnNo[3]++.ToString()].Value = staffName;
                    }
                    else if (invDutyList[i].CategoryOfInvigilator.Equals("Relief") && invDutyList[i].Location.Equals("East Campus"))
                    {
                        worksheet.Cells["I" + columnNo[6].ToString()].Value = "-";
                        worksheet.Cells["J" + columnNo[6]++.ToString()].Value = staffName;
                    }
                }

                // New row
                for (int a = 0; a < columnNo.Length; a++)
                {
                    if (a != 6)
                        columnNo[a] = columnNo[6] + 2;
                }
                columnNo[6] += 2;

                //print header
                worksheet.Cells["A" + columnNo[0].ToString()].Value = "Venue";
                worksheet.Cells["A" + columnNo[0].ToString() + ":" + "B" + columnNo[0].ToString()].Merge = true;
                worksheet.Cells["A" + columnNo[0].ToString() + ":" + "B" + columnNo[0]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["D" + columnNo[2].ToString()].Value = "Paper";
                worksheet.Cells["D" + columnNo[2].ToString() + ":" + "F" + columnNo[2].ToString()].Merge = true;
                worksheet.Cells["D" + columnNo[2].ToString() + ":" + "F" + columnNo[2]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["G" + columnNo[4].ToString()].Value = "Programme";
                worksheet.Cells["G" + columnNo[4]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["H" + columnNo[5].ToString()].Value = "Duration";
                worksheet.Cells["H" + columnNo[5]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["H" + columnNo[5].ToString()].Value = "(Hours)";
                worksheet.Cells["H" + columnNo[5]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["J" + columnNo[6].ToString()].Value = "Invigilators";
                worksheet.Cells["J" + columnNo[6]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                //temp var used to store cell index for adding border
                int rowFrom = 0;

                //set top and bottom border for header
                using (var range = worksheet.Cells["A" + (columnNo[0] - 2).ToString() + ":" + "J" + (columnNo[0] - 2).ToString()])
                {
                    range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rowFrom = columnNo[0] - 2;
                }

                using (var range = worksheet.Cells["A" + columnNo[0].ToString() + ":" + "J" + columnNo[0].ToString()])
                {
                    range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                //new row
                for (int a = 0; a < columnNo.Length; a++)
                {
                    if (a != 5)
                        columnNo[a] = columnNo[5] + 1;
                }
                columnNo[5]++;

                string venueID = "";
                string staffID = "";
                int examCount = 0;
                int invCount = 0;

                //print examinations and invigilators
                for (int i = 0; i < examList.Count(); i++)
                {
                    //To avoid same repeating VenueID
                    if (!venueID.Equals(examList[i].VenueID))
                    {
                        if (invCount > examCount)
                        {
                            //new row
                            for (int a = 0; a < columnNo.Length; a++)
                            {
                                if (a != 6)
                                    columnNo[a] = columnNo[6] + 1;
                            }
                            columnNo[6]++;
                        }
                        else
                        {
                            //new row
                            for (int a = 0; a < columnNo.Length; a++)
                            {
                                if (a != 1)
                                    columnNo[a] = columnNo[1] + 1;
                            }
                            columnNo[1]++;
                        }

                        examCount = 0;
                        invCount = 0;
                        venueID = examList[i].VenueID;
                        worksheet.Cells["A" + columnNo[0].ToString()].Value = venueID;
                        worksheet.Cells["A" + columnNo[0]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }

                    examCount++;

                    worksheet.Cells["B" + columnNo[1].ToString()].Value = "(" + examList[i].SitFrom + "-" + examList[i].SitTo + ")";
                    worksheet.Cells["B" + columnNo[1]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["D" + columnNo[2]++.ToString()].Value = examList[i].CourseCode + " " + courseControl.getCourseTitle(examList[i].CourseCode);

                    if (examList[i].PaperType == 'M')
                        worksheet.Cells["G" + columnNo[4].ToString()].Value = examList[i].ExamType + examList[i].ProgrammeCode + examList[i].Year;
                    else
                        worksheet.Cells["G" + columnNo[4].ToString()].Value = examList[i].PaperType + "(" + examList[i].ExamType + examList[i].ProgrammeCode + examList[i].Year + ")";

                    worksheet.Cells["G" + columnNo[4]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    //Find duration in invDutyList with the same venueID
                    var value = invDutyList.First(x => x.VenueID.Equals(examList[i].VenueID));
                    worksheet.Cells["H" + columnNo[5].ToString()].Value = value.Duration;
                    worksheet.Cells["H" + columnNo[5]++.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    //Get the first staffID provided with timeslotID and venueID, to prevent printing repeated invigilator for same venueID
                    var v = invDutyList.First(x => x.TimeslotID.Equals(timeslotList[y]) && x.VenueID.Equals(venueID));
                    staffID = v.StaffID;

                    //Print invigilator
                    for (int b = 0; b < invDutyList.Count; b++)
                    {
                        if (invDutyList[b].TimeslotID.Equals(timeslotList[y]) && invDutyList[b].VenueID.Equals(examList[i].VenueID))
                        {
                            //Avoid printing repeated invigilator for same venueID
                            if (!staffID.Equals(invDutyList[b].StaffID) || invCount == 0)
                            {
                                invCount++;
                                Staff staff = staffControl.getStaffName(invDutyList[b].StaffID);
                                string staffName = staff.Title + " " + staff.Name;

                                //check for in-charge
                                if (invDutyList[b].CategoryOfInvigilator.Equals("In-charge"))
                                    worksheet.Cells["J" + columnNo[6]++.ToString()].Value = staffName + "*";
                                else
                                    worksheet.Cells["J" + columnNo[6]++.ToString()].Value = staffName;
                            }
                            else
                                break;
                        }
                    }
                }

                //set border
                using (var range = worksheet.Cells["A" + columnNo[1].ToString() + ":" + "J" + columnNo[1].ToString()])
                {
                    range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                using (var range = worksheet.Cells["B" + rowFrom.ToString() + ":" + "B" + columnNo[1].ToString()])
                {
                    range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                using (var range = worksheet.Cells["F" + rowFrom.ToString() + ":" + "F" + columnNo[1].ToString()])
                {
                    range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                using (var range = worksheet.Cells["G" + rowFrom.ToString() + ":" + "G" + columnNo[1].ToString()])
                {
                    range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                using (var range = worksheet.Cells["H" + rowFrom.ToString() + ":" + "H" + columnNo[1].ToString()])
                {
                    range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                }

                //leaves 20 rows between session
                for (int a = 0; a < columnNo.Length; a++)
                {
                    if (a != 1)
                        columnNo[a] = columnNo[1] + 20;
                }
                columnNo[1] += 20;
            }

            // save new workbook
            package.Save();
        }
    }
}