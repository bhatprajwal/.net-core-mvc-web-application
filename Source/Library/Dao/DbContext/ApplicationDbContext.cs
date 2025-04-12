using Dao.Extension;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Entity;

namespace Dao.DbContext
{
    /// <summary>
    /// Application DB context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserRole, string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
        : base(dbContextOptions) { }

        /// <summary>
        /// On Model Creating
        /// </summary>
        /// <param name="modelBuilder">Model Builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This will support to apply the configuration for the entity via Fluent API
            modelBuilder.ApplyFluentConfigurations();
        }
    }
}
