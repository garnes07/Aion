using Aion.Areas.WFM.ViewModels.Minerva;
using Aion.Controllers;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Models.WFM;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.WFM.Controllers
{
    [Authorize]
    public class MinervaController : BaseController
    {
        private readonly IAuthManager _authManager;
        private readonly IStoreManager _storeManager;

        public MinervaController()
        {
            _authManager = new AuthManager();
            _storeManager = new StoreManager();
        }

        // GET: WFM/Minerva
        public async Task<ActionResult> Index()
        {
            if (!await _authManager.CheckMinervaAccess(User.Identity.Name))
            {
                return RedirectToAction("Unauthorised", "Profile", new { area = "" });
            }

            MinervaVm vm = new MinervaVm();
            vm.collection = mapper.Map<List<StoreLocationsView>>(await _storeManager.GetAllStoreLocations());

            return View(vm);
        }
    }
}