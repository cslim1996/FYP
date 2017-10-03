using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Configuration;
using System.Data.SqlClient;

namespace Exam_timetabling.classes
{
    [XmlRoot(ElementName = "Programme")]
    public class Programme
    {
        [XmlElement(ElementName = "ProgrammeCode")]
        public string ProgrammeCode { get; set; }
        [XmlElement(ElementName = "Population")]
        public string Population { get; set; }
    }

    [XmlRoot(ElementName = "MainProgrammes")]
    public class MainProgrammes
    {
        [XmlElement(ElementName = "Programme")]
        public List<Programme> Programme { get; set; }
    }

    [XmlRoot(ElementName = "ResitProgramme")]
    public class ResitProgramme
    {
        [XmlElement(ElementName = "ProgrammeCode")]
        public string ProgrammeCode { get; set; }
        [XmlElement(ElementName = "Population")]
        public string Population { get; set; }
    }

    [XmlRoot(ElementName = "ResitProgrammes")]
    public class ResitProgrammes
    {
        [XmlElement(ElementName = "ResitProgramme")]
        public List<ResitProgramme> ResitProgramme { get; set; }
    }

    [XmlRoot(ElementName = "RepeatProgramme")]
    public class RepeatProgramme
    {
        [XmlElement(ElementName = "ProgrammeCode")]
        public string ProgrammeCode { get; set; }
        [XmlElement(ElementName = "Population")]
        public string Population { get; set; }
    }

    [XmlRoot(ElementName = "RepeatProgrammes")]
    public class RepeatProgrammes
    {
        [XmlElement(ElementName = "RepeatProgramme")]
        public List<RepeatProgramme> RepeatProgramme { get; set; }
    }

    [XmlRoot(ElementName = "clashes")]
    public class Clashes
    {
        [XmlElement(ElementName = "clash")]
        public List<string> Clash { get; set; }
    }

    [XmlRoot(ElementName = "course")]
    public class Course
    {
        [XmlElement(ElementName = "CourseName")]
        public string CourseName { get; set; }
        [XmlElement(ElementName = "Duration")]
        public string Duration { get; set; }
        [XmlElement(ElementName = "TotalMainProgrammes")]
        public string TotalMainProgrammes { get; set; }
        [XmlElement(ElementName = "MainPopulation")]
        public string MainPopulation { get; set; }
        [XmlElement(ElementName = "MainProgrammes")]
        public MainProgrammes MainProgrammes { get; set; }
        [XmlElement(ElementName = "TotalResitProgrammes")]
        public string TotalResitProgrammes { get; set; }
        [XmlElement(ElementName = "ResitPopulation")]
        public string ResitPopulation { get; set; }
        [XmlElement(ElementName = "ResitProgrammes")]
        public ResitProgrammes ResitProgrammes { get; set; }
        [XmlElement(ElementName = "TotalRepeatProgrammes")]
        public string TotalRepeatProgrammes { get; set; }
        [XmlElement(ElementName = "RepeatPopulation")]
        public string RepeatPopulation { get; set; }
        [XmlElement(ElementName = "RepeatProgrammes")]
        public RepeatProgrammes RepeatProgrammes { get; set; }
        [XmlElement(ElementName = "clashes")]
        public Clashes Clashes { get; set; }
        [XmlElement(ElementName = "branchRelated")]
        public string BranchRelated { get; set; }
        [XmlElement(ElementName = "TotalDualAwardProgrammes")]
        public string TotalDualAwardProgrammes { get; set; }
        [XmlElement(ElementName = "DualAwardPopulation")]
        public string DualAwardPopulation { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlAttribute(AttributeName = "courseCode")]
        public string CourseCode { get; set; }
        [XmlElement(ElementName = "SimultaneousCourses")]
        public SimultaneousCourses SimultaneousCourses { get; set; }
    }

    [XmlRoot(ElementName = "SimultaneousCourses")]
    public class SimultaneousCourses
    {
        [XmlElement(ElementName = "SimultaneousCourse")]
        public string SimultaneousCourse { get; set; }
    }


    [XmlRoot(ElementName = "Courses")]
    public class Courses
    {
        [XmlElement(ElementName = "course")]
        public List<Course> Course { get; set; }

        public Courses getExam()
        {

            XmlSerializer serializer = new XmlSerializer(typeof(Courses));
            StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(@"\PreProcessFile\TARUCExam.xml"));
            Courses courses = (Courses)serializer.Deserialize(sr);
            sr.Close();
            return courses;
        }

    }

}