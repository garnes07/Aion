namespace Aion.Areas.WFM.Models.Deployment
{
    public class DeploymentAggregate
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public double? SOHSpend { get; set; }
        public double? FinalTarget { get; set; }
        public double? DeploymentScore { get; set; }
    }
}