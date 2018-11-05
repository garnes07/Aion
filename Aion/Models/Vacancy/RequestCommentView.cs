namespace Aion.Models.Vacancy
{
    public class RequestCommentView
    {
        public int EntryId { get; set; }
        public int RequestId { get; set; }
        public string CommentType { get; set; }
        public string Comment { get; set; }
        public System.DateTime EnteredOn { get; set; }
        public string EnteredBy { get; set; }
    }
}