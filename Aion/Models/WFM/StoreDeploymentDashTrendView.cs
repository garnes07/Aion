namespace Aion.Models.WFM
{
    public class StoreDeploymentDashTrendView
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? PlanningAhead { get; set; }
        public int? Recruitment { get; set; }
        public int? Absence { get; set; }
        public int? GMWorking { get; set; }
        public int? WeekCount { get; set; }
    }
}