using Exceptions;
using I18N.Extensions;

namespace Web.Extensions;

public static class AppExtensions
{
    public static WebApplication AddAppConfigurations(this WebApplication app, WebApplicationBuilder builder)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        // I18N
        app.AddAppI18NLocalizer(builder);
        app.UseStaticFiles();

        app.MapControllers();

        app.UseRouting();

        // Session
        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.AddRoutes();
        app.MapRazorPages();

        // Exception
        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}
