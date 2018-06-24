using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IActivityManager _activityManager;
        private readonly IDashboardDataManager _dashManager;

        public HomeController()
        {
            _activityManager = new ActivityManager();
            _dashManager = new DashboardDataManager();
        }

        public async Task<ActionResult> Index()
        {
            HomeVm vm = new HomeVm();
            vm.TopNews = await _activityManager.GetTopNews();

            switch (selectArea)
            {
                case "S":
                    if (System.Web.HttpContext.Current.Session["_PilotFlag"] != null && (bool)System.Web.HttpContext.Current.Session["_PilotFlag"] == true)
                    {
                        vm.ScoreSummary = await _dashManager.Get4WeekSummaryStore(selectCrit);
                    }
                    else
                    {
                        vm.ScoreSummary = mapper.Map<List<vw_4WeekSummary>>(await _dashManager.Get4WeekSummaryStorePilot(selectCrit));                        
                    }                        
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    if (System.Web.HttpContext.Current.Session["_PilotFlag"] != null && (bool)System.Web.HttpContext.Current.Session["_PilotFlag"] == true)
                    {
                        vm.ScoreSummary = await _dashManager.Get4WeekSummaryRegion(selectCrit);
                    }
                    else
                    {
                        vm.ScoreSummary = mapper.Map<List<vw_4WeekSummary>>(await _dashManager.Get4WeekSummaryRegionPilot(selectCrit));
                    }
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.ScoreSummary = await _dashManager.Get4WeekSummaryDivision(selectCrit);
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.ScoreSummary = await _dashManager.Get4WeekSummaryChain(selectCrit);
                    vm.DisplayLevel = 4;
                    break;
            }

            return View(vm);
        }

        public async Task<ActionResult> NewsArchive()
        {
            return View(await _activityManager.GetAllNews());
        }

        public FileResult Documents(string f)
        {
            return File("/Uploads/" + f, "application/pdf");
        }

        public ActionResult STARHandbook()
        {
            return View();
        }
    }
}