using Aion.Areas.WFM.Models.RFTP;
using Aion.Areas.WFM.ViewModels.RFTP;
using Aion.Controllers;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Aion.Areas.WFM.Controllers
{
    public class RFTPController : BaseController
    {
        private IDashboardDataManager _dashDataManager;
        private IWeeksManager _weeksManager;
        private IClockingDataManager _clockManager;

        public RFTPController()
        {
            _dashDataManager = new DashboardDataManager();
            _clockManager = new ClockingDataManager();
            _weeksManager = new WeeksManager();
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

            if(vm.collection.Count > 0)
            {
                vm.selectedDate = string.Format("{0}_{1}", vm.collection.FirstOrDefault().Year, vm.collection.FirstOrDefault().Period);
                vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == vm.selectedDate);
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
                var _empCompliance = await _dashDataManager.GetComplianceDetail(selectCrit, weekNum);

                if (_empCompliance.Count() > 0)
                    vm.loadTimecardDetails(_empCompliance);
                if (vm._dashboardView.First().PunchCompliance < 0.9)
                    vm.loadPunchDetails(await _clockManager.GetClockDetailStore(selectCrit, weekNum));
                if (vm._dashboardView.First().ShortShifts > 0)
                    vm.loadSSDetails(await _dashDataManager.GetShortShiftDetail(selectCrit, weekNum));

                vm.DisplayLevel = 1;

                if (vm._dashboardView.Sum(x => x.PunchCompliance) == 0)
                    vm.MessageType = Aion.ViewModels.MessageType.Warning;
            }
            else if (selectArea == "R")
            {
                vm._dashboardView = await _dashDataManager.GetAllRegionDashData(selectCrit, weekNum);
                vm.DisplayLevel = 2;

                if (vm._dashboardView.Sum(x => x.PunchCompliance) == 0)
                    vm.MessageType = Aion.ViewModels.MessageType.Warning;
            }
            else if (selectArea == "D")
            {
                vm._chainView = await _dashDataManager.GetAllDivisionDashData(selectCrit, weekNum);
                vm.DisplayLevel = 3;

                if (vm._chainView.Sum(x => x.PunchCompliance) == 0)
                    vm.MessageType = Aion.ViewModels.MessageType.Warning;
            }
            else if (selectArea == "C")
            {
                vm._chainView = await _dashDataManager.GetAllChainDashData(selectCrit, weekNum);
                vm.DisplayLevel = 4;

                if (vm._chainView.Sum(x => x.PunchCompliance) == 0)
                    vm.MessageType = Aion.ViewModels.MessageType.Warning;
            }

            vm.SetWeeksOfYear(DateTime.Now.FirstDayOfWeek().AddDays(-7), await _weeksManager.GetMultipleWeeks(DateTime.Now.FirstDayOfWeek().AddDays(-56), DateTime.Now.FirstDayOfWeek().AddDays(-7).FirstDayOfWeek()));
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekNum.ToString());

            return View(vm);
        }
    }
}