using System;

namespace Aion.Areas.WFM.Models.RFTP
{
    public class CompSummaryView
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public string Year { get; set; }
        public byte? Period { get; set; }
        public int? WeekNumber { get; set; }
        public int? StoreCount { get; set; }
        public int? Compliant { get; set; }
        public int? TCComp { get; set; }
        public int? TimecardsMissing { get; set; }
        public int? TotalHC { get; set; }
        public int? ZeroHour { get; set; }
        public int? PunchCompliance { get; set; }
        public int? ShortShiftCompliance { get; set; }
        public int? ShortShifts { get; set; }

        public int lvl => Region != null ? 1 : (Division != null ? 2 : 3);

        public string fullName => string.Format("{0} - {1}", StoreCount, Chain);

        public int CompPerc => (int)(Math.Round((double)Compliant / (double)StoreCount * 100, 0));

        public int ClockPerc => (int)(Math.Round((double)PunchCompliance / (double)StoreCount * 100, 0));

        public int TCPerc => (int)(Math.Round((double)TCComp / (double)StoreCount * 100, 0));

        public int SSPerc => (int)(Math.Round((double)ShortShiftCompliance / (double)StoreCount * 100, 0));

        public int wk => WeekNumber ?? 999999;
    }
}