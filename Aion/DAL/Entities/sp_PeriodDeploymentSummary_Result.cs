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
    
    public partial class sp_PeriodDeploymentSummary_Result
    {
        public long RowId { get; set; }
        public string Year { get; set; }
        public Nullable<byte> Period { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public Nullable<short> Region { get; set; }
        public Nullable<short> StoreNumber { get; set; }
        public string StoreName { get; set; }
        public Nullable<double> SOHSpend { get; set; }
        public Nullable<double> TKHSpend { get; set; }
        public Nullable<double> SOHBudget { get; set; }
        public Nullable<double> TKHBudget { get; set; }
        public Nullable<double> Variance { get; set; }
        public Nullable<double> ToBudget { get; set; }
    }
}