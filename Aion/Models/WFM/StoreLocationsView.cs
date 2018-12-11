namespace Aion.Models.WFM
{
    public class StoreLocationsView
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        public int MinervaStore { get; set; }
        public int MinervaTransferStore { get; set; }
        public short? Contract_Base { get; set; }
        public double? Contract_Hours { get; set; }
        public byte? StoreBand { get; set; }
        public decimal? TotalTradeTransfer { get; set; }
        public string TradeTransfer { get; set; }
    }
}