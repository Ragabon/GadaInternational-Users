using Owin;
using System.Web.Http;

namespace Gada.Users.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            Configure(app, config);
            app.UseWebApi(config);
        }
    }
}