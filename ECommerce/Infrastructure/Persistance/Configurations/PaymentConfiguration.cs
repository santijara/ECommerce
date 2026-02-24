using ECommerce.Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistance.Configurations
{
    /// <summary>
    /// Configuración de la entidad payment para Entity Framework Core
    /// Patrón Fluent API Configuration
    /// Permite configurar la tabla, claves, relaciones y comportamiento de navegación
    /// Esto reemplaza o complementa las anotaciones de datos en la entidad
    /// </summary>
    public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            // 🔹 Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            // OrderId (FK lógica)
            builder.Property(x => x.OrderId)
                .IsRequired();

            //  PaymentStatus (enum)
            builder.Property(x => x.Status)
                .HasConversion<string>() 
                .HasMaxLength(50)
                .IsRequired();

            // TransactionId
            builder.Property(x => x.TransactionId)
                .HasMaxLength(100)
                .IsRequired(false);

            //  CreatedAt
            builder.Property(x => x.CreatedAt)
                .IsRequired();

            // ValueObject Money
            builder.OwnsOne(x => x.Amount, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("Amount")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("Currency")
                    .HasMaxLength(3)
                    .IsRequired();
            });

          
            builder.HasIndex(x => x.OrderId);
        }
    }
}
