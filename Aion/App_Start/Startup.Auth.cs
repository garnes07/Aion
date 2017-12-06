using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

namespace Aion.App_Start
{
    public static class AionAuthentication
    {
        public const string ApplicationCookie = "AionAuthentication";
    }

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = AionAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Profile/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "AionAuthCookie",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(20), // adjust to your needs
            });
        }
    }
}