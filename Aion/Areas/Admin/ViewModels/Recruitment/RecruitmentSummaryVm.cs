using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.Admin.ViewModels.Recruitment
{
    public class RecruitmentSummaryVm
    {
        //public List<vw_OfferApprovals> OfferApprovals { get; set; }
        public List<vw_IncorrectVacancies> IncorrectVacancies { get; set; }
        public List<vw_VacancyRequestsAdmin> AllPending { get; set; }

        private List<vw_VacancyRequestsAdmin> _ToReview;
        public List<vw_VacancyRequestsAdmin> ToReview => _ToReview ?? (_ToReview = AllPending.Where(x => !x.Approved).ToList());

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

        public int IncorrectCount => IncorrectVacancies.Count;
        public int ReviewCount => ToReview.Count;
        public int RaiseCount => ToRaise.Count;
        public int OfferCount => 0;//OfferApprovals.Count;

        public class ToRaiseStub
        {
            public string Chain { get; set; }
            public int Store { get; set; }
            public int JobCode { get; set; }
            public string FriendlyName { get; set; }
            public bool OnHold { get; set; }
        }
    }    
}