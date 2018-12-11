namespace Aion.Models.WFM
{
    public class CPW_Clocking_Repeat_StoresView
    {
        public long RowId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? WeekNumber { get; set; }
        public decimal? Compliance { get; set; }
    }
}