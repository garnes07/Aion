using Aion.Areas.Admin.Models;
using Aion.Areas.Admin.ViewModels.Recruitment;
using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

            System.Web.HttpContext.Current.Session["RefIds"] = vm.VacancyRequests.Select(x => x.EntryId).ToArray();

            return View(vm);
        }

        public async Task<bool> _MarkIncorrectDone(int jobReqId)
        {
            var result = await _vacancyManager.MarkIncorrectDone(jobReqId, User.Identity.Name);

            return result;
        }

        public async Task<bool> _OfferOutcome(int jobReqId, bool approved)
        {
            var result = await _vacancyManager.MarkOfferApproved(jobReqId, approved, User.Identity.Name);

            return result;
        }

        [HttpPost]
        public async Task<PartialViewResult> _PostNewComment(string commentText)
        {
            var RefIds = (int[])System.Web.HttpContext.Current.Session["RefIds"];
            var result = await _vacancyManager.AddNewComment(RefIds, User.Identity.Name, commentText, "HeadOffice");

            return PartialView("~/Areas/Admin/Views/Recruitment/Partials/_NewComment.cshtml", result);
        }
    }
}