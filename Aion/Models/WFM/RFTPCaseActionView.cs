namespace Aion.Models.WFM
{
    public class RFTPCaseActionView
    {
        public int ActionID { get; set; }
        public byte Stage { get; set; }
        public string Owner { get; set; }
        public byte OwnerLevel { get; set; }
        public string ActionType { get; set; }
    }
}