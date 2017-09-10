using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearnMore.Mvc.Startup))]
namespace LearnMore.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
