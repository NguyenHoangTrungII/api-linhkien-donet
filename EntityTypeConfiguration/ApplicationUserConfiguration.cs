using linhkien_donet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace linhkien_donet.EntityTypeConfiguration
{
    public class ApplicationUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUser");
            //builder.HasNoKey();

           


            builder.HasOne(c => c.Cart)
               .WithOne(c => c.User)
               .HasForeignKey<Cart>(c => c.UserId);


        }
    }
}
