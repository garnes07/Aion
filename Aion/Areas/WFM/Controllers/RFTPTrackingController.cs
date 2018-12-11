using Aion.Areas.WFM.Models.RFTP;
using Aion.Areas.WFM.ViewModels.RFTPTracking;
using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Models.WFM;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.WFM.Controllers
{

    public class RFTPTrackingController : BaseController
    {
        private readonly IRFTPTrackingManager _RFTPTrackingManager;
        private readonly IEmpSummaryManager _empSummaryManager;
        private readonly IWeeksManager _weeksManager;
        private readonly IDashboardDataManager _dashDataManager;

        public RFTPTrackingController()
        {
            _RFTPTrackingManager = new RFTPTrackingManager();
            _empSummaryManager = new EmpSummaryManager();
            _weeksManager = new WeeksManager();
            _dashDataManager = new DashboardDataManager();
        }

        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 8 })]
        public async Task<ActionResult> ManagerTracking()
        {
            RFTPManagerSummaryVm vm = new RFTPManagerSummaryVm(_store.Chain);

            if (System.Web.HttpContext.Current.Session["_AccessLevel"].ToString() == "2")
            {
                vm.Cases = mapper.Map<List<RFTPCaseStubView>>(await _RFTPTrackingManager.GetRFTPCaseSWAS(System.Web.HttpContext.Current.Session["_SWASGMStore"].ToString()));
                vm.Actions = mapper.Map<List<RFTPCaseActionView>>(await _RFTPTrackingManager.GetRFTPCaseActions());
                vm.RegionManagers = mapper.Map<List<KronosEmployeeSummaryView>>(await _empSummaryManager.GetActiveManagersRegionUsingStore(System.Web.HttpContext.Current.Session["_SWASGMStore"].ToString()));
                vm.SWAS = true;
                vm.DisplayLevel = 2;
            }
            else
            {
                switch (selectArea)
                {
                    case "S":
                        vm.Cases = mapper.Map<List<RFTPCaseStubView>>(await _RFTPTrackingManager.GetRFTPCasesStore(selectCrit));
                        vm.Actions = mapper.Map<List<RFTPCaseActionView>>(await _RFTPTrackingManager.GetRFTPCaseActions());
                        vm.RegionManagers = mapper.Map<List<KronosEmployeeSummaryView>>(await _empSummaryManager.GetActiveManagersRegionUsingStore(selectCrit));
                        vm.DisplayLevel = 2;
                        break;
                    case "R":
                        vm.Cases = mapper.Map<List<RFTPCaseStubView>>(await _RFTPTrackingManager.GetRFTPCasesRegion(selectCrit));
                        vm.Actions = mapper.Map<List<RFTPCaseActionView>>(await _RFTPTrackingManager.GetRFTPCaseActions());
                        vm.RegionManagers = mapper.Map<List<KronosEmployeeSummaryView>>(await _empSummaryManager.GetActiveManagersRegion(selectCrit));
                        vm.DisplayLevel = 2;
                        break;
                    case "D":
                        vm.Cases = mapper.Map<List<RFTPCaseStubView>>(await _RFTPTrackingManager.GetRFTPCasesDivision(selectCrit));
                        vm.Actions = mapper.Map<List<RFTPCaseActionView>>(await _RFTPTrackingManager.GetRFTPCaseActions());
                        vm.RegionManagers = mapper.Map<List<KronosEmployeeSummaryView>>(await _empSummaryManager.GetEmployeeDetails(vm.Cases.Select(x => x.PersonNumber).ToList()));
                        vm.DisplayLevel = 3;
                        break;
                    case "C":
                        vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                        vm.MessageType = MessageType.Error;
                        break;
                }
            }

            ViewBag.UpdateError = TempData["error"] != null;

            return View(vm);
        }

        [HttpGet]
        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 8 })]
        public async Task<PartialViewResult> _employeeSearch(string crit)
        {
            var searchResult = new List<KronosEmployeeSummaryView>();
            if (crit.Length != 0)
            {
                searchResult = mapper.Map<List<KronosEmployeeSummaryView>>(await _empSummaryManager.GetEmployeeMatchingName(crit));
            }
            return PartialView("../RFTPTracking/Partials/_employeeSearch", searchResult);
        }

        [HttpPost]
        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 8 })]
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
        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 8 })]
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
        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 8 })]
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
        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 8 })]
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

        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 8 })]
        public async Task<ActionResult> ManagerTrend()
        {
            RFTPManagerTrendVm vm = new RFTPManagerTrendVm();

            if (System.Web.HttpContext.Current.Session["_AccessLevel"].ToString() == "2")
            {
                vm.Cases = mapper.Map<List<Last12MonthRFTPCasesView>>(await _RFTPTrackingManager.GetLast12MonthRFTPCasesSWAS(System.Web.HttpContext.Current.Session["_SWASGMStore"].ToString()));
                vm.PeriodList = mapper.Map<List<Last12MonthListView>>(await _weeksManager.GetLast12MonthList());
                vm.EmployeeList = mapper.Map<List<KronosEmployeeSummaryView>>(await _empSummaryManager.GetEmployeeDetails(vm.Cases.GroupBy(x => x.PersonNumber).Select(x => x.Key).ToList()));
                vm.DisplayLevel = 2;
            }
            else
            {
                if (selectArea == "S" || selectArea == "R")
                {
                    vm.Cases = mapper.Map<List<Last12MonthRFTPCasesView>>(selectArea == "S" ? await _RFTPTrackingManager.GetLast12MonthRFTPCasesStore(selectCrit) : await _RFTPTrackingManager.GetLast12MonthRFTPCasesRegion(selectCrit));
                    vm.PeriodList = mapper.Map<List<Last12MonthListView>>(await _weeksManager.GetLast12MonthList());
                    vm.EmployeeList = mapper.Map<List<KronosEmployeeSummaryView>>(await _empSummaryManager.GetEmployeeDetails(vm.Cases.GroupBy(x => x.PersonNumber).Select(x => x.Key).ToList()));
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
            }

            return View(vm);
        }

        [UserFilter(MinLevel = 2, ExcludeLevels = new[] { 8 })]
        public async Task<ActionResult> ManagerDetail(string personNum)
        {
            if (personNum == null)
                return RedirectToAction("ManagerTracking");

            RFTPManagerDetailVm vm = new RFTPManagerDetailVm
            {
                caseDetails = mapper.Map<List<RFTPCaseStubView>>(await _RFTPTrackingManager.GetAllCasesForPerson(personNum))
            };

            if (vm.caseDetails == null)
            {
                vm.Message = "No case details for the selected colleague.";
                vm.MessageType = MessageType.Error;
            }

            return View(vm);
        }

        public async Task<ActionResult> IDD(string personNum)
        {
            if(personNum == null)
            {
                return RedirectToAction("ManagerTracking");
            }

            IDDVm vm = new IDDVm();

            vm.historicCases = mapper.Map<List<RFTPCaseStubView>>(await _RFTPTrackingManager.GetAllCasesForPerson(personNum));
            if (vm.historicCases.Any())
            {
                var lastCase = vm.historicCases.OrderByDescending(x => x.Year).ThenByDescending(x => x.Period).First();
                vm.lastPeriod = mapper.Map<CompSummaryView>((await _dashDataManager.GetCompSummaryStore(lastCase.Year, (byte)lastCase.Period, lastCase.StoreNumber.ToString())).FirstOrDefault(x => x.WeekNumber == null));
                vm.empDetails = mapper.Map<KronosEmployeeSummaryView>(await _empSummaryManager.GetEmployeeMatchingNumber(personNum));
            }

            return View(vm);
        }

        [Authorize]
        public async Task<PartialViewResult> _getNotifications()
        {
            HttpContext.Session.Remove("_RFTPpopup");
            return PartialView("~/Areas/WFM/Views/RFTPTracking/Partials/_rftpNotifications.cshtml", mapper.Map<RFTPNotificationsView>(await _RFTPTrackingManager.GetRFTPNotifications(User.Identity.Name, System.Web.HttpContext.Current.Session["_EmpNum"].ToString())));
        }
    }
}