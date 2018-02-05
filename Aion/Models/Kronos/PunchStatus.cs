using System.Xml.Serialization;

namespace Aion.Models.Kronos
{
    public class PunchStatus
    {
        [XmlAttribute]
        public string Status { get; set; }

        [XmlElement]
        public Employee Employee { get; set; }
    }
}