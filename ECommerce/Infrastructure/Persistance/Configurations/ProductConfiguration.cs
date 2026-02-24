using ECommerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistance.Configurations
{
    /// <summary>
    /// Configuración de la entidad products para Entity Framework Core
    /// </summary>
    public sealed class EproductsConfiguration
     : IEntityTypeConfiguration<Eproducts>
    {
        public void Configure(EntityTypeBuilder<Eproducts> builder)
        {
            builder.ToTable("ProductsEcommerce");

            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            //Name
            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            //Description
            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            // CategoryId
            builder.Property(x => x.CategoryId)
                .IsRequired();

            //IsActive
            builder.Property(x => x.IsActive)
                .IsRequired();

            // Money (ValueObject)
            builder.OwnsOne(x => x.Price, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("Price")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            
            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.IsActive);
        }
    }
}
