using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jobweb.Startup))]
namespace Jobweb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
