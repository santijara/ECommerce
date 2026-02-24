using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistance.Repositories
{
    /// <summary>
    /// Repositorio de la orden que implementa OrderRepository.
    /// Patrón: Repository
    /// Responsable de la persistencia y recuperación de entidades Order desde la base de datos.
    /// Usa EF Core como ORM.
    /// </summary>
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Consulta una orden por id
        /// </summary>
        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        /// <summary>
        /// Agrega una nueva orden
        /// </summary>
        public async Task AddAsync(Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
        }
    }

}
