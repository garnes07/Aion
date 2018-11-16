namespace Aion.Models.Home
{
    public class SummaryView
    {
        public long RowId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public int? WeekNumber { get; set; }
        public int? NonCompliant { get; set; }
        public double Deployment { get; set; }
    }
}