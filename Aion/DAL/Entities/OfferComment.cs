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
    
    public partial class OfferComment
    {
        public int EntryId { get; set; }
        public int ApplicationID { get; set; }
        public string CommentType { get; set; }
        public string Comment { get; set; }
        public System.DateTime EnteredOn { get; set; }
        public string EnteredBy { get; set; }
    
        public virtual OfferApproval OfferApproval { get; set; }
        public virtual vw_OfferApprovals vw_OfferApprovals { get; set; }
    }
}
