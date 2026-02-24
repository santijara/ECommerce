using ECommerce.Domain.Entities.Carts;

namespace ECommerce.Application.Interfaces
{
    /// <summary>
    /// Repositorio para manejar la persistencia y consulta de los carritos de compra.
    /// Define las operaciones que cualquier implementación de repositorio debe cumplir.
    /// </summary>
    public interface ICartRepository
    {
        Task<Cart?> GetByIdCart(Guid userId,CancellationToken cancellation);
        Task<Cart?> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellation);
        Task AddAsync(Cart cart);
        Task Update(Cart cart);
    }
}
