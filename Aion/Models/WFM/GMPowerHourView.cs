using System;

namespace Aion.Models.WFM
{
    public class GMPowerHourView
    {
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int Week { get; set; }
        public short Days { get; set; }
        public string WeekType { get; set; }
        public TimeSpan Hour { get; set; }
        public float Trade { get; set; }
        public short Rank { get; set; }
        public int Working { get; set; }
        public int Holiday { get; set; }
    }
}