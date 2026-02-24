using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistance.Repositories
{
    /// <summary>
    /// Repositorio del inventario que implementa IInventoryRepository.
    /// Patrón: Repository
    /// Responsable de la persistencia y recuperación de entidades inventory desde la base de datos.
    /// Usa EF Core como ORM.
    /// </summary>
    public class InventoryRepository: IInventoryRepository
    {
        private readonly AppDbContext _context;

        public InventoryRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un inventartio por su Id
        /// </summary>
        public async Task<Inventory?> GetByProductId(Guid productId, CancellationToken token)
            => await _context.Inventories
                .FirstOrDefaultAsync(x => x.ProductId == productId, token);

        /// <summary>
        /// Agrega un nuevo inventario
        /// </summary>
        public async Task Add(Inventory inventory, CancellationToken token)
            => await _context.Inventories.AddAsync(inventory, token);

        /// <summary>
        /// Actualiza el inventario
        /// </summary>
        public Task Update(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            return Task.CompletedTask;
        }
    }
}
