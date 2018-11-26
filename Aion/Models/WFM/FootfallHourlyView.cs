namespace Aion.Models.WFM
{
    public class FootfallHourlyView
    {
        public string Key { get; set; }
        public string Year { get; set; }
        public short? WeekNumber { get; set; }
        public string Day { get; set; }
        public short? Hour_In_Day_24 { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public short? Footfall_Volume { get; set; }
    }
}