namespace Web.Extension;

/// <summary>
/// Razor page configuration
/// </summary>
public static class RazorPageExtension
{
    /// <summary>
    /// Add Custom RazorPage
    /// </summary>
    /// <param name="service">IServiceCollection</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddCustomRazorPage(this IServiceCollection service)
    {
        service.AddControllersWithViews()
            .AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Features/{1}/Views/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
        return service;
    }
}
