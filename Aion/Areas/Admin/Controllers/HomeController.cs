using Aion.Attributes;
using Aion.Controllers;
using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using System.Web.Mvc;

namespace Aion.Areas.Admin.Controllers
{
    [UserFilter(MinLevel = 9)]
    public class HomeController : BaseController
    {
        private readonly IStoreManager _storeManager;

        public HomeController()
        {
            _storeManager = new StoreManager();
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}