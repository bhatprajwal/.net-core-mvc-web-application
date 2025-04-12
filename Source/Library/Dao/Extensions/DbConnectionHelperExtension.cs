using Dao.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Dao.Extensions;

/// <summary>
/// Database Connection Helper
/// </summary>
public static class DbConnectionHelperExtension
{
    #region Public Functions
    /// <summary>
    /// Get Connect string based on Dev or Prod environment
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static string? GetConnectionString(this WebApplicationBuilder builder)
    {
        var connectionStrings = GetConnectionStringConfig(builder);
        return builder.Environment.IsDevelopment() ? connectionStrings?.Dev : connectionStrings?.Prod;
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Get Connection string record
    /// </summary>
    /// <param name="webApplicationBuilder">WebApplication Builder</param>
    /// <returns>ConnectionStrings</returns>
    /// <exception cref="InvalidOperationException">Invalid Operation Exception</exception>
    private static ConnectionStrings? GetConnectionStringConfig(WebApplicationBuilder webApplicationBuilder)
    {
        return webApplicationBuilder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>() ?? throw new InvalidOperationException("Connection string 'Prod - MSSQL' not found.");
    } 
    #endregion
}
