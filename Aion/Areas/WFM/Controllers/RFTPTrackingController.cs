using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Aion.Areas.WFM.ViewModels.RFTPTracking;
using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.Entities;
using Aion.DAL.Managers;
using Aion.ViewModels;

namespace Aion.Areas.WFM.Controllers
{
    public class RFTPTrackingController : BaseController
    {
        private readonly IRFTPTrackingManager _RFTPTrackingManager;
        private readonly IEmpSummaryManager _empSummaryManager;
        private readonly IWeeksManager _weeksManager;

        public RFTPTrackingController()
        {
            _RFTPTrackingManager = new RFTPTrackingManager();
            _empSummaryManager = new EmpSummaryManager();
            _weeksManager = new WeeksManager();
        }

        [UserFilter(MinLevel = 2, ExcludeLevels = new []{7})]
        public async Task<ActionResult> ManagerTracking()
        {
            RFTPManagerSummaryVm vm = new RFTPManagerSummaryVm();

            switch (selectArea)
            {
                case "S":
                    vm.Cases = await _RFTPTrackingManager.GetRFTPCasesStore(selectCrit);
                    vm.Actions = await _RFTPTrackingManager.GetRFTPCaseActions();
                    vm.RegionManagers = await _empSummaryManager.GetActiveManagersRegionUsingStore(selectCrit);
                    vm.DisplayLevel = 2;
                    break;
                case "R":
                    vm.Cases = await _RFTPTrackingManager.GetRFTPCasesRegion(selectCrit);
                    vm.Actions = await _RFTPTrackingManager.GetRFTPCaseActions();
                    vm.RegionManagers = await _empSummaryManager.GetActiveManagersRegion(selectCrit);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.Cases = await _RFTPTrackingManager.GetRFTPCasesDivision(selectCrit);
                    vm.Actions = await _RFTPTrackingManager.GetRFTPCaseActions();
                    vm.RegionManagers = await _empSummaryManager.GetEmployeeDetails(vm.Cases.Select(x => x.PersonNumber).ToList());
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    vm.MessageType = MessageType.Error;
                    break;
            }

            ViewBag.UpdateError = TempData["error"] != null;

            return View(vm);
        }

        [HttpGet]
        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 7 })]
        public async Task<PartialViewResult> _employeeSearch(string crit)
        {
            var searchResult = new List<KronosEmployeeSummary>();
            if (crit.Length != 0)
            {
                searchResult = await _empSummaryManager.GetEmployeeMatchingName(crit);
            }
            return PartialView("../RFTPTracking/Partials/_employeeSearch", searchResult);
        }

        [HttpPost]
        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 7 })]
        public async Task<ActionResult> CaseConfirm(int caseID)
        {
            if (await _RFTPTrackingManager.CheckCaseAuth(caseID, selectArea, selectCrit))
            {
                var result = await _RFTPTrackingManager.ConfirmCase(caseID, System.Web.HttpContext.Current.Session["_UserName"].ToString());
                if (result == -5)
                {
                    TempData["error"] = true;
                }
            }
            else
            {
                TempData["error"] = true;
            }

            return RedirectToAction("ManagerTracking");
        }

        [HttpPost]
        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 7 })]
        public async Task<ActionResult> CaseOverride(int caseID, string reason, string comment)
        {
            if (await _RFTPTrackingManager.CheckCaseAuth(caseID, selectArea, selectCrit))
            {
                var result = await _RFTPTrackingManager.OverrideCase(caseID, System.Web.HttpContext.Current.Session["_UserName"].ToString(), reason, comment);
                if (result == -5)
                {
                    TempData["error"] = true;
                }
            }
            else
            {
                TempData["error"] = true;
            }

            return RedirectToAction("ManagerTracking");
        }

        [HttpPost]
        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 7 })]
        public async Task<ActionResult> CaseReassign(int caseID, string empNumber, string comment)
        {
            if (await _RFTPTrackingManager.CheckCaseAuth(caseID, selectArea, selectCrit))
            {
                int result;
                if (empNumber != "Terminated")
                {
                    result = await _RFTPTrackingManager.ReassignCase(caseID, empNumber, System.Web.HttpContext.Current.Session["_UserName"].ToString(), comment);
                }
                else
                {
                    result = await _RFTPTrackingManager.OverrideCase(caseID, System.Web.HttpContext.Current.Session["_UserName"].ToString(), "GM Terminated", comment);
                }

                if (result == -5)
                {
                    TempData["error"] = true;
                }
            }
            else
            {
                TempData["error"] = true;
            }

            return RedirectToAction("ManagerTracking");
        }

        [HttpPost]
        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 7 })]
        public async Task<ActionResult> CaseSubmitAction(int caseID, string actionType, string comment)
        {
            if (await _RFTPTrackingManager.CheckCaseAuth(caseID, selectArea, selectCrit))
            {
                var result = await _RFTPTrackingManager.SubmitAction(caseID, actionType, comment, System.Web.HttpContext.Current.Session["_UserName"].ToString(), System.Web.HttpContext.Current.Session["_AccessLevel"].ToString());
                if (result == -5)
                {
                    TempData["error"] = true;
                }
            }
            else
            {
                TempData["error"] = true;
            }

            return RedirectToAction("ManagerTracking");
        }

        [UserFilter(MinLevel = 2, ExcludeLevels = new[] {7})]
        public async Task<ActionResult> ManagerTrend()
        {
            RFTPManagerTrendVm vm = new RFTPManagerTrendVm();

            if (selectArea == "S" || selectArea == "R")
            {
                vm.Cases = selectArea == "S" ? await _RFTPTrackingManager.GetLast12MonthRFTPCasesStore(selectCrit) : await _RFTPTrackingManager.GetLast12MonthRFTPCasesRegion(selectCrit);
                vm.PeriodList = await _weeksManager.GetLast12MonthList();
                vm.EmployeeList = await _empSummaryManager.GetEmployeeDetails(vm.Cases.GroupBy(x => x.PersonNumber).Select(x => x.Key).ToList());
                vm.DisplayLevel = 2;
            }
            else if (selectArea == "D")
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }
            else if (selectArea == "C")
            {
                vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                vm.MessageType = MessageType.Error;
            }

            return View(vm);
        }

        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 7 })]
        public async Task<ActionResult> ManagerDetail(string personNum)
        {
            if(personNum == null)
                return RedirectToAction("ManagerTracking");

            RFTPManagerDetailVm vm = new RFTPManagerDetailVm
            {
                caseDetails = await _RFTPTrackingManager.GetAllCasesForPerson(personNum)
            };

            if (vm.caseDetails == null)
            {
                vm.Message = "No case details for the selected colleague.";
                vm.MessageType = MessageType.Error;
            }

            return View(vm);
        }
    }
}