using ECommerce.Domain.Entities.Orders;

namespace ECommerce.Application.Interfaces
{
    /// <summary>
    /// Repositorio para manejar la persistencia y consulta de los ordenes generadas.
    /// </summary>
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddAsync(Order order, CancellationToken cancellationToken);
    }

}
