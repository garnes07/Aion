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
    
    public partial class vw_OpenVacancySummary
    {
        public string Chain { get; set; }
        public short StoreNumber { get; set; }
        public short JobCode { get; set; }
        public string FriendlyName { get; set; }
        public short Remaining { get; set; }
        public int Onboarding { get; set; }
        public int Offer { get; set; }
        public int Started { get; set; }
        public long RowId { get; set; }
    }
}
