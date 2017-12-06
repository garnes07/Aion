using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Aion
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
        }

        void Session_Start(object sender, EventArgs e)
        {
            var init = Helpers.MvcHelper.InitStoreDetails();
            
            //if (store.IpRange == null)
            //    Response.Redirect("/Profile/UnknownStore");
            //if (store.IpRange == "DUPLICATE")
            //    Response.Redirect("Profile/DuplicateRecords");            
            //if (Session.IsNewSession)
            //    Response.Redirect("/");
        }

        void Session_End(object sender, EventArgs E)
        {
            Helpers.MvcHelper.LogOut();
        }
    }
}
