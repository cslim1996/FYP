using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Exam_timetabling.classes
{
    [XmlRoot(ElementName = "venue")]
    public class Venue
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "floor")]
        public string Floor { get; set; }
        [XmlElement(ElementName = "capacity")]
        public string Capacity { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "block")]
    public class Block
    {
        [XmlElement(ElementName = "group")]
        public string Group { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "capacity")]
        public string Capacity { get; set; }
        [XmlElement(ElementName = "venue")]
        public List<Venue> Venue { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "blocks")]
    public class Blocks
    {
        [XmlElement(ElementName = "block")]
        public List<Block> Block { get; set; }

        public Blocks getBlock()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Blocks));
            StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(@"\PreProcessFile\VenueCapacity.xml"));
            Blocks blocks = (Blocks)serializer.Deserialize(sr);
            sr.Close();
            return blocks;
        }

    }

}