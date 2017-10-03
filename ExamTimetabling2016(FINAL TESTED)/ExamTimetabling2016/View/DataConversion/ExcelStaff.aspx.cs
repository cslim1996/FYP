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
    public partial class ExcelStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblGuideline.Text = "1. Select Faculty and Level, or Invigilator in the table. <br /><br />" +
                "2. Select and browse for the file to be imported. <br /> Note: Use \"Invigilation TT (201603)\" if Invigilator is selected. <br /><br />" +
                "3. Enter starting row number of header of the selected file, in the \"Starting Row No\"  textbox. <a href='Example.aspx' target='_blank'>Example</a><br />" +
                "Note: If Invigilator is selected, \"Inv names\" worksheet will be imported and row number is <b>NOT NEEDED</b>.<br /><br />" +
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
                    if (Extension.ToLower() == ".xls" && RadioButtonInv.Checked)
                    {
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=2\"";
                    }
                    else if (Extension.ToLower() == ".xlsx" && RadioButtonInv.Checked)
                    {
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=2\"";
                    }
                    else if (Extension.ToLower() == ".xls")
                    {
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (Extension.ToLower() == ".xlsx")
                    {
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }

                    string query = "";

                    //issue query based on selected radio button
                    if (rListExaminer.SelectedValue.Equals("CNBL") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Examiner] from [201603BACHELOR$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("CNBL") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Examiner] from [201603DIPLOMA$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("FAFB") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Examiner] from [Bachelor201603$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("FAFB") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Examiner] from [Diploma201603$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("FSAH") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Examiner] from [Bachelor201603$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("FSAH") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Examiner] from [Diploma201603$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("FASC") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Examiner] from [Bachelor201603$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("FASC") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Examiner] from [Diploma201603$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("FEBE") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Examiner] from [Bachelor201603$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("FEBE") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Examiner] from [Diploma201603$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("CPUS") && rListLevel.SelectedValue.Equals("Bachelor"))
                        query = "Select [Examiner] from [Bachelor201603$D" + txtRowNo.Text + ":D10000]";
                    else if (rListExaminer.SelectedValue.Equals("CPUS") && rListLevel.SelectedValue.Equals("Diploma"))
                        query = "Select [Examiner] from [Diploma201603$D" + txtRowNo.Text + ":D10000]";
                    else
                        query = "Select * from [Inv names$]";


                    //check if radio buttons are checked
                    if ((rListExaminer.SelectedIndex >= 0 && rListLevel.SelectedIndex >= 0) || RadioButtonInv.Checked)
                    {
                        //check if uploaded file matches selected Option
                        if ((rListExaminer.SelectedValue.Equals("CNBL") && FileName.Contains("CNBL")) || (rListExaminer.SelectedValue.Equals("FAFB") && FileName.Contains("FAFB")) || (rListExaminer.SelectedValue.Equals("FSAH") && FileName.Contains("FSAH")) || (rListExaminer.SelectedValue.Equals("FASC") && FileName.Contains("FASC")) || (rListExaminer.SelectedValue.Equals("FEBE") && FileName.Contains("FEBE")) || (rListExaminer.SelectedValue.Equals("CPUS") && FileName.Contains("CPUS")) || (RadioButtonInv.Checked && FileName.Contains("Invigilation")))
                        {
                            if (RadioButtonInv.Checked)
                                importData(conString, query, "invigilator");
                            else
                                importData(conString, query, "examiner");
                        }
                        else
                            lblMsg.Text = "Uploaded file does not match chosen Option. Pleaes try again.";
                    }
                    else
                        lblMsg.Text = "Please select one Examiner and Level or Invigilator only.";

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

        //type parameter to indicate invigilator (from Invigilation TT (201603) sheet "inv names") or examiner (from CNBL201603 etc)
        private void importData(String conString, String query, String type)
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

            //import to Database
            using (Database1Entities de = new Database1Entities())
            {
                if (type.Equals("invigilator"))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            string name = dr[dc].ToString();
                            //check for empty name
                            if (!String.IsNullOrWhiteSpace(name))
                            {
                                string title = null;
                                string isMuslim = null;
                                string gender = null;

                                //check and assign title and name
                                if (name.Contains("Assoc. Prof. Dr"))
                                {
                                    title = "Dr";
                                    name = name.Substring(name.IndexOf("Assoc. Prof. Dr ") + 16);
                                }
                                else
                                {
                                    //split title and name
                                    string[] nameSplit = name.Split(new[] { ' ' }, 2);
                                    title = nameSplit[0];
                                    name = nameSplit[1];

                                    //special case, one Dr with 2 titles
                                    if (name.Substring(0, 2).Equals("Dr"))
                                    {
                                        name = name.Remove(0, 2);
                                        string tempName = title + name;
                                        name = tempName;
                                        title = "Dr";  //Dr title is prioritized
                                    }
                                }

                                //check and assign gender
                                if (title.Equals("Cik") || title.Equals("Pn") || title.Equals("Ms") || title.Equals("Datin") || name.Contains("bt"))
                                    gender = "F";
                                else if (title.Equals("Mr") || title.Equals("En"))
                                    gender = "M";

                                //check and assign isMuslim
                                if ((name.Contains("bin") && title.Equals("En")) || name.Contains("bt"))
                                    isMuslim = "Y";
                                else
                                    isMuslim = "N";

                                //search database for staff with the same name
                                var v = de.Staff2.Where(a => a.Name.Equals(name)).FirstOrDefault();
                                if (v == null)
                                {
                                    //insert here, if record does not exist in database
                                    recordCount++;
                                    de.Staff2.Add(new Staff2
                                    {
                                        Title = title,
                                        Name = name,
                                        Gender = gender,
                                        isMuslim = isMuslim,
                                        isInvi = "Y",
                                        isExam = "N"
                                    });
                                }
                                else
                                {
                                    //update here if record exists in database
                                    v.isInvi = "Y";
                                }
                                de.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string name = dr["Examiner"].ToString().Trim();
                        string title = null;
                        string isMuslim = null;
                        string gender = null;

                        //check for empty name
                        if (!String.IsNullOrWhiteSpace(name))
                        {
                            //split title and name
                            string[] nameSplit = name.Split(new[] { ' ' }, 2);  
                            title = nameSplit[0];
                            name = nameSplit[1];

                            //check and assign gender
                            if (title.Equals("Cik") || title.Equals("Pn") || title.Equals("Ms") || title.Equals("Datin") || name.Contains("bt"))
                                gender = "F";
                            else if (title.Equals("Mr") || title.Equals("En"))
                                gender = "M";

                            //check and assign isMuslim
                            if ((name.Contains("bin") && title.Equals("En")) || name.Contains("bt"))
                                isMuslim = "Y";
                            else
                                isMuslim = "N";

                            //search database for staff with the same name
                            var v = de.Staff2.Where(a => a.Name.Equals(name)).FirstOrDefault();
                            if (v == null)
                            {
                                //insert here, if record does not exist in database
                                recordCount++;
                                de.Staff2.Add(new Staff2
                                {
                                    Title = title,
                                    Name = name,
                                    Gender = gender,
                                    isMuslim = isMuslim,
                                    isExam = "Y",
                                    isInvi = "N"
                                });
                            }
                            else
                            {
                                //update here if record exists in database
                                v.isExam = "Y";
                            }
                            de.SaveChanges();
                        }
                    }
                }
            }

            //display successful msg
            lblMsg.Text = "Data Successfully Imported! <br /> Number of Recods Imported: " + recordCount;
        }

        protected void RadioButtonInv_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonInv.Checked)
            {
                rListExaminer.ClearSelection();
                rListLevel.ClearSelection();
                txtRowNo.Enabled = false;
            }
        }

        protected void rListExaminer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rListExaminer.SelectedIndex >= 0)
            {
                RadioButtonInv.Checked = false;
                txtRowNo.Enabled = true;
            }
        }
    }
}