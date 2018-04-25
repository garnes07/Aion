using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Aion.Areas.WFM.Models.MyStore;
using Aion.Areas.WFM.ViewModels.MyStore;
using Aion.Controllers;
using Aion.DAL.Entities;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Helpers;
using Aion.ViewModels;
using Aion.Attributes;

namespace Aion.Areas.WFM.Controllers
{
    public class MyStoreController : BaseController
    {
        private readonly IOpeningTimesManager _openingTimesManager;
        private readonly ISOHBudgetsManager _sohBudgetsManager;
        private readonly IHRDataManager _hrDataManager;
        private readonly IScheduleManager _scheduleManager;
        private readonly IWeeksManager _weeksManager;
        private readonly IHolidayPlanningManager _holidayPlanningManager;
        private readonly IDashboardDataManager _dashboardDataManager;
        private readonly IVacancyManager _vacancyManger;

        public MyStoreController()
        {
            _openingTimesManager = new OpeningTimesManager();
            _sohBudgetsManager = new SOHBudgetsManager();
            _hrDataManager = new HRDataManager();
            _scheduleManager = new ScheduleManager();
            _weeksManager = new WeeksManager();
            _holidayPlanningManager = new HolidayPlanningManager();
            _dashboardDataManager = new DashboardDataManager();
            _vacancyManger = new VacancyManager();
        }
        
        public async Task<ActionResult> OpeningTimes()
        {
            OpeningTimesVm vm = new OpeningTimesVm();
            if (TempData["ErrorMessage"] != null)
            {
                vm.MessageType = MessageType.Warning;
                vm.Message = TempData["ErrorMessage"].ToString();
            }

            if (selectArea == "S")
            {
                if (selectCrit.Length == 4 && selectCrit.StartsWith("7"))
                {
                    vm.MessageType = MessageType.Warning;
                    vm.Message = "This page is only intended for use by SAS. SIS stores should use the existing Currys PC World process.";
                }
                else
                {
                    vm.Collection = await _openingTimesManager.GetOpeningTimesStore(selectCrit);
                    if (vm.Collection.Count == 0)
                    {
                        vm.MessageType = MessageType.Error;
                        vm.Message = "No Opening times found for your store, please raise this through Medic Feedback.";
                    }
                }
            }
            else
            {
                vm.MessageType = MessageType.Error;
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
            }

            return View(vm);
        }
        
        [Authorize]
        public async Task<ActionResult> NewOpeningTime()
        {
            NewOpeningTimeVm vm = new NewOpeningTimeVm();
            
            if (selectArea == "S")
            {
                vm.NewTime = mapper.Map<NewOpeningTime>(await _openingTimesManager.GetCurrentOpeningTimeStore(selectCrit));
                if (vm.NewTime == null)
                {
                    return RedirectToAction("OpeningTimes");
                }

                vm.NewTime.ReasonForChange = "";
                vm.NewTime.SubmittedByUser = "";
                vm.NewTime.DateTimeSubmitted = new DateTime();
                vm.NewTime.EffectiveDate = new DateTime();
            }
            else
            {
                return RedirectToAction("OpeningTimes");
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> SubmitChange(NewOpeningTimeVm newEntry)
        {
            if (ModelState.IsValid && newEntry.NewTime.StoreNumber.ToString() == selectCrit)
            {
                newEntry.NewTime.ReasonForChange = Server.HtmlEncode(newEntry.NewTime.ReasonForChange);
                await _openingTimesManager.SubmitOpeningTimeChange(mapper.Map<StoreOpeningTime>(newEntry.NewTime), User.Identity.Name);
            }
            else
            {
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and contact STAR if you continue to experience issues.";
            }

            return RedirectToAction("OpeningTimes");
        }

        [Authorize]
        public async Task<ActionResult> CancelChange(int id)
        {
            if (selectArea != "S")
            {
                return RedirectToAction("OpeningTimes");
            }

            var result = await _openingTimesManager.GetSinglePendingById(id, selectCrit);
            if (result == null)
            {
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and contact STAR if you continue to experience issues.";
                return RedirectToAction("OpeningTimes");
            }

            System.Web.HttpContext.Current.Session["_openEntryID"] = result.EntryID;
            return View(result);
        }

        [Authorize]
        [HttpPost, ActionName("CancelChange")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CancelConfirmed(int id)
        {
            if (id.ToString() != System.Web.HttpContext.Current.Session["_openEntryID"].ToString())
            {
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and contact STAR if you continue to experience issues.";
            }
            else
            {
                var result = await _openingTimesManager.CancelPendingChange(id, User.Identity.Name);
                if(result == -5)
                    TempData["ErrorMessage"] = "Looks like something went wrong, please try again and contact STAR if you continue to experience issues.";
            }

            Session.Remove("_openEntryID");
            return RedirectToAction("OpeningTimes");
        }

        [Authorize]
        public async Task<ActionResult> EditPeak(int id)
        {
            if (selectArea != "S")
            {
                return RedirectToAction("OpeningTimes");
            }

            NewOpeningTimeVm vm = new NewOpeningTimeVm();

            var result = await _openingTimesManager.GetSinglePeakById(id, selectCrit);
            if (result == null)
            {
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and contact STAR if you continue to experience issues.";
                return RedirectToAction("OpeningTimes");
            }

            vm.NewTime = mapper.Map<NewOpeningTime>(result);
            System.Web.HttpContext.Current.Session["_openEntryID"] = result.EntryID;
            return View(vm);
        }

        [Authorize]
        [HttpPost, ActionName("EditPeak")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditConfirmed(NewOpeningTimeVm entry)
        {
            if (entry.NewTime.EntryId.ToString() != System.Web.HttpContext.Current.Session["_openEntryID"].ToString())
            {
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and contact STAR if you continue to experience issues.";
            }
            else
            {
                var result = await _openingTimesManager.EditExistingPeak(mapper.Map<StoreOpeningTime>(entry.NewTime), User.Identity.Name);
                if (result == -5)
                    TempData["ErrorMessage"] = "Looks like something went wrong, please try again and contact STAR if you continue to experience issues.";
            }

            Session.Remove("_openEntryID");
            return RedirectToAction("OpeningTimes");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AcceptPeak(int id)
        {
            var result = await _openingTimesManager.AcceptPeak(id, selectCrit, User.Identity.Name);

            if (result == -5)
            {
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and contact STAR if you continue to experience issues.";
            }

            return RedirectToAction("OpeningTimes");
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

        public async Task<ActionResult> HolidayPlanning(int year = 201800)
        {
            HolidayPlanningVm vm = new HolidayPlanningVm();
            vm.CurrentWeek = ("This Week").GetWeekNumber();

            switch (selectArea)
            {
                case "S":
                    vm.StoreCollection = await _holidayPlanningManager.GetHolidayStore(selectCrit, year + 1, year + 52);
                    vm.EmpCollection = await _holidayPlanningManager.GetHolidayStoreEmp(selectCrit, year + 1);
                    vm.DashCollection = await _dashboardDataManager.GetStoreDetailBetween(selectCrit, year + 1, year + 52);
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.StoreCollection = await _holidayPlanningManager.GetHolidayRegion(selectCrit, year + 1, year + 52);
                    vm.RollupCollection = await _holidayPlanningManager.GetHolidayRegionRollup(selectCrit, year + 1);
                    vm.DashCollection = await _dashboardDataManager.GetRegionDetailBetween(selectCrit, year + 1, year + 52);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.StoreCollection = await _holidayPlanningManager.GetHolidayDivision(selectCrit, year + 1, year + 52);
                    vm.RollupCollection = await _holidayPlanningManager.GetHolidayDivisionRollup(selectCrit, year + 1);
                    vm.DashCollection = await _dashboardDataManager.GetDivisionDetailBetween(selectCrit, year + 1, year + 52);
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.MessageType = MessageType.Error;
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    break;
            }
            return View(vm);
        }
        
        [Authorize]
        public async Task<ActionResult> Recruitment()
        {
            VacancyRequestVm vm = new VacancyRequestVm();
            if(selectArea == "S")
            {
                var store = selectCrit;
                vm.Populate(mapper.Map<List<RecruitmentDetail>>(await _vacancyManger.GetVacancyDetailCPW(store)));
                vm.PendingRequests = await _vacancyManger.GetPendingRequests(selectCrit);
            }
            else
            {
                vm.MessageType = MessageType.Error;
                vm.Message = vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
            }

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> NewVacancy(List<RecruitmentRequest> r, string Notes)
        {
            var result = await _vacancyManger.PostNewRequests(r, Notes, selectCrit, HttpContext.Session["Email"].ToString());

            return RedirectToAction("Recruitment");
        }

        [Authorize]
        [HttpPost]
        public ActionResult CancelLive(int ReferenceId)
        {
            return RedirectToAction("Recruitment");
        }

        [Authorize]
        [HttpPost]
        public ActionResult CancelPending(int ReferenceId)
        {
            return RedirectToAction("Recruitment");
        }
    }
}