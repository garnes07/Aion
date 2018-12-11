namespace Aion.Models.WebMaster
{
    public class TicketAuditView
    {
        public int EntryId { get; set; }
        public int TicketId { get; set; }
        public string Action { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string User { get; set; }
    }
}