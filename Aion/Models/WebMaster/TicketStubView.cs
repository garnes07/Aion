using System;
using System.Collections.Generic;

namespace Aion.Models.WebMaster
{
    public class TicketStubView
    {
        public TicketStubView()
        {
            this.TicketAnswers = new List<TicketAnswerView>();
            this.TicketAudits = new List<TicketAuditView>();
            this.TicketComments = new List<TicketCommentView>();
        }

        public int TicketId { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string Status { get; set; }
        public int TicketTypeId { get; set; }
        public DateTime LastUpdate { get; set; }
        public string RaisedBy { get; set; }
        public byte EscalationLevel { get; set; }
        public short? BranchNumber { get; set; }
        public string LastUpdatedBy { get; set; }
        public int? Exception { get; set; }
        
        public virtual ICollection<TicketAnswerView> TicketAnswers { get; set; }
        public virtual ICollection<TicketAuditView> TicketAudits { get; set; }
        public virtual ICollection<TicketCommentView> TicketComments { get; set; }
        public TicketTypeView TicketType { get; set; }
    }
}