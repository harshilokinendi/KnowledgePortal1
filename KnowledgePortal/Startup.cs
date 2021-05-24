using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KnowledgePortal.Startup))]
namespace KnowledgePortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
