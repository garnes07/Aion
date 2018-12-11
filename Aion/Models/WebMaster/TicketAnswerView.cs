namespace Aion.Models.WebMaster
{
    public class TicketAnswerView
    {
        public int EntryId { get; set; }
        public int TicketId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }

        public TicketTemplateView TicketTemplate { get; set; }
    }
}