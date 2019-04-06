using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogSitesiMVC_Proje_YaseminGoktas.Startup))]
namespace BlogSitesiMVC_Proje_YaseminGoktas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
