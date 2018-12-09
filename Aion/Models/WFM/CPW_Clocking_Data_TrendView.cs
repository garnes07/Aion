namespace Aion.Models.WFM
{
    public class CPW_Clocking_Data_TrendView
    {
        public long RowId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? FNCL_WK_NUM { get; set; }
        public int? ShiftCount { get; set; }
        public int? MissedIn { get; set; }
        public int? MissedOut { get; set; }
        public int? LateIn { get; set; }
        public decimal? CompPercent { get; set; }
    }
}