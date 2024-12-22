using Web.Dtos;

namespace Web.Extensions;

public static class AddSessionConfigExtension
{
    public static IServiceCollection AddSessionConfig(this IServiceCollection service, WebApplicationBuilder builder)
    {
        var config = builder.Configuration.GetSection("SessionConfig").Get<SessionConfig>() ?? throw new InvalidOperationException("Session Configuration not found.");
        
        service.AddSession(options =>
        {
            switch(config.SessionTimeOutIn.ToLower())
            {
                case "milliseconds":
                    options.IdleTimeout = TimeSpan.FromMilliseconds(config.IdleTimeout);
                    break;
                case "seconds":
                    options.IdleTimeout = TimeSpan.FromSeconds(config.IdleTimeout);
                    break;
                case "minutes":
                default:
                    options.IdleTimeout = TimeSpan.FromMinutes(config.IdleTimeout);
                    break;
            }
            
            options.Cookie.HttpOnly = config.HttpOnly;
            options.Cookie.IsEssential = config.IsEssential;
        });

        return service;
    }
}
