using Aion.Areas.Admin.ViewModels.OpeningTimes;
using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.Managers;
using Aion.Models.WFM;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.Admin.Controllers
{
    [UserFilter(MinLevel = 9)]
    public class OpeningTimesController : BaseController
    {
        private IOpeningTimesManager _openingTimesManager;

        public OpeningTimesController()
        {
            _openingTimesManager = new OpeningTimesManager();
        }

        // GET: Admin/OpeningTimes
        public async Task<ActionResult> Index()
        {
            return View(mapper.Map<List<StoreOpeningTimeView>>(await _openingTimesManager.GetPendingOpeningTimes()));
        }

        public async Task<ActionResult> ReviewTime(int entryId, int storeId)
        {
            return View(new ReviewTimesVm(entryId, mapper.Map<List<StoreOpeningTimeView>>(await _openingTimesManager.GetStoreTimesForReview(storeId))));
        }

        [HttpPost]
        public async Task<PartialViewResult> _AddComment(int entryId, string comment)
        {
            var toReturn = mapper.Map<OpeningTimesCommentView>(await _openingTimesManager.AddNewComment(entryId, comment, User.Identity.Name));
            return PartialView("~/Areas/Admin/Views/OpeningTimes/Partials/_comment.cshtml", toReturn);
        }

        public async Task<ActionResult> ApproveOpeningTime(int entryId)
        {
            await _openingTimesManager.ApproveRejectPendingRequest(entryId, true);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RejectOpeningTime(int entryId)
        {
            await _openingTimesManager.ApproveRejectPendingRequest(entryId, false);
            return RedirectToAction("Index");
        }
    }
}