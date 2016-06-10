using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Campus_virtual.Startup))]
namespace Campus_virtual
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
