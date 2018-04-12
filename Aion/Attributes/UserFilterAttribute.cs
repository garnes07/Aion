using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aion.Attributes
{
    public class UserFilterAttribute : ActionFilterAttribute
    {
        public int[] AccessLevels { get; set; }
        public int[] ExcludeLevels { get; set; }
        public int MinLevel { get; set; }
        private bool IsAuthorised { get; set; }
        private int CurrentUserLevel { get; set; }

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
                            returnUrl = filterContext.HttpContext.Request.Url.PathAndQuery,
                            action = "Login"
                        })
                    );
                return;
            }
            
            CurrentUserLevel = HttpContext.Current.Session["_AccessLevel"] == null ? 0 : int.Parse(HttpContext.Current.Session["_AccessLevel"].ToString());
            if (AccessLevels != null)
            {
                IsAuthorised = AccessLevels.Any(x => x == CurrentUserLevel);
            }
            else if(ExcludeLevels != null)
            {
                IsAuthorised = CurrentUserLevel >= MinLevel && !ExcludeLevels.Contains(CurrentUserLevel);
            }
            else
            {
                IsAuthorised = CurrentUserLevel >= MinLevel;
            }
            

            if (!IsAuthorised)
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