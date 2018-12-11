namespace Aion.Models.WFM
{
    public class AvailabilityCompletionRateView
    {
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? Headcount { get; set; }
        public int? AvlbltyUpToDate { get; set; }
    }
}