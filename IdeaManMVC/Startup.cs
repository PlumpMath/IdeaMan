using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdeaManMVC.Startup))]
namespace IdeaManMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
