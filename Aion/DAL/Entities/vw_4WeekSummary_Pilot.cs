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
    using System.Collections.Generic;
    
    public partial class vw_4WeekSummary_Pilot
    {
        public long RowId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public Nullable<short> Region { get; set; }
        public Nullable<short> StoreNumber { get; set; }
        public Nullable<int> WeekNumber { get; set; }
        public Nullable<int> NonCompliant { get; set; }
        public double Deployment { get; set; }
    }
}
