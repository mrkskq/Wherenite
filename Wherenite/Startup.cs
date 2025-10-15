using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wherenite.Startup))]
namespace Wherenite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
