using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ExamTimetabling2016
{
    public partial class SimultaneousPaperEdit : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (PreviousPage != null)
            {
                //Find Previous page ListView
                Control mainContent = PreviousPage.Master.FindControl("MainContent");
                ListView listView = (ListView)mainContent.FindControl("ListView1");
                //Get selected item from the ListView
                ListViewItem listViewItem = listView.Items[listView.SelectedIndex];
                //Initialize value into control
                Label SCIDLabel = listViewItem.FindControl("SCIDLabel") as Label;
                Label facultyLabel = listViewItem.FindControl("FacultyLabel") as Label;
                Label SCList = listViewItem.FindControl("SCListLabel") as Label;

                if (facultyLabel != null)
                {
                    Label1.Text = SCIDLabel.Text;
                    string facultyCode = facultyLabel.Text;
                    string[] list1 = SCList.Text.Split('|');

                    switch (facultyCode)
                    {
                        case "FASC":
                            ddlFaculty.Text = "A";
                            break;
                        case "FAFB":
                            ddlFaculty.Text = "B";
                            break;
                        case "CPE":
                            ddlFaculty.Text = "C";
                            break;
                        case "CNBL":
                            ddlFaculty.Text = "E";
                            break;
                        case "FSAH":
                            ddlFaculty.Text = "H";
                            break;
                        case "CPUS":
                            ddlFaculty.Text = "P";
                            break;
                        case "CPSR":
                            ddlFaculty.Text = "R";
                            break;
                        case "FEBE":
                            ddlFaculty.Text = "T";
                            break;
                        default:
                            break;
                    }

                    foreach (var list in list1)
                    {
                        ListBox1.Items.Add(list);
                    }

                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            lblUpdateStatus.Visible = false;
            lblErrorMsg.Enabled = false;
            lblErrorMsg.Text = "";

            if (cbCourse.SelectedIndex == -1 && ListBox1.Items.Count == 0)
            {
                Panel1.Visible = true;
                lblErrorMsg.Enabled = true;
                lblErrorMsg.Text = "Please select a course";
            }
            else if (cbCourse.SelectedIndex == -1 && ListBox1.Items.Count > 0)
            {
                Panel1.Visible = true;
                lblErrorMsg.Enabled = true;
                lblErrorMsg.Text = "Please select a course";
            }
            else
            {
                string sclist = "";
                foreach (var item in ListBox1.Items)
                {
                    sclist += item.ToString() + "|";
                }

                if (sclist.Contains(cbCourse.Text))
                {
                    Panel1.Visible = true;
                    lblErrorMsg.Enabled = true;
                    lblErrorMsg.Text = "The course is already added to the list";
                }
                else
                {
                    SqlConnection conn;
                    string connStr = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
                    conn = new SqlConnection(connStr);
                    conn.Open();

                    string strSelect;
                    SqlCommand cmdSelect;

                    strSelect = "Select * From SimultaneousCourses Where Status = 'Active' AND FacultyCode = '" + ddlFaculty.Text + "'";
                    cmdSelect = new SqlCommand(strSelect, conn);

                    SqlDataReader dtr;
                    dtr = cmdSelect.ExecuteReader();

                    if (dtr.HasRows)
                    {
                        while (dtr.Read())
                        {
                            if (dtr["SCList"].ToString().Contains(cbCourse.Text))
                            {
                                Panel1.Visible = true;
                                lblErrorMsg.Enabled = true;
                                lblErrorMsg.Text = "This course is already exits in other simultaneous record.";
                            }
                            else
                            {
                                ListBox1.Items.Add(new ListItem(cbCourse.Text, cbCourse.Text));
                                cbCourse.SelectedIndex = -1;
                            }
                        }
                    }
                    else
                    {
                        ListBox1.Items.Add(new ListItem(cbCourse.Text, cbCourse.Text));
                        cbCourse.SelectedIndex = -1;
                    }

                    conn.Close();
                    dtr.Close();
                }

                if (ListBox1.Items.Count > 1)
                {
                    btnSave.Enabled = true;
                }
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex <= -1)
            {
                //Do nothing
            }
            else
            {
                //Remove selected course from the list
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            btnSave.Enabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string sclist = "";
            foreach (var item in ListBox1.Items)
            {
                sclist += item.ToString() + "|";
            }

            /*Step 1: Create and Open Connection*/
            SqlConnection conn;
            string connStr = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
            conn = new SqlConnection(connStr);
            conn.Open();

            /*Step 2: Create Sql Update statement and Sql Update Object*/
            string strUpdate;
            SqlCommand cmdUpdate;
            strUpdate = "Update SimultaneousCourses Set FacultyCode = @FacultyCode, SCList = @SCList Where SCID = @SCID";

            cmdUpdate = new SqlCommand(strUpdate, conn);

            cmdUpdate.Parameters.AddWithValue("@FacultyCode", ddlFaculty.Text);
            cmdUpdate.Parameters.AddWithValue("@SCList", sclist.Remove(sclist.Length - 1));
            cmdUpdate.Parameters.AddWithValue("@SCID", int.Parse(Label1.Text));

            /*Step 3: Execute command to insert*/
            int n = cmdUpdate.ExecuteNonQuery();

            Panel1.Visible = true;

            /*Display update status*/
            if (n > 0)
            {
                lblUpdateStatus.Enabled = true;
                lblUpdateStatus.Visible = true;
                lblErrorMsg.Visible = false;
                lblUpdateStatus.Text = "Updated Successfully!";
            }
            else
            {
                lblUpdateStatus.Enabled = false;
                lblUpdateStatus.Visible = false;
                lblErrorMsg.Enabled = true;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "Sorry, update failed.";

            }

            /*Step 4: Close database connection*/
            conn.Close();

            //OVERWRITE PREPROCESS FILE
            SqlConnection conBook;
            //string connStr = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
            conBook = new SqlConnection(connStr);
            conBook.Open();

            /*Step2 : SQL Command object to retrieve data from Books table*/
            string strSelect;
            SqlCommand cmdSelect;

            strSelect = "SELECT ProgrammeStructure.Session, ProgrammeStructureCourse.CourseCode, Course.CourseTitle, Course.CnblPaper, Course.Duration, Course.ExamWeight, SUM(ProgrammeStructureCourse.Population) AS Total, COUNT(ProgrammeStructure.ProgrammeCode) AS TotalProgrammes FROM Course INNER JOIN ProgrammeStructureCourse ON Course.CourseCode = ProgrammeStructureCourse.CourseCode INNER JOIN ProgrammeStructure ON ProgrammeStructureCourse.PSID = ProgrammeStructure.PSID WHERE (ProgrammeStructure.Session = '201505') AND (Course.CnblPaper = 'Y') AND (Course.ExamWeight > 0) GROUP BY ProgrammeStructure.Session, ProgrammeStructureCourse.CourseCode, Course.CourseTitle, Course.CnblPaper, Course.Duration, Course.ExamWeight";
            cmdSelect = new SqlCommand(strSelect, conBook);

            /*Step 3: Execute command to retrieve data*/
            SqlDataReader dtr;
            dtr = cmdSelect.ExecuteReader();

            ArrayList resultAL = new ArrayList();

            /*Step 4: Display result set from the query*/
            int i = 0;

            SqlConnection conBook1;
            SqlConnection conBook2;
            SqlConnection conBook3;
            SqlConnection conBook4;
            SqlConnection conBook5;
            SqlConnection conBook6;

            string strSelect1;
            SqlCommand cmdSelect1;

            string strSelect2;
            SqlCommand cmdSelect2;

            string strSelect3;
            SqlCommand cmdSelect3;

            string strSelect4;
            SqlCommand cmdSelect4;

            string strSelect5;
            SqlCommand cmdSelect5;

            string strSelect6;
            SqlCommand cmdSelect6;

            SqlDataReader dtr1;
            SqlDataReader dtr2;
            SqlDataReader dtr3;
            SqlDataReader dtr4;
            SqlDataReader dtr5;
            SqlDataReader dtr6;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(Server.MapPath(@"\PreProcessFile\TARUCExam.xml"), settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Courses");

                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        conBook1 = new SqlConnection(connStr);
                        conBook1.Open();
                        conBook2 = new SqlConnection(connStr);
                        conBook2.Open();
                        conBook3 = new SqlConnection(connStr);
                        conBook3.Open();
                        conBook4 = new SqlConnection(connStr);
                        conBook4.Open();
                        conBook5 = new SqlConnection(connStr);
                        conBook5.Open();
                        conBook6 = new SqlConnection(connStr);
                        conBook6.Open();

                        strSelect3 = "SELECT COUNT(DISTINCT ProgrammeStructure.ProgrammeCode + CAST(ProgrammeStructure.YearStudy AS varchar)) AS TotalProgrammes FROM Course INNER JOIN ProgrammeStructureCourse ON Course.CourseCode = ProgrammeStructureCourse.CourseCode INNER JOIN ProgrammeStructure ON ProgrammeStructureCourse.PSID = ProgrammeStructure.PSID WHERE (Course.CnblPaper = 'Y') AND (Course.ExamWeight > 0) AND (ProgrammeStructure.Session = '201505') AND (Course.CourseCode = '" + dtr["CourseCode"].ToString() + "')";
                        cmdSelect3 = new SqlCommand(strSelect3, conBook3);

                        strSelect4 = "SELECT DISTINCT ProgrammeStructure.ExamType + ProgrammeStructure.ProgrammeCode + CAST(ProgrammeStructure.YearStudy AS varchar) AS Expr1, SUM(ProgrammeStructureCourse.Population) AS Expr2 FROM Course INNER JOIN ProgrammeStructureCourse ON Course.CourseCode = ProgrammeStructureCourse.CourseCode INNER JOIN ProgrammeStructure ON ProgrammeStructureCourse.PSID = ProgrammeStructure.PSID WHERE (Course.CnblPaper = 'Y') AND (Course.ExamWeight > 0) AND (ProgrammeStructure.Session = '201505') AND (Course.CourseCode = '" + dtr["CourseCode"].ToString() + "') GROUP BY ProgrammeStructure.ExamType + ProgrammeStructure.ProgrammeCode + CAST(ProgrammeStructure.YearStudy AS varchar)";
                        cmdSelect4 = new SqlCommand(strSelect4, conBook4);

                        //Count total resit programmes
                        strSelect1 = "SELECT DISTINCT RepeatResitRegistration.PaperType + '(' + RepeatResitRegistration.ExamType + RepeatResitRegistration.ProgrammeCode + CAST(RepeatResitRegistration.YearStudy AS varchar) + ')' AS ProgrammeCode, RepeatResitRegistration.CourseCode, RepeatResitRegistration.Session, RepeatResitRegistration.PaperType, SUM(RepeatResitRegistration.NoOfStudRegistered) AS TotalResit, COUNT(DISTINCT RepeatResitRegistration.ExamType + RepeatResitRegistration.ProgrammeCode + CAST(RepeatResitRegistration.YearStudy AS varchar)) AS TotalProgrammes FROM Course INNER JOIN RepeatResitRegistration ON Course.CourseCode = RepeatResitRegistration.CourseCode WHERE (RepeatResitRegistration.Session = '201505') AND (Course.CnblPaper = 'Y') AND (RepeatResitRegistration.CourseCode = '" + dtr["CourseCode"].ToString() + "') AND (RepeatResitRegistration.PaperType = 'R') GROUP BY RepeatResitRegistration.CourseCode, RepeatResitRegistration.Session, RepeatResitRegistration.PaperType, RepeatResitRegistration.ExamType, RepeatResitRegistration.ProgrammeCode, RepeatResitRegistration.YearStudy";
                        cmdSelect1 = new SqlCommand(strSelect1, conBook1);

                        strSelect5 = "SELECT COUNT(DISTINCT RepeatResitRegistration.ExamType + RepeatResitRegistration.ProgrammeCode + CAST(RepeatResitRegistration.YearStudy AS varchar)) AS TotalProgrammes, SUM(RepeatResitRegistration.NoOfStudRegistered) AS TotalResit FROM Course INNER JOIN RepeatResitRegistration ON Course.CourseCode = RepeatResitRegistration.CourseCode WHERE (RepeatResitRegistration.Session = '201505') AND (Course.CnblPaper = 'Y') AND (RepeatResitRegistration.CourseCode = '" + dtr["CourseCode"].ToString() + "') AND (RepeatResitRegistration.PaperType = 'R')";
                        cmdSelect5 = new SqlCommand(strSelect5, conBook5);

                        strSelect2 = "SELECT DISTINCT RepeatResitRegistration.PaperType + '(' + RepeatResitRegistration.ExamType + RepeatResitRegistration.ProgrammeCode + CAST(RepeatResitRegistration.YearStudy AS varchar) + ')'AS ProgrammeCode, RepeatResitRegistration.CourseCode, RepeatResitRegistration.Session, RepeatResitRegistration.PaperType, SUM(RepeatResitRegistration.NoOfStudRegistered) AS TotalRepeat, COUNT(DISTINCT RepeatResitRegistration.ExamType + RepeatResitRegistration.ProgrammeCode + CAST(RepeatResitRegistration.YearStudy AS varchar)) AS TotalProgrammes FROM Course INNER JOIN RepeatResitRegistration ON Course.CourseCode = RepeatResitRegistration.CourseCode WHERE (RepeatResitRegistration.Session = '201505') AND (Course.CnblPaper = 'Y') AND (RepeatResitRegistration.CourseCode = '" + dtr["CourseCode"].ToString() + "') AND (RepeatResitRegistration.PaperType = 'T') GROUP BY RepeatResitRegistration.CourseCode, RepeatResitRegistration.Session, RepeatResitRegistration.PaperType, RepeatResitRegistration.ExamType, RepeatResitRegistration.ProgrammeCode, RepeatResitRegistration.YearStudy";
                        cmdSelect2 = new SqlCommand(strSelect2, conBook2);

                        strSelect6 = "SELECT COUNT(DISTINCT RepeatResitRegistration.ExamType + RepeatResitRegistration.ProgrammeCode + CAST(RepeatResitRegistration.YearStudy AS varchar)) AS TotalProgrammes, SUM(RepeatResitRegistration.NoOfStudRegistered) AS TotalRepeat FROM Course INNER JOIN RepeatResitRegistration ON Course.CourseCode = RepeatResitRegistration.CourseCode WHERE (RepeatResitRegistration.Session = '201505') AND (Course.CnblPaper = 'Y') AND (RepeatResitRegistration.CourseCode = '" + dtr["CourseCode"].ToString() + "') AND (RepeatResitRegistration.PaperType = 'T')";
                        cmdSelect6 = new SqlCommand(strSelect6, conBook6);


                        dtr1 = cmdSelect1.ExecuteReader();
                        dtr2 = cmdSelect2.ExecuteReader();
                        dtr3 = cmdSelect3.ExecuteReader();
                        dtr4 = cmdSelect4.ExecuteReader();
                        dtr5 = cmdSelect5.ExecuteReader();
                        dtr6 = cmdSelect6.ExecuteReader();

                        //lblResult.Text += dtr["CourseCode"].ToString() + "<br/>";
                        writer.WriteStartElement("course");
                        writer.WriteAttributeString("id", i.ToString());
                        writer.WriteAttributeString("courseCode", dtr["CourseCode"].ToString());
                        writer.WriteElementString("CourseName", dtr["CourseTitle"].ToString());
                        writer.WriteElementString("Duration", dtr["Duration"].ToString());

                        if (dtr3.Read())
                        {
                            writer.WriteElementString("TotalMainProgrammes", dtr3["TotalProgrammes"].ToString());
                            writer.WriteElementString("MainPopulation", dtr["Total"].ToString());
                            writer.WriteStartElement("MainProgrammes");
                            if (dtr4.HasRows)
                            {
                                while (dtr4.Read())
                                {
                                    writer.WriteStartElement("Programme");
                                    writer.WriteElementString("ProgrammeCode", dtr4["Expr1"].ToString());
                                    writer.WriteElementString("Population", dtr4["Expr2"].ToString());
                                    writer.WriteEndElement();
                                }
                            }
                            writer.WriteEndElement();
                            conBook4.Close();
                            dtr4.Close();
                            conBook3.Close();
                            dtr3.Close();
                        }

                        if (dtr5.Read() && dtr6.Read())
                        {
                            if (Convert.ToInt32(dtr5["TotalProgrammes"].ToString()) > 0)
                            {
                                writer.WriteElementString("TotalResitProgrammes", dtr5["TotalProgrammes"].ToString());
                                writer.WriteElementString("ResitPopulation", dtr5["TotalResit"].ToString());
                                writer.WriteStartElement("ResitProgrammes");
                                if (dtr1.HasRows)
                                {
                                    while (dtr1.Read())
                                    {
                                        writer.WriteStartElement("ResitProgramme");
                                        writer.WriteElementString("ProgrammeCode", dtr1["ProgrammeCode"].ToString());
                                        writer.WriteElementString("Population", dtr1["TotalResit"].ToString());
                                        writer.WriteEndElement();
                                    }
                                }
                                writer.WriteEndElement();
                            }
                            else
                            {
                                writer.WriteElementString("TotalResitProgrammes", "0");
                                writer.WriteElementString("ResitPopulation", "0");
                            }

                            if (Convert.ToInt32(dtr6["TotalProgrammes"].ToString()) > 0)
                            {
                                writer.WriteElementString("TotalRepeatProgrammes", dtr6["TotalProgrammes"].ToString());
                                writer.WriteElementString("RepeatPopulation", dtr6["TotalRepeat"].ToString());
                                writer.WriteStartElement("RepeatProgrammes");
                                if (dtr2.HasRows)
                                {
                                    while (dtr2.Read())
                                    {
                                        writer.WriteStartElement("RepeatProgramme");
                                        writer.WriteElementString("ProgrammeCode", dtr2["ProgrammeCode"].ToString());
                                        writer.WriteElementString("Population", dtr2["TotalRepeat"].ToString());
                                        writer.WriteEndElement();
                                    }
                                }
                                writer.WriteEndElement();
                            }
                            else
                            {
                                writer.WriteElementString("TotalRepeatProgrammes", "0");
                                writer.WriteElementString("RepeatPopulation", "0");
                            }

                            conBook1.Close();
                            dtr1.Close();
                            conBook2.Close();
                            dtr2.Close();
                            conBook5.Close();
                            dtr5.Close();
                            conBook6.Close();
                            dtr6.Close();
                        }

                        writer.WriteEndElement();


                        resultAL.Add(i + "|" + dtr["CourseCode"].ToString() + " " + dtr["CourseTitle"].ToString());
                        i++;
                    }


                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            //lblResult.Text += i;

            /*Step 5: Close SqlReader and Database connection*/
            conBook.Close();
            dtr.Close();

            //Add clashes related
            SqlConnection conClashes;
            string connClashes = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
            conClashes = new SqlConnection(connClashes);
            conClashes.Open();

            XmlDocument TARUCMainFileClashes = new XmlDocument();
            TARUCMainFileClashes.Load(Server.MapPath(@"\PreProcessFile\TARUCExam.xml"));

            XmlNodeList nodesClashes = TARUCMainFileClashes.GetElementsByTagName("course");
            foreach (XmlNode value in nodesClashes)
            {

                //Debug.WriteLine("===>" + value.Attributes["courseCode"].Value);
                string strSelectClashes = "SELECT DISTINCT ProgrammeStructureCourse_1.CourseCode FROM ProgrammeStructureCourse INNER JOIN ProgrammeStructure ON ProgrammeStructureCourse.PSID = ProgrammeStructure.PSID INNER JOIN ProgrammeStructureCourse AS ProgrammeStructureCourse_1 ON ProgrammeStructure.PSID = ProgrammeStructureCourse_1.PSID INNER JOIN Course ON ProgrammeStructureCourse_1.CourseCode = Course.CourseCode WHERE (ProgrammeStructure.Session = '201505') AND (ProgrammeStructureCourse.CourseCode = '" + value.Attributes["courseCode"].Value + "') AND (Course.CnblPaper = 'Y') AND (Course.ExamWeight > 0.00)";

                SqlCommand cmdSelectClashes = new SqlCommand(strSelectClashes, conClashes);
                SqlDataReader dtrClashes = cmdSelectClashes.ExecuteReader();

                if (dtrClashes.HasRows)
                {
                    XmlElement clashes = TARUCMainFileClashes.CreateElement("clashes");
                    value.AppendChild(clashes);

                    while (dtrClashes.Read())
                    {
                        //Debug.WriteLine(node.Attributes["id"].Value);
                        XmlElement clash = TARUCMainFileClashes.CreateElement("clash");
                        clash.InnerText = "" + TARUCMainFileClashes.SelectSingleNode("/Courses/course[@courseCode='" + dtrClashes["CourseCode"].ToString() + "']").Attributes["id"].Value; //get the id of clashed subject
                        clashes.AppendChild(clash);
                    }
                }
                dtrClashes.Close();
            }
            conClashes.Close();

            TARUCMainFileClashes.Save(Server.MapPath(@"\PreProcessFile\TARUCExam.xml"));

            //Add Branch Related
            XmlDocument TARUCMainFileBranch = new XmlDocument();
            TARUCMainFileBranch.Load(Server.MapPath(@"\PreProcessFile\TARUCExam.xml"));

            SqlConnection conBranch;
            string connBranch = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
            conBranch = new SqlConnection(connBranch);
            conBranch.Open();

            XmlNodeList nodesBranch = TARUCMainFileBranch.GetElementsByTagName("course");
            foreach (XmlNode value in nodesBranch)
            {
                string strSelectBranch = "SELECT BranchCode FROM BranchCourse WHERE (CourseCode = '" + value.Attributes["courseCode"].Value + "') AND (Session = '201505')";

                SqlCommand cmdSelectBranch = new SqlCommand(strSelectBranch, conBranch);
                SqlDataReader dtrBranch = cmdSelectBranch.ExecuteReader();

                if (dtrBranch.HasRows)
                {
                    XmlElement branchRelated = TARUCMainFileBranch.CreateElement("branchRelated");
                    branchRelated.InnerText = "1";
                    value.AppendChild(branchRelated);
                }
                else
                {
                    XmlElement branchRelated = TARUCMainFileBranch.CreateElement("branchRelated");
                    branchRelated.InnerText = "0";
                    value.AppendChild(branchRelated);
                }
                dtrBranch.Close();
            }
            conBranch.Close();

            TARUCMainFileBranch.Save(Server.MapPath(@"\PreProcessFile\TARUCExam.xml"));

            //Add Dual Awards
            XmlDocument TARUCMainFileDual = new XmlDocument();
            TARUCMainFileDual.Load(Server.MapPath(@"\PreProcessFile\TARUCExam.xml"));

            SqlConnection conDual;
            string connDual = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
            conDual = new SqlConnection(connDual);
            conDual.Open();

            XmlNodeList nodesDual = TARUCMainFileDual.GetElementsByTagName("course");
            foreach (XmlNode value in nodesDual)
            {
                XmlElement TotalDualAwardProgrammes = TARUCMainFileDual.CreateElement("TotalDualAwardProgrammes");
                XmlElement DualAwardPopulation = TARUCMainFileDual.CreateElement("DualAwardPopulation");

                if (!value.Attributes["courseCode"].Value.Equals("BHEL2023"))
                {

                    TotalDualAwardProgrammes.InnerText = "0";
                    DualAwardPopulation.InnerText = "0";

                    value.AppendChild(TotalDualAwardProgrammes);
                    value.AppendChild(DualAwardPopulation);

                }
                else
                {

                    string strSelectDual = "SELECT COUNT(Student.IndexNo) AS Expr1, COUNT(DISTINCT Student.ProgrammeCode) AS Expr2 FROM Course INNER JOIN StudentCourse ON Course.CourseCode = StudentCourse.CourseCode INNER JOIN Student ON StudentCourse.IndexNo = Student.IndexNo AND StudentCourse.ExamType = Student.ExamType AND StudentCourse.Year = Student.Year AND StudentCourse.Semester = Student.Semester WHERE (Course.CnblPaper = 'Y') AND (StudentCourse.CourseCode = 'BHEL2023') AND (Student.IndexNo LIKE '______AU__%')";

                    SqlCommand cmdSelectDual = new SqlCommand(strSelectDual, conDual);
                    SqlDataReader dtrDual = cmdSelectDual.ExecuteReader();

                    if (dtrDual.HasRows)
                    {
                        while (dtrDual.Read())
                        {

                            TotalDualAwardProgrammes.InnerText = "" + dtrDual["Expr2"].ToString();
                            DualAwardPopulation.InnerText = "" + dtrDual["Expr1"].ToString();

                            value.AppendChild(TotalDualAwardProgrammes);
                            value.AppendChild(DualAwardPopulation);
                        }
                    }
                    dtrDual.Close();
                }

            }
            TARUCMainFileDual.Save(Server.MapPath(@"\PreProcessFile\TARUCExam.xml"));

            //Add Simultaneous
            SqlConnection conSimultaneous;
            string connSimultaneous = ConfigurationManager.ConnectionStrings["Examination"].ConnectionString;
            conSimultaneous = new SqlConnection(connSimultaneous);
            conSimultaneous.Open();

            string strSelectSimultaneous;
            SqlCommand cmdSelectSimultaneous;

            strSelectSimultaneous = "SELECT * FROM SimultaneousCourses Where Status = 'Active'";
            cmdSelectSimultaneous = new SqlCommand(strSelectSimultaneous, conSimultaneous);

            SqlDataReader dtrSimultaneous;
            dtrSimultaneous = cmdSelectSimultaneous.ExecuteReader();

            XmlDocument TARUCMainFileSimultaneous = new XmlDocument();
            TARUCMainFileSimultaneous.Load(Server.MapPath(@"\PreProcessFile\TARUCExam.xml"));

            XmlNodeList nodesSimultaneous = TARUCMainFileSimultaneous.GetElementsByTagName("course");
            try
            {

                if (dtrSimultaneous.HasRows)
                {
                    while (dtrSimultaneous.Read())
                    {
                        foreach (XmlNode value in nodesSimultaneous)
                        {
                            string courseCode = value.Attributes["courseCode"].Value;
                            //Debug.WriteLine(courseCode);

                            XmlElement simultaneous = TARUCMainFileSimultaneous.CreateElement("SimultaneousCourse");
                            if (dtrSimultaneous["SCList"].ToString().Contains(courseCode))
                            {
                                XmlElement sc = TARUCMainFileSimultaneous.CreateElement("SimultaneousCourses");
                                value.AppendChild(sc);

                                string b = dtrSimultaneous["SCList"].ToString();
                                string[] a = b.Split('|');
                                //Debug.WriteLine(b);

                                foreach (string x in a)
                                {
                                    if (x.Equals(courseCode))
                                    {

                                    }
                                    else
                                    {
                                        simultaneous.InnerText = "" + TARUCMainFileSimultaneous.SelectSingleNode("/Courses/course[@courseCode='" + x + "']").Attributes["id"].Value; //get the id of clashed subject
                                        sc.AppendChild(simultaneous);
                                    }
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {

            }

            conSimultaneous.Close();
            dtrSimultaneous.Close();

            TARUCMainFileSimultaneous.Save(Server.MapPath(@"\PreProcessFile\TARUCExam.xml"));
        }
    }
}