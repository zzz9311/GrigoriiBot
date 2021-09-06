using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GrigoriiBot.Startup))]
namespace GrigoriiBot
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
