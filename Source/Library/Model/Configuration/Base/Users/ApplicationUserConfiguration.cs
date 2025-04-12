using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.Configuration;

/// <summary>
/// Application User Configuration
/// </summary>
public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    /// <summary>
    /// EntityType Configuration
    /// </summary>
    /// <param name="entityTypeBuilder">EntityType Builder</param>
    public void Configure(EntityTypeBuilder<ApplicationUser> entityTypeBuilder)
    {
        entityTypeBuilder.Property(name => name.FullName)
            .IsRequired();

        entityTypeBuilder.Property(image => image.ProfilePictureUrl)
            .IsRequired();
    }
}
