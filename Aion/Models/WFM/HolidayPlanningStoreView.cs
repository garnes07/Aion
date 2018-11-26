namespace Aion.Models.WFM
{
    public class HolidayPlanningStoreView
    {
        public long RowId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? WeekNumber { get; set; }
        public int? Taken { get; set; }
        public int? Scheduled { get; set; }
        public int? Guideline { get; set; }
    }
}