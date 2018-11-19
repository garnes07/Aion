namespace Aion.Models.WebMaster
{
    public class TicketTypeView
    {
        public int TicketTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte SLAPeriod { get; set; }
        public short EscalationId { get; set; }
        public bool? Live { get; set; }
    }
}