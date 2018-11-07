using Aion.Models.Vacancy;
using Aion.Models.WFM;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.Admin.ViewModels.Recruitment
{
    public class ReviewPendingVm
    {
        public List<VacancyRequestsAdminView> VacancyRequests { get; set; }
        public List<RecruitmentDetailView> RecruitmentDetail { get; set; }
        public List<WFMEmployeeInfoView> HRCurrent { get; set; }
        public List<WFMFutureDatedView> HRChanges { get; set; }
        public List<OpenVacancySummaryView> OpenVacancies { get; set; }

        private RecruitmentDetailView _PositionDetail;
        public RecruitmentDetailView PositionDetail => _PositionDetail ?? (_PositionDetail = RecruitmentDetail.Where(x => x.PositionId == VacancyRequests.First().PositionCode).FirstOrDefault());
        public List<string> UniqueComments
        {
            get
            {
                var toReturn = new List<RequestCommentView>();

                foreach(var request in VacancyRequests)
                {
                    if(request.RequestComments.Where(x => x.CommentType == "Notes").Count() > 0)
                    {
                        toReturn.AddRange(request.RequestComments.Where(x => x.CommentType == "Notes"));
                    }                    
                }
                return toReturn.GroupBy(x => x.Comment).Select(x => x.Key).ToList();
            }
        }

        private List<RequestCommentView> _HOComments;
        public List<RequestCommentView> HOComments
        {
            get
            {                
                if (_HOComments == null)
                {
                    var toReturn = new List<RequestCommentView>();
                    foreach (var request in VacancyRequests)
                    {
                        if(request.RequestComments.Where(x => x.CommentType != "Notes").Count() > 0)
                        {
                            toReturn.AddRange(request.RequestComments.Where(x => x.CommentType != "Notes"));
                        }
                    }
                    _HOComments = toReturn.GroupBy(x => x.Comment).Select(x => new RequestCommentView { CommentType = x.Min(y => y.CommentType), Comment = x.Key, EnteredOn = x.Min(y => y.EnteredOn), EnteredBy = x.Min(y => y.EnteredBy) }).ToList();
                }               

                return _HOComments;
            }
        }
    }
}