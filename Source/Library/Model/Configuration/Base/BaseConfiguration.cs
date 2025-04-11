using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Entity.Configuration;

public class BaseConfiguration<T, TId> : IEntityTypeConfiguration<T>
    where T : Base<TId>
    where TId : struct
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).IsRequired();

        if (typeof(TId) == typeof(Guid))
        {
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
        }
    }
}
