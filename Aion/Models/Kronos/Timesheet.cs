using System.Collections.Generic;
using System.Xml.Serialization;

namespace Aion.Models.Kronos
{
    [XmlRoot]
    public class Timesheet
    {
        [XmlAttribute]
        public string LastTotalizationDateTime { get; set; }

        [XmlAttribute]
        public string ManagerSignoffDateTime { get; set; }

        [XmlAttribute]
        public string TotalsUpToDateFlag { get; set; }

        [XmlElement]
        public Employee Employee { get; set; }

        [XmlElement]
        public PeriodTotalData PeriodTotalData { get; set; }

        [XmlElement]
        public Period Period { get; set; }
    }

    public class Period
    {
        [XmlElement]
        public TimeFramePeriod TimeFramePeriod { get; set; }
    }

    public class TimeFramePeriod
    {
        [XmlAttribute]
        public string PeriodDateSpan { get; set; }

        [XmlAttribute]
        public string TimeFrameName { get; set; }
    }

    public class PeriodTotalData
    {
        [XmlElement]
        public PeriodTotals PeriodTotals { get; set; }
    }

    public class PeriodTotals
    {
        [XmlAttribute]
        public string PeriodDateSpan { get; set; }

        [XmlElement]
        public Totals Totals { get; set; }
    }

    public class Totals
    {
        [XmlElement]
        public List<Total> Total { get; set; }
    }

    public class Total
    {
        [XmlAttribute]
        public string IsCurrencyFlag { get; set; }

        [XmlAttribute]
        public string LaborAccountDescription { get; set; }

        [XmlAttribute]
        public string LaborAccountId { get; set; }

        [XmlAttribute]
        public string LaborAccountName { get; set; }

        [XmlAttribute]
        public string AmmountInCurrency { get; set; }

        [XmlAttribute]
        public string PayCodeId { get; set; }

        [XmlAttribute]
        public string PayCodeName { get; set; }

        [XmlAttribute]
        public string AmountInTime { get; set; }

        [XmlAttribute]
        public string OrgJobId { get; set; }

        [XmlAttribute]
        public string OrgJobName { get; set; }

        [XmlAttribute]
        public string OrgJobDescription { get; set; }
    }
}