using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LawOffice.Startup))]
namespace LawOffice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
