using System;

namespace Aion.Areas.Admin.Models
{
    public class RecruitmentDetail
    {
        public Nullable<short> StoreNumber { get; set; }
        public string StoreName { get; set; }
        public short PositionId { get; set; }
        public string FriendlyName { get; set; }
        public Nullable<short> SortOrder { get; set; }
        public Nullable<decimal> Allowance { get; set; }
        public short ContractBase { get; set; }
        public decimal CurrentContract { get; set; }
        public decimal AverageContract { get; set; }
        public decimal OpenVacancies { get; set; }
        public Nullable<decimal> RateOfPay { get; set; }
        public Nullable<decimal> TotalBase { get; set; }
        public Nullable<decimal> CurrentBase { get; set; }
    }
}