namespace Aion.Areas.WFM.Models.RFTP
{
    public class ClockAggregate
    {
        public int? WeekNumber { get; set; }
        public string Name { get ; set; }
        public string StoreName { get; set; }
        public string Division { get; set; }
        public int? empNum { get; set; }
        public int ShiftCount { get; set; }
        public int MissedIn { get; set; }
        public int MissedOut { get; set; }
        public int LateIn { get; set; }

        public decimal CompPercent => 100 - ((decimal)(MissedIn + MissedOut) / ShiftCount * 50);
    }
}