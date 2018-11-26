namespace Aion.Models.WFM
{
    public class PeakDataView
    {
        public int StoreNumber { get; set; }
        public int WeekNumber { get; set; }
        public double? Budget { get; set; }
        public double? Contract { get; set; }
        public double? Holiday { get; set; }
        public byte? Temps { get; set; }
        public byte? TempsOption { get; set; }
        public short? ContractBase { get; set; }
    }
}