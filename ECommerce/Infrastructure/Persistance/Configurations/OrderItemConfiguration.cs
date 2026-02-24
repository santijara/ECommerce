using ECommerce.Domain.Entities.Carts;
using ECommerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistance.Configurations
{
    /// <summary>
    /// Configuración de la entidad OrderItem para Entity Framework Core
    /// </summary>
    public sealed class OrderItemConfiguration
     : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItemsEcommerce");

            // Clave primaria compuesta
            builder.HasKey(x => new { x.OrderId, x.ProductId });

            builder.Property(x => x.OrderId)
                .IsRequired();

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            // Value Object Money
            builder.OwnsOne(x => x.UnitPrice, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("UnitPrice")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });
        }
    }

}
