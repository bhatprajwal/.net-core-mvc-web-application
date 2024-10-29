using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dao.Extensions;

public static class HelperExtension
{
    public static IServiceCollection AddDao(this IServiceCollection services, WebApplicationBuilder builder)
    {
        // Entity Framework - Set accordingly - MSSQL | MySQL
        services.AddMSSQLContext(builder);

        // Identity
        services.AddUserIdentity();

        return services;
    }
}
