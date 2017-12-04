using Aion.DAL.Managers;
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
            var _storeManager = new StoreManager();
            var ip = Helpers.MvcHelper.GetIPHelper();
            var t = Task.Run(() => _storeManager.GetStoreDetails(ip));
            var store = t.Result ?? new DAL.Entities.Store();

            HttpContext.Current.Session.Add("_StoreDetails", store);
            HttpContext.Current.Session.Add("_ROIFlag", store.Channel == "ROI");
            HttpContext.Current.Session.Add("_AccessLevel", 0);
            HttpContext.Current.Session.Add("_AccessArea", "");
            HttpContext.Current.Session.Add("_menuSearch", "e");

            //HttpContext.Current.Session["_ChannelName"] = "SAS";
            //HttpContext.Current.Session["_DivisionName"] = "DCPW01";
            //HttpContext.Current.Session["_RegionNumber"] = "101";

            //if (store.IpRange == null)
            //    Response.Redirect("/Profile/UnknownStore");
            //if (store.IpRange == "DUPLICATE")
            //    Response.Redirect("Profile/DuplicateRecords");            
            if (Session.IsNewSession)
                Response.Redirect("/");
        }

        void Session_End(object sender, EventArgs E)
        {
            Helpers.MvcHelper.LogOut();
        }
    }
}
