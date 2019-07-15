using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Brothers.Startup))]
namespace Brothers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
