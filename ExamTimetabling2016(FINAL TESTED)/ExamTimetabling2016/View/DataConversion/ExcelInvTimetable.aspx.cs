using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamTimetabling2016.View
{
    public partial class ExcelInvTimetable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblGuideline.Text = "1. Use Invigilation TT (201603) file for importing.<br /><br />" +
               "2. Worksheet \"Inv.t.t\" will be imported.";
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            //check file format is excel
            if (FileUpload1.PostedFile.ContentType == "application/vnd.ms-excel" ||
                FileUpload1.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                try
                {
                    //get file name, extension, and folder path
                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    //set file path
                    string FilePath = Server.MapPath(FolderPath + FileName);

                    //check if folder path directory exists, else create one
                    bool isExists = System.IO.Directory.Exists(Server.MapPath(FolderPath));
                    if (!isExists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(FolderPath));

                    //save uploaded file to file path
                    FileUpload1.SaveAs(FilePath);

                    string conString = "";

                    //check extension to determine version of excel file
                    if (Extension.ToLower() == ".xls")
                    {
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\""; ;
                    }
                    else if (Extension.ToLower() == ".xlsx")
                    {
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }

                    //List to store timeslots, queries
                    ArrayList timeslotList = new ArrayList();
                    ArrayList queryList = new ArrayList();
                    string[] typeList = new string[] { "Chief", "Relief", "TT" };

                    //add fixed lists of timeslot into timeslotList
                    timeslotList.Add("AM110416");
                    timeslotList.Add("PM110416");
                    timeslotList.Add("AM120416");
                    timeslotList.Add("PM120416");
                    timeslotList.Add("AM130416");
                    timeslotList.Add("PM130416");
                    timeslotList.Add("AM140416");
                    timeslotList.Add("PM140416");
                    timeslotList.Add("AM150416");
                    timeslotList.Add("PM150416");
                    timeslotList.Add("AM160416");
                    timeslotList.Add("PM160416");

                    //add fixed sequence: Chief, Relief, TT. with respective columnNo into queryList
                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D6:F12]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J6:J13]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A16:J268]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D306:F313]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J306:J314]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A317:J548]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D562:F565]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J562:J566]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A569:J780]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D819:F825]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J819:J826]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A829:J1040]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D1072:F1078]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J1072:J1079]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A1082:J1298]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D1327:F1331]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J1327:J1332]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A1335:J1492]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D1530:F1532]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J1530:J1533]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A1536:J1655]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D1685:F1687]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J1685:J1688]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A1691:J1776]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D1787:F1788]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J1787:J1789]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A1792:J1863]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D1889:F1891]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J1889:J1892]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A1895:J1972]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D1988:F1989]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J1988:J1990]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A1993:J2060]");

                    queryList.Add("Select [Venue], [Chief] from [Inv.t.t$D2092:F2093]");
                    queryList.Add("Select [Relief] from [Inv.t.t$J2092:J2094]");
                    queryList.Add("Select [Venue], [Seat], [Paper], [Programme], [Duration], [Invigilators] from [Inv.t.t$A2097:J2161]");


                    int i = 0;
                    int u = 0;

                    //loop to automate the data insertion
                    while (i < queryList.Count)
                    {
                        //a < 3 for three types of Chief, Relief and TT
                        for (int a = 0; a < 3; a++)
                            importData(conString, queryList[i++].ToString(), typeList[a], timeslotList[u].ToString());

                        //each time finishing one set of Chief, Relief and TT, then u++ to move to another timeslot
                        u++;
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }
            }
            else
            {
                lblMsg.Text = "Please upload excel files only.";
            }
        }

        private void importData(String conString, String query, String type, String timeslot)
        {
            //create an OleDbConnection
            OleDbConnection con = new OleDbConnection(conString);
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            OleDbCommand cmd = new OleDbCommand(query, con);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            //create and fill Dataset
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            con.Close();
            con.Dispose();

            //import to Database
            using (Database1Entities de = new Database1Entities())
            {
                if (type.Equals("Chief"))
                {
                    string venue = "";
                    string chief = "";

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        venue = dr["Venue"].ToString();
                        chief = dr["Chief"].ToString();
                        chief = chief.Substring(chief.IndexOf(' ') + 1);

                        //remove ( ) in venue
                        venue = venue.Remove(0, 1);
                        venue = venue.Remove(venue.Length - 1);

                        //get staffID from staff table
                        var v = de.Staff2.Where(a => a.Name.Equals(chief)).FirstOrDefault();

                        if (v != null)
                        {
                            //insert here, if record does not exists in database
                            de.InvigilationDuty2.Add(new InvigilationDuty2
                            {
                                TimeslotID = timeslot,
                                Location = venue,
                                StaffID = v.StaffID,
                                CatOfInvi = "Chief",
                                Duration = 3
                            });
                            de.SaveChanges();
                        }
                    }
                }
                else if (type.Equals("Relief"))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string relief = dr["Relief"].ToString();
                        relief = relief.Substring(relief.IndexOf(' ') + 1);

                        //get staffID from staff table
                        var v = de.Staff2.Where(a => a.Name.Equals(relief)).FirstOrDefault();

                        if (v != null)
                        {
                            //insert here, if record does not exists in database
                            de.InvigilationDuty2.Add(new InvigilationDuty2
                            {
                                TimeslotID = timeslot,
                                Location = "West Campus",
                                StaffID = v.StaffID,
                                CatOfInvi = "Relief",
                                Duration = 3
                            });
                            de.SaveChanges();
                        }
                    }
                }
                else if (type.Equals("TT"))
                {
                    bool canImportInv = false;
                    bool canImportExam = false;
                    string venue = "";
                    string seat = "";
                    string paper = "";
                    char examType = '\0';
                    char paperType = '\0';
                    string programme = "";
                    string location = "";
                    string typeOfInv = "";
                    int year = 0;
                    int duration = 0;
                    int sitFrom = 0;
                    int sitTo = 0;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string invigilator = dr["Invigilators"].ToString();

                        if (!String.IsNullOrWhiteSpace(dr["Venue"].ToString()) && dr["Venue"].ToString().Length <= 4)
                        {
                            venue = dr["Venue"].ToString();

                            if (venue.Contains('M'))
                                location = "Block M";
                            else if (venue.Contains("PA"))
                                location = "Block PA";
                            else if (venue.Contains('Q'))
                                location = "Block Q";
                            else if (venue.Contains('R'))
                                location = "Block R";
                            else if (venue.Contains('V'))
                                location = "Block V";
                            else if (venue.Contains("DU"))
                                location = "Dewan Utama";
                            else if (venue.Contains("PA"))
                                location = "Block PA";
                        }

                        if (!String.IsNullOrWhiteSpace(dr["Seat"].ToString()) && !dr["Seat"].ToString().Equals("Seat"))
                        {
                            if (!dr["Programme"].ToString().Contains("Jr") && !dr["Programme"].ToString().Contains("Sr"))
                            {
                                canImportExam = true;
                                seat = dr["Seat"].ToString();
                                paper = dr["Paper"].ToString();
                                programme = dr["Programme"].ToString();
                                duration = Convert.ToInt32(dr["Duration"].ToString());

                                //remove ( ) in seat
                                seat = seat.Remove(0, 1);
                                seat = seat.Remove(seat.Length - 1);
                                string[] seatSplit = seat.Split(new[] { '-' }, 2);  //split seat
                                sitFrom = Convert.ToInt32(seatSplit[0]);
                                sitTo = Convert.ToInt32(seatSplit[1]);

                                paper = paper.Substring(0, paper.IndexOf(' '));  //get paper code before ' '

                                if (programme.Contains('('))
                                {
                                    paperType = programme[0];
                                    examType = programme[2];
                                    year = (int)Char.GetNumericValue(programme[programme.Length - 2]);
                                    programme = programme.Substring(programme.IndexOf('(') + 2, 2);
                                }
                                else
                                {
                                    paperType = 'M';
                                    examType = programme[0];
                                    year = (int)Char.GetNumericValue(programme[programme.Length - 1]);
                                    programme = programme.Substring(1, 2);
                                }
                            }
                        }
                        else
                            //allow to insert into Examination table only if Seat data is present
                            canImportExam = false;

                        if (!String.IsNullOrWhiteSpace(invigilator) && !invigilator.Equals("Invigilators"))
                        {
                            canImportInv = true;

                            if (invigilator.Contains("Assoc. Prof. Dr"))
                                invigilator = invigilator.Substring(invigilator.IndexOf("Assoc. Prof. Dr ") + 16);
                            else
                            {
                                string[] nameSplit = invigilator.Split(new[] { ' ' }, 2);  //split seat
                                invigilator = nameSplit[1];
                            }

                            if (invigilator.Contains('*'))
                            {
                                invigilator = invigilator.Remove(invigilator.IndexOf('*'), 1);  //remove *
                                typeOfInv = "In-charge";
                            }
                            else
                                typeOfInv = "Invigilator";
                        }
                        else
                            //allow to insert into InvigilationDuty table only if Invigilator data is present
                            canImportInv = false;


                        //if both Examination and InvigilationDuty can import
                        if (canImportExam && canImportInv)
                        {
                            //search database for duplicating record
                            var v = de.Examination2.Find(timeslot, venue, paper, programme, paperType.ToString(), year);
                            if (v != null)
                            {
                                //update here, if record exists in database
                                v.SitTo = sitTo;
                            }
                            else
                            {
                                //insert here, if record does not exist in database
                                de.Examination2.Add(new Examination2
                                {
                                    TimeslotID = timeslot,
                                    VenueID = venue,
                                    CourseCode = paper,
                                    ProgrammeCode = programme,
                                    PaperType = paperType.ToString(),
                                    Year = year,
                                    ExamType = examType.ToString(),
                                    SitFrom = sitFrom,
                                    SitTo = sitTo
                                });
                            }

                            //get staffID from database
                            var b = de.Staff2.Where(a => a.Name.Equals(invigilator)).FirstOrDefault();

                            if (b != null)
                            {
                                de.InvigilationDuty2.Add(new InvigilationDuty2
                                {
                                    TimeslotID = timeslot,
                                    VenueID = venue,
                                    Location = location,
                                    StaffID = b.StaffID,
                                    CatOfInvi = typeOfInv,
                                    Duration = duration
                                });
                            }
                        }
                        //if can only import Examination
                        else if (canImportExam)
                        {
                            //search database for duplicating record
                            var v = de.Examination2.Find(timeslot, venue, paper, programme, paperType.ToString(), year);
                            if (v != null)
                            {
                                v.SitTo = sitTo;
                            }
                            else
                            {
                                de.Examination2.Add(new Examination2
                                {
                                    TimeslotID = timeslot,
                                    VenueID = venue,
                                    CourseCode = paper,
                                    ProgrammeCode = programme,
                                    PaperType = paperType.ToString(),
                                    Year = year,
                                    ExamType = examType.ToString(),
                                    SitFrom = sitFrom,
                                    SitTo = sitTo
                                });
                            }
                        }
                        //if can only import InvigilationDuty
                        else if (canImportInv)
                        {
                            //search database for staffID
                            var v = de.Staff2.Where(a => a.Name.Equals(invigilator)).FirstOrDefault();
                            if (v != null)
                            {
                                de.InvigilationDuty2.Add(new InvigilationDuty2
                                {
                                    TimeslotID = timeslot,
                                    VenueID = venue,
                                    Location = location,
                                    StaffID = v.StaffID,
                                    CatOfInvi = typeOfInv,
                                    Duration = duration
                                });
                            }
                        }
                        de.SaveChanges();
                    }
                }

                //display successful msg
                lblMsg.Text = "Data Successfully Imported!";
            }
        }
    }
}