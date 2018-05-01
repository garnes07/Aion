using Aion.Areas.Admin.ViewModels.OpeningTimes;
using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.Managers;
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
            return View(await _openingTimesManager.GetPendingOpeningTimes());
        }

        public async Task<ActionResult> ReviewTime(int entryId, int storeId)
        {
            return View(new ReviewTimesVm(entryId, await _openingTimesManager.GetStoreTimesForReview(storeId)));
        }

        [HttpPost]
        public async Task<PartialViewResult> _AddComment(int entryId, string comment)
        {
            var toReturn = await _openingTimesManager.AddNewComment(entryId, comment, User.Identity.Name);
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