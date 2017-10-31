using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyBookingsApp.Startup))]
namespace MyBookingsApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
