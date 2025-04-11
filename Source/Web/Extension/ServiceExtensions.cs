using Dao.Extensions;
using Exceptions;
using I18N.Extensions;
using Serilogs.Extensions;
using Web.Extension;

namespace Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServiceConfigurations(this IServiceCollection service, WebApplicationBuilder builder)
    {
        // Entity Framework & Identity
        service.AddDao(builder);

        // I18N
        service.AddI18NLocalizer();

        service.AddControllers();

        service.AddEndpointsApiExplorer();

        service.AddDatabaseDeveloperPageExceptionFilter();
        service.AddCustomRazorPage();

        // Session
        service.AddSessionConfig(builder);

        service.AddRazorPages();

        // Serilog
        builder.AddSerilog();

        // Exception
        service.AddTransient<ExceptionMiddleware>();

        // Custom logic
        service.AddBLServices(builder);

        return service;
    }
}
