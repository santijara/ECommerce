using ECommerce.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistance.Configurations
{
    /// <summary>
    /// Configuración de la entidad Inventories para Entity Framework Core
    /// </summary>
    public sealed class InventoryConfiguration
      : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventories");  

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId)
                .IsRequired();

            builder.Property(x => x.TotalStock)
                .IsRequired();

            builder.Property(x => x.ReservedStock)
                .IsRequired();
        }
    }
}
