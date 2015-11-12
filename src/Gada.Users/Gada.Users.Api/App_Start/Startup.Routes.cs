using System.Web.Http;

namespace Gada.Users.Api
{
    public partial class Startup
    {
        public void ConfigureRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}