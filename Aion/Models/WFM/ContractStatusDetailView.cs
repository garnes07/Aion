namespace Aion.Models.WFM
{
    public class ContractStatusDetailView
    {
        public long RowId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public int? UnderBase { get; set; }
        public double? HoursUnderBase { get; set; }
        public int? VacancyUnderBase { get; set; }
        public double? VacancyHoursToRaise { get; set; }
    }
}