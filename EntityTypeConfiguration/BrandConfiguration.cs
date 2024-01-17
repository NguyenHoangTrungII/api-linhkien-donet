using linhkien_donet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace linhkien_donet.EntityTypeConfiguration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void  Configure(EntityTypeBuilder<Brand> builder) {
            builder.ToTable("Brand");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.HasMany(p => p.Products)
                .WithOne(b => b.Brand)
                .HasForeignKey(b => b.BrandId);
        } 
    }
}
