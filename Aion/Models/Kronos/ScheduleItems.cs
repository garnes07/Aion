using System.Collections.Generic;
using System.Xml.Serialization;

namespace Aion.Models.Kronos
{
    public class ScheduleItems
    {
        [XmlElement]
        public List<ScheduleShift> ScheduleShift { get; set; }

        [XmlElement(ElementName = "SchedulePayCodeEdit")]
        public List<SchedulePayCodeEdit> Absentees { get; set; }
    }

    public class ScheduleShift
    {
        [XmlAttribute]
        public bool LockedFlag { get; set; }

        [XmlAttribute]
        public string StartDate { get; set; }

        [XmlAttribute]
        public bool IsDeleted { get; set; }

        [XmlElement]
        public Employee Employee { get; set; }

        [XmlElement]
        public ShiftSegments ShiftSegments { get; set; }

    }

    public class SchedulePayCodeEdit
    {
        [XmlAttribute]
        public bool LockedFlag { get; set; }

        [XmlAttribute]
        public string StartDate { get; set; }

        [XmlAttribute]
        public bool IsDeleted { get; set; }

        [XmlAttribute]
        public string AmountInTime { get; set; }

        [XmlAttribute]
        public string DisplayTime { get; set; }

        [XmlAttribute]
        public string OrgJobPath { get; set; }

        [XmlAttribute]
        public string PayCodeName { get; set; }

        [XmlElement]
        public Employee Employee { get; set; }
    }

    public class ShiftSegments
    {
        [XmlElement]
        public ShiftSegment ShiftSegment { get; set; }
    }
}