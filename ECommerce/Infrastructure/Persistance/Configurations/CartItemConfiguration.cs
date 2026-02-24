using ECommerce.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistance.Configurations
{
    /// <summary>
    /// Configuración de la entidad CartItem para Entity Framework Core
    /// Patrón Fluent API Configuration
    /// Permite configurar la tabla, claves, relaciones y comportamiento de navegación
    /// Esto reemplaza o complementa las anotaciones de datos en la entidad
    /// </summary>
    public sealed class CartItemConfiguration
     : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItemsEcommerce");

            // 🔥 Declarar propiedad sombra (Shadow Property)
            builder.Property<Guid>("CartId");

            // 🔥 Clave primaria compuesta
            builder.HasKey("CartId", nameof(CartItem.ProductId));

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

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
