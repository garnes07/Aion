namespace Aion.Models.WFM
{
    public class PayCalendarDateView
    {
        public string Chain { get; set; }
        public string Year { get; set; }
        public string Period { get; set; }
        public int Week { get; set; }
        public System.DateTime WCDate { get; set; }
    }
}