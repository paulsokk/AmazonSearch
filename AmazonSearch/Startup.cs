using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmazonSearch.Startup))]
namespace AmazonSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
