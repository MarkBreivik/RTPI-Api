using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RTPIAPI.Models
{
    [XmlRoot("stopInfo")]
    public class LUASStopInfo
    {
        [XmlAttribute(AttributeName ="created", DataType = "dateTime")]
        public DateTime Created { get; set; }

        [XmlAttribute(AttributeName ="stop")]
        public string LongName { get; set; }

        [XmlAttribute(AttributeName = "stopAbv")]
        public string ShortName { get; set; }

        [XmlElement (ElementName = "message")]
        public string ServiceMessage { get; set; }

        [XmlElement(ElementName = "direction")]
        public LUASDirection[] Directions { get; set; }
    }
}
