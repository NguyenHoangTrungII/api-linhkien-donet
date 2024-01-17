using linhkien_donet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace linhkien_donet.EntityTypeConfiguration
{
    public class ImageConfiguration : IEntityTypeConfiguration<Images>
    {
        public void Configure(EntityTypeBuilder<Images> builder)
        {
            builder.HasKey(i => i.Id); 

            builder.Property(i => i.images)
                .IsRequired(); 

            builder.HasOne(i => i.Product) 
                .WithMany(p => p.Images) 
                .HasForeignKey(i => i.ProductId) 
                .OnDelete(DeleteBehavior.Cascade); 

            
        }
    }
}
