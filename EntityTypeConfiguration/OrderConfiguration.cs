using linhkien_donet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace linhkien_donet.EntityTypeConfiguration
{
    public class OrderConfiguration: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(p => p.Payment)
            //        .WithOne(o => o.Orders)
            //        .HasForeignKey<Payment>(p => p.PaymentId);

            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.Address).IsRequired();
            builder.Property(o => o.CreatedDate).IsRequired();
        }
    }
}
