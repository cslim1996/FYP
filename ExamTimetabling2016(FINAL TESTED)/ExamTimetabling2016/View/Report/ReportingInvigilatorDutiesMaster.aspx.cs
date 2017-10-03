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
    public partial class ReportingInvigilatorDutiesMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnDutyMaster_Click(object sender, EventArgs e)
        {
            //Create folder if it does not exist
            if (!Directory.Exists(@"C:\ExamTimetablingFile\Report"))
                Directory.CreateDirectory(@"C:\ExamTimetablingFile\Report");

            //Assign file name
            string fileName = "InvDutyMaster_" + DateTime.Now.ToString("ddMMyy_hhmmss") + ".xlsx";

            //Create a new workbook
            FileInfo newFile = new FileInfo(@"C:\ExamTimetablingFile\Report\" + fileName);

            string[] types = new string[] { "CHIEF", "CNBL", "FEBE", "FAFB", "FASC" };

            foreach (var type in types)
                writeToExcelDM(newFile, type);

            lblMsg.Text = "Invigilation Duty Master Generated Successfully! <br /> Generated file stored at: <br /> " +
                @"C:\ExamTimetablingFile\Report\" + fileName;
        }

        private void writeToExcelDM(FileInfo newFile, string type)
        {
            // EPPlus library is required
            ExcelPackage package = new ExcelPackage(newFile);

            MaintainStaffControl staffControl = new MaintainStaffControl();
            MaintainExaminationControl examControl = new MaintainExaminationControl();
            MaintainInvigilationDutyControl dutyControl = new MaintainInvigilationDutyControl();
            MaintainExemptionControl exemptionControl = new MaintainExemptionControl();
            MaintainPaperExaminedControl paperExaminedControl = new MaintainPaperExaminedControl();

            //Check for CHIEF, or others
            if (type.Equals("CHIEF"))
            {
                // Add a worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(type);

                // Set column width
                worksheet.Column(1).Width = 4.5;
                worksheet.Column(2).Width = 45;

                //print header
                worksheet.Cells[1, 1].Value = "No.";
                worksheet.Cells[1, 1, 3, 1].Merge = true;
                worksheet.Cells[1, 1, 3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, 3, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 2, 3, 2].Merge = true;
                worksheet.Cells[1, 2, 3, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 2, 3, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                List<Staff> staffList = staffControl.getStaffList();
                staffControl.shutDown();
                List<string> timeslotList = examControl.getTimeslot();
                examControl.shutDown();
                List<Exemption> exemptionList = null;
                List<InvigilationDuty> dutyList = null;
                List<PaperExamined> paperExaminedList = null;

                //row and column as counter
                int rowNoStaff = 4;
                int rowNoDate = 1;
                int colNoDate = 3;
                int colNoTotal = 0;
                string timeslotID = "EMPTY";

                //array as counter for printing logo purpose
                List<DateTime> dateList = new List<DateTime>();

                //print timeslot
                for (int i = 0; i < timeslotList.Count; i++)
                {
                    //checking to avoid printing the same date twice
                    if (!timeslotID.Substring(2).Equals(timeslotList[i].Substring(2)))
                    {
                        timeslotID = timeslotList[i];
                        int year = Convert.ToInt32("20" + timeslotList[i].Substring(6, 2));
                        int month = Convert.ToInt32(timeslotList[i].Substring(4, 2));
                        int day = Convert.ToInt32(timeslotList[i].Substring(2, 2));
                        DateTime dt = new DateTime(year, month, day);
                        dateList.Add(dt);

                        worksheet.Cells[rowNoDate, colNoDate].Value = dt.ToString("ddd").ToUpper();
                        worksheet.Cells[rowNoDate, colNoDate, rowNoDate, colNoDate + 2].Merge = true;
                        worksheet.Cells[rowNoDate, colNoDate, rowNoDate++, colNoDate + 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        worksheet.Cells[rowNoDate, colNoDate].Value = dt.ToString("dd/MM/yy").ToUpper();
                        worksheet.Cells[rowNoDate, colNoDate, rowNoDate, colNoDate + 2].Merge = true;
                        worksheet.Cells[rowNoDate, colNoDate, rowNoDate++, colNoDate + 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        worksheet.Cells[rowNoDate, colNoDate++].Value = "AM";
                        worksheet.Cells[rowNoDate, colNoDate++].Value = "PM";
                        worksheet.Cells[rowNoDate, colNoDate++].Value = "EV";
                        worksheet.Cells[rowNoDate, colNoDate - 3, rowNoDate, colNoDate - 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        rowNoDate = 1;
                    }
                }

                //print total header
                worksheet.Cells[rowNoDate, colNoDate].Value = "Total";
                worksheet.Cells[rowNoDate, colNoDate, 3, colNoDate].Merge = true;
                worksheet.Cells[rowNoDate, colNoDate, 3, colNoDate].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[rowNoDate, colNoDate, 3, colNoDate].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                //assign the col of "Total" for writing inv duties
                colNoTotal = colNoDate;

                //set column width
                worksheet.Column(colNoDate).Width = 5.5;
                while (colNoDate >= 4)
                    worksheet.Column((colNoDate--) - 1).Width = 6.5;

                //count number of inv printed
                int countInvi = 0;

                //print staffs
                for (int i = 0; i < staffList.Count; i++)
                {
                    //check and print chief invigilator name
                    if (staffList[i].IsChief == 'Y')
                    {
                        //print chief invigilator
                        worksheet.Cells[countInvi + 4, 1].Value = ++countInvi;
                        worksheet.Cells["B" + rowNoStaff++.ToString()].Value = staffList[i].Name + " " + staffList[i].Title;

                        //check and print exemption
                        exemptionList = exemptionControl.searchExemption(staffList[i].StaffID);
                        if (exemptionList.Count > 0)
                        {
                            for (int a = 0; a < exemptionList.Count; a++)
                            {
                                //get the column number to insert
                                int colToInsert = 3 + (dateList.FindIndex(b => b.Date == exemptionList[a].Date) * 3);

                                //get the column number if not AM
                                if (exemptionList[a].Session.Equals("PM"))
                                    colToInsert += 1;
                                else if (exemptionList[a].Session.Equals("EV"))
                                    colToInsert += 2;

                                //insert exemption
                                worksheet.Cells[rowNoStaff - 1, colToInsert].IsRichText = true;
                                ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("X");
                            }
                        }

                        //count total number of duties for each inv
                        int totalDuty = 0;

                        //check and print invigilation duty 
                        dutyList = dutyControl.searchInvigilationDuty2(Convert.ToInt32(staffList[i].StaffID));
                        paperExaminedList = paperExaminedControl.getPaperExaminedList(staffList[i].StaffID);
                        if (dutyList.Count > 0)
                        {
                            totalDuty = 0;

                            for (int a = 0; a < dutyList.Count; a++)
                            {
                                //get timeslot Date to compare with dutyList Date
                                int year = Convert.ToInt32("20" + dutyList[a].TimeslotID.Substring(6, 2));
                                int month = Convert.ToInt32(dutyList[a].TimeslotID.Substring(4, 2));
                                int day = Convert.ToInt32(dutyList[a].TimeslotID.Substring(2, 2));
                                DateTime dt = new DateTime(year, month, day);

                                //get the column number to insert
                                int colToInsert = 3 + (dateList.FindIndex(b => b.Date == dt.Date) * 3);

                                //get the column number if not AM
                                if (dutyList[a].TimeslotID.Substring(0, 2).Equals("PM"))
                                    colToInsert += 1;
                                else if (dutyList[a].TimeslotID.Substring(0, 2).Equals("EV"))
                                    colToInsert += 2;

                                //retrieve course list(exam) on given location and timeslotID
                                List<string> paperList = paperExaminedControl.searchPaperExamined(dutyList[a].Location, dutyList[a].TimeslotID);

                                //check if the chief is also an examiner
                                var result = paperExaminedList.Select(s => s.CourseCode).Intersect(paperList);

                                //insert only if no exemption is inserted
                                if (worksheet.Cells[rowNoStaff - 1, colToInsert].Value == null)
                                {
                                    totalDuty++;
                                    worksheet.Cells[rowNoStaff - 1, colToInsert].IsRichText = true;

                                    //compare duty type
                                    if (dutyList[a].Location.Equals("Block V") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("V");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block SE") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("SE");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block SD") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("SD");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block SB") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("SB");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block R") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("R");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block Q") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("Q");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block PA, PA7-PA12") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("PA7");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block PA, PA1-PA6") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("PA1");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block M") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("M");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block L") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("L");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block KS") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("KS");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block H, H7-H14") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("H7");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block H, H1-H6") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("H1");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block H") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("H");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Dewan Utama") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("DU");
                                        ert.Size = 7;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Location.Equals("Block DS") && dutyList[a].CategoryOfInvigilator.Equals("Chief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("DS");
                                        ert.Size = 7;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                }

                                //if printing last duty in a duty list, print totalDuty
                                if (a + 1 == dutyList.Count)
                                    worksheet.Cells[rowNoStaff - 1, colNoTotal].Value = totalDuty;
                            }
                        }
                    }
                }

                //shutdown connection
                exemptionControl.shutDown();
                dutyControl.shutDown();
                paperExaminedControl.shutDown();

                //center "No." and all the ticks
                worksheet.Cells[4, 1, staffList.Count + 4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 3, rowNoStaff, colNoTotal].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            else
            {
                char facultyCode = '\0';
                //compare type to get facultyCode
                if (type.Equals("CNBL"))
                    facultyCode = 'E';
                else if (type.Equals("FEBE"))
                    facultyCode = 'T';
                else if (type.Equals("FAFB"))
                    facultyCode = 'B';
                else if (type.Equals("FASC"))
                    facultyCode = 'A';

                // Add a worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(type);

                // Set column width
                worksheet.Column(1).Width = 4.5;
                worksheet.Column(2).Width = 45;

                //print header
                worksheet.Cells[1, 1].Value = "No.";
                worksheet.Cells[1, 1, 3, 1].Merge = true;
                worksheet.Cells[1, 1, 3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 1, 3, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 2, 3, 2].Merge = true;
                worksheet.Cells[1, 2, 3, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, 2, 3, 2].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                List<Staff> staffList = staffControl.getStaffList();
                staffControl.shutDown();
                List<string> timeslotList = examControl.getTimeslot();
                examControl.shutDown();
                List<Exemption> exemptionList = null;
                List<InvigilationDuty> dutyList = null;
                List<PaperExamined> paperExaminedList = null;

                //row and column as counter
                int rowNoStaff = 4;
                int rowNoDate = 1;
                int colNoDate = 3;
                int colNoTotal = 0;
                string timeslotID = "EMPTY";

                //array as counter for printing logo purpose
                List<DateTime> dateList = new List<DateTime>();

                //print timeslot
                for (int i = 0; i < timeslotList.Count; i++)
                {
                    //checking to avoid printing the same date twice
                    if (!timeslotID.Substring(2).Equals(timeslotList[i].Substring(2)))
                    {
                        timeslotID = timeslotList[i];
                        int year = Convert.ToInt32("20" + timeslotList[i].Substring(6, 2));
                        int month = Convert.ToInt32(timeslotList[i].Substring(4, 2));
                        int day = Convert.ToInt32(timeslotList[i].Substring(2, 2));
                        DateTime dt = new DateTime(year, month, day);
                        dateList.Add(dt);

                        worksheet.Cells[rowNoDate, colNoDate].Value = dt.ToString("ddd").ToUpper();
                        worksheet.Cells[rowNoDate, colNoDate, rowNoDate, colNoDate + 2].Merge = true;
                        worksheet.Cells[rowNoDate, colNoDate, rowNoDate++, colNoDate + 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        worksheet.Cells[rowNoDate, colNoDate].Value = dt.ToString("dd/MM/yy").ToUpper();
                        worksheet.Cells[rowNoDate, colNoDate, rowNoDate, colNoDate + 2].Merge = true;
                        worksheet.Cells[rowNoDate, colNoDate, rowNoDate++, colNoDate + 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        worksheet.Cells[rowNoDate, colNoDate++].Value = "AM";
                        worksheet.Cells[rowNoDate, colNoDate++].Value = "PM";
                        worksheet.Cells[rowNoDate, colNoDate++].Value = "EV";
                        worksheet.Cells[rowNoDate, colNoDate - 3, rowNoDate, colNoDate - 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        rowNoDate = 1;
                    }
                }

                //print total header
                worksheet.Cells[rowNoDate, colNoDate].Value = "Total";
                worksheet.Cells[rowNoDate, colNoDate, 3, colNoDate].Merge = true;
                worksheet.Cells[rowNoDate, colNoDate, 3, colNoDate].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[rowNoDate, colNoDate, 3, colNoDate].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                //assign the col of "Total" for writing inv duties
                colNoTotal = colNoDate;

                //set column width
                worksheet.Column(colNoDate).Width = 5.5;
                while (colNoDate >= 4)
                    worksheet.Column((colNoDate--) - 1).Width = 6.5;

                //count inv number of inv printed
                int countInvi = 0;

                //print staffs
                for (int i = 0; i < staffList.Count; i++)
                {
                    //check and print invigilator based on faculty
                    if (staffList[i].IsInvi == 'Y' && staffList[i].FacultyCode == facultyCode)
                    {
                        //print invigilator
                        worksheet.Cells[countInvi + 4, 1].Value = ++countInvi;
                        worksheet.Cells["B" + rowNoStaff++.ToString()].Value = staffList[i].Name + " " + staffList[i].Title;

                        //check and print exemption
                        exemptionList = exemptionControl.searchExemption(staffList[i].StaffID);
                        if (exemptionList.Count > 0)
                        {
                            for (int a = 0; a < exemptionList.Count; a++)
                            {
                                //get the column number to insert
                                int colToInsert = 3 + (dateList.FindIndex(b => b.Date == exemptionList[a].Date) * 3);

                                //get the column number if not AM
                                if (exemptionList[a].Session.Equals("PM"))
                                    colToInsert += 1;
                                else if (exemptionList[a].Session.Equals("EV"))
                                    colToInsert += 2;

                                //insert exemption
                                worksheet.Cells[rowNoStaff - 1, colToInsert].IsRichText = true;
                                ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("X");
                            }
                        }

                        //count total number of duties for each inv
                        int totalDuty = 0;

                        //check and print invigilation duty 
                        dutyList = dutyControl.searchInvigilationDuty2(Convert.ToInt32(staffList[i].StaffID));
                        paperExaminedList = paperExaminedControl.getPaperExaminedList(staffList[i].StaffID);
                        if (dutyList.Count > 0)
                        {
                            totalDuty = 0;

                            for (int a = 0; a < dutyList.Count; a++)
                            {
                                //get timeslot Date to compare with dutyList Date
                                int year = Convert.ToInt32("20" + dutyList[a].TimeslotID.Substring(6, 2));
                                int month = Convert.ToInt32(dutyList[a].TimeslotID.Substring(4, 2));
                                int day = Convert.ToInt32(dutyList[a].TimeslotID.Substring(2, 2));
                                DateTime dt = new DateTime(year, month, day);

                                //get the column number to insert
                                int colToInsert = 3 + (dateList.FindIndex(b => b.Date == dt.Date) * 3);

                                //get the column number if not AM
                                if (dutyList[a].TimeslotID.Substring(0, 2).Equals("PM"))
                                    colToInsert += 1;
                                else if (dutyList[a].TimeslotID.Substring(0, 2).Equals("EV"))
                                    colToInsert += 2;

                                //retrieve course list(exam) on given location and timeslotID
                                List<string> paperList = paperExaminedControl.searchPaperExamined(dutyList[a].Location, dutyList[a].TimeslotID);

                                //check if the chief is also an examiner
                                var result = paperExaminedList.Select(s => s.CourseCode).Intersect(paperList);

                                //insert only if no exemption is inserted
                                if (worksheet.Cells[rowNoStaff - 1, colToInsert].Value == null)
                                {
                                    totalDuty++;
                                    worksheet.Cells[rowNoStaff - 1, colToInsert].IsRichText = true;

                                    //compare duty type
                                    if (dutyList[a].Location.Equals("Block DS"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("DS");
                                        ert.Size = 7;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].CategoryOfInvigilator.Equals("Relief"))
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;
                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("R");
                                        ert.Size = 7;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        if (dutyList[a].Duration != 2)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add(dutyList[a].Duration.ToString());
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Duration == 3)
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("3");
                                        ert.Size = 7;
                                    }
                                    else if (dutyList[a].Duration == 2)
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }
                                    }
                                    else if (dutyList[a].Duration == 1)
                                    {
                                        ExcelRichText ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("\u2713");
                                        ert.Size = 12;

                                        if (result.ToList().Count > 0)
                                        {
                                            ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("E");
                                            ert.Size = 7;
                                        }

                                        ert = worksheet.Cells[rowNoStaff - 1, colToInsert].RichText.Add("1");
                                        ert.Size = 7;

                                    }
                                }

                                //if printing last duty in a duty list, print totalDuty
                                if (a + 1 == dutyList.Count)
                                    worksheet.Cells[rowNoStaff - 1, colNoTotal].Value = totalDuty;
                            }
                        }
                    }
                }

                //shutdown connection
                exemptionControl.shutDown();
                dutyControl.shutDown();

                //center "No." and all the ticks
                worksheet.Cells[4, 1, staffList.Count + 4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 3, rowNoStaff, colNoTotal].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            //save new workbook
            package.Save();
        }
    }
}