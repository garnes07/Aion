namespace Aion.Models.WFM
{
    public class AvailabilityDayView
    {
        public int DayId { get; set; }
        public int PatternId { get; set; }
        public byte DayNum { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
    }
}