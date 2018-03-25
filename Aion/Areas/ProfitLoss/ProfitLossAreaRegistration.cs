using System.Web.Mvc;

namespace Aion.Areas.ProfitLoss
{
    public class ProfitLossAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProfitLoss";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProfitLoss_default",
                "ProfitLoss/{controller}/{action}/{id}",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional },
                new string[] { "Aion.Areas.ProfitLoss.Controllers" }
            );
        }
    }
}