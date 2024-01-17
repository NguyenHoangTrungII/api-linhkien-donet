using linhkien_donet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace linhkien_donet.EntityTypeConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.OldPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.StatusProduct).IsRequired();

            builder.HasMany(p => p.CartDetail)
                .WithOne(cd => cd.Product)
                .HasForeignKey(cd => cd.ProductId);

            builder.HasMany(p => p.Images)
                .WithOne(cd => cd.Product)
                .HasForeignKey(cd => cd.ProductId);
        }
    }
}
