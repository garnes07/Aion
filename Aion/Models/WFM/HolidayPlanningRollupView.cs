namespace Aion.Models.WFM
{
    public class HolidayPlanningRollupView
    {
        public long RowId { get; set; }
        public int? YearStart { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? Balance { get; set; }
        public int? Taken { get; set; }
        public int? Scheduled { get; set; }
    }
}