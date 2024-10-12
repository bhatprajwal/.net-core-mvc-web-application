using Dao.Extensions;
using Exceptions;
using I18N.Extensions;
using Serilogs.Extensions;

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
        service.AddControllersWithViews();

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
