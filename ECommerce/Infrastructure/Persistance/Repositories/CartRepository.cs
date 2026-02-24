using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistance.Repositories
{
    /// <summary>
    /// Repositorio de carrito de compras que implementa ICartRepository.
    /// Patrón: Repository
    /// Responsable de la persistencia y recuperación de entidades Cart desde la base de datos.
    /// Usa EF Core como ORM.
    /// </summary>
    public sealed class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Obtiene un carrito por su Id, incluyendo sus items.           
        /// </summary>
        public async Task<Cart?> GetByIdCart(Guid id, CancellationToken cancellation)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id, cancellation);
        }

        /// <summary>
        /// Obtiene el carrito activo de un usuario por su Id.
        /// Útil para validar si el usuario ya tiene un carrito en curso
        /// </summary>
        public async Task<Cart?> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellation)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.IsActive, cancellation);
        }

        /// <summary>
        /// Agrega un nuevo carrito al contexto para su posterior persistencia.
        /// Patrón: Unit of Work
        /// No hace commit inmediato; el guardado real ocurre en _unitOfWork.SaveChangesAsync()
        /// </summary>
        public async Task AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        /// <summary>
        /// Marca un carrito como modificado en el contexto para su posterior actualización en BD.
        /// Patrón: Unit of Work
        /// </summary>
        public async Task Update(Cart cart)
        {
            _context.Carts.Update(cart);
        }
    }

}
