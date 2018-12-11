namespace Aion.Models.WFM
{
    public class AllChainDashboardData_v2View
    {
        public int? DashboardDataId { get; set; }
        public int? WeekNumber { get; set; }
        public string StoreFlag { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public int? StoreCount { get; set; }
        public int? TotalHeadCount { get; set; }
        public int? TimeCardsCompleted { get; set; }
        public int? ZeroHour { get; set; }
        public double? FinalTarget { get; set; }
        public double? SOH { get; set; }
        public int? ComplianceScore { get; set; }
        public double? SOHUtilization { get; set; }
        public double? PunchCompliance { get; set; }
        public int? ShortShifts { get; set; }
    }
}