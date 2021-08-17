using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Core.Identity;

namespace Portal.Infra.Configuration
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //builder.Entity<ApplicationUser>()
            //    .Ignore(b => b.NormalizedUserName);
            //builder.Entity<ApplicationUser>()
            //    .Ignore(b => b.ConcurrencyStamp);
            //builder.Entity<ApplicationUser>()
            //    .Ignore(b => b.LockoutEnd);
            //builder.Entity<ApplicationUser>()
            //    .Ignore(b => b.NormalizedEmail);
        }
    }
}
