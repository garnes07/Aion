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
    
    public partial class RequestComment
    {
        public int EntryId { get; set; }
        public int RequestId { get; set; }
        public string CommentType { get; set; }
        public string Comment { get; set; }
        public System.DateTime EnteredOn { get; set; }
        public string EnteredBy { get; set; }
    
        public virtual VacancyRequest VacancyRequest { get; set; }
        public virtual vw_VacancyRequestsAdmin vw_VacancyRequestsAdmin { get; set; }
    }
}
