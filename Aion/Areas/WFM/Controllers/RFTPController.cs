using Aion.Areas.WFM.Models.RFTP;
using Aion.Areas.WFM.ViewModels.RFTP;
using Aion.Controllers;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Helpers;
using Aion.Models.Kronos;
using Aion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.WFM.Controllers
{
    public class RFTPController : BaseController
    {
        private readonly IDashboardDataManager _dashDataManager;
        private readonly IWeeksManager _weeksManager;
        private readonly IClockingDataManager _clockManager;
        private readonly IKronosManager _kronosManager;
        private readonly ITicketManager _ticketManager;
        private readonly IStoreManager _storeManager;
        private readonly IEmpSummaryManager _empSummaryManager;
        private readonly IEditedClockManager _editedClockManager;
        private readonly IPayCalendarManager _payCalendarManager;
        private readonly IAvlbltyManager _avlbltyManager;

        public RFTPController()
        {
            _dashDataManager = new DashboardDataManager();
            _clockManager = new ClockingDataManager();
            _weeksManager = new WeeksManager();
            _kronosManager = new KronosManager();
            _ticketManager = new TicketManager();
            _storeManager = new StoreManager();
            _empSummaryManager = new EmpSummaryManager();
            _editedClockManager = new EditedClockManager();
            _payCalendarManager = new PayCalendarManager();
            _avlbltyManager = new AvlbltyManager();
        }
        
        //Summary
        public async Task<ActionResult> Summary(string c = "e_0")
        {
            string[] input = c.Split('_');
            byte period = byte.Parse(input[1]);
            CompSummaryVm vm = new CompSummaryVm();

            switch (selectArea)
            {
                case "S":
                    vm.collection = mapper.Map<List<CompSummaryView>>(await _dashDataManager.GetCompSummaryStore(input[0], period, selectCrit));
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.collection = mapper.Map<List<CompSummaryView>>(await _dashDataManager.GetCompSummaryRegion(input[0], period, selectCrit));
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.collection = mapper.Map<List<CompSummaryView>>(await _dashDataManager.GetCompSummaryDivision(input[0], period, selectCrit));
                    vm.priority = selectCrit;
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.collection = mapper.Map<List<CompSummaryView>>(await _dashDataManager.GetCompSummary(input[0], period, selectCrit));
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
        
        //Detail
        public async Task<ActionResult> Detail(string selectedDate = "Last Week")
        {
            CompDetailVm vm = new CompDetailVm();
            int weekNum = selectedDate.GetWeekNumber();

            switch (selectArea)
            {
                case "S":
                    vm._dashboardView = await _dashDataManager.GetStoreDashData(selectCrit, weekNum);
                    var empCompliance = await _dashDataManager.GetComplianceDetail(selectCrit, weekNum);

                    if (empCompliance.Any())
                        vm.loadTimecardDetails(empCompliance);
                    if (vm._dashboardView.Any(x => x.PunchCompliance < 0.9))
                        vm.loadPunchDetails(await _clockManager.GetClockDetailStore(selectCrit, weekNum));
                    if (vm._dashboardView.Any(x => x.ShortShifts > 0))
                        vm.loadSSDetails(await _editedClockManager.GetEditedClocksStore(selectCrit, weekNum));

                    vm.DisplayLevel = 1;

                    if (vm._dashboardView.Sum(x => x.TotalHeadCount) == 0)
                        vm.MessageType = MessageType.Warning;
                    break;
                case "R":
                    vm._dashboardView = await _dashDataManager.GetAllRegionDashData(selectCrit, weekNum);
                    vm.DisplayLevel = 2;

                    if (vm._dashboardView.Sum(x => x.TotalHeadCount) == 0)
                        vm.MessageType = MessageType.Warning;
                    break;
                case "D":
                    vm._chainView = await _dashDataManager.GetAllDivisionDashData(selectCrit, weekNum);
                    vm.DisplayLevel = 3;

                    if (vm._chainView.Sum(x => x.TotalHeadCount) == 0)
                        vm.MessageType = MessageType.Warning;
                    break;
                case "C":
                    vm._chainView = await _dashDataManager.GetAllChainDashData(selectCrit, weekNum);
                    vm.DisplayLevel = 4;

                    if (vm._chainView.Sum(x => x.TotalHeadCount) == 0)
                        vm.MessageType = MessageType.Warning;
                    break;
            }

            vm.SetWeeksOfYear(DateTime.Now.FirstDayOfWeek().AddDays(-7), await _weeksManager.GetMultipleWeeks(DateTime.Now.FirstDayOfWeek().AddDays(-84), DateTime.Now.FirstDayOfWeek().AddDays(-7).FirstDayOfWeek()));
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekNum.ToString());

            return View(vm);
        }

        //Timecard Sign Off
        public async Task<ActionResult> TimecardSignOff()
        {
            TimecardSignOffVm vm = new TimecardSignOffVm();

            switch (selectArea)
            {
                case "S":
                    var weekOfYr = (int)_weeksManager.GetSingleWeek(vm.weekStart);
                    string storeName = await _storeManager.GetKronosName(selectCrit);
                    vm.hf = await _kronosManager.GetKronosHyperFind(storeName, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString(), System.Web.HttpContext.Current.Session.SessionID);
                    vm.ss = await _editedClockManager.GetEditedClocksStore(selectCrit, weekOfYr);
                    vm.HelpTcks = await _ticketManager.GetHelpTickets(selectCrit);

                    short a = 1;
                    while ((vm.hf == null || !vm.hf.Any()) && a < 3)
                    {
                        vm.hf = await _kronosManager.GetKronosHyperFind(storeName, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString(), System.Web.HttpContext.Current.Session.SessionID);
                        a++;
                    }
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    vm.MessageType = MessageType.Error;
                    break;
                case "C":
                    vm.Message = "This page is not available in the currently selected view, please select a store from the top right menu or go back.";
                    vm.MessageType = MessageType.Error;
                    break;
            }

            return View(vm);
        }

        public async Task<PartialViewResult> _regionTCSignOff()
        {
            TimecardSignOffVm vm = new TimecardSignOffVm();

            var storeList = await _storeManager.GetStoresInRegion(selectCrit);
            //vm.hf = await _kronosManager.GetKronosHyperFind(storeList, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString(), System.Web.HttpContext.Current.Session.SessionID);
            vm.hf = await _kronosManager.GetKronosHyperFindBatch(storeList, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString(), System.Web.HttpContext.Current.Session.SessionID);
            var empList = await _empSummaryManager.GetAllByRegion(selectCrit);
            var punched = await _kronosManager.GetPunchStatus(empList.Where(x => x.KronosUser).Select(x => x.PersonNumber).ToList(), System.Web.HttpContext.Current.Session.SessionID);
            var punchCombined = empList.Where(x => x.KronosUser).Join(punched, db => db.PersonNumber, kronos => kronos.Employee.PersonIdentity.PersonNumber, (db, kronos) => new { PersonNumer = db.PersonNumber, BranchNumber = db.HomeBranch, punched = kronos.Status }).ToList();

            vm.rso = new List<RegionSignOff>();
            foreach (var item in storeList.ToList())
            {
                var data = vm.hf.Where(x => x.storeNumber == item.StoreNumber).ToList();
                if (data.Any())
                {
                    bool userScheduled = empList.Any(x => x.HomeBranch == item.StoreNumber && x.Scheduled && x.KronosUser);
                    bool userPunched = punchCombined.Any(x => x.BranchNumber == item.StoreNumber && x.punched == "In");

                    vm.rso.Add(new RegionSignOff { BranchNumber = item.StoreNumber, BranchName = item.StoreName, Headcount = data.Count(), SignedOff = data.Count(x => x.PersonData.Person.SignOffDate.Date >= vm.weekStart), KronosScheduled = userScheduled, KronosPunched = userPunched });
                }
            }

            vm.DisplayLevel = 2;

            return PartialView("~/Areas/WFM/Views/RFTP/Partials/_regionTCSignOff.cshtml", vm);
        }

        [HttpPost]
        public ActionResult RaiseTicket(string empId, string formType)
        {
            TempData["empId"] = int.Parse(empId.Replace("UK", ""));

            return RedirectToAction("NewSubmission", "Form", new { area = "Workflow", FormTypeId = formType });
        }

        public async Task<ActionResult> ClockingCompliance(string selectedDate = "Last Week")
        {
            ClockCompVm vm = new ClockCompVm();
            int weekNum = selectedDate.GetWeekNumber();

            switch (selectArea)
            {
                case "S":
                    vm.PunchDetail = await _clockManager.GetClockDetailStore(selectCrit, weekNum);
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.PunchDetail = await _clockManager.GetClockDetailRegion(selectCrit, weekNum);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.PunchDetail = await _clockManager.GetClockDetailDivision(selectCrit, weekNum);
                    vm.Priority = selectCrit;
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.PunchDetail = await _clockManager.GetClockDetailChain(selectCrit, weekNum);
                    vm.DisplayLevel = 4;
                    break;
            }

            vm.SetWeeksOfYear(DateTime.Now.FirstDayOfWeek().AddDays(-7), await _weeksManager.GetMultipleWeeks(DateTime.Now.FirstDayOfWeek().AddDays(-56), DateTime.Now.FirstDayOfWeek().AddDays(-7).FirstDayOfWeek()));
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekNum.ToString());

            if (!vm.PunchDetail.Any())
            {
                vm.MessageType = MessageType.Error;
                vm.Message = "Reporting for the selected period has not yet been finalised, check back later.";
            }

            return View(vm);
        }

        public async Task<ActionResult> ClockingBehaviours()
        {
            ClockBehavioursVm vm = new ClockBehavioursVm();

            switch (selectArea)
            {
                case "S":
                    vm.PunchDetail = await _clockManager.GetClockTrendStore(selectCrit);
                    vm.RepeatEmployeeDetail = await _clockManager.GetRepeatOffendersStore(selectCrit);
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.PunchDetail = await _clockManager.GetClockTrendRegion(selectCrit);
                    vm.RepeatEmployeeDetail = await _clockManager.GetRepeatOffendersRegion(selectCrit);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.PunchDetail = await _clockManager.GetClockTrendDivision(selectCrit);
                    vm.RepeatEmployeeDetail = await _clockManager.GetRepeatOffendersDivision(selectCrit);
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.PunchDetail = await _clockManager.GetClockTrendChain(selectCrit);
                    vm.RepeatStoresDetail = await _clockManager.GetRepeatOffendersChain(selectCrit);
                    vm.DisplayLevel = 4;
                    break;
            }

            if (!vm.PunchDetail.Any())
            {
                vm.MessageType = MessageType.Error;
                vm.Message = "Reporting for the selected period has not yet been finalised, check back later.";
            }

            return View(vm);
        }

        public async Task<ActionResult> EditedClocks(string selectedDate = "Last Week")
        {
            EditedClocksVm vm = new EditedClocksVm();
            int weekNum = selectedDate.GetWeekNumber();

            switch (selectArea)
            {
                case "S":
                    vm.StoreDetail = await _editedClockManager.GetEditedClocksStore(selectCrit, weekNum);
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.AggregateDetail = await _editedClockManager.GetEditedClocksRegion(selectCrit, weekNum);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.AggregateDetail = await _editedClockManager.GetEditedClocksDivision(selectCrit, weekNum);
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.AggregateDetail = await _editedClockManager.GetEditedClocksChain(selectCrit, weekNum);
                    vm.DisplayLevel = 4;
                    break;
            }

            vm.SetWeeksOfYear(DateTime.Now.FirstDayOfWeek().AddDays(-7), await _weeksManager.GetMultipleWeeks(DateTime.Now.FirstDayOfWeek().AddDays(-56), DateTime.Now.FirstDayOfWeek().AddDays(-7).FirstDayOfWeek()));
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekNum.ToString());

            if (selectArea != "S" && !vm.AggregateDetail.Any())
            {
                vm.MessageType = MessageType.Error;
                vm.Message = "Reporting for the selected period has not yet been finalised, check back later.";
            }

            return View(vm);
        }        

        public ActionResult Guide()
        {
            return View();
        }

        public ActionResult Fallback()
        {
            return View();
        }
    }
}