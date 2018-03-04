using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Aion.Areas.WFM.ViewModels.Deployment;
using Aion.Controllers;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;

namespace Aion.Areas.WFM.Controllers
{
    public class DeploymentController : BaseController
    {
        private readonly IDashboardDataManager _dashDataManager;

        public DeploymentController()
        {
            _dashDataManager = new DashboardDataManager();
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
    }
}