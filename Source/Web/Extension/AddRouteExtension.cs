namespace Web.Extensions;

public static class AddRouteExtension
{
    public static WebApplication AddRoutes(this WebApplication app)
    {
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        return app;
    }
}
