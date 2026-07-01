using SBThub.Domain.Entities;
using SBThub.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBThub.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Uuid).IsUnique();

        builder.Property(user => user.FullName)
            .HasConversion(name => name.Value, value => UserName.Create(value).Value)
            .HasMaxLength(UserName.MaxLength);

        builder.Property(user => user.Phone).HasMaxLength(30);
    }
}
