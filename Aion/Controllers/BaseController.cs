using Aion.DAL.Entities;
using Aion.Helpers;
using System.Web.Mvc;

namespace Aion.Controllers
{
    public class BaseController : Controller
    {
        protected StoreMaster _store = System.Web.HttpContext.Current.GetSessionObject<StoreMaster>("_store");
        //protected IMapper mapper;
        //private MapperConfiguration config = AutoMapperWebConfiguration.MapperConfig;

        //public BaseController()
        //{
        //    mapper = config.CreateMapper();
        //}

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (_store == null)
                filterContext.Result = new RedirectResult(Url.Action("UnknownStore", "Profile"));
        }
    }
}