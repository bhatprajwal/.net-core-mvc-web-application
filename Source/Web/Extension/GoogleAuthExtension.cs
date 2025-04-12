using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Web.Dtos;

namespace Web.Extension;

/// <summary>
/// Google Auth Extension
/// </summary>
public static class GoogleAuthExtension
{
    /// <summary>
    /// Add Google Auth configuration 
    /// </summary>
    /// <param name="service">IServiceCollection</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddGoogleAuthentication(this IServiceCollection service, WebApplicationBuilder builder)
    {
        var googleConfig = builder.Configuration
            .GetSection("Authentication:Google")
            .Get<GoogleAuthConfig>() ?? throw new InvalidOperationException("Google Auth configuration not found.");

        service.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
        .AddCookie(IdentityConstants.ApplicationScheme)
        .AddCookie(IdentityConstants.ExternalScheme)
        .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
        {
            options.ClientId = googleConfig.ClientId;
            options.ClientSecret = googleConfig.ClientSecret;
            options.ClaimActions.MapJsonKey("picture", "picture", "url");
            options.SignInScheme = IdentityConstants.ExternalScheme;

            options.Events.OnRemoteFailure = context =>
            {
                context.HandleResponse();
                context.Response.Redirect("/GoogleAuth/LoginCancelled");
                return Task.CompletedTask;
            };

            options.Events.OnRedirectToAuthorizationEndpoint = context =>
            {
                context.Response.Redirect(context.RedirectUri + "&prompt=select_account");
                return Task.CompletedTask;
            };
        });

        service.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/GoogleAuth/ExternalLogin";

            options.Events.OnRedirectToLogin = context =>
            {
                var returnUrl = context.Request.Path + context.Request.QueryString;
                var redirectUrl = $"/GoogleAuth/ExternalLogin?provider=Google&returnUrl={Uri.EscapeDataString(returnUrl)}";
                context.Response.Redirect(redirectUrl);
                return Task.CompletedTask;
            };

            options.Cookie.Name = "Web_AuthCookie";
            options.AccessDeniedPath = "/GoogleAuth/AccessDenied";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.ExpireTimeSpan = TimeSpan.FromDays(14);
        });

        service.Configure<CookiePolicyOptions>(options =>
        {
            options.Secure = CookieSecurePolicy.Always;
        });

        return service;
    }
}
