using System;

namespace Aion.Models.WFM
{
    public class ScheduleDataView
    {
        public int EntryId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? PersonNumber { get; set; }
        public decimal? ContractHours { get; set; }
        public string FullName { get; set; }
        public string ShiftType { get; set; }
        public string TransferBranch { get; set; }
        public int? WeekNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public byte? DayNum { get; set; }
        public decimal? PersonWeekTotal { get; set; }
        public decimal? DayWeekTotal { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public short? JobCode { get; set; }
    }
}