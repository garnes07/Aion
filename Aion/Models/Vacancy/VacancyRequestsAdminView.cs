using System;
using System.Collections.Generic;

namespace Aion.Models.Vacancy
{
    public class VacancyRequestsAdminView
    {
        public int EntryId { get; set; }
        public int? SFRefNo { get; set; }
        public short PositionCode { get; set; }
        public short Heads { get; set; }
        public short ContractHrs { get; set; }
        public bool Approved { get; set; }
        public bool Rejected { get; set; }
        public string RaisedBy { get; set; }
        public DateTime RaisedDate { get; set; }
        public string PostedBy { get; set; }
        public DateTime? PostedDate { get; set; }
        public bool Replace { get; set; }
        public bool Repost { get; set; }
        public bool Internal { get; set; }
        public bool? Emailed { get; set; }
        public short StoreNumber { get; set; }
        public bool Show { get; set; }
        public bool ToCancel { get; set; }
        public bool Cancelled { get; set; }
        public string Chain { get; set; }
        public string ContractType { get; set; }

        public ICollection<RequestCommentView> RequestComments { get; set; }
        public VacancyPositionView VacancyPosition { get; set; }
    }
}