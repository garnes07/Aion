﻿namespace Aion.Models.Vacancy
{
    public class RecruitmentDetailView
    {
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public short PositionId { get; set; }
        public string FriendlyName { get; set; }
        public short? SortOrder { get; set; }
        public decimal? Allowance { get; set; }
        public short ContractBase { get; set; }
        public decimal CurrentContract { get; set; }
        public decimal AverageContract { get; set; }
        public decimal OpenVacancies { get; set; }
        public decimal? RateOfPay { get; set; }
        public decimal? TotalBase { get; set; }
        public decimal? CurrentBase { get; set; }
    }
}