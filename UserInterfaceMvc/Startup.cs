using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserInterfaceMvc.Startup))]
namespace UserInterfaceMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
