using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

namespace Gada.Users.Api
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app, HttpConfiguration config)
        {
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                TokenEndpointPath = new Microsoft.Owin.PathString("/token"),
                Provider = new SimpleOAuthAuthorizationServerProvider()
            });
        }
    }
}