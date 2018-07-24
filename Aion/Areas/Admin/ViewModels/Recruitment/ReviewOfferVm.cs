using Aion.Areas.Admin.Models;
using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.Admin.ViewModels.Recruitment
{
    public class ReviewOfferVm
    {
        public List<vw_OfferApprovals> OfferToReview { get; set; }
        public vw_SFOpenVacancies OpenVacancy { get; set; }
        public List<RecruitmentDetail> RecruitmentDetail { get; set; }
        public List<WFM_EMPLOYEE_INFO_UNEDITED> HRCurrent { get; set; }
        public List<WFM_FUTURE_DATED> HRChanges { get; set; }

        private RecruitmentDetail _PositionDetail;
        public RecruitmentDetail PositionDetail => _PositionDetail ?? (_PositionDetail = RecruitmentDetail.FirstOrDefault(x => x.PositionId == OpenVacancy.JobCode));

        private List<OfferComment> _HOComments;
        public List<OfferComment> HOComments
        {
            get
            {
                if (_HOComments == null)
                {
                    var toReturn = new List<OfferComment>();
                    foreach (var request in OfferToReview)
                    {
                        if (request.OfferComments.Where(x => x.CommentType != "Notes").Count() > 0)
                        {
                            toReturn.AddRange(request.OfferComments.Where(x => x.CommentType != "Notes"));
                        }
                    }
                    _HOComments = toReturn.GroupBy(x => x.Comment).Select(x => new OfferComment { CommentType = x.Min(y => y.CommentType), Comment = x.Key, EnteredOn = x.Min(y => y.EnteredOn), EnteredBy = x.Min(y => y.EnteredBy) }).ToList();
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