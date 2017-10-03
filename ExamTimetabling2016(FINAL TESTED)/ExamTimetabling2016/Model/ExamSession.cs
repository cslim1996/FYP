using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Exam_timetabling.classes
{

    [XmlRoot(ElementName = "TimeSlot")]
    public class TimeSlot
    {
        [XmlElement(ElementName = "Date")]
        public string Date { get; set; }
        [XmlElement(ElementName = "SessionCount")]
        public string SessionCount { get; set; }
        [XmlElement(ElementName = "Session")]
        public string Session { get; set; }
        [XmlAttribute(AttributeName = "ID")]
        public string ID { get; set; }
    }

    [XmlRoot(ElementName = "ExamSession")]
    public class ExamSession
    {
        [XmlElement(ElementName = "TimeSlot")]
        public List<TimeSlot> TimeSlot { get; set; }
        [XmlAttribute(AttributeName = "TotalDay")]
        public string TotalDay { get; set; }
        [XmlAttribute(AttributeName = "TotalSession")]
        public string TotalSession { get; set; }


        public ExamSession getSession()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ExamSession));
            StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(@"\PreProcessFile\ExamSession.xml"));
            ExamSession session = (ExamSession)serializer.Deserialize(sr);
            sr.Close();
            return session;
        }
    }


}