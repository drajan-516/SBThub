using SBThub.Domain.Entities;
using SBThub.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBThub.Domain.ValueObjects.User;

namespace SBThub.Infrastructure.Persistence.Configurations;

internal sealed class RefreshTokenConfiguration  : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(token => token.Id);
        builder.Property(token => token.TokenHash).IsRequired().HasMaxLength(128);
        builder.HasIndex(token => token.TokenHash).IsUnique();
    }
}