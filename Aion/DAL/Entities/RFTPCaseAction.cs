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
    
    public partial class RFTPCaseAction
    {
        public int ActionID { get; set; }
        public byte Stage { get; set; }
        public string Owner { get; set; }
        public byte OwnerLevel { get; set; }
        public string ActionType { get; set; }
    }
}
