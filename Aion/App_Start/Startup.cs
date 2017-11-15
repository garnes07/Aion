using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aion.App_Start.Startup))]
namespace Aion.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}