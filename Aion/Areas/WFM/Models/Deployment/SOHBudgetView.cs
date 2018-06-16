namespace Aion.Areas.WFM.Models.Deployment
{
    public class SOHBudgetView
    {
        public long? RowID { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? YearWeek { get; set; }
        public float? OriginalTarget { get; set; }
        public string Type { get; set; }
        public double? Hours { get; set; }
    }
}