using Dao.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Dao.Extensions;

public static class DbConnectionHelperExtension
{
    #region Public Functions
    /// <summary>
    /// Get Connect string based on Dev or Prod environment
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static string GetConnectionString(this WebApplicationBuilder builder)
    {
        var connectionStrings = GetConnectionStringConfig(builder);
        return builder.Environment.IsDevelopment() ? connectionStrings.Dev : connectionStrings.Prod;
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Get Connection string record
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static ConnectionStrings? GetConnectionStringConfig(WebApplicationBuilder builder)
    {
        return builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>() ?? throw new InvalidOperationException("Connection string 'Prod - MSSQL' not found.");
    } 
    #endregion
}
