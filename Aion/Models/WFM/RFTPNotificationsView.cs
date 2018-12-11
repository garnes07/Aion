namespace Aion.Models.WFM
{
    public class RFTPNotificationsView
    {
        public short Region { get; set; }
        public int Confirms { get; set; }
        public int Outstanding { get; set; }
        public int SAOverdue { get; set; }
        public string Username { get; set; }
    }
}