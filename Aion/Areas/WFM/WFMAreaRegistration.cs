using System.Web.Mvc;

namespace Aion.Areas.WFM
{
    public class WFMAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WFM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WFM_default",
                "WFM/{controller}/{action}/{id}",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional },
                new string[] {"Aion.Areas.WFM.Controllers"}
            );
        }
    }
}