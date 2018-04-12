using Aion.Areas.Admin.ViewModels.OpeningTimes;
using Aion.DAL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Aion.Areas.Admin.Controllers
{
    public class OpeningTimesController : Controller
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
    }
}