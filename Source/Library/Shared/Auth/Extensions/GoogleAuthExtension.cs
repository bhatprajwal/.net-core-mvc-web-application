using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System.Security.Claims;

namespace Auth.Extensions;

public static class GoogleAuthExtension
{
    public static AuthenticationBuilder AddGoogleAuth(this AuthenticationBuilder authBuilder, WebApplicationBuilder builder)
    {
        var config = builder.Configuration.GetSection("Authentications").Get<Authentications>();
        return authBuilder.AddGoogle(options =>
        {
            options.ClientId = config.Google.ClientId;
            options.ClientSecret = config.Google.ClientSecret;
            options.Events.OnCreatingTicket = async context =>
            {
                var googleId = context.User.GetProperty("sub").GetString();

                if (!string.IsNullOrEmpty(googleId))
                {
                    var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                    var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<ApplicationUser>>();

                    // Get the logged-in user
                    var user = await userManager.FindByLoginAsync("Google", googleId);

                    if (user == null)
                    {
                        // New user; create a new ApplicationUser and save the Google ID
                        user = new ApplicationUser
                        {
                            UserName = context.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = context.Principal.FindFirstValue(ClaimTypes.Email),
                            GoogleId = googleId
                        };

                        await userManager.CreateAsync(user);
                        await userManager.AddLoginAsync(user, new UserLoginInfo("Google", googleId, "Google"));
                    }
                    else if (user.GoogleId == null)
                    {
                        // Existing user; update Google ID if it hasn't been saved yet
                        user.GoogleId = googleId;
                        await userManager.UpdateAsync(user);
                    }

                    // Sign in the user
                    await signInManager.SignInAsync(user, isPersistent: false);
                }
            };
        });
    }
}
