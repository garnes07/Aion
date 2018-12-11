using Aion.Areas.Admin.Models;
using Aion.Areas.Admin.ViewModels.Recruitment;
using Aion.Areas.WFM.Models.Deployment;
using Aion.Areas.WFM.ViewModels.Deployment;
using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.Managers;
using Aion.Models.Vacancy;
using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.Admin.Controllers
{
    [UserFilter(MinLevel = 9)]
    public class RecruitmentController : BaseController
    {
        private readonly IVacancyManager _vacancyManager;

        public RecruitmentController()
        {
            _vacancyManager = new VacancyManager();
        }

        public async Task<ActionResult> Index()
        {
            RecruitmentSummaryVm vm = new RecruitmentSummaryVm();

            vm.IncorrectVacancies = mapper.Map<List<IncorrectVacanciesView>>(await _vacancyManager.GetIncorrectVacancies());
            vm.OfferApprovals = mapper.Map<List<OfferApprovalsView>>(await _vacancyManager.GetOfferApprovals());
            vm.AllPending = mapper.Map<List<VacancyRequestsAdminView>>(await _vacancyManager.GetPendingForAdmin());

            return View(vm);
        }

        public async Task<ActionResult> ReviewPending(string Chain, int StoreNumber, int PositionCode)
        {
            ReviewPendingVm vm = new ReviewPendingVm();

            vm.VacancyRequests = mapper.Map<List<VacancyRequestsAdminView>>(await _vacancyManager.GetPendingForAdmin(Chain, StoreNumber, PositionCode));
            vm.RecruitmentDetail = Chain == "CPW" ? 
                mapper.Map<List<RecruitmentDetailView>>(await _vacancyManager.GetVacancyDetailCPW(StoreNumber.ToString())) : 
                mapper.Map<List<RecruitmentDetailView>>(await _vacancyManager.GetVacancyDetailDXNS(StoreNumber.ToString()));
            vm.HRCurrent = mapper.Map<List<WFMEmployeeInfoView>>(await _vacancyManager.GetHrCurrent(Chain, StoreNumber));
            vm.HRChanges = mapper.Map<List<WFMFutureDatedView>>(await _vacancyManager.GetHrChanges(Chain, StoreNumber));
            vm.OpenVacancies = mapper.Map<List<OpenVacancySummaryView>>(await _vacancyManager.GetOpenVacancySummary(Chain, StoreNumber, PositionCode));
            
            System.Web.HttpContext.Current.Session["RefIds"] = vm.VacancyRequests.Select(x => x.EntryId).ToArray();

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> ReviewPending(List<ReviewOutcome> r)
        {
            var result = await _vacancyManager.AddReviewOutcome(r.Where(x => x.ApprovalStatus != null).ToList(), User.Identity.Name);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> HoldPending(string Chain, int StoreNumber, int PositionCode)
        {
            var result = await _vacancyManager.ReviewOnHold(Chain, StoreNumber, PositionCode);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<PartialViewResult> _SearchHistoric(int storenumber)
        {
            return PartialView("~/Areas/Admin/Views/Recruitment/Partials/_SearchResult.cshtml", mapper.Map<List<VacancyRequestsAdminView>>(await _vacancyManager.GetHistoricVacancies(storenumber)));
        }

        public async Task<bool> _MarkIncorrectDone(int jobReqId)
        {
            var result = await _vacancyManager.MarkIncorrectDone(jobReqId, User.Identity.Name);

            return result;
        }

        [HttpPost]
        public async Task<PartialViewResult> _PostNewComment(string commentText)
        {
            var RefIds = (int[])System.Web.HttpContext.Current.Session["RefIds"];
            var result = mapper.Map<RequestCommentView>(await _vacancyManager.AddNewComment(RefIds, User.Identity.Name, commentText, "HeadOffice"));

            return PartialView("~/Areas/Admin/Views/Recruitment/Partials/_NewComment.cshtml", result);
        }

        public async Task<PartialViewResult> _GetToPost(string chain, int store, int jobcode)
        {
            ToPostVm vm = new ToPostVm();
            vm.RequestDetail = mapper.Map<List<VacancyRequestsAdminView>>(await _vacancyManager.GetToPostForAdmin(chain, store, jobcode));
            vm.OpenVacancys = mapper.Map<List<OpenVacancySummaryView>>(await _vacancyManager.GetOpenVacancySummary(chain, store, jobcode));

            return PartialView("~/Areas/Admin/Views/Recruitment/Partials/_ToPost.cshtml", vm);
        }

        [HttpPost]
        public async Task<bool> _UpdateToPost(string chain, int store, int jobcode, int SFRef, string contract)
        {
            var result = await _vacancyManager.MarkAsPosted(chain, store, jobcode, SFRef, contract, User.Identity.Name);
            return result;
        }

        [HttpPost]
        public async Task<bool> _HoldToPost(string chain, int store, int jobcode)
        {
            var result = await _vacancyManager.HoldToPost(chain, store, jobcode);
            return result;
        }        

        [HttpPost]
        public async Task<ActionResult> UnapproveToPost(string chain, int store, int jobcode)
        {
            var result = await _vacancyManager.UnapproveToPost(chain, store, jobcode);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ReviewOffer(int JobReqId)
        {
            ReviewOfferVm vm = new ReviewOfferVm();

            vm.OfferToReview = mapper.Map<List<OfferApprovalsView>>(await _vacancyManager.GetOfferToReview(JobReqId));
            vm.RecruitmentDetail = vm.OfferToReview.First().Company == "CPW" ?
                mapper.Map<List<RecruitmentDetailView>>(await _vacancyManager.GetVacancyDetailCPW(vm.OfferToReview.First().Store_Number.ToString())) :
                mapper.Map<List<RecruitmentDetailView>>(await _vacancyManager.GetVacancyDetailDXNS(vm.OfferToReview.First().Store_Number.ToString()));
            vm.HRCurrent = mapper.Map<List<WFMEmployeeInfoView>>(await _vacancyManager.GetHrCurrent(vm.OfferToReview.First().Company, (int)vm.OfferToReview.First().Store_Number));
            vm.HRChanges = mapper.Map<List<WFMFutureDatedView>>(await _vacancyManager.GetHrChanges(vm.OfferToReview.First().Company, (int)vm.OfferToReview.First().Store_Number));
            vm.OpenVacancy = mapper.Map<SFOpenVacancyView>(await _vacancyManager.GetOpenVacancyByRef(JobReqId));
            if(vm.OfferToReview.First().Job_Code == 1434)
            {
                vm.PeakVacancySummary = mapper.Map<List<OpenVacancySummaryView>>(await _vacancyManager.GetOpenPeakVacancySummary(vm.OfferToReview.First().Company, (int)vm.OfferToReview.First().Store_Number));
            }

            System.Web.HttpContext.Current.Session["RefIds"] = vm.OfferToReview.Select(x => x.Application_ID).ToArray();

            return View(vm);
        }

        [HttpPost]
        public async Task<PartialViewResult> _PostNewOfferComment(string commentText)
        {
            var RefIds = (int[])System.Web.HttpContext.Current.Session["RefIds"];
            var result = mapper.Map<OfferCommentView>(await _vacancyManager.AddNewOfferComment(RefIds, User.Identity.Name, commentText, "HeadOffice"));

            return PartialView("~/Areas/Admin/Views/Recruitment/Partials/_NewOfferComment.cshtml", result);
        }

        [HttpPost]
        public async Task<ActionResult> HoldOffer(int JobReqId)
        {
            var result = await _vacancyManager.OfferOnHold(JobReqId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> ReviewOffer(List<ReviewOutcome> r)
        {
            var result = await _vacancyManager.AddOfferOutcome(r.Where(x => x.ApprovalStatus != null).ToList(), User.Identity.Name);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> RejectedToReview(int entryId)
        {
            var result = await _vacancyManager.RejectedToReview(entryId);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RecruitmentAdmin(string storeNum = "e", string chain = "e")
        {
            if(storeNum == "e")
            {
                return View(new VacancyRequestVm());
            }

            VacancyRequestVm vm = new VacancyRequestVm();
            var detail = chain == "CPW" ? mapper.Map<List<RecruitmentDetailView>>(await _vacancyManager.GetVacancyDetailCPW(storeNum)) : mapper.Map<List<RecruitmentDetailView>>(await _vacancyManager.GetVacancyDetailDXNS(storeNum));
            if (detail.Any())
            {
                vm.Populate(detail);
                vm.PendingRequests = mapper.Map<List<VacancyRequestsAdminView>>(await _vacancyManager.GetPendingRequestsCPW(storeNum));
                vm.PendingRequests.AddRange(mapper.Map<List<VacancyRequestsAdminView>>(await _vacancyManager.GetPendingRequestsDXNS(storeNum)));
                vm.LiveRequests = mapper.Map<List<SFOpenVacancyView>>(await _vacancyManager.GetOpenVacanciesCPW(storeNum));
                vm.LiveRequests.AddRange(mapper.Map<List<SFOpenVacancyView>>(await _vacancyManager.GetOpenVacanciesDXNS(storeNum)));
            }
            else
            {
                vm.MessageType = MessageType.Error;
                vm.Message = "Your store is not currently set up to use this process, please raise this with The Medics";
            }

            System.Web.HttpContext.Current.Session["adminChain"] = chain;
            System.Web.HttpContext.Current.Session["adminStore"] = storeNum;

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> NewVacancy(List<RecruitmentRequest> r, string Notes)
        {
            var result = await _vacancyManager.PostNewRequestsAdmin(r, Notes, HttpContext.Session["Email"].ToString(), System.Web.HttpContext.Current.Session["adminChain"].ToString(), System.Web.HttpContext.Current.Session["adminStore"].ToString());

            return RedirectToAction("Index");
        }
    }
}
