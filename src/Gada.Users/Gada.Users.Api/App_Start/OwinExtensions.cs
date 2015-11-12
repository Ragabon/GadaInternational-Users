using Autofac;
using Autofac.Integration.Owin;
using Gada.Users.Data;
using Gada.Users.Services;
using Microsoft.Owin;

namespace Gada.Users.Api
{
    public static class OwinExtentions
    {
        public static IApplicationUserManager GetApplicationUserManager(this IOwinContext context)
        {
            // Get the IoC scope to ask it for the UserManager
            var scope = context.GetAutofacLifetimeScope();

            // Return the UserManager
            return scope == null ? null : scope.Resolve<IApplicationUserManager>();
        }

        public static UserDbContext GetApplicationDbContext(this IOwinContext context)
        {
            // Get the IoC scope to ask it for the DbContext
            var scope = context.GetAutofacLifetimeScope();

            // Return the DbContext
            return scope == null ? null : scope.Resolve<UserDbContext>();
        }
    }
}