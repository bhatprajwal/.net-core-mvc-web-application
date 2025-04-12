using Dao.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dao.Extensions;

/// <summary>
/// MSSQL Database Extension
/// </summary>
public static class MSSQLDBExtension
{
    /// <summary>
    /// Add MSSQL DB Configuration
    /// </summary>
    /// <param name="serviceCollection">IService Collection</param>
    /// <param name="webApplicationBuilder">WebApplication Builder</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddMSSQLContext(this IServiceCollection serviceCollection, WebApplicationBuilder webApplicationBuilder)
    {        
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(webApplicationBuilder.GetConnectionString()));

        serviceCollection.AddUserIdentity();

        return serviceCollection;
    }
}
