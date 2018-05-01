using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.Admin.ViewModels.Recruitment
{
    public class RecruitmentSummaryVm
    {
        public List<vw_OfferApprovals> OfferApprovals { get; set; }
        public List<vw_IncorrectVacancies> IncorrectVacancies { get; set; }
        public List<VacancyRequest> AllPending { get; set; }

        private List<VacancyRequest> _ToReview;
        public List<VacancyRequest> ToReview => _ToReview ?? (_ToReview = AllPending.Where(x => !x.Approved).ToList());

        private List<VacancyRequest> _ToRaise;
        public List<VacancyRequest> ToRaise => _ToRaise ?? (_ToRaise = AllPending.Where(x => x.Approved).ToList());

        public int IncorrectCount => IncorrectVacancies.Count;
        public int ReviewCount => ToReview.Count;
        public int RaiseCount => ToRaise.Count;
        public int OfferCount => OfferApprovals.Count;
    }
}