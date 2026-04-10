using Domain_Layer.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configurations
{
    public class OrderItemConfigrations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.Property(OI => OI.Price)
                   .HasColumnType("decimal(8,2)");

            builder.OwnsOne(OI => OI.Product);
        }
    }
}
