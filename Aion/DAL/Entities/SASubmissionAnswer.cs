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
    
    public partial class SASubmissionAnswer
    {
        public int EntryId { get; set; }
        public int SubmissionId { get; set; }
        public int QuestionId { get; set; }
    
        public virtual SAQuestion SAQuestion { get; set; }
        public virtual SASubmission SASubmission { get; set; }
    }
}
