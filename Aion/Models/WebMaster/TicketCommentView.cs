namespace Aion.Models.WebMaster
{
    public class TicketCommentView
    {
        public int CommentId { get; set; }
        public int TicketId { get; set; }
        public string User { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string Comment { get; set; }
    }
}