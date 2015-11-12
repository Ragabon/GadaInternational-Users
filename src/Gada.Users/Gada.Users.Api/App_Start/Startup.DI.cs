using Autofac;
using Autofac.Integration.WebApi;
using Gada.Users.Data;
using Gada.Users.Services;
//using Gada.Users.Data;
//using Gada.Users.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using System.Web.Http;

namespace Gada.Users.Api
{
    public partial class Startup
    {
        public void ConfigureDI(IAppBuilder app, HttpConfiguration config)
        {
            // Start to build a container that will be able to produce objects
            var builder = new ContainerBuilder();

            // Register all the WebApi controllers in the Api assmebly, so that the container knows about them and can fill their dependencies when a controller is created.
            builder.RegisterApiControllers(typeof(Startup).Assembly);

             //Register the objects/types you want to be accessible from the container
            builder.Register(c => new UserDbContext()).As<UserDbContext>().InstancePerRequest();
            builder.Register(c =>
            {
                return new ApplicationUserManager(
                    new UserStore<ApplicationUser>(c.Resolve<UserDbContext>())
                );
            }).As<IApplicationUserManager>();

            // generate a container
            var container = builder.Build();

            // add a resolver to the WebApi framework so that it knows when it creates a controller it should ask the container for the controller.
            // before trying to create one with an empty constructor instead
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Add container into the WebApi pipeline
            // Note: Not used in this project, but useful for creating custom Middleware and having their dependencies filled in.
            // See http://alexmg.com/owin-support-for-the-web-api-2-and-mvc-5-integrations-in-autofac/ for more information
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
        }
    }
}