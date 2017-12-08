using Aion.Mapping;
using System;
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
            AutoMapperWebConfiguration.Configure();

            AntiForgeryConfig.SuppressIdentityHeuristicChecks = true;
        }

        void Session_Start(object sender, EventArgs e)
        {
            var init = Helpers.MvcHelper.InitStoreDetails();

            if (init == 0)
                Response.Redirect("/Profile/UnknownStore");
            else if (init == -1)
                Response.Redirect("Profile/DuplicateRecords");

            //if (Session.IsNewSession)
            //    Response.Redirect("/");
        }

        void Session_End(object sender, EventArgs E)
        {
            Helpers.MvcHelper.LogOut();
        }
    }
}
