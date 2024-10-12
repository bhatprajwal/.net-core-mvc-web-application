using I18N.LocalizerFactory;
using I18N.MiddleWare;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace I18N.Extensions;

public static class I18NServiceExtensions
{
    public static IServiceCollection AddI18NLocalizer(this IServiceCollection services)
    {
        services.AddLocalization();
        services.AddDistributedMemoryCache();
        services.AddSingleton<LocalizerMiddleware>();
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

        return services;
    }
}
