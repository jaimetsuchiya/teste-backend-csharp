using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Presentation.TorreHanoi.Startup))]

namespace Presentation.TorreHanoi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
