namespace Aion.Models.WFM
{
    public class StoreDeploymentRankView
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public string Year { get; set; }
        public byte? Period { get; set; }
        public int WeekNumber { get; set; }
        public int BudgetFit { get; set; }
        public double? CustomerFit { get; set; }
        public long? Rank { get; set; }
    }
}