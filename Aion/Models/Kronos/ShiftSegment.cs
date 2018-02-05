using System.Xml.Serialization;

namespace Aion.Models.Kronos
{
    public class ShiftSegment
    {
        [XmlAttribute]
        public string SegmentTypeName { get; set; }

        [XmlAttribute]
        public string StartDate { get; set; }

        [XmlAttribute]
        public string StartTime { get; set; }

        [XmlAttribute]
        public int StartDayNumber { get; set; }

        [XmlAttribute]
        public string EndDate { get; set; }

        [XmlAttribute]
        public string EndTime { get; set; }

        [XmlAttribute]
        public int EndDayNumber { get; set; }

        [XmlAttribute]
        public string LaborAccountName { get; set; }
    }
}