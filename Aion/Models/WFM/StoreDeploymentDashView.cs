namespace Aion.Models.WFM
{
    public class StoreDeploymentDashView
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public string Year { get; set; }
        public byte? Period { get; set; }
        public int WeekNumber { get; set; }
        public string PlanningAhead { get; set; }
        public string Recruitment { get; set; }
        public string Absence { get; set; }
        public string GMWorking { get; set; }
    }
}