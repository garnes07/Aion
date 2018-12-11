using System;

namespace Aion.Models.WFM
{
    public class KronosEmployeeSummaryView
    {
        public short? HomeBranch { get; set; }
        public short? Region { get; set; }
        public string Division { get; set; }
        public string Channel { get; set; }
        public string PersonNumber { get; set; }
        public decimal? EmployeeStandardHours { get; set; }
        public DateTime? SignedOffThrough { get; set; }
        public string JobRole { get; set; }
        public string PersonName { get; set; }
        public bool KronosUser { get; set; }
        public string BranchName { get; set; }
        public bool Scheduled { get; set; }
        public byte? ReportingRoleFlag { get; set; }
        public bool? Active { get; set; }
    }
}