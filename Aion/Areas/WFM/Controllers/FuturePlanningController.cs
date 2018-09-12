using Aion.Areas.WFM.Models.FuturePlanning;
using Aion.Areas.WFM.ViewModels.FuturePlanning;
using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.WFM.Controllers
{
    [UserFilter(MinLevel=3)]
    public class FuturePlanningController : BaseController
    {
        private readonly IContractStatusManager _contractStatusManager;
        private readonly IDashboardDataManager _dashboardManager;

        public FuturePlanningController()
        {
            _contractStatusManager = new ContractStatusManager();
            _dashboardManager = new DashboardDataManager();
        }

        public async Task<ActionResult> RecruitmentStatus()
        {
            RecruitmentStatusVm vm = new RecruitmentStatusVm();

            switch (selectArea)
            {
                case "S":
                    vm.collection = await _contractStatusManager.GetContractStatusStore(selectCrit);
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.collection = await _contractStatusManager.GetContractStatusRegion(selectCrit);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.collection = await _contractStatusManager.GetContractStatusDivision(selectCrit);
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.collection = await _contractStatusManager.GetContractStatusChain(selectCrit);
                    vm.DisplayLevel = 4;
                    break;
            }

            return View(vm);
        }

        public async Task<ActionResult> DeploymentStatus(bool adv = false)
        {
            DeploymentStatusVm vm = new DeploymentStatusVm();

            switch (selectArea)
            {
                case "S":
                    vm.collection = await _dashboardManager.GetDeploymentStatusStore(selectCrit);
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.collection = await _dashboardManager.GetDeploymentStatusRegion(selectCrit);
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.collection = await _dashboardManager.GetDeploymentStatusDivision(selectCrit);
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.collection = await _dashboardManager.GetDeploymentStatusChain(selectCrit);
                    vm.DisplayLevel = 4;
                    break;
            }

            ViewBag.adv = adv.ToString().ToLower();
            return View(vm);
        }

        public async Task<ActionResult> DeploymentSwings()
        {
            DeploymentSwingsVm vm = new DeploymentSwingsVm();

            switch (selectArea)
            {
                case "S":
                    vm.collection = mapper.Map<List<DeploymentSwingView>>(await _dashboardManager.GetSOHSwingsStore(selectCrit));
                    vm.DisplayLevel = 1;
                    break;
                case "R":
                    vm.collection = mapper.Map<List<DeploymentSwingView>>(await _dashboardManager.GetSOHSwingsRegion(selectCrit));
                    vm.DisplayLevel = 2;
                    break;
                case "D":
                    vm.collection = mapper.Map<List<DeploymentSwingView>>(await _dashboardManager.GetSOHSwingsDivision(selectCrit));
                    vm.DisplayLevel = 3;
                    break;
                case "C":
                    vm.collection = mapper.Map<List<DeploymentSwingView>>(await _dashboardManager.GetSOHSwingsChain(selectCrit));
                    vm.DisplayLevel = 4;
                    break;
            }

            return View(vm);
        }
    }
}