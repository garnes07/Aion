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
    
    public partial class vw_RegionDeploymentDash
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public byte Period { get; set; }
        public int WeekNumber { get; set; }
        public Nullable<decimal> PlanningAhead { get; set; }
        public Nullable<decimal> Recruitment { get; set; }
        public Nullable<decimal> Absence { get; set; }
        public Nullable<decimal> GMWorking { get; set; }
        public Nullable<int> BudgetFit { get; set; }
        public Nullable<double> CustomerFit { get; set; }
        public string Year { get; set; }
    }
}
