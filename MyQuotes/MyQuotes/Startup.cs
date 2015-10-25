using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyQuotes.Startup))]
namespace MyQuotes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
