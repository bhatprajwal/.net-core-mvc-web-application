using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Serilogs.Extensions;

public static class SerilogExtension
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        var basePath = AppContext.BaseDirectory;
        var configPath = Path.Combine(basePath ?? Directory.GetCurrentDirectory(), "seri-log-config.json");

        var logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(new ConfigurationBuilder()
                            .AddJsonFile(configPath, optional: false, reloadOnChange: true)
                            .Build())
                            .Enrich.FromLogContext()
                            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        return builder;
    }
}
