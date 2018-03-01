using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aion.Attributes;

namespace Aion.Areas.WFM.Controllers
{
    public class RFTPTrackingController : Controller
    {
        [UserFilter(MinLevel = 2, ExcludeLevels = new []{7})]
        public async Task<ActionResult> ManagerTracking()
        {


            return View();
        }
    }
}