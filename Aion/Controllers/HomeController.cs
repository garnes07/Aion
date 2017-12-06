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

        public ActionResult Index()
        {
            //var data = await _storeManager.GetBranchMenu(1, "1877");

            //var vm = new HMenu(data, "S_1877");
            //System.Web.HttpContext.Current.Session.Add("_StoreMenu", vm);

            return View();
        }

        //public ActionResult Test(string a)
        //{
        //    var _menu = (HMenu)System.Web.HttpContext.Current.Session["_StoreMenu"];

        //    ViewBag.content = _menu.menuSelect(a);

        //    return View("Index");
        //}

        public ActionResult GetMenu()
        {
            var _menu = (StoreMenu)System.Web.HttpContext.Current.Session["_storeMenu"];
            return Json(_menu.JsonString(0), JsonRequestBehavior.AllowGet);
            //return Json(_menu.Channels.First().nodes.First().nodes, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult UpOne()
        //{
        //    var _menu = (HMenu)System.Web.HttpContext.Current.Session["_StoreMenu"];
        //    var result = _menu.menuUpOne();

        //    return View("Index");
        //}
    }
}