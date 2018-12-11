namespace Aion.Models.WFM
{
    public class RegionDeploymentDashView
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public byte Period { get; set; }
        public int WeekNumber { get; set; }
        public decimal? PlanningAhead { get; set; }
        public decimal? Recruitment { get; set; }
        public decimal? Absence { get; set; }
        public decimal? GMWorking { get; set; }
        public int? BudgetFit { get; set; }
        public double? CustomerFit { get; set; }
        public string Year { get; set; }
    }
}