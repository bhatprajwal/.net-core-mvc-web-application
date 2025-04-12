using Entity;
using Entity.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Dao.Extension;

/// <summary>
/// Fluent Configuration
/// </summary>
public static class FluentConfigurationExtension
{
    /// <summary>
    /// Apply Fluent Configuration
    /// </summary>
    /// <param name="modelBuilder">Model Builder</param>
    /// <returns>ModelBuilder</returns>
    public static ModelBuilder ApplyFluentConfigurations(this ModelBuilder modelBuilder)
    {
        // Application User
        modelBuilder.ApplyConfiguration<ApplicationUser>(new ApplicationUserConfiguration());
        
        return modelBuilder;
    }
}
