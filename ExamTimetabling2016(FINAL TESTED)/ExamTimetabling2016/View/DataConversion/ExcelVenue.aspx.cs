using System;
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
    public partial class ExcelVenue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblGuideline.Text = "1. Use Invigilator TT (Master) file for importing.<br /><br />" +
                "2. Data from cell B14 to D78 will be imported.";
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
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=2\""; ;
                    }
                    else if (Extension.ToLower() == ".xlsx")
                    {
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=2\"";
                    }

                    //venue data is fixed in specified column in Invigilation TT (Master)
                    string query = "Select * from [Invigilator TT (201601&03)$B14:D78]";

                    importData(conString, query);

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

        private void importData(String conString, String query)
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
            try
            {
                da.Fill(ds);
            }
            catch (Exception)
            {
                lblMsg.Text = "Data not found. <br /> Please try again.";
                con.Close();
                return;
            }
            da.Dispose();
            con.Close();
            con.Dispose();

            //to store number of records imported
            int recordCount = 0;

            //import to Database
            using (Database1Entities de = new Database1Entities())
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        string venue = dr[dc].ToString();

                        //check for empty name
                        if (!String.IsNullOrWhiteSpace(venue))
                        {
                            string block = null;
                            string venueID = null;
                            string location = null;
                            string roomCode = null;
                            int arrayCount = 0;
                            string[] roomSplit = null;

                            if (venue.Equals("CH - COLLEGE HALL"))
                            {
                                block = "CH";
                                venueID = "CH";
                                location = "College Hall";
                                roomCode = "CH";
                            }
                            else if (venue.Equals("DU - DEWAN UTAMA"))
                            {
                                block = "DU";
                                venueID = "DU";
                                location = "Dewan Utama";
                                roomCode = "DU";
                            }
                            else if (venue.Equals("BLOCK DU"))
                            {
                                block = "DU";
                            }
                            else if (venue.Equals("BLOCK FR"))
                            {
                                block = "FR";
                            }
                            else if (venue.Equals("BLOCK H"))
                            {
                                block = "H";
                            }
                            else if (venue.Equals("KS = KOMPLEK SUKAN"))
                            {
                                block = "KS";
                            }
                            else if (venue.Equals("BLOCK L"))
                            {
                                block = "L";
                            }
                            else if (venue.Equals("BLOCK M"))
                            {
                                block = "M";
                            }
                            else if (venue.Equals("BLOCK PA"))
                            {
                                block = "PA";
                            }
                            else if (venue.Equals("BLOCK Q"))
                            {
                                block = "Q";
                            }
                            else if (venue.Equals("BLOCK R"))
                            {
                                block = "R";
                            }
                            else if (venue.Equals("BLOCK SB"))
                            {
                                block = "SB";
                            }
                            else if (venue.Equals("BLOCK SD"))
                            {
                                block = "SD";
                            }
                            else if (venue.Equals("BLOCK SE"))
                            {
                                block = "SE";
                            }
                            else if (venue.Equals("BLOCK V"))
                            {
                                block = "V";
                            }
                            else
                            {
                                //split venueID and roomCode
                                string[] venueSplit = venue.Split(new[] { '-' }, 2);  
                                venueID = venueSplit[0].Trim();
                                roomCode = venueSplit[1].Trim();
                                arrayCount = roomCode.Split('/').Length - 1;

                                if (arrayCount > 0)
                                {
                                    roomSplit = roomCode.Split(new[] { '/' }, arrayCount + 1);
                                    roomCode = roomSplit[0];
                                }

                                if (venueID.Contains("DU"))
                                {
                                    block = "DU";
                                    location = "Block DU";
                                }
                                else if (venueID.Contains("FR"))
                                {
                                    block = "FR";
                                    location = "Block FR";
                                    roomCode = "U104";
                                }
                                else if (venueID.Contains("H"))
                                {
                                    block = "H";
                                    location = "Block H";

                                    if (roomSplit != null)
                                    {
                                        for (int i = 1; i < roomSplit.Length; i++)
                                        {
                                            roomSplit[i] = "H" + roomSplit[i];
                                        }
                                    }
                                }
                                else if (venueID.Contains("KS"))
                                {
                                    block = "KS";
                                    location = "Kompleks Sukan";
                                }
                                else if (venueID.Contains("L"))
                                {
                                    block = "L";
                                    location = "Block L";

                                    if (roomCode.Contains("READING ROOM"))
                                        roomCode = "RR3";
                                }
                                else if (venueID.Contains("M"))
                                {
                                    block = "M";
                                    location = "Block M";

                                    if (roomCode.Contains("QUARANTINE"))
                                        roomCode = roomCode.Substring(0, 4);

                                    if (roomSplit != null)
                                    {
                                        for (int i = 1; i < roomSplit.Length; i++)
                                        {
                                            roomSplit[i] = "M" + roomSplit[i];
                                        }
                                    }
                                }
                                else if (venueID.Contains("PA"))
                                {
                                    block = "PA";
                                    location = "Block PA";

                                    if (roomSplit != null)
                                    {
                                        for (int i = 1; i < roomSplit.Length; i++)
                                        {
                                            roomSplit[i] = "PA" + roomSplit[i];
                                        }
                                    }
                                }
                                else if (venueID.Contains("Q"))
                                {
                                    block = "Q";
                                    location = "Block Q";

                                    if (roomCode.Contains("QUARANTINE"))
                                        roomCode = roomCode.Substring(0, 5);

                                    if (roomSplit != null)
                                    {
                                        for (int i = 1; i < roomSplit.Length; i++)
                                        {
                                            roomSplit[i] = "Q" + roomSplit[i];
                                        }
                                    }
                                }
                                else if (venueID.Contains("R"))
                                {
                                    block = "R";
                                    location = "Block R";

                                    if (roomSplit != null)
                                    {
                                        for (int i = 1; i < roomSplit.Length; i++)
                                        {
                                            roomSplit[i] = "R" + roomSplit[i];
                                        }
                                    }
                                }
                                else if (venueID.Contains("SB"))
                                {
                                    block = "SB";
                                    location = "Block SB";

                                    if (roomCode.Contains("QUARANTINE"))
                                        roomCode = roomCode.Substring(0, 5);
                                    else if (roomCode.Contains("1 ST"))
                                        roomCode = venueID + "1F";
                                    else if (roomCode.Contains("2 ND"))
                                        roomCode = venueID + "2F";
                                }
                                else if (venueID.Contains("SD"))
                                {
                                    block = "SD";
                                    location = "Block SD";

                                    if (roomSplit != null)
                                    {
                                        for (int i = 1; i < roomSplit.Length; i++)
                                        {
                                            roomSplit[i] = "SD" + roomSplit[i];
                                        }
                                    }
                                }
                                else if (venueID.Contains("SE"))
                                {
                                    block = "SE";
                                    location = "Block SE";

                                    if (roomSplit != null)
                                    {
                                        for (int i = 1; i < roomSplit.Length; i++)
                                        {
                                            roomSplit[i] = "SE" + roomSplit[i];
                                        }
                                    }
                                }
                                else if (venueID.Contains("V"))
                                {
                                    block = "V";
                                    location = "Block V";

                                    if (roomSplit != null)
                                    {
                                        for (int i = 1; i < roomSplit.Length; i++)
                                        {
                                            roomSplit[i] = "V" + roomSplit[i];
                                        }
                                    }
                                }
                            }

                            for (int i = 1; i <= arrayCount + 1; i++)
                            {
                                recordCount++;

                                if (roomCode == null)
                                {
                                    de.Block2.Add(new Block2
                                    {
                                        BlockCode = block
                                    });
                                }
                                else if (block.Equals("CH"))
                                {
                                    de.Block2.Add(new Block2
                                    {
                                        BlockCode = block
                                    });

                                    de.Venue2.Add(new Venue2
                                    {
                                        VenueID = venueID,
                                        Location = location
                                    });

                                    de.Room2.Add(new Room2
                                    {
                                        RoomCode = roomCode,
                                        VenueID = venueID,
                                        BlockCode = block
                                    });
                                }
                                else if (venue.Equals("DU - DEWAN UTAMA"))
                                {
                                    de.Venue2.Add(new Venue2
                                    {
                                        VenueID = venueID,
                                        Location = location
                                    });

                                    de.Room2.Add(new Room2
                                    {
                                        RoomCode = roomCode,
                                        VenueID = venueID,
                                        BlockCode = block
                                    });
                                }
                                else if (arrayCount + 1 > 1 && i > 1)
                                {
                                    de.Room2.Add(new Room2
                                    {
                                        RoomCode = roomSplit[i - 1],
                                        VenueID = venueID,
                                        BlockCode = block
                                    });
                                }
                                else
                                {
                                    de.Venue2.Add(new Venue2
                                    {
                                        VenueID = venueID,
                                        Location = location
                                    });

                                    de.Room2.Add(new Room2
                                    {
                                        RoomCode = roomCode,
                                        VenueID = venueID,
                                        BlockCode = block
                                    });
                                }
                                try
                                {
                                    de.SaveChanges();
                                }
                                catch (Exception)
                                {
                                    lblMsg.Text = "Data already exists in database.";
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            //display successful msg
            lblMsg.Text = "Data Successfully Imported! <br /> Number of Recods Imported: " + recordCount;
        }
    }
}