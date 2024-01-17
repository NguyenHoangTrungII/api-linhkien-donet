using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using linhkien_donet.Entities;

namespace linhkien_donet.EntityTypeConfiguration
{
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.ToTable("CartDetail");


            //builder.HasNoKey();
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

           

        }
    }
}
