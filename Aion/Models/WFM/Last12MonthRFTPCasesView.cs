using System;

namespace Aion.Models.WFM
{
    public class Last12MonthRFTPCasesView
    {
        public int CaseID { get; set; }
        public string PersonNumber { get; set; }
        public short StoreNumber { get; set; }
        public byte Stage { get; set; }
        public bool Override { get; set; }
        public bool Reassign { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool Confirmed { get; set; }
        public bool Completed { get; set; }
        public string Year { get; set; }
        public byte? Period { get; set; }
        public bool Show { get; set; }
    }
}