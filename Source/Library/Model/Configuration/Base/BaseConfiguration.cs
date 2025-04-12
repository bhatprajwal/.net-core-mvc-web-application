using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Entity.Configuration;

/// <summary>
/// Base Configuration
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TId"></typeparam>
public class BaseConfiguration<T, TId> : IEntityTypeConfiguration<T>
    where T : Base<TId>
    where TId : struct
{
    /// <summary>
    /// Configure Base
    /// </summary>
    /// <param name="entityTypeBuilder">Entity TypeBuilder</param>
    public void Configure(EntityTypeBuilder<T> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(i => i.Id);
        entityTypeBuilder.Property(i => i.Id).IsRequired();

        if (typeof(TId) == typeof(Guid))
        {
            entityTypeBuilder.Property(i => i.Id).ValueGeneratedOnAdd();
        }
    }
}
