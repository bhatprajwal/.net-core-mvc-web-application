using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dao.Extensions;

/// <summary>
/// Database Helper Extension
/// </summary>
public static class HelperExtension
{
    /// <summary>
    /// Add Data Access Object
    /// </summary>
    /// <param name="serviceCollection">IService Collection</param>
    /// <param name="webApplicationBuilder"> WebApplicationBuilder</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddDao(this IServiceCollection serviceCollection, WebApplicationBuilder webApplicationBuilder)
    {
        // Entity Framework - Set accordingly - MSSQL | MySQL
        serviceCollection.AddMySQLContext(webApplicationBuilder);

        return serviceCollection;
    }
}
