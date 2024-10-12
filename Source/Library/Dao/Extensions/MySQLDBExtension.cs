using Dao.DbContext;
using Dao.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dao.Extensions;

public static class MySQLDBExtension
{
    public static IServiceCollection AddMySQLContext(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>() ?? throw new InvalidOperationException("Connection string 'Default - MySQL' not found.");

        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseMySQL(connectionStrings.Default));

        return services;
    }
}
