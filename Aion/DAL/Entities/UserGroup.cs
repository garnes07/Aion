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
    
    public partial class UserGroup
    {
        public int EntryId { get; set; }
        public int GroupId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FriendlyName { get; set; }
        public string CoverEmail { get; set; }
        public Nullable<System.DateTime> CoverEndDate { get; set; }
    
        public virtual Group Group { get; set; }
    }
}
