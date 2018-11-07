namespace Aion.Models.Vacancy
{
    public class OfferCommentView
    {
        public int EntryId { get; set; }
        public int ApplicationID { get; set; }
        public string CommentType { get; set; }
        public string Comment { get; set; }
        public System.DateTime EnteredOn { get; set; }
        public string EnteredBy { get; set; }
    }
}