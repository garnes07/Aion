using Aion.DAL.Entities;
using Aion.Helpers;
using Aion.Mapping;
using AutoMapper;
using System.Web.Mvc;

namespace Aion.Controllers
{
    public class BaseController : Controller
    {
        protected StoreMaster _store = System.Web.HttpContext.Current.GetSessionObject<StoreMaster>("_store");
        private readonly string[] _selection = System.Web.HttpContext.Current.Session["_menuSelection"] != null ? System.Web.HttpContext.Current.Session["_menuSelection"].ToString().Split('_') : new[] { "e", "e" };
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

            if (_store == null)
                filterContext.Result = new RedirectResult(Url.Action("UnknownStore", "Profile"));
        }
    }
}