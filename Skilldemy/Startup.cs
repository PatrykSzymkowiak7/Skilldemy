using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Skilldemy.Startup))]
namespace Skilldemy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
