namespace Aion.Models.WebMaster
{
    public class TicketTemplateView
    {
        public int QuestionId { get; set; }
        public int TicketTypeId { get; set; }
        public string Question { get; set; }
        public string ReturnType { get; set; }
        public byte? GrpLimit { get; set; }
        public string apiIdent { get; set; }
    }
}