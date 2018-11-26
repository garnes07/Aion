using System;

namespace Aion.Models.WFM
{
    public class DashboardData_v2View
    {
        public int DashBoardDataId { get; set; }
        public string Year { get; set; }
        public byte? Quarter { get; set; }
        public byte? Period { get; set; }
        public int? WeekNumber { get; set; }
        public short? StoreNumber { get; set; }
        public string UkStoreNumber { get; set; }
        public string StoreName { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public string RegionName { get; set; }
        public short? TotalHeadCount { get; set; }
        public short? TimeCardsCompleted { get; set; }
        public short? ZeroHour { get; set; }
        public double? ContractHours { get; set; }
        public double? FTContractHours { get; set; }
        public double? PTContractHours { get; set; }
        public double? OriginalTarget { get; set; }
        public double? FinalTarget { get; set; }
        public double? HolidayTaken { get; set; }
        public double? FTLeakage { get; set; }
        public short? LeakageCount { get; set; }
        public double? AllApprovedHours { get; set; }
        public double? SOH { get; set; }
        public bool? OverTargetFlag { get; set; }
        public bool? OverContractFlag { get; set; }
        public short? ComplianceScore { get; set; }
        public short? SOHUtilization { get; set; }
        public bool? UtilizationZeroFlag { get; set; }
        public double? GSContract { get; set; }
        public double? GSTarget { get; set; }
        public double? GSHolidayTaken { get; set; }
        public double? GSFTLeakage { get; set; }
        public double? GSAllApprovedHours { get; set; }
        public double? GSSOHSpend { get; set; }
        public bool? GSOverTargetFlag { get; set; }
        public bool? GSOverContractFlag { get; set; }
        public string StoreFlag { get; set; }
        public double? ContractBaseTarget { get; set; }
        public double? IgniteCredits { get; set; }
        public double? PunchCompliance { get; set; }
        public short? ShortShifts { get; set; }
        public double? PayEscalations { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}