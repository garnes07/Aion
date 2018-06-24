using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Aion.Areas.WFM.ViewModels.Deployment;
using Aion.Controllers;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Helpers;
using Aion.ViewModels;
using Aion.Attributes;
using Aion.DAL.Entities;
using System.Collections.Generic;
using Aion.Areas.WFM.Models.Deployment;

namespace Aion.Areas.WFM.Controllers
{
    public class DeploymentController : BaseController
    {
        private readonly IDashboardDataManager _dashDataManager;
        private readonly IWeeksManager _weeksManager;
        private readonly IFootfallManager _footfallManager;
        private readonly IGmWeWorkingManager _gmWeWorkingManager;
        private readonly IAvlbltyManager _avlbltyManager;
        private readonly IEmpSummaryManager _empSummaryManager;
        private readonly IStoreManager _storeManager;
        private readonly ISOHBudgetsManager _sohBudgetsManager;
        private readonly IHRDataManager _hrDataManager;
        private readonly IVacancyManager _vacancyManger;
        private readonly IHolidayPlanningManager _holidayPlanningManager;
        private readonly IScheduleManager _scheduleManager;
        private readonly IOpeningTimesManager _openingTimesManager;

        public DeploymentController()
        {
            _dashDataManager = new DashboardDataManager();
            _weeksManager = new WeeksManager();
            _footfallManager = new FootfallManager();
            _gmWeWorkingManager = new GmWeWorkingManager();
            _avlbltyManager = new AvlbltyManager();
            _empSummaryManager = new EmpSummaryManager();
            _storeManager = new StoreManager();
            _sohBudgetsManager = new SOHBudgetsManager();
            _hrDataManager = new HRDataManager();
            _vacancyManger = new VacancyManager();
            _holidayPlanningManager = new HolidayPlanningManager();
            _scheduleManager = new ScheduleManager();
            _openingTimesManager = new OpeningTimesManager();
        }

        public async Task<ActionResult> Summary(string c = "e_0")
        {
            string[] input = c.Split('_');
            byte period = byte.Parse(input[1]);
            DeploymentSummaryVm vm = new DeploymentSummaryVm();

            switch (selectArea)
            {
                case "S":
                    vm.collection = await _dashDataManager.GetDeploymentSummaryStore(input[0], period, selectCrit);
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.collection = await _dashDataManager.GetDeploymentSummaryRegion(input[0], period, selectCrit);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.collection = await _dashDataManager.GetDeploymentSummaryDivision(input[0], period, selectCrit);
                    vm.priority = selectCrit;
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.collection = await _dashDataManager.GetDeploymentSummaryChain(input[0], period, selectCrit);
                    vm.DisplayLevel = 4;
                    break;
            }

            if (vm.collection.Any())
            {
                vm.selectedDate = string.Format("{0}_{1}", vm.collection.FirstOrDefault().Year,
                    vm.collection.FirstOrDefault().Period);
                vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == vm.selectedDate);
            }

            return View(vm);
        }

        public async Task<ActionResult> Detail(string selectedDate = "Last Week")
        {
            if (System.Web.HttpContext.Current.Session["_PilotFlag"] != null && (bool)System.Web.HttpContext.Current.Session["_PilotFlag"] == true)
            {
                return RedirectToAction("DetailPilot", new { selectedDate = selectedDate });
            }

            DeploymentDetailVm vm = new DeploymentDetailVm();
            int weekNum = selectedDate.GetWeekNumber();

            switch (selectArea)
            {
                case "S":
                    vm.WeekData = await _dashDataManager.GetStoreDashData(selectCrit, weekNum);
                    vm.DailyData = await _dashDataManager.GetDailyDeploymentStore(selectCrit, weekNum);
                    if (vm.WeekData.Count == 0)
                        vm.MessageType = MessageType.Warning;
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.WeekData = await _dashDataManager.GetAllRegionDashData(selectCrit, weekNum);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.ChainData = await _dashDataManager.GetAllDivisionDashData(selectCrit, weekNum);
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.ChainData = await _dashDataManager.GetAllChainDashData(selectCrit, weekNum);
                    vm.DisplayLevel = 4;
                    break;
            }

            vm.SetWeeksOfYear(DateTime.Now.FirstDayOfWeek().AddDays(49), await _weeksManager.GetMultipleWeeks(DateTime.Now.FirstDayOfWeek().AddDays(-56), DateTime.Now.FirstDayOfWeek().AddDays(49).FirstDayOfWeek()));
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekNum.ToString());

            return View(vm);
        }

        public async Task<ActionResult> DetailPilot(string selectedDate = "Last Week")
        {
            if (System.Web.HttpContext.Current.Session["_PilotFlag"] != null && (bool)System.Web.HttpContext.Current.Session["_PilotFlag"] == true)
            {
                return RedirectToAction("Detail", new { selectedDate = selectedDate });
            }

            DeploymentDetailPilotVm vm = new DeploymentDetailPilotVm();
            int weekNum = selectedDate.GetWeekNumber();

            switch (selectArea)
            {
                case "S":
                    vm.WeekData = await _dashDataManager.GetStoreDashDataPilot(selectCrit, weekNum);
                    vm.DailyData = await _dashDataManager.GetDailyDeploymentStorePilot(selectCrit, weekNum);
                    vm.PowerHours = await _dashDataManager.GetStorePowerHours(selectCrit, weekNum);
                    if (vm.WeekData.Count == 0)
                        vm.MessageType = MessageType.Warning;
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.WeekData = await _dashDataManager.GetAllRegionDashDataPilot(selectCrit, weekNum);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    return RedirectToAction("Detail", new { selectedDate = selectedDate });
                case "C":
                    return RedirectToAction("Detail", new { selectedDate = selectedDate });
            }

            vm.SetWeeksOfYear(DateTime.Now.FirstDayOfWeek().AddDays(49), await _weeksManager.GetMultipleWeeks(DateTime.Now.FirstDayOfWeek().AddDays(-56), DateTime.Now.FirstDayOfWeek().AddDays(49).FirstDayOfWeek()));
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekNum.ToString());

            return View(vm);
        }

        public async Task<ActionResult> SOHBudgets()
        {
            SOHBudgetsVm vm = new SOHBudgetsVm();

            switch (selectArea)
            {
                case "S":
                    vm.Collection = !(bool)System.Web.HttpContext.Current.Session["_ROIFlag"] ?
                        mapper.Map<List<SOHBudgetView>>(await _sohBudgetsManager.GetBudgetUKStore(selectCrit)) :
                        mapper.Map<List<SOHBudgetView>>(await _sohBudgetsManager.GetBudgetROIStore(selectCrit));
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.Collection = !(bool)System.Web.HttpContext.Current.Session["_ROIFlag"] ?
                        mapper.Map<List<SOHBudgetView>>(await _sohBudgetsManager.GetBudgetUKRegion(selectCrit)) :
                        mapper.Map<List<SOHBudgetView>>(await _sohBudgetsManager.GetBudgetROIRegion(selectCrit));
                    vm.DisplayLevel = 2;
                    break;
                default:
                    vm.MessageType = MessageType.Error;
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    break;
            }

            return View(vm);
        }

        public async Task<ActionResult> Footfall(string selectedYear = null, string selectedWeek = null)
        {
            FootfallVm vm = new FootfallVm();

            if (selectedWeek == null)
            {
                var weekDetail = _weeksManager.GetSingleWeek(DateTime.Now.AddDays(-7).Date);
                selectedYear = weekDetail.ToString().Substring(2, 2);
                vm.SelectedYear = selectedYear = (int.Parse(selectedYear) - 1) + "/" + selectedYear;
                vm.SelectedWeek = selectedWeek = weekDetail.ToString().Substring(4, 2);
            }

            switch (selectArea)
            {
                case "S":
                    vm.FootfallCollection = await _footfallManager.GetFootfallStore(selectCrit,selectedYear, int.Parse(selectedWeek));
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.FootfallCollection = await _footfallManager.GetFootfallRegion(selectCrit, selectedYear, int.Parse(selectedWeek));
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.FootfallCollection = await _footfallManager.GetFootfallDivision(selectCrit, selectedYear, int.Parse(selectedWeek));
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.FootfallCollection = await _footfallManager.GetFootfallChain(selectCrit, selectedYear, int.Parse(selectedWeek));
                    vm.DisplayLevel = 4;
                    break;
            }

            if (!vm.FootfallCollection.Any())
            {
                vm.MessageType = MessageType.Warning;
                vm.Message = "No data found for the selected period";
            }

            return View(vm);
        }

        [UserFilter(MinLevel = 1)]
        public async Task<ActionResult> MyTeam()
        {
            MyTeamVm vm = new MyTeamVm();

            switch (selectArea)
            {
                case "S":
                    vm.ContractBaseDetailStore = await _hrDataManager.GetContractAndHolidayStore(selectCrit);
                    vm.StaffList = await _hrDataManager.GetStaffListStore(selectCrit);
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.ContractBaseDetailRegion = await _hrDataManager.GetContractAndHolidayRegion(selectCrit);
                    vm.DisplayLevel = 2;
                    break;
                default:
                    vm.MessageType = MessageType.Error;
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    break;
            }
            return View(vm);
        }

        [UserFilter(MinLevel = 2)]
        public async Task<ActionResult> WeekendWorking()
        {
            WeekendWorkingVm vm = new WeekendWorkingVm();

            switch (selectArea)
            {
                case "S":
                    vm.Collection = await _gmWeWorkingManager.GetGmWorkingRegionUsingStore(selectCrit);
                    vm.DisplayLevel = 2;
                    break;
                case "R":
                    vm.Collection = await _gmWeWorkingManager.GetGmWorkingRegion(selectCrit);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.Collection = await _gmWeWorkingManager.GetGmWorkingDivision(selectCrit);
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.Collection = await _gmWeWorkingManager.GetGmWorkingChain(selectCrit);
                    vm.DisplayLevel = 4;
                    break;
            }

            return View(vm);
        }

        [UserFilter(MinLevel = 1)]
        public async Task<ActionResult> Availability()
        {
            AvlbltySummaryVm vm = new AvlbltySummaryVm();

            switch (selectArea)
            {
                case "S":
                    vm.AvlbltyCollection = await _avlbltyManager.GetAllPatternsStore(selectCrit);
                    vm.AvlbltyMissing = await _avlbltyManager.GetAllColleaguesWithoutPattern(selectCrit);
                    vm.AvlbltySummary = await _avlbltyManager.GetPatternStore(selectCrit);
                    vm.tradingHrs = await _avlbltyManager.GetCurrentTradingHrs(selectCrit);
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.areaCompletion = await _avlbltyManager.GetCompletionRateRegion(selectCrit);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.MessageType = MessageType.Error;
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    break;
                case "C":
                    vm.MessageType = MessageType.Error;
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    break;
            }

            return View(vm);
        }

        [Authorize]
        public async Task<ActionResult> UpdateAvailability(string personNumber = "e")
        {
            if(selectArea != "S" && personNumber != "e")
            {
                return RedirectToAction("Availability");
            }

            var _personNumber = personNumber == "e" ? System.Web.HttpContext.Current.Session["_EmpNum"].ToString() : personNumber;

            UpdateAvlbltyVm vm = new UpdateAvlbltyVm();

            if (personNumber == "e")
            {
                vm.contactDetails = await _avlbltyManager.GetContactDetailsPerson(_personNumber);
                vm.displayContact = true;
            }
            else
            {
                if(! await _avlbltyManager.CheckAddRightsForUser(_personNumber, selectCrit))
                    return RedirectToAction("Availability");
            }

            vm.existingPattern = await _avlbltyManager.GetAllPatternsPerson(_personNumber);
            vm.empDetails = await _empSummaryManager.GetEmployeeMatchingNumber(_personNumber);
            vm.LocalStores = await _storeManager.GetStoresInSameRegion(selectCrit);
            vm.AvlbltyStores = await _avlbltyManager.GetAllPatternStoresPerson(_personNumber);

            if (vm.existingPattern.Count != 0 && personNumber != "e")
                return RedirectToAction("Availability");

            System.Web.HttpContext.Current.Session["_avlbltyRedirect"] = HttpContext.Request.UrlReferrer.Segments.Last() == "ColleaguePayPortal" ? "colleague" : "store";
            System.Web.HttpContext.Current.Session["_avlbltyToChange"] = _personNumber;
            HttpContext.Request.UrlReferrer.Segments.Last();

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAvailability(AvailabilityPattern a, AvailabilityContact c, short[] s, bool d = false, bool confirmOnly = false)
        {
            if (ModelState.IsValid && System.Web.HttpContext.Current.Session["_avlbltyToChange"].ToString() == a.PersonNumber)
            {
                var patternResult = await _avlbltyManager.SubmitNewPattern(a, s,User.Identity.Name);
                var contactResult = await _avlbltyManager.UpdateContactDetails(c, d, a.PersonNumber, confirmOnly);
            }

            if(System.Web.HttpContext.Current.Session["_avlbltyRedirect"] != null)
            {
                if(System.Web.HttpContext.Current.Session["_avlbltyRedirect"].ToString() == "colleague")
                {
                    System.Web.HttpContext.Current.Session.Remove("_avlbltyRedirect");
                    System.Web.HttpContext.Current.Session.Remove("_avlbltyToChange");
                    return RedirectToAction("ColleaguePayPortal", "RFTP", new { area = "WFM"});
                }
            }
            System.Web.HttpContext.Current.Session.Remove("_avlbltyRedirect");
            System.Web.HttpContext.Current.Session.Remove("_avlbltyToChange");
            return RedirectToAction("Availability");
        }

        [UserFilter(MinLevel = 1)]
        public async Task<PartialViewResult> _GetCoverColleagues()
        {
            return PartialView("../Deployment/Partials/_coverColleagues", await _avlbltyManager.GetPatternsForAvailableCover(selectCrit));
        }

        [UserFilter(MinLevel = 1)]
        public async Task<ActionResult> Recruitment()
        {
            VacancyRequestVm vm = new VacancyRequestVm();
            if (selectArea == "S")
            {
                var detail = await _vacancyManger.GetVacancyDetailCPW(selectCrit);
                if (detail.Any())
                {
                    vm.Populate(mapper.Map<List<RecruitmentDetail>>(detail));
                    vm.PendingRequests = await _vacancyManger.GetPendingRequestsCPW(selectCrit);
                    vm.LiveRequests = await _vacancyManger.GetOpenVacanciesCPW(selectCrit);
                }
                else
                {
                    vm.MessageType = MessageType.Error;
                    vm.Message = "Your store is not currently set up to use this process, please raise this with The Medics";
                }

            }
            else
            {
                vm.MessageType = MessageType.Error;
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
            }

            return View(vm);
        }

        [UserFilter(MinLevel = 1)]
        public async Task<ActionResult> RecruitmentDixons()
        {
            VacancyRequestVm vm = new VacancyRequestVm();
            vm.Populate(mapper.Map<List<RecruitmentDetail>>(await _vacancyManger.GetVacancyDetailDXNS("2216")));
            vm.PendingRequests = await _vacancyManger.GetPendingRequestsDXNS("2216");
            vm.LiveRequests = await _vacancyManger.GetOpenVacanciesDXNS("2216");

            return View("Recruitment", vm);
        }

        [UserFilter(MinLevel = 1)]
        [HttpPost]
        public async Task<ActionResult> NewVacancy(List<RecruitmentRequest> r, string Notes)
        {
            var result = await _vacancyManger.PostNewRequestsCPW(r, Notes, selectCrit, HttpContext.Session["Email"].ToString());

            return RedirectToAction("Recruitment");
        }

        [UserFilter(MinLevel = 1)]
        [HttpPost]
        public async Task<ActionResult> CancelLive(int ReferenceId)
        {
            var result = await _vacancyManger.CancelLive(selectCrit, ReferenceId, User.Identity.Name);
            return RedirectToAction("Recruitment");
        }

        [UserFilter(MinLevel = 1)]
        [HttpPost]
        public async Task<ActionResult> CancelPending(int ReferenceId)
        {
            var result = await _vacancyManger.CancelPending(selectCrit, ReferenceId);
            return RedirectToAction("Recruitment");
        }

        public async Task<ActionResult> HolidayPlanning(int year = 201900)
        {
            HolidayPlanningVm vm = new HolidayPlanningVm();
            vm.CurrentWeek = ("This Week").GetWeekNumber();

            switch (selectArea)
            {
                case "S":
                    vm.StoreCollection = await _holidayPlanningManager.GetHolidayStore(selectCrit, year + 1, year + 52);
                    vm.EmpCollection = await _holidayPlanningManager.GetHolidayStoreEmp(selectCrit, year + 1);
                    vm.DashCollection = await _dashDataManager.GetStoreDetailBetween(selectCrit, year + 1, year + 52);
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.StoreCollection = await _holidayPlanningManager.GetHolidayRegion(selectCrit, year + 1, year + 52);
                    vm.RollupCollection = await _holidayPlanningManager.GetHolidayRegionRollup(selectCrit, year + 1);
                    vm.DashCollection = await _dashDataManager.GetRegionDetailBetween(selectCrit, year + 1, year + 52);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.StoreCollection = await _holidayPlanningManager.GetHolidayDivision(selectCrit, year + 1, year + 52);
                    vm.RollupCollection = await _holidayPlanningManager.GetHolidayDivisionRollup(selectCrit, year + 1);
                    vm.DashCollection = await _dashDataManager.GetDivisionDetailBetween(selectCrit, year + 1, year + 52);
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.MessageType = MessageType.Error;
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    break;
            }
            return View(vm);
        }

        public async Task<ActionResult> Schedules(string selectedDate = "This Week")
        {
            ScheduleVm vm = new ScheduleVm();
            int weekNum = selectedDate.GetWeekNumber();

            switch (selectArea)
            {
                case "S":
                    vm.Collection = await _scheduleManager.GetStoreSchedule(selectCrit, weekNum);
                    vm.HomeStore = !(bool)System.Web.HttpContext.Current.Session["_ROIFlag"] ? "UK " + selectCrit : "IE " + selectCrit;
                    if (vm.Collection.Min(x => x.StartDate) < DateTime.Now.Date.FirstDayOfWeek())
                    {
                        ViewBag.historic = true;
                    }
                    if (vm.Collection.Any())
                    {
                        var oTimes = await _openingTimesManager.GetSpecificStoreOpeningTime(selectCrit, (DateTime)vm.Collection.Min(x => x.StartDate));
                        if (oTimes.Any(x => x.Status != "Live"))
                        {
                            vm.OpeningTime = oTimes.First(x => x.Status != "Live");
                        }
                        else if (oTimes.Any(x => x.Status == "Live"))
                        {
                            vm.OpeningTime = oTimes.First(x => x.Status == "Live");
                        }
                        else
                        {
                            vm.OpeningTime = null;
                        }
                    }
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.Collection = await _scheduleManager.GetRegionGMScheduleData(selectCrit, weekNum);
                    if (vm.Collection.Min(x => x.StartDate) < DateTime.Now.Date.FirstDayOfWeek())
                    {
                        ViewBag.historic = true;
                    }
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.MessageType = MessageType.Error;
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.MessageType = MessageType.Error;
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    vm.DisplayLevel = 4;
                    break;
            }

            vm.SetWeeksOfYear(DateTime.Now.FirstDayOfWeek().AddDays(42), await _weeksManager.GetMultipleWeeks(DateTime.Now.FirstDayOfWeek().AddDays(-28), DateTime.Now.FirstDayOfWeek().AddDays(42).FirstDayOfWeek()));
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekNum.ToString());

            return View(vm);
        }
    }
}