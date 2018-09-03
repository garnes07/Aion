using Aion.Helpers;
using Aion.Mapping;
using Aion.Models.Utils;
using AutoMapper;
using System.Web.Mvc;

namespace Aion.Controllers
{
    public class BaseController : Controller
    {
        protected StoreStub _store = System.Web.HttpContext.Current.GetSessionObject<StoreStub>("_store");
        protected string[] _selection = System.Web.HttpContext.Current.Session["_menuSelection"] != null ? System.Web.HttpContext.Current.Session["_menuSelection"].ToString().Split('_') : new[] { "e", "e" };
        protected IMapper mapper;
        private MapperConfiguration config = AutoMapperWebConfiguration.MapperConfig;

        protected string selectArea => _selection[0];

        protected string selectCrit => _selection[1];

        public BaseController()
        {
            mapper = config.CreateMapper();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (selectArea == "e")
                filterContext.Result = new RedirectResult(Url.Action("UnknownStore", "Profile"));
        }

        public string CheckStoreAuth(string s)
        {
            StoreMenu _menu = (StoreMenu)System.Web.HttpContext.Current.Session["_storeMenu"];

            var result = _menu.selectCheck(s);
            if (result != "e")
            {
                _selection = s.Split('_');
            }

            return result;
        }
    }
}