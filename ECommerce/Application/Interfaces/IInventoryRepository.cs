using ECommerce.Domain.Entities.Inventory;
using ECommerce.Domain.Entities.Orders;

namespace ECommerce.Application.Interfaces
{
    /// <summary>
    /// Repositorio para manejar la persistencia y consulta del inventario de productos.
    /// Define las operaciones básicas que cualquier implementación debe cumplir.
    /// </summary>
    public interface IInventoryRepository
    {
        Task<Inventory?> GetByProductId(Guid productId, CancellationToken cancellationToken);

        Task Add(Inventory inventory, CancellationToken cancellation);

        Task Update(Inventory inventory);
    }
}
