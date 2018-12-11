namespace Aion.Models.WFM
{
    public class StoreContractStatusView
    {
        public long RowId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public double? ContractHours { get; set; }
        public double? ContractBase { get; set; }
        public double? VacancyHours { get; set; }
    }
}