using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Interfaces
{
    /// <summary>
    /// Repositorio para manejar la persistencia y consultas de los productos
    /// </summary>
    public interface IProductRepository
    {
        Task AddAsync(Eproducts product, CancellationToken cancellationToken);
        Task<Eproducts?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Eproducts>> GetAllAsync(CancellationToken cancellationToken);
        void Update(Eproducts product);
        void Remove(Eproducts product);
    }

}
