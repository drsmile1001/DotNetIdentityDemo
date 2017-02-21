using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DotNetIdentityDemo.Startup))]
namespace DotNetIdentityDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
