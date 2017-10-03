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
    public partial class ExcelExaminer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblGuideline.Text = "1. Select Faculty and Level in the table. <br /><br />" +
                "2. Select and browse for the file to be imported. <br /><br />" +
                "3. Enter starting column number of the header of the selected file, in the \"Starting Row No\"  textbox. <a href='Example.aspx' target='_blank'>Example</a><br /><br />" +
                "4. Click \"Import Data\" to start importing.";
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

                    string query = "";
                    //to indiciate Bachelor or Diploma
                    string lvlOfProgramme = "";

                    //issue query based on selected radio button
                    if (rListFaculty.SelectedValue.Equals("CNBL") && rListLevel.SelectedValue.Equals("Bachelor"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [201603BACHELOR$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "R";
                    }
                    else if (rListFaculty.SelectedValue.Equals("CNBL") && rListLevel.SelectedValue.Equals("Diploma"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [201603DIPLOMA$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "D";
                    }
                    else if (rListFaculty.SelectedValue.Equals("FAFB") && rListLevel.SelectedValue.Equals("Bachelor"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [Bachelor201603$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "R";
                    }
                    else if (rListFaculty.SelectedValue.Equals("FAFB") && rListLevel.SelectedValue.Equals("Diploma"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [Diploma201603$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "D";
                    }
                    else if (rListFaculty.SelectedValue.Equals("FSAH") && rListLevel.SelectedValue.Equals("Bachelor"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [Bachelor201603$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "R";
                    }
                    else if (rListFaculty.SelectedValue.Equals("FSAH") && rListLevel.SelectedValue.Equals("Diploma"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [Diploma201603$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "D";
                    }
                    else if (rListFaculty.SelectedValue.Equals("FASC") && rListLevel.SelectedValue.Equals("Bachelor"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [Bachelor201603$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "R";
                    }
                    else if (rListFaculty.SelectedValue.Equals("FASC") && rListLevel.SelectedValue.Equals("Diploma"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [Diploma201603$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "D";
                    }
                    else if (rListFaculty.SelectedValue.Equals("FEBE") && rListLevel.SelectedValue.Equals("Bachelor"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [Bachelor201603$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "R";
                    }
                    else if (rListFaculty.SelectedValue.Equals("FEBE") && rListLevel.SelectedValue.Equals("Diploma"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [Diploma201603$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "D";
                    }
                    else if (rListFaculty.SelectedValue.Equals("CPUS") && rListLevel.SelectedValue.Equals("Bachelor"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [Bachelor201603$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "R";
                    }
                    else if (rListFaculty.SelectedValue.Equals("CPUS") && rListLevel.SelectedValue.Equals("Diploma"))
                    {
                        query = "Select [Subject Code], [Examiner], [Special Remark] from [Diploma201603$B" + txtRowNo.Text + ":E10000]";
                        lvlOfProgramme = "D";
                    }


                    //check if radio buttons are checked
                    if (rListFaculty.SelectedIndex >= 0 && rListLevel.SelectedIndex >= 0)
                    {
                        //check if uploaded file matches selected Faculty
                        if ((rListFaculty.SelectedValue.Equals("CNBL") && FileName.Contains("CNBL")) || (rListFaculty.SelectedValue.Equals("FAFB") && FileName.Contains("FAFB")) || (rListFaculty.SelectedValue.Equals("FSAH") && FileName.Contains("FSAH")) || (rListFaculty.SelectedValue.Equals("FASC") && FileName.Contains("FASC")) || (rListFaculty.SelectedValue.Equals("FEBE") && FileName.Contains("FEBE")) || (rListFaculty.SelectedValue.Equals("CPUS") && FileName.Contains("CPUS")))
                        {
                            //check for faculty
                            if (rListFaculty.SelectedValue.Equals("CNBL"))
                                importData(conString, query, "E", lvlOfProgramme);  //import data
                            else if (rListFaculty.SelectedValue.Equals("FAFB"))
                                importData(conString, query, "B", lvlOfProgramme);  //import data
                            else if (rListFaculty.SelectedValue.Equals("FSAH"))
                                importData(conString, query, "H", lvlOfProgramme);  //import data
                            else if (rListFaculty.SelectedValue.Equals("FASC"))
                                importData(conString, query, "A", lvlOfProgramme);  //import data
                            else if (rListFaculty.SelectedValue.Equals("FEBE"))
                                importData(conString, query, "T", lvlOfProgramme);  //import data
                            else if (rListFaculty.SelectedValue.Equals("CPUS"))
                                importData(conString, query, "P", lvlOfProgramme);  //import data
                        }
                        else
                            lblMsg.Text = "Uploaded file does not match chosen Faculty. Pleaes try again.";
                    }
                    else
                        lblMsg.Text = "Please select one Faculty and one Level.";
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

        private void importData(String conString, String query, String facultyCode, String lvlOfProgramme)
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
                lblMsg.Text = "Incorrect row starting number. <br /> Please try again.";
                con.Close();
                return;
            }
            da.Dispose();
            con.Close();
            con.Dispose();

            //delete first row if empty
            if (String.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["Subject Code"].ToString()))
            {
                ds.Tables[0].Rows[0].Delete();
                ds.AcceptChanges();
            }

            //to store number of records imported
            int recordCount = 0;

            //import to Database
            using (Database1Entities de = new Database1Entities())
            {
                string courseCode = "";
                string examiner = "";
                string remark = "";
                //flag to stop import if whitespace is encountered
                bool canImport = false;  

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (!String.IsNullOrWhiteSpace(dr["Subject Code"].ToString()))
                    {
                        canImport = true;
                        courseCode = dr["Subject Code"].ToString();
                        examiner = dr["Examiner"].ToString().Trim();
                        examiner = examiner.Substring(examiner.IndexOf(' ') + 1);
                        remark = dr["Special Remark"].ToString().Trim();

                        if (String.IsNullOrWhiteSpace(remark) || remark.Length == 1)
                        {
                            remark = null;
                        }
                        else
                            remark = remark.Substring(1);  //to skip '-' character
                    }
                    else if (!String.IsNullOrWhiteSpace(dr["Examiner"].ToString())) //for second examiner for same subject
                    {
                        canImport = true;
                        examiner = dr["Examiner"].ToString().Trim();
                        examiner = examiner.Substring(examiner.IndexOf(' ') + 1);
                    }
                    else
                        canImport = false;


                    if (canImport)
                    {
                        //get staffID of examiner from staff table
                        var v = de.Staff2.Where(a => a.Name.Equals(examiner)).FirstOrDefault();

                        if (v != null)
                        {
                            //check for duplication
                            var a = de.PaperExamined2.Find(v.StaffID, courseCode);

                            if (a == null)
                            {
                                recordCount++;
                                //insert into database
                                de.PaperExamined2.Add(new PaperExamined2
                                {
                                    StaffID = v.StaffID,
                                    CourseCode = courseCode,
                                    MaterialsByDECA = remark,
                                    FacultyCode = facultyCode,
                                    LvlOfProgramme = lvlOfProgramme
                                });
                                de.SaveChanges();
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