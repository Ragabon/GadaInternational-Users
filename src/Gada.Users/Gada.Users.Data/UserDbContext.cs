using Microsoft.AspNet.Identity.EntityFramework;

namespace Gada.Users.Data
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext()
            : base("UserDbConnection", throwIfV1Schema: false) { }
    }
}