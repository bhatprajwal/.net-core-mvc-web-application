using Microsoft.EntityFrameworkCore;

namespace Dao.Extension;

public static class FluentConfigurationExtension
{
    public static ModelBuilder ApplyFluentConfigurations(this ModelBuilder builder)
    {
        return builder;
    }
}
