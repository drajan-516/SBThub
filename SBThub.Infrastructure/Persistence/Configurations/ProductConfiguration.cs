using SBThub.Domain.Entities;
using SBThub.Domain.ValueObjects.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBThub.Infrastructure.Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(product => product.Id);
        builder.HasIndex(product => product.Uuid).IsUnique();

        builder.Property(product => product.FullTitle)
            .HasMaxLength(ProductTitle.MaxLength);

        builder.Property(product => product.Description).HasMaxLength(1000);
    }
}