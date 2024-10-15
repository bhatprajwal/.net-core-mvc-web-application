using Dao.DbContext;
using Dao.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dao.Extensions;

public static class MSSQLDBExtension
{
    public static IServiceCollection AddMSSQLContext(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>() ?? throw new InvalidOperationException("Connection string 'Prod - MSSQL' not found.");
        var connectionString = builder.Environment.IsDevelopment() ? connectionStrings.Dev : connectionStrings.Prod;

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
