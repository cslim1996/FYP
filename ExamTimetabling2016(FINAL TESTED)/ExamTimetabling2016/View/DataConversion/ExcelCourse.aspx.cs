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
    public partial class ExcelCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblGuideline.Text = "1. Select Faculty and Level, or Invigilator in the table. <br /><br />" +
                "2. Select and browse for the file to be imported. <br /> Note: Use \"Invigilation TT (201603)\" if Invigilator is selected. <br /><br />" +
                "3. Enter starting row number of header of the selected file, in the \"Starting Row No\"  textbox. <a href='Example.aspx' target='_blank'>Example</a><br />" +
                "Note: If Invigilator is selected, \"Inv.master\" worksheet will be imported.<br /><br />" +
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

                    //issue query based on selected radio button
                    if (rListFaculty.SelectedValue.Equals("CNBL") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Subject Code], [Subject] from [201603BACHELOR$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("CNBL") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Subject Code], [Subject] from [201603DIPLOMA$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("FAFB") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Subject Code], [Subject] from [Bachelor201603$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("FAFB") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Subject Code], [Subject] from [Diploma201603$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("FSAH") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Subject Code], [Subject] from [Bachelor201603$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("FSAH") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Subject Code], [Subject] from [Diploma201603$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("FASC") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Subject Code], [Subject] from [Bachelor201603$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("FASC") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Subject Code], [Subject] from [Diploma201603$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("FEBE") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Subject Code], [Subject] from [Bachelor201603$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("FEBE") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Subject Code], [Subject] from [Diploma201603$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("CPUS") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Subject Code], [Subject] from [Bachelor201603$B" + txtRowNo.Text + ":C10000]";
                    else if (rListFaculty.SelectedValue.Equals("CPUS") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Subject Code], [Subject] from [Diploma201603$B" + txtRowNo.Text + ":C10000]";
                    else if (RadioButtonInv.Checked)
                        query = "Select [Paper] from [Inv.master$D" + txtRowNo.Text + ":D10000]";


                    //check if radio buttons are checked
                    if ((rListFaculty.SelectedIndex >= 0 && rListLevel.SelectedIndex >= 0) || RadioButtonInv.Checked)
                    {
                        //check if uploaded file matches selected Faculty
                        if ((rListFaculty.SelectedValue.Equals("CNBL") && FileName.Contains("CNBL")) || (rListFaculty.SelectedValue.Equals("FAFB") && FileName.Contains("FAFB")) || (rListFaculty.SelectedValue.Equals("FSAH") && FileName.Contains("FSAH")) || (rListFaculty.SelectedValue.Equals("FASC") && FileName.Contains("FASC")) || (rListFaculty.SelectedValue.Equals("FEBE") && FileName.Contains("FEBE")) || (rListFaculty.SelectedValue.Equals("CPUS") && FileName.Contains("CPUS")) || (RadioButtonInv.Checked && FileName.Contains("Invigilation")))
                        {
                            //check for CNBL courses
                            if (RadioButtonInv.Checked)
                                importData(conString, query, "N", "inv");
                            else if (rListFaculty.SelectedIndex >= 0 && rListFaculty.SelectedValue.Equals("CNBL"))
                                importData(conString, query, "Y", "");
                            else
                                importData(conString, query, "N", "");
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

        //type parameter used to specify files, eg. inv means file "Invigilation TT (201603)" 
        private void importData(String conString, String query, String cnblPaper, String type)
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

            //to store number of records imported
            int recordCount = 0;

            //for Invigilation TT file
            if (!type.Equals("inv"))
            {
                //delete first row if empty
                if (String.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["Subject Code"].ToString()))
                {
                    ds.Tables[0].Rows[0].Delete();
                    ds.AcceptChanges();
                }

                //import to Database
                using (Database1Entities de = new Database1Entities())
                {
                    string courseCode = "";
                    string courseTitle = "";
                    //
                    bool canImport = false;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        if (!String.IsNullOrWhiteSpace(dr["Subject Code"].ToString()))
                        {
                            courseCode = dr["Subject Code"].ToString();
                            courseTitle = dr["Subject"].ToString();
                            canImport = false;
                        }
                        else if (!String.IsNullOrWhiteSpace(dr["Subject"].ToString()))
                        {
                            //for subject name occupying two cells in excel
                            courseTitle += " " + dr["Subject"].ToString();
                            canImport = true;
                        }
                        else
                            canImport = true;

                        if (canImport)
                        {
                            //search for duplicating record in database
                            var v = de.Course2.Where(a => a.CourseCode.Equals(courseCode)).FirstOrDefault();
                            if (v != null)
                            {
                                //update here, if record exists in database
                                v.CourseTitle = courseTitle;
                                v.CnblPaper = cnblPaper;
                                v.Session = 201603;
                                v.CreditHours = (int)Char.GetNumericValue(courseCode[courseCode.Length - 1]);
                            }
                            else
                            {
                                recordCount++;
                                //insert here, if record does not exist in database
                                de.Course2.Add(new Course2
                                {
                                    CourseCode = courseCode,
                                    CourseTitle = courseTitle,
                                    CnblPaper = cnblPaper,
                                    Session = 201603,
                                    CreditHours = (int)Char.GetNumericValue(courseCode[courseCode.Length - 1])
                                });
                            }
                            de.SaveChanges();
                        }
                    }
                }
            }
            else  //for other file like CNBL201603
            {
                //import to Database
                using (Database1Entities de = new Database1Entities())
                {
                    string courseCode = "";
                    string courseName = "";

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (!String.IsNullOrWhiteSpace(dr["Paper"].ToString()))
                        {
                            string[] paperSplit = dr["Paper"].ToString().Split(new[] { ' ' }, 2);  //split courseCode and courseName

                            if (paperSplit[0].Length == 8 && !paperSplit[0].Contains(','))
                            {
                                courseCode = paperSplit[0];
                                courseName = paperSplit[1];

                                //search for duplicating record in database
                                var v = de.Course2.Where(a => a.CourseCode.Equals(courseCode)).FirstOrDefault();
                                if (v == null)
                                {
                                    recordCount++;
                                    //insert here, if record does not exists in database
                                    de.Course2.Add(new Course2
                                    {
                                        CourseCode = courseCode,
                                        CourseTitle = courseName,
                                        CreditHours = (int)Char.GetNumericValue(courseCode[courseCode.Length - 1])
                                    });
                                    de.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }

            //display successful msg
            lblMsg.Text = "Data Successfully Imported! <br /> Number of Recods Imported: " + recordCount;
        }

        protected void RadioButtonInv_CheckedChanged(object sender, EventArgs e)
        {
            rListFaculty.ClearSelection();
            rListLevel.ClearSelection();
        }

        protected void rListFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rListFaculty.SelectedIndex >= 0)
                RadioButtonInv.Checked = false;
        }
    }
}