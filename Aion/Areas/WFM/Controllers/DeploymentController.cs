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

namespace Aion.Areas.WFM.Controllers
{
    public class DeploymentController : BaseController
    {
        private readonly IDashboardDataManager _dashDataManager;
        private readonly IWeeksManager _weeksManager;
        private readonly IFootfallManager _footfallManager;

        public DeploymentController()
        {
            _dashDataManager = new DashboardDataManager();
            _weeksManager = new WeeksManager();
            _footfallManager = new FootfallManager();
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

            vm.SetWeeksOfYear(DateTime.Now.FirstDayOfWeek().AddDays(-7), await _weeksManager.GetMultipleWeeks(DateTime.Now.FirstDayOfWeek().AddDays(-56), DateTime.Now.FirstDayOfWeek().AddDays(-7).FirstDayOfWeek()));
            vm.WeeksOfYear.ForEach(x => x.Selected = x.Value == weekNum.ToString());

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
    }
}