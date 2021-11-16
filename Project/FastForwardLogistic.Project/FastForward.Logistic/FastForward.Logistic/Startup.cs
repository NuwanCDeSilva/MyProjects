using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FastForward.Logistic.Startup))]
namespace FastForward.Logistic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
