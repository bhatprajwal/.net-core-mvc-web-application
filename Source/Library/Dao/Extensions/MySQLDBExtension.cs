using Dao.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dao.Extensions;

/// <summary>
/// MySQL Database Extension
/// </summary>
public static class MySQLDBExtension
{
    /// <summary>
    /// Add MySQL DB Configuration
    /// </summary>
    /// <param name="serviceCollection">IService Collection</param>
    /// <param name="webApplicationBuilder">WebApplication Builder</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddMySQLContext(this IServiceCollection serviceCollection, WebApplicationBuilder webApplicationBuilder)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options => 
            options.UseMySQL(webApplicationBuilder?.GetConnectionString()));

        serviceCollection.AddUserIdentity();

        return serviceCollection;
    }
}
