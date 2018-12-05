using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(blankspaces.Startup))]
namespace blankspaces
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
