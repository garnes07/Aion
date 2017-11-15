using System.Web;

namespace Aion.Helpers
{
    public static class MvcHelper
    {
        public static string GetIPHelper()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }

        public static T GetSessionObject<T>(this HttpContext current, string sessionObjectName) where T : new()
        {
            return current != null ? (T)current.Session[sessionObjectName] : default(T);
        }

        public static int GetAuthLevel(this HttpContext current, string sessionObjectName)
        {
            var level = current != null ? (int)current.Session[sessionObjectName] : 0;
            if (level.ToString().Length == 3)
                level -= 100;
            return level;
        }

        public static void LogOut()
        {
            HttpContext.Current?.Session?.Abandon();
            //IAuthenticationManager authenticationManager = HttpContext.Current?.Request?.GetOwinContext()?.Authentication;
            //authenticationManager?.SignOut(CpwWfmAuthentication.ApplicationCookie);
        }
    }
}