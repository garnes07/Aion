using System;

namespace Aion.Models.Vacancy
{
    public class SFOpenVacancyView
    {
        public int EntryId { get; set; }
        public int JobReqId { get; set; }
        public string PositionNumber { get; set; }
        public string Company { get; set; }
        public string BusinessUnit { get; set; }
        public short StoreNumber { get; set; }
        public short JobCode { get; set; }
        public string FriendlyName { get; set; }
        public DateTime? DateCreated { get; set; }
        public short TotalVacantHours { get; set; }
        public short Headcount { get; set; }
        public string Chain { get; set; }
    }
}