//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aion.DAL.Entities
{
    using System;
    
    public partial class sp_ComplianceSummary_Result
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public Nullable<short> Region { get; set; }
        public string Year { get; set; }
        public Nullable<byte> Period { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public Nullable<int> StoreCount { get; set; }
        public Nullable<int> Compliant { get; set; }
        public Nullable<int> TCComp { get; set; }
        public Nullable<int> TimecardsMissing { get; set; }
        public Nullable<int> TotalHC { get; set; }
        public Nullable<int> ZeroHour { get; set; }
        public Nullable<int> PunchCompliance { get; set; }
        public Nullable<int> ShortShiftCompliance { get; set; }
        public Nullable<int> ShortShifts { get; set; }
    }
}
