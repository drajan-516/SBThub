using SBThub.Domain.Entities;
using SBThub.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBThub.Domain.ValueObjects.User;

namespace SBThub.Infrastructure.Persistence.Configurations;

internal sealed class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfile");
        
        builder
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<UserProfile>(p => p.UserId)
            .HasPrincipalKey<User>(u => u.Uuid);
    
        builder.HasKey(user => user.Id);
        builder.HasIndex(user => user.Uuid).IsUnique();;

        builder.Property(user => user.FullName)
            .HasConversion(name => name.Value, value => UserName.Create(value).Value)
            .HasMaxLength(UserName.MaxLength);

        builder.Property(profile => profile.Email).HasMaxLength(320);
        builder.Property(profile => profile.AvatarUrl).HasMaxLength(2048);
        builder.Property(profile => profile.Phone).HasMaxLength(30);
    }
}