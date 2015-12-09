using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PizzaShop.Startup))]
namespace PizzaShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
