using Microsoft.Owin.Cors;
using Owin;

namespace Gada.Users.Api
{
    public partial class Startup
    {
        public void ConfigureCors(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}