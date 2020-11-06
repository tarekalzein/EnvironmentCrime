using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnvironmentCrime.Models
{

    /// <summary>
    /// Class to create the context of the identity database.
    /// </summary>
    public class IdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options) { }

    }
}
