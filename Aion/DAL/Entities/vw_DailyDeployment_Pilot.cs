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
    
    public partial class vw_DailyDeployment_Pilot
    {
        public int EntryID { get; set; }
        public int WeekNumber { get; set; }
        public Nullable<short> StoreNumber { get; set; }
        public Nullable<double> SundayReq { get; set; }
        public Nullable<double> MondayReq { get; set; }
        public Nullable<double> TuesdayReq { get; set; }
        public Nullable<double> WednesdayReq { get; set; }
        public Nullable<double> ThursdayReq { get; set; }
        public Nullable<double> FridayReq { get; set; }
        public Nullable<double> SaturdayReq { get; set; }
        public decimal SundayDeployed { get; set; }
        public decimal MondayDeployed { get; set; }
        public decimal TuesdayDeployed { get; set; }
        public decimal WednesdayDeployed { get; set; }
        public decimal ThursdayDeployed { get; set; }
        public decimal FridayDeployed { get; set; }
        public decimal SaturdayDeployed { get; set; }
        public System.DateTime DateTimeModified { get; set; }
        public decimal DeployedTotal { get; set; }
        public Nullable<decimal> SundayMove { get; set; }
        public Nullable<decimal> MondayMove { get; set; }
        public Nullable<decimal> TuesdayMove { get; set; }
        public Nullable<decimal> WednesdayMove { get; set; }
        public Nullable<decimal> ThursdayMove { get; set; }
        public Nullable<decimal> FridayMove { get; set; }
        public Nullable<decimal> SaturdayMove { get; set; }
    }
}
