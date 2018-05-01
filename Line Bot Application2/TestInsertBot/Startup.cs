using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestInsertBot.Startup))]
namespace TestInsertBot
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
