using GoogleAuth.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleAuth.Extension;

/// <summary>
/// Google Auth Extension
/// </summary>
public static class GoogleAuthExtension
{
    /// <summary>
    /// Add Google Auth configuration 
    /// </summary>
    /// <param name="serviceCollection">Service Collection</param>
    /// <param name="webApplicationBuilder">Web Application Builder</param>
    /// <returns>IServiceCollection</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IServiceCollection AddGoogleAuthentication(this IServiceCollection serviceCollection, WebApplicationBuilder webApplicationBuilder)
    {
        var googleConfig = webApplicationBuilder.Configuration
            .GetSection("Authentication:Google")
            .Get<GoogleAuthConfig>() ?? throw new InvalidOperationException("Google Auth configuration not found.");

        serviceCollection.AddAuthentication(options =>
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

        serviceCollection.ConfigureApplicationCookie(options =>
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

        serviceCollection.Configure<CookiePolicyOptions>(options =>
        {
            options.Secure = CookieSecurePolicy.Always;
        });

        return serviceCollection;
    }
}
