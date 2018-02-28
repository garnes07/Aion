using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aion.Attributes
{
    public class UserFilterAttribute : ActionFilterAttribute
    {
        public short[] AccessLevel { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            area = "",
                            controller = "Profile",
                            action = "Login"
                        })
                    );
                return;
            }

            var isAuthorised = false;

            if ((short)HttpContext.Current.Session["_AccessLevel"] != 0)
            {
                if (AccessLevel.Any(x => x >= (short) HttpContext.Current.Session["_AccessLevel"]))
                    isAuthorised = true;
            }

            if (!isAuthorised)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            area = "",
                            controller = "Profile",
                            action = "Unauthorised"
                        })
                );
            }
        }
    }
}