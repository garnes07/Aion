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
    
    public partial class WeekRef
    {
        public System.DateTime Date { get; set; }
        public Nullable<byte> Week { get; set; }
        public Nullable<byte> Period { get; set; }
        public string FY { get; set; }
        public Nullable<byte> Quarter { get; set; }
        public string DateString { get; set; }
        public Nullable<int> YearWeek { get; set; }
    }
}
