using I18N.MiddleWare;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace I18N.Extensions;

public static class I18NAppExtensions
{
    public static WebApplication AddAppI18NLocalizer(this WebApplication app, WebApplicationBuilder builder)
    {
        var options = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-US"),
            SupportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("fr-FR")

                // Add rest of the language culture
            },
            SupportedUICultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("fr-FR")

                // Add rest of the language culture
            }
        };

        app.UseRequestLocalization(options);
        app.UseMiddleware<LocalizerMiddleware>();

        return app;
    }
}
