using Dao.DbContext;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Dao.Extensions;

/// <summary>
/// Identity Extension
/// </summary>
public static class IdentityExtension
{
    /// <summary>
    /// Add User Identity
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddUserIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentityCore<ApplicationUser>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.SignIn.RequireConfirmedAccount = true;
                }
            )
            .AddRoles<UserRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddDefaultUI();

        return serviceCollection;
    }
}
