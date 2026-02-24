using ECommerce.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistance.Configurations
{
    /// <summary>
    /// Configuración de la entidad Cart para Entity Framework Core
    /// Patrón Fluent API Configuration
    /// Permite configurar la tabla, claves, relaciones y comportamiento de navegación
    /// Esto reemplaza o complementa las anotaciones de datos en la entidad
    /// </summary>
    public sealed class CartConfiguration
     : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("CartsEcommerce");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder
                .HasMany(x => x.Items) // ✅ usar propiedad pública
                .WithOne()
                .HasForeignKey("CartId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(x => x.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }




}