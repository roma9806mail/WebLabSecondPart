using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebLabSecondPart.Startup))]
namespace WebLabSecondPart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
