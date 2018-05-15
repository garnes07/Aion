using Aion.Areas.Admin.Models;
using Aion.Areas.Admin.ViewModels.Recruitment;
using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.Managers;
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

            vm.IncorrectVacancies = await _vacancyManager.GetIncorrectVacancies();
            vm.OfferApprovals = await _vacancyManager.GetOfferApprovals();
            vm.AllPending = await _vacancyManager.GetPendingForAdmin();

            return View(vm);
        }

        public async Task<ActionResult> ReviewPending(string Chain, int StoreNumber, int PositionCode)
        {
            ReviewPendingVm vm = new ReviewPendingVm();

            vm.VacancyRequests = await _vacancyManager.GetPendingForAdmin(Chain, StoreNumber, PositionCode);
            vm.RecruitmentDetail = Chain == "CPW" ? 
                mapper.Map<List<RecruitmentDetail>>(await _vacancyManager.GetVacancyDetailCPW(StoreNumber.ToString())) : 
                mapper.Map<List<RecruitmentDetail>>(await _vacancyManager.GetVacancyDetailDXNS(StoreNumber.ToString()));
            vm.HRCurrent = await _vacancyManager.GetHrCurrent(Chain, StoreNumber);
            vm.HRChanges = await _vacancyManager.GetHrChanges(Chain, StoreNumber);
            
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

        public async Task<bool> _MarkIncorrectDone(int jobReqId)
        {
            var result = await _vacancyManager.MarkIncorrectDone(jobReqId, User.Identity.Name);

            return result;
        }

        [HttpPost]
        public async Task<PartialViewResult> _PostNewComment(string commentText)
        {
            var RefIds = (int[])System.Web.HttpContext.Current.Session["RefIds"];
            var result = await _vacancyManager.AddNewComment(RefIds, User.Identity.Name, commentText, "HeadOffice");

            return PartialView("~/Areas/Admin/Views/Recruitment/Partials/_NewComment.cshtml", result);
        }

        public async Task<PartialViewResult> _GetToPost(string chain, int store, int jobcode)
        {
            var result = await _vacancyManager.GetToPostForAdmin(chain, store, jobcode);

            return PartialView("~/Areas/Admin/Views/Recruitment/Partials/_ToPost.cshtml", result);
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

        public async Task<ActionResult> ReviewOffer(int JobReqId)
        {
            ReviewOfferVm vm = new ReviewOfferVm();

            vm.OfferToReview = await _vacancyManager.GetOfferToReview(JobReqId);
            vm.RecruitmentDetail = vm.OfferToReview.First().Company == "CPW" ?
                mapper.Map<List<RecruitmentDetail>>(await _vacancyManager.GetVacancyDetailCPW(vm.OfferToReview.First().Store_Number.ToString())) :
                mapper.Map<List<RecruitmentDetail>>(await _vacancyManager.GetVacancyDetailDXNS(vm.OfferToReview.First().Store_Number.ToString()));
            vm.HRCurrent = await _vacancyManager.GetHrCurrent(vm.OfferToReview.First().Company, (int)vm.OfferToReview.First().Store_Number);
            vm.HRChanges = await _vacancyManager.GetHrChanges(vm.OfferToReview.First().Company, (int)vm.OfferToReview.First().Store_Number);
            vm.OpenVacancy = await _vacancyManager.GetOpenVacancyByRef(JobReqId);

            System.Web.HttpContext.Current.Session["RefIds"] = vm.OfferToReview.Select(x => x.Application_ID).ToArray();

            return View(vm);
        }

        [HttpPost]
        public async Task<PartialViewResult> _PostNewOfferComment(string commentText)
        {
            var RefIds = (int[])System.Web.HttpContext.Current.Session["RefIds"];
            var result = await _vacancyManager.AddNewOfferComment(RefIds, User.Identity.Name, commentText, "HeadOffice");

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
    }
}