using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WA_Blogers_MVC.Startup))]
namespace WA_Blogers_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
