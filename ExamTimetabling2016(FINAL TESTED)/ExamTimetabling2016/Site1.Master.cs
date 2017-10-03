using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace ExamTimetabling2016
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        String[,] selectedDate;
        String startDate, endDate;
        int counter = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void onclick(object sender, EventArgs e)
        {
            checkSessionPreviousInsertedXML();
            DateTime myDateStartDate = DateTime.ParseExact(startDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime myDateEndDate = DateTime.ParseExact(endDate, "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (myDateStartDate < DateTime.Today && myDateEndDate <= DateTime.Today)
            {
                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + "Sorry, not allow to view and update past date" + "');", true);
            }
            else
            {
                Session["selectedDates"] = selectedDate;
                Session["startDate"] = startDate;
                Session["endDate"] = endDate;
                Response.Redirect("ExamSesssion_Create.aspx?IsRedirect=1");
            }
        }

    private void checkSessionPreviousInsertedXML()
        {
            string fileLoc = HostingEnvironment.ApplicationPhysicalPath + @"\PreProcessFile\ExamSession.xml";

            if (File.Exists(fileLoc))
            {
                counter = 0;
                int count = 1, loopCount = 0;

                XmlDocument xmlDoc = new XmlDocument();
                XDocument xdoc = XDocument.Load(HostingEnvironment.ApplicationPhysicalPath + @"\PreProcessFile\ExamSession.xml");
                xmlDoc.Load(HostingEnvironment.ApplicationPhysicalPath + @"\PreProcessFile\ExamSession.xml");
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/ExamSession/TimeSlot");

                counter = int.Parse(xdoc.Root.Attribute("TotalSession").Value);
                selectedDate = new String[counter, 2];

                foreach (XmlNode node in nodeList)
                {
                    if (count == 1)
                    {
                        startDate = node.SelectSingleNode("Date").InnerText;
                    }
                    else if (count == int.Parse(xdoc.Root.Attribute("TotalDay").Value))
                    {
                        endDate = node.SelectSingleNode("Date").InnerText;
                    }

                    if (node.SelectSingleNode("SessionCount").InnerText.Equals("2"))
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            selectedDate[loopCount, 0] = node.SelectSingleNode("Date").InnerText;
                            if (i == 0)
                            {
                                selectedDate[loopCount, 1] = "AM";
                            }
                            else if (i == 1)
                            {
                                selectedDate[loopCount, 1] = "PM";
                            }
                            loopCount++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            selectedDate[loopCount, 0] = node.SelectSingleNode("Date").InnerText;
                            if (i == 0)
                            {
                                selectedDate[loopCount, 1] = "AM";
                            }
                            else if (i == 1)
                            {
                                selectedDate[loopCount, 1] = "PM";
                            }
                            else
                            {
                                selectedDate[loopCount, 1] = "VM";
                            }
                            loopCount++;
                        }
                    }
                    count++;
                }
                Session["availableDate"] = counter;
                Session["selectedDate"] = selectedDate;
            }
            else
            {
                //ClientScripts.RegisterStartupScript(GetType(), "alert", "alert('" + "No records exist, please create a new examination session" + "');", true);
            }
        }
    }
}