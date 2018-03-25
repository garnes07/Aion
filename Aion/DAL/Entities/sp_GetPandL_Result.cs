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
    
    public partial class sp_GetPandL_Result
    {
        public long AccountEntryHeaderId { get; set; }
        public string AccountEntryHeaderText { get; set; }
        public string PeriodYear { get; set; }
        public Nullable<short> PeriodMonth { get; set; }
        public Nullable<int> StoreNumber { get; set; }
        public string ManagerName { get; set; }
        public long AccountEntryDetailId { get; set; }
        public short AccountEntryTypeId { get; set; }
        public Nullable<int> AccountEntrySubTypeId { get; set; }
        public string AccountEntrySubTypeText { get; set; }
        public string DetailTitle { get; set; }
        public string DetailText { get; set; }
        public Nullable<decimal> ActualAmount { get; set; }
        public Nullable<decimal> BudgetAmount { get; set; }
        public Nullable<decimal> QtdActualAmount { get; set; }
        public Nullable<decimal> QtdBudgetAmount { get; set; }
        public Nullable<decimal> YtdActualAmount { get; set; }
        public Nullable<decimal> YtdBudgetAmount { get; set; }
        public Nullable<long> AccountEntryDetailBreakDownId { get; set; }
        public string BreakdownTitle { get; set; }
        public string BreakdownText { get; set; }
        public Nullable<decimal> BreakdownActualAmount { get; set; }
        public Nullable<decimal> BreakdownBudgetAmount { get; set; }
    }
}
