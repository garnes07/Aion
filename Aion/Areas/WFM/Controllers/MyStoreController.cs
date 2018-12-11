using Aion.Areas.WFM.Models.MyStore;
using Aion.Areas.WFM.ViewModels.MyStore;
using Aion.Controllers;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Models.WFM;
using Aion.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.WFM.Controllers
{
    public class MyStoreController : BaseController
    {
        private readonly IOpeningTimesManager _openingTimesManager;
        private readonly IHRDataManager _hrDataManager;
        private readonly IScheduleManager _scheduleManager;
        private readonly IWeeksManager _weeksManager;
        private readonly IHolidayPlanningManager _holidayPlanningManager;
        private readonly IDashboardDataManager _dashboardDataManager;
        private readonly IVacancyManager _vacancyManger;

        public MyStoreController()
        {
            _openingTimesManager = new OpeningTimesManager();
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
                    vm.Collection = mapper.Map<List<StoreOpeningTimeView>>(await _openingTimesManager.GetOpeningTimesStore(selectCrit));
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
                await _openingTimesManager.SubmitOpeningTimeChange(newEntry.NewTime, User.Identity.Name);
            }
            else
            {
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and raise with Medics if you continue to experience issues.";
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

            var result = mapper.Map<StoreOpeningTimeView>(await _openingTimesManager.GetSinglePendingById(id, selectCrit));
            if (result == null)
            {
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and raise with Medics if you continue to experience issues.";
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
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and raise with Medics if you continue to experience issues.";
            }
            else
            {
                var result = await _openingTimesManager.CancelPendingChange(id, User.Identity.Name);
                if(result == -5)
                    TempData["ErrorMessage"] = "Looks like something went wrong, please try again and raise with Medics if you continue to experience issues.";
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

            var result = mapper.Map<StoreOpeningTimeView>(await _openingTimesManager.GetSinglePeakById(id, selectCrit));
            if (result == null)
            {
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and raise with Medics if you continue to experience issues.";
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
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and raise with Medics if you continue to experience issues.";
            }
            else
            {
                var result = await _openingTimesManager.EditExistingPeak(entry.NewTime, User.Identity.Name);
                if (result == -5)
                    TempData["ErrorMessage"] = "Looks like something went wrong, please try again and raise with Medics if you continue to experience issues.";
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
                TempData["ErrorMessage"] = "Looks like something went wrong, please try again and raise with Medics if you continue to experience issues.";
            }

            return RedirectToAction("OpeningTimes");
        }    
    }
}