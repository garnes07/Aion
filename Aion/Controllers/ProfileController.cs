using Aion.Helpers;
using Aion.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Aion.Controllers
{
    public class ProfileController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Login(bool o = false)
        {
            ViewBag.ReturnUrl = Request.QueryString["returnUrl"];
            if (!o)
            {
                ViewBag.SecureCheck = true;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVm a, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                if(string.Equals(Request.UrlReferrer.AbsolutePath.ToString(), "/oos", StringComparison.CurrentCultureIgnoreCase))
                {
                    TempData["modelPass"] = a;
                    return RedirectToAction("OOS");
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logoff()
        {
            MvcHelper.LogOut();

            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult GetMenu()
        {
            StoreMenu _menu = (StoreMenu)System.Web.HttpContext.Current.Session["_storeMenu"];
            return Json(_menu.JsonString(0), JsonRequestBehavior.AllowGet);
        }

        //[Authorize]
        public ActionResult MenuSelect(string a)
        {
            StoreMenu _menu = (StoreMenu)System.Web.HttpContext.Current.Session["_storeMenu"];

            bool result = _menu.menuSelect(a);
            if (!result)
            {
                ViewBag.menuError = true;
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //[Authorize]
        public ActionResult MenuReset()
        {
            StoreMenu _menu = (StoreMenu)System.Web.HttpContext.Current.Session["_storeMenu"];

            bool result = _menu.menuReset();
            if (!result)
            {
                ViewBag.menuError = true;
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //[Authorize]
        public ActionResult MenuUpOne()
        {
            StoreMenu _menu = (StoreMenu)System.Web.HttpContext.Current.Session["_storeMenu"];

            bool result = _menu.menuUpOne();
            if (!result)
            {
                ViewBag.menuError = true;
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}