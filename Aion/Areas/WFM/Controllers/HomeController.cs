using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aion.Areas.WFM.Controllers
{
    public class HomeController : Controller
    {
        // GET: WFM/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}