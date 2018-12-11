namespace Aion.Models.WFM
{
    public class HolidayPlanningEmpView
    {
        public int EntryId { get; set; }
        public int StoreNumber { get; set; }
        public string PersonNum { get; set; }
        public string EmployeeName { get; set; }
        public int? Balance { get; set; }
        public int? Taken { get; set; }
        public int? Scheduled { get; set; }
        public int? YearStart { get; set; }
    }
}