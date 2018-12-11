namespace Aion.Models.WFM
{
    public class PeriodDeploymentSummaryView
    {
        public long RowId { get; set; }
        public string Year { get; set; }
        public byte? Period { get; set; }
        public int? WeekNumber { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public double? SOHSpend { get; set; }
        public double? TKHSpend { get; set; }
        public double? SOHBudget { get; set; }
        public double? TKHBudget { get; set; }
        public double? Variance { get; set; }
        public double? ToBudget { get; set; }
    }
}