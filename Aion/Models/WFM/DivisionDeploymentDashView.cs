namespace Aion.Models.WFM
{
    public class DivisionDeploymentDashView
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public byte Period { get; set; }
        public int WeekNumber { get; set; }
        public int? PlanningAhead { get; set; }
        public int? Recruitment { get; set; }
        public int? Absence { get; set; }
        public int? GMWorking { get; set; }
        public int? BudgetFit { get; set; }
        public double? CustomerFit { get; set; }
    }
}