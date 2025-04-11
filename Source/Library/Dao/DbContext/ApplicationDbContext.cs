using Dao.Extension;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entity;

namespace Dao.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // This will support to apply the configuration for the entity via Fluent API
            builder.ApplyFluentConfigurations();
        }

        // Application User | Identity User
        public DbSet<ApplicationUser> Users { get; set; }

        // User Roles | Identity Role
        public DbSet<UserRole> Roles { get; set; }

        // Rest of the model class for table creation
    }
}
