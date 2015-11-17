using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WatMvc.Startup))]
namespace WatMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
