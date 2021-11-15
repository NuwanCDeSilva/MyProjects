using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FastForward.WebASYCUDA.Startup))]
namespace FastForward.WebASYCUDA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
