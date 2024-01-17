using linhkien_donet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace linhkien_donet.EntityTypeConfiguration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            //builder.HasNoKey();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();


            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired();

            builder.HasOne(c => c.User)
               .WithOne(c => c.Cart)
               .HasForeignKey<Cart>(c => c.UserId);


        }
    }
}
