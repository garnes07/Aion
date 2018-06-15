using Aion.Areas.WFM.ViewModels.Minerva;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aion.Areas.WFM.Controllers
{
    [Authorize]
    public class MinervaController : Controller
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
            vm.collection = await _storeManager.GetAllStoreLocations();

            return View(vm);
        }
    }
}