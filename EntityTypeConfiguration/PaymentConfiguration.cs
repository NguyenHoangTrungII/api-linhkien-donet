using linhkien_donet.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace linhkien_donet.EntityTypeConfiguration
{
    public class PaymentConfiguration:IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentId);
            builder.Property(p => p.PaymentDate).IsRequired();
            builder.Property(p => p.PaymentMethod).HasMaxLength(50);
            builder.Property(p => p.OrderId).IsRequired();
            builder.Property(p => p.TransactionId).IsRequired();

            

            builder.ToTable("Payments"); 
        }
    }
}
