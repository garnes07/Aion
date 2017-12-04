using Aion.DAL.IManagers;
using Aion.DAL.Managers;
using Aion.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Aion.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreManager _storeManager;

        public HomeController()
        {
            _storeManager = new StoreManager();
        }

        public async Task<ActionResult> Index()
        {
            var data = await _storeManager.GetBranchMenu(4, "SAS");

            var vm = new HMenu(data);
            System.Web.HttpContext.Current.Session.Add("_StoreMenu", vm);

            return View();
        }

        public ActionResult Test(string a)
        {
            var _menu = (HMenu)System.Web.HttpContext.Current.Session["_StoreMenu"];
            
            ViewBag.content = _menu.newSelection(a);

            return View("Index");
        }

        public ActionResult GetMenu()
        {
            var _menu = (HMenu)System.Web.HttpContext.Current.Session["_StoreMenu"];
            return Json(_menu.Channels, JsonRequestBehavior.AllowGet);
        }
    }
}