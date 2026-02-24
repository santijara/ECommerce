using ECommerce.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistance.Configurations
{
    /// <summary>
    /// Configuración de la entidad Users para Entity Framework Core
    /// Patrón Fluent API Configuration
    /// Permite configurar la tabla, claves, relaciones y comportamiento de navegación
    /// Esto reemplaza o complementa las anotaciones de datos en la entidad
    /// </summary>
    public class ConfigurationDbContext: IEntityTypeConfiguration<EUsers>
    {
        public void Configure(EntityTypeBuilder<EUsers> builder)
        {
            builder.ToTable("UsersEcommerce");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).HasConversion(email =>email.Value, value =>  Email.FromDatabase(value)).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Adress).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NumberPhone).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Document).IsRequired().HasMaxLength(100);
        }
    
    }
    
}
