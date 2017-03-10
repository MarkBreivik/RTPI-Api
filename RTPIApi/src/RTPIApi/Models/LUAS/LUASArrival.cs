using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RTPIAPI.Models
{
    [XmlRoot(ElementName = "tram")]
    public class LUASArrival
    {
        [XmlAttribute(AttributeName = "dueMins")]
        public string DueMins { get; set; }

        [XmlAttribute(AttributeName = "destination")]
        public string Destination { get; set; }
    }
}
