using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RTPIAPI.Models
{
    [XmlRoot(ElementName ="direction")]
    public class LUASDirection
    {
        [XmlAttribute(AttributeName ="name")]
        public string DirectionName { get; set; }
        
        [XmlElement(ElementName ="tram")]
        public LUASArrival[] Arrivals { get; set; }
    }
}
