using Gada.Users.Data;
using System;
using System.Threading.Tasks;

namespace Gada.Users.Services
{
    public interface IApplicationUserManager
    {
        Task<Microsoft.AspNet.Identity.IdentityResult> CreateAsync(Gada.Users.Data.ApplicationUser user, string password);
        void Dispose();
        Task<Gada.Users.Data.ApplicationUser> FindAsync(string username, string password);
        Task<Gada.Users.Data.ApplicationUser> FindByNameAsync(string username);
        void SendRegistrationEmail(string emailAddress);
        Task ConfirmUserEmail(string base64UserId);
        Task<ApplicationUser> FindByEmailAsync(string email);

        Task TestEmail();
    }
}
