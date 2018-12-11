namespace Aion.Models.WFM
{
    public class OpeningTimesCommentView
    {
        public int CommentID { get; set; }
        public int EntryID { get; set; }
        public string Comment { get; set; }
        public string AddedBy { get; set; }
        public System.DateTime Datetime { get; set; }
    }
}