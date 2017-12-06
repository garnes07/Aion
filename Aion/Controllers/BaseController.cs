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
        //protected IMapper mapper;
        //private MapperConfiguration config = AutoMapperWebConfiguration.MapperConfig;

        //public BaseController()
        //{
        //    mapper = config.CreateMapper();
        //}
    }
}