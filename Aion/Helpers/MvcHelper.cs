using Aion.App_Start;
using Aion.DAL.Managers;
using Aion.Models.Utils;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
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
            IAuthenticationManager authenticationManager = HttpContext.Current?.Request?.GetOwinContext()?.Authentication;
            authenticationManager?.SignOut(AionAuthentication.ApplicationCookie);
        }

        public static short InitStoreDetails()
        {
            StoreManager _storeManager = new StoreManager();

            string ip = GetIPHelper();
#if DEBUG
            string ipBase = "10.224.240";
#else
            string ipBase = ip.Substring(0, ip.LastIndexOf("."));
#endif
            var t = Task.Run(() => _storeManager.GetStoreDetails(ipBase));
            var storeList = t.Result;

            //Matches found
            if (storeList.Count >= 1)
            {
                if(storeList.Count > 1)
                {
                    var fullIpSearch = storeList.Where(x => x.IpRefs.Any(y => y.IpRange == ip)).ToList();
                    if(fullIpSearch.Count == 1)
                    {
                        storeList = fullIpSearch;
                    }
                    else
                    {
                        return -1;
                    }
                }

                var u = Task.Run(() => _storeManager.GetStoreMenu(storeList.First().StoreNumber));
                var menuList = u.Result;

                StoreMenu _menu = new StoreMenu(menuList, "S_" + storeList.First().StoreNumber.ToString(),0);

                HttpContext.Current.Session.Add("_storeMenu", _menu);
                HttpContext.Current.Session.Add("_store", storeList.First());
                return 1;

            }            
            //No match for IP address
            else
            {
                return 0;
            }
        }
    }
}