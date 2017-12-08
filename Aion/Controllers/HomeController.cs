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
    public class HomeController : BaseController
    {
        private readonly IStoreManager _storeManager;

        public HomeController()
        {
            _storeManager = new StoreManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

    }
}