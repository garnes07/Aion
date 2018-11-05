using Aion.Models.Vacancy;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.Admin.ViewModels.Recruitment
{
    public class RecruitmentSummaryVm
    {
        public List<OfferApprovalsView> OfferApprovals { get; set; }
        public List<IncorrectVacanciesView> IncorrectVacancies { get; set; }
        public List<VacancyRequestsAdminView> AllPending { get; set; }

        private List<VacancyRequestsAdminView> _ToReview;
        public List<VacancyRequestsAdminView> ToReview => _ToReview ?? (_ToReview = AllPending.Where(x => !x.Approved).ToList());

        private List<ToRaiseStub> _ToRaise;
        public List<ToRaiseStub> ToRaise => _ToRaise ?? (_ToRaise = 
            AllPending
            .Where(x => x.Approved)
            .GroupBy(x => new { x.Chain, x.StoreNumber, x.PositionCode, x.VacancyPosition.FriendlyName, x.PostedBy })
            .Select(x => new ToRaiseStub
            {
                Chain = x.Key.Chain,
                Store = x.Key.StoreNumber,
                JobCode = x.Key.PositionCode,
                FriendlyName = x.Key.FriendlyName,
                OnHold = x.Key.PostedBy == "On Hold"
            })
            .ToList());

        private List<OfferStub> _ToOffer;
        public List<OfferStub> ToOffer => _ToOffer ?? (_ToOffer =
            OfferApprovals
            .GroupBy(x => new { x.Job_Req_Id, x.Company, x.Store_Number, x.Job_Role, x.ReviewedBy })
            .Select(x => new OfferStub
            {
                JobReqId = x.Key.Job_Req_Id,
                Chain = x.Key.Company,
                Store = x.Key.Store_Number,
                FriendlyName = x.Key.Job_Role,
                OnHold = x.Key.ReviewedBy == "On Hold"
            })
            .ToList());

        public int IncorrectCount => IncorrectVacancies.Count;
        public int ReviewCount => ToReview.Count;
        public int RaiseCount => ToRaise.Count;
        public int OfferCount => ToOffer.Count;

        public class ToRaiseStub
        {
            public string Chain { get; set; }
            public int Store { get; set; }
            public int JobCode { get; set; }
            public string FriendlyName { get; set; }
            public bool OnHold { get; set; }
        }

        public class OfferStub
        {
            public int JobReqId { get; set; }
            public string Chain { get; set; }
            public short? Store { get; set; }
            public string FriendlyName { get; set; }
            public bool OnHold { get; set; }
        }

    }    
}