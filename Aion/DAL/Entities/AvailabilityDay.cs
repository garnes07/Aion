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
    
    public partial class AvailabilityDay
    {
        public int DayId { get; set; }
        public int PatternId { get; set; }
        public byte DayNum { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
    
        public virtual AvailabilityPattern AvailabilityPattern { get; set; }
        public virtual vw_AvailabilityPattern vw_AvailabilityPattern { get; set; }
    }
}