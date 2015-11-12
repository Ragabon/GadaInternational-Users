using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gada.Users.Api
{
    public class SimpleOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Get the user manager so we can query our data store
            var userManager = context.OwinContext.GetApplicationUserManager();

            if (userManager == null)
            {
                throw new NullReferenceException("The user manager was not set");
            }

            // This would be where you could check for things like the they are locked out, they have confirmed their email/phone etc.
            var user = await userManager.FindAsync(context.UserName, context.Password);

            // User not found or password is incorrect then refuse authorisation.
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            // Create the identity to add to the token, the auth type was the one passed in 'Bearer' but can be set to whatever system you want to catch it in the chain
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            identity.AddClaim(new Claim("sub", user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

            // pass back the identity to be handled by the server
            context.Validated(identity);

            //return Task.FromResult(0);
        }
    }
}