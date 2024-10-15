using Dao.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Dao.Extensions;

public static class DbConnectionExtension
{
    public static string GetConnectionString(this WebApplicationBuilder builder)
    {
        var connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>() ?? throw new InvalidOperationException("Connection string 'Prod - MSSQL' not found.");
        return builder.Environment.IsDevelopment() ? connectionStrings.Dev : connectionStrings.Prod;
    }
}
