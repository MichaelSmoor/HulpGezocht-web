using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HulpGezocht.Startup))]
namespace HulpGezocht
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
