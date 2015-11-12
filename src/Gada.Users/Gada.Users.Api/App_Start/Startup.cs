using Owin;
using System.Web.Http;

namespace Gada.Users.Api
{
    public partial class Startup
    {
        public void Configure(IAppBuilder app, HttpConfiguration config)
        {
            ConfigureDI(app, config);
            ConfigureCors(app);
            ConfigureAuth(app, config);
            ConfigureRoutes(config);
            ConfigureFormatters(config);
        }
    }
}