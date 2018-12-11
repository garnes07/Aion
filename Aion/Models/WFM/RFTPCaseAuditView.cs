namespace Aion.Models.WFM
{
    public class RFTPCaseAuditView
    {
        public int ActionID { get; set; }
        public int CaseID { get; set; }
        public string ActionType { get; set; }
        public string Comment { get; set; }
        public string CompletedBy { get; set; }
        public System.DateTime DateTimeCreated { get; set; }
    }
}