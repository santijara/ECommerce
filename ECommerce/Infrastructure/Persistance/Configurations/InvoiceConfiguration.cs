using ECommerce.Domain.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistance.Configurations
{
    /// <summary>
    /// Configuración de la entidad Invoice para Entity Framework Core
    /// </summary>
    public sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("InvoicesECommerce");

            //Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            // Relación con Order
            builder.Property(x => x.OrderId)
                   .IsRequired();

            builder.HasIndex(x => x.OrderId);

            // Invoice Number
            builder.Property(x => x.InvoiceNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(x => x.InvoiceNumber)
                   .IsUnique();

            // Valores monetarios
            builder.Property(x => x.SubTotal)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(x => x.Tax)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(x => x.Total)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // Fecha emisión
            builder.Property(x => x.IssuedAtUtc)
                   .IsRequired();

            // Status (enum)
            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasConversion<int>();

            // Message
            builder.Property(x => x.Message)
                   .HasMaxLength(500)
                   .IsRequired(false);
        }
    }

}
