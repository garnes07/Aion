using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aion.DAL.Entities;
using Aion.Areas.Admin.Models;

namespace Aion.Areas.Admin.ViewModels.Recruitment
{
    public class ReviewPendingVm
    {
        public List<VacancyRequest> VacancyRequests { get; set; }
        public List<RecruitmentDetail> RecruitmentDetail { get; set; }

        private RecruitmentDetail _PositionDetail;
        public RecruitmentDetail PositionDetail => _PositionDetail ?? (_PositionDetail = RecruitmentDetail.Where(x => x.PositionId == VacancyRequests.First().PositionCode).FirstOrDefault());
        public List<string> UniqueComments
        {
            get
            {
                var toReturn = new List<RequestComment>();

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

        private List<RequestComment> _HOComments;
        public List<RequestComment> HOComments
        {
            get
            {                
                if (_HOComments == null)
                {
                    var toReturn = new List<RequestComment>();
                    foreach (var request in VacancyRequests)
                    {
                        if(request.RequestComments.Where(x => x.CommentType != "Notes").Count() > 0)
                        {
                            toReturn.AddRange(request.RequestComments.Where(x => x.CommentType != "Notes"));
                        }
                    }
                    _HOComments = toReturn.GroupBy(x => x.Comment).Select(x => new RequestComment { CommentType = x.Min(y => y.CommentType), Comment = x.Key, EnteredOn = x.Min(y => y.EnteredOn), EnteredBy = x.Min(y => y.EnteredBy) }).ToList();
                }               

                return _HOComments;
            }
        }
    }
}