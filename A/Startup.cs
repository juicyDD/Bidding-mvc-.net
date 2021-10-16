using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(A.Startup))]
namespace A
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
