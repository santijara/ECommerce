using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistance.Repositories
{
    /// <summary>
    /// Repositorio de un producto que implementa IProductRepository.
    /// Patrón: Repository
    /// Responsable de la persistencia y recuperación de entidades Eproducts desde la base de datos.
    /// Usa EF Core como ORM.
    /// </summary>
    public sealed class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Agrega un nuevo producto
        /// </summary>

        public async Task AddAsync(Eproducts product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        /// <summary>
        /// Consulta un producto por id
        /// </summary>
        public async Task<Eproducts?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        /// <summary>
        /// Consulta todos los productos creados
        /// </summary>
        public async Task<List<Eproducts>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }


        /// <summary>
        /// Actualiza los productos
        /// </summary>
        public void Update(Eproducts product)
        {
           _context.Products.Update(product);
        }

        /// <summary>
        /// Elimina los productos
        /// </summary>

        public void Remove(Eproducts product)
        {
            _context.Products.Remove(product);
        }
    }
}
