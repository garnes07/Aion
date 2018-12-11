using System;

namespace Aion.Models.WebMaster
{
    public class TicketStubRefView
    {
        public int TicketId { get; set; }
        public int GroupId { get; set; }
        public int TicketTypeId { get; set; }
        public string TPCUsername { get; set; }
        public string Title { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public string RaisedBy { get; set; }
        public byte EscalationLevel { get; set; }
        public short? BranchNumber { get; set; }
        public short Region { get; set; }
    }
}