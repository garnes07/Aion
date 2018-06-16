namespace Aion.Areas.WFM.Models.Deployment
{
    public class RecruitmentRequest
    {
        public bool Repost { get; set; }
        public string Action { get; set; }
        public int Position { get; set; }
        public int Heads { get; set; }
        public int ContractHours { get; set; }
        public string Approval { get; set; }
    }
}