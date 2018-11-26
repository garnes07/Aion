namespace Aion.Models.WFM
{
    public class DailyDeploymentView
    {
        public int EntryID { get; set; }
        public int WeekNumber { get; set; }
        public short StoreNumber { get; set; }
        public decimal SundayReq { get; set; }
        public decimal MondayReq { get; set; }
        public decimal TuesdayReq { get; set; }
        public decimal WednesdayReq { get; set; }
        public decimal ThursdayReq { get; set; }
        public decimal FridayReq { get; set; }
        public decimal SaturdayReq { get; set; }
        public decimal SundayDeployed { get; set; }
        public decimal MondayDeployed { get; set; }
        public decimal TuesdayDeployed { get; set; }
        public decimal WednesdayDeployed { get; set; }
        public decimal ThursdayDeployed { get; set; }
        public decimal FridayDeployed { get; set; }
        public decimal SaturdayDeployed { get; set; }
        public System.DateTime DateTimeModified { get; set; }
        public decimal DeployedTotal { get; set; }
        public decimal SundayMove { get; set; }
        public decimal MondayMove { get; set; }
        public decimal TuesdayMove { get; set; }
        public decimal WednesdayMove { get; set; }
        public decimal ThursdayMove { get; set; }
        public decimal FridayMove { get; set; }
        public decimal SaturdayMove { get; set; }
        public short SundayCredit { get; set; }
        public short SaturdayCredit { get; set; }
    }
}