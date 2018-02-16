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

        public RFTPController()
        {
            _dashDataManager = new DashboardDataManager();
            _clockManager = new ClockingDataManager();
            _weeksManager = new WeeksManager();
            _kronosManager = new KronosManager();
            _ticketManager = new TicketManager();
            _storeManager = new StoreManager();
            _empSummaryManager = new EmpSummaryManager();
        }
        
        //Summary
        public async Task<ActionResult> Summary(string c = "e_0")
        {
            string[] input = c.Split('_');
            byte period = byte.Parse(input[1]);
            CompSummaryVm vm = new CompSummaryVm();

            if(selectArea == "S")
            {
                vm.collection = mapper.Map<List<CompSummaryView>>(await _dashDataManager.GetCompSummaryStore(input[0], period, selectCrit));
                vm.DisplayLevel = 2;
            }
            else if (selectArea == "R")
            {
                vm.collection = mapper.Map<List<CompSummaryView>>(await _dashDataManager.GetCompSummaryRegion(input[0], period, selectCrit));
                vm.DisplayLevel = 2;
            }
            else if(selectArea == "D")
            {
                vm.collection = mapper.Map<List<CompSummaryView>>(await _dashDataManager.GetCompSummaryDivision(input[0], period, selectCrit));
                vm.DisplayLevel = 3;
            }
            else if (selectArea == "C")
            {
                vm.collection = mapper.Map<List<CompSummaryView>>(await _dashDataManager.GetCompSummary(input[0], period, selectCrit));
                vm.DisplayLevel = 3;
            }

            if (vm.collection.Any())
            {
                vm.selectedDate = string.Format("{0}_{1}", vm.collection.FirstOrDefault().Year,
                    vm.collection.FirstOrDefault().Period);
                vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == vm.selectedDate);

                return View(vm);
            }

            return View(vm);
        }
        
        //Detail
        public async Task<ActionResult> Detail(string selectedDate = "Last Week")
        {
            CompDetailVm vm = new CompDetailVm();
            int weekNum = selectedDate.GetWeekNumber();

            if(selectArea == "S")
            {
                vm._dashboardView = await _dashDataManager.GetStoreDashData(selectCrit, weekNum);
                var empCompliance = await _dashDataManager.GetComplianceDetail(selectCrit, weekNum);

                if (empCompliance.Any())
                    vm.loadTimecardDetails(empCompliance);
                if (vm._dashboardView.First().PunchCompliance < 0.9)
                    vm.loadPunchDetails(await _clockManager.GetClockDetailStore(selectCrit, weekNum));
                if (vm._dashboardView.First().ShortShifts > 0)
                    vm.loadSSDetails(await _dashDataManager.GetShortShiftDetail(selectCrit, weekNum));

                vm.DisplayLevel = 1;

                if (vm._dashboardView.Sum(x => x.PunchCompliance) == 0)
                    vm.MessageType = MessageType.Warning;
            }
            else if (selectArea == "R")
            {
                vm._dashboardView = await _dashDataManager.GetAllRegionDashData(selectCrit, weekNum);
                vm.DisplayLevel = 2;

                if (vm._dashboardView.Sum(x => x.PunchCompliance) == 0)
                    vm.MessageType = MessageType.Warning;
            }
            else if (selectArea == "D")
            {
                vm._chainView = await _dashDataManager.GetAllDivisionDashData(selectCrit, weekNum);
                vm.DisplayLevel = 3;

                if (vm._chainView.Sum(x => x.PunchCompliance) == 0)
                    vm.MessageType = MessageType.Warning;
            }
            else if (selectArea == "C")
            {
                vm._chainView = await _dashDataManager.GetAllChainDashData(selectCrit, weekNum);
                vm.DisplayLevel = 4;

                if (vm._chainView.Sum(x => x.PunchCompliance) == 0)
                    vm.MessageType = MessageType.Warning;
            }

            vm.SetWeeksOfYear(DateTime.Now.FirstDayOfWeek().AddDays(-7), await _weeksManager.GetMultipleWeeks(DateTime.Now.FirstDayOfWeek().AddDays(-56), DateTime.Now.FirstDayOfWeek().AddDays(-7).FirstDayOfWeek()));
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekNum.ToString());

            return View(vm);
        }

        //Timecard Sign Off
        public async Task<ActionResult> TimecardSignOff()
        {
            TimecardSignOffVm vm = new TimecardSignOffVm();
            var weekOfYr = (int)_weeksManager.GetSingleWeek(vm.weekStart);

            if (selectArea == "S")
            {
                string storeName = await _storeManager.GetKronosName(selectCrit);
                vm.hf = await _kronosManager.GetKronosHyperFind(storeName, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString(), System.Web.HttpContext.Current.Session.SessionID);
                vm.ss = await _dashDataManager.GetShortShiftDetail(selectCrit, weekOfYr);
                vm.HelpTcks = await _ticketManager.GetHelpTickets(selectCrit);

                short a = 1;
                while ((vm.hf == null || !vm.hf.Any()) && a < 3)
                {
                    vm.hf = mapper.Map<List<HyperFindResult>>(await _kronosManager.GetKronosHyperFind(storeName, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString(), System.Web.HttpContext.Current.Session.SessionID));
                    a++;
                }
                vm.DisplayLevel = 1;
            }
            else if (selectArea == "R")
            {
                string hfQuery = selectCrit.Length == 1 ? "IE Region " : "UK - Region CPW";
                vm.hf = mapper.Map<List<HyperFindResult>>(await _kronosManager.GetKronosHyperFind(hfQuery + selectCrit, vm.weekStart.ToShortDateString(), vm.weekStart.AddDays(6).ToShortDateString(), System.Web.HttpContext.Current.Session.SessionID));
                var empList = await _empSummaryManager.GetActiveByRegion(selectCrit);
                var combined = vm.hf.Where(x => x.PersonData.Person.SignOffDate.Year != 1901).Join(empList, kronos => kronos.PersonNumber, db => db.PersonNumber, (kronos, db) => new {kronos.PersonNumber, BranchNumber = db.HomeBranch, db.BranchName, signedOff = kronos.PersonData.Person.SignOffDate, db.PersonName }).ToList();
                var punched = await _kronosManager.GetPunchStatus(empList.Where(x => x.KronosUser).Select(x => x.PersonNumber).ToList(), System.Web.HttpContext.Current.Session.SessionID);
                var punchCombined = empList.Where(x => x.KronosUser).Join(punched, db => db.PersonNumber, kronos => kronos.Employee.PersonIdentity.PersonNumber, (db, kronos) => new { PersonNumer = db.PersonNumber, BranchNumber = db.HomeBranch, punched = kronos.Status }).ToList();

                vm.rso = new List<RegionSignOff>();
                foreach (var item in empList.GroupBy(x => x.HomeBranch).Select(c => c.Key).OrderBy(x => x).ToList())
                {
                    var data = combined.Where(x => x.BranchNumber == item).ToList();
                    if (data.Any())
                    {
                        bool userScheduled = empList.Any(x => x.HomeBranch == item && x.Scheduled && x.KronosUser);
                        bool userPunched = punchCombined.Any(x => x.BranchNumber == item && x.punched == "In");

                        vm.rso.Add(new RegionSignOff { BranchNumber = item, BranchName = data.First().BranchName, Headcount = data.Count(), SignedOff = data.Count(x => x.signedOff.Date >= vm.weekStart), KronosScheduled = userScheduled, KronosPunched = userPunched });
                    }
                }

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
    }
}