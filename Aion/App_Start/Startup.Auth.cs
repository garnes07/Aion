using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

namespace Aion.App_Start
{
    public static class CpwWfmAuthentication
    {
        public const string ApplicationCookie = "CpwWfmAuthentication";
    }

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CpwWfmAuthentication.ApplicationCookie,
                LoginPath = new PathString("/Login"),
                Provider = new CookieAuthenticationProvider(),
                CookieName = "CpwWfmAuthCookie",
                CookieHttpOnly = true,
                ExpireTimeSpan = TimeSpan.FromMinutes(20), // adjust to your needs
            });
        }
    }
}