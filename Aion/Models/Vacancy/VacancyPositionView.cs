namespace Aion.Models.Vacancy
{
    public class VacancyPositionView
    {
        public short PositionId { get; set; }
        public string FriendlyName { get; set; }
        public string SFName { get; set; }
        public short? SortOrder { get; set; }
        public decimal? RateOfPay { get; set; }
        public short? SFPositionId { get; set; }
        public string Chain { get; set; }
    }
}