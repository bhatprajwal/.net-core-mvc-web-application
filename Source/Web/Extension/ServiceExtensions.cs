using Dao.Extensions;
using Exceptions;
using I18N.Extensions;
using Serilogs.Extensions;
using Web.Extension;

namespace Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServiceConfigurations(this IServiceCollection service, WebApplicationBuilder webApplicationBuilder)
    {
        // Entity Framework & Identity
        service.AddDao(webApplicationBuilder);

        // Google Authentication
        service.AddGoogleAuthentication(webApplicationBuilder);

        // I18N
        service.AddI18NLocalizer();

        service.AddControllers();

        service.AddEndpointsApiExplorer();

        service.AddDatabaseDeveloperPageExceptionFilter();
        service.AddCustomRazorPage();

        // Session
        service.AddSessionConfig(webApplicationBuilder);

        service.AddRazorPages();

        // Serilog
        webApplicationBuilder.AddSerilog();

        // Exception
        service.AddTransient<ExceptionMiddleware>();

        // Custom logic
        service.AddBLServices(webApplicationBuilder);

        return service;
    }
}
