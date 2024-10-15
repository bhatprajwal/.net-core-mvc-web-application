using Dao.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dao.Extensions;

public static class MSSQLDBExtension
{
    public static IServiceCollection AddMSSQLContext(this IServiceCollection services, WebApplicationBuilder builder)
    {        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.GetConnectionString()));

        return services;
    }
}
