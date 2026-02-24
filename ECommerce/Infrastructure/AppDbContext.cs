using ECommerce.Application.Interfaces;
using ECommerce.Domain;
using ECommerce.Domain.Entities.Carts;
using ECommerce.Domain.Entities.Inventory;
using ECommerce.Domain.Entities.Invoices;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Payments;
using ECommerce.Domain.Entities.Products;
using ECommerce.Domain.Entities.Users;
using ECommerce.Infrastructure.Persistance;
using ECommerce.Infrastructure.Persistance.Configurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure
{
    /// <summary>
    /// Contexto principal de Entity Framework Core para la aplicación.
    /// Implementa IUnitOfWork para coordinar la persistencia de cambios.
    /// Coordina los eventos de dominio mediante MediatR.
    /// Contiene todos los DbSet necesarios para los agregados de la aplicación.
    /// </summary>
    public class AppDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor principal
        /// Recibe opciones de EF Core y el mediador para manejar eventos de dominio.
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        // Tablas de la base de datos (DbSets) correspondientes a los agregados y entidades.
        public DbSet<EUsers> Users => Set<EUsers>();
        public DbSet<Inventory> Inventories => Set<Inventory>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItem => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Eproducts> Products => Set<Eproducts>();
        public DbSet<Invoice> Invoices => Set<Invoice>();

        /// <summary>
        /// Persistencia de cambios con soporte para eventos de dominio.
        /// Patrón Unit of Work
        /// Detecta agregados que contienen eventos de dominio.
        /// Persiste los cambios y luego publica los eventos vía MediatR.
        /// Finalmente limpia los eventos de los agregados para evitar duplicados.
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Detecta todos los agregados con eventos pendientes
            var domainEntities = ChangeTracker
                .Entries<AgregateRoot>()
                .Where(x => x.Entity.DomainEvents.Any())
                .ToList();

            // Extrae todos los eventos de dominio de los agregados
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            // Persistir cambios en la base de datos
            var result = await base.SaveChangesAsync(cancellationToken);

            // Publicar todos los eventos de dominio usando MediatR
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            // Limpiar eventos de los agregados para no dispararlos de nuevo
            foreach (var entity in domainEntities)
            {
                entity.Entity.ClearDomainEvents();
            }

            return result;
        }

        /// <summary>
        /// Configuración de entidades mediante Fluent API
        /// Aplica todas las configuraciones perinentes al Assembly
        /// Permite centralizar configuraciones y mantener el código limpio
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
