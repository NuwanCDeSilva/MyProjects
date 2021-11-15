using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FastForward.WebAbansTours.Startup))]
namespace FastForward.WebAbansTours
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
