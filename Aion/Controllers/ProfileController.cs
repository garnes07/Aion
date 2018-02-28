using Aion.Helpers;
using Aion.Models.Utils;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aion.Services;

namespace Aion.Controllers
{
    public class ProfileController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(bool o = false)
        {
            ViewBag.ReturnUrl = Request.UrlReferrer.AbsolutePath;
            if (!o)
            {
                ViewBag.SecureCheck = true;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVm a)
        {
            if (!ModelState.IsValid)
            {
                if(string.Equals(Request.UrlReferrer.AbsolutePath, "/oos", StringComparison.CurrentCultureIgnoreCase))
                {
                    TempData["modelPass"] = a;
                    return RedirectToAction("OOS");
                }
                return View();
            }

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AuthenticationService(authenticationManager);

            var authenticationResult = await authService.SignIn(a.Username, a.Password);
            if (authenticationResult.IsSuccess)
            {
                if (!string.IsNullOrEmpty(a.ReturnURL) && Url.IsLocalUrl(a.ReturnURL))
                {
                    return Redirect(a.ReturnURL);
                }

                return RedirectToAction("Index", "Home");
            }

            if (string.Equals(Request.UrlReferrer.AbsolutePath, "/oos", StringComparison.CurrentCultureIgnoreCase))
            {
                TempData["modelPass"] = a;
                TempData["errorMessage"] = authenticationResult.ErrorMessage;
                return RedirectToAction("Index", "OOS");
            }
            ViewBag.errorMessage = authenticationResult.ErrorMessage;
            ModelState.AddModelError("", authenticationResult.ErrorMessage);
            return View(a);
        }

        public ActionResult Unauthorised()
        {
            return View();
        }

        public ActionResult Logoff()
        {
            MvcHelper.LogOut();

            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult GetMenu()
        {
            StoreMenu _menu = (StoreMenu)System.Web.HttpContext.Current.Session["_storeMenu"];
            return Json(_menu.JsonString(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult MenuSelect(string a)
        {
            StoreMenu _menu = (StoreMenu)System.Web.HttpContext.Current.Session["_storeMenu"];

            bool result = _menu.menuSelect(a);
            if (!result)
            {
                ViewBag.menuError = true;
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [Authorize]
        public ActionResult MenuReset()
        {
            StoreMenu _menu = (StoreMenu)System.Web.HttpContext.Current.Session["_storeMenu"];

            bool result = _menu.menuReset();
            if (!result)
            {
                ViewBag.menuError = true;
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [Authorize]
        public ActionResult MenuUpOne()
        {
            StoreMenu _menu = (StoreMenu)System.Web.HttpContext.Current.Session["_storeMenu"];

            bool result = _menu.menuUpOne();
            if (!result)
            {
                ViewBag.menuError = true;
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UnknownStore()
        {
            return View();
        }

        public ActionResult RegisterStore()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterStore(int storeNumber)
        {
            var authService = new AuthenticationService();

            var result = await authService.RegisterUnknownStore(storeNumber);

            if (!result)
            {
                ViewBag.Error = "Oops, something went wrong there, please try again";
                return View();
            }
            else
            {
                return View("Confirmed");
            }            
        }

        public async Task<ActionResult> DuplicateRecords()
        {
            var authService = new AuthenticationService();

            return View(await authService.AllStoresMatchingIp());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DuplicateRecords(short storeNumber)
        {
            var authService = new AuthenticationService();
            var result = await authService.RegisterStoreFullIP(storeNumber);

            if (!result)
            {
                ViewBag.Error = "Oops, something went wrong there, please try again";
                return View();
            }
            else
            {
                MvcHelper.LogOut();
                return RedirectToAction("Index", "Home");
            }            
        }
    }
}