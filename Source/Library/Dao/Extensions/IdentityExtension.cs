using Dao.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace Dao.Extensions;

public static class IdentityExtension
{
    public static IServiceCollection AddUserIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, UserRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.SignIn.RequireConfirmedAccount = true;
                }                
            )
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

        return services;
    }
}
