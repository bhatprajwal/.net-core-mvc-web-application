using Dao.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dao.Extensions;

public static class MySQLDBExtension
{
    public static IServiceCollection AddMySQLContext(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseMySQL(builder.GetConnectionString()));

        return services;
    }
}
