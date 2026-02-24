using ECommerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistance.Configurations
{
    /// <summary>
    /// Configuración de la entidad Order para Entity Framework Core
    /// </summary>
    public sealed class OrderConfiguration
      : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("OrdersEcommerce");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.Currency)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .HasMany(x => x.Items) // usar propiedad pública
                .WithOne()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Navigation(x => x.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }


}
