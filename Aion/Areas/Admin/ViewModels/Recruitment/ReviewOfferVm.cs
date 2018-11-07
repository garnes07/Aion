//using Aion.DAL.Entities;
using Aion.Models.Vacancy;
using Aion.Models.WFM;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.Admin.ViewModels.Recruitment
{
    public class ReviewOfferVm
    {
        public List<OfferApprovalsView> OfferToReview { get; set; }
        public SFOpenVacancyView OpenVacancy { get; set; }
        public List<RecruitmentDetailView> RecruitmentDetail { get; set; }
        public List<WFMEmployeeInfoView> HRCurrent { get; set; }
        public List<WFMFutureDatedView> HRChanges { get; set; }
        public List<OpenVacancySummaryView> PeakVacancySummary { get; set; }

        private RecruitmentDetailView _PositionDetail;
        public RecruitmentDetailView PositionDetail => _PositionDetail ?? (_PositionDetail = RecruitmentDetail.FirstOrDefault(x => x.PositionId == OfferToReview.First().Job_Code));

        private List<OfferCommentView> _HOComments;
        public List<OfferCommentView> HOComments
        {
            get
            {
                if (_HOComments == null)
                {
                    var toReturn = new List<OfferCommentView>();
                    foreach (var request in OfferToReview)
                    {
                        if (request.OfferComments.Where(x => x.CommentType != "Notes").Count() > 0)
                        {
                            toReturn.AddRange(request.OfferComments.Where(x => x.CommentType != "Notes"));
                        }
                    }
                    _HOComments = toReturn.GroupBy(x => x.Comment).Select(x => new OfferCommentView { CommentType = x.Min(y => y.CommentType), Comment = x.Key, EnteredOn = x.Min(y => y.EnteredOn), EnteredBy = x.Min(y => y.EnteredBy) }).ToList();
                }

                return _HOComments;
            }
        }

        public decimal? NewAvgContract
        {
            get
            {
                var allContract = HRCurrent.Where(x => x.JobCode == OfferToReview.First().Job_Code).Select(x => x.Std_Hrs_Wk).ToList();
                allContract.AddRange(OfferToReview.Select(x => x.Contracted_Hours).ToList());

                return allContract.Average(x => x);
            }
        }
    }
}