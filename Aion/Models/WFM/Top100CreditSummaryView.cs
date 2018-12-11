namespace Aion.Models.WFM
{
    public class Top100CreditSummaryView
    {
        public int EntryID { get; set; }
        public int WeekNumber { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public double? SundayReq { get; set; }
        public double? SaturdayReq { get; set; }
        public decimal SundayDeployed { get; set; }
        public decimal SaturdayDeployed { get; set; }
        public double? SundayCredit { get; set; }
        public double? SaturdayCredit { get; set; }
        public short SundayAvailable { get; set; }
        public short SaturdayAvailable { get; set; }
        public string SundayRAG { get; set; }
        public string SaturdayRAG { get; set; }
        public string TotalRAG { get; set; }
        public double? FinalTarget { get; set; }
        public double? SOH { get; set; }
    }
}