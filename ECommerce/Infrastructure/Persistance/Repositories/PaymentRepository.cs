using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Payments;

namespace ECommerce.Infrastructure.Persistance.Repositories
{
    /// <summary>
    /// Repositorio de pago que implementa IPaymentRepository.
    /// Patrón: Repository
    /// Responsable de la persistencia y recuperación de entidades Payment desde la base de datos.
    /// Usa EF Core como ORM.
    /// </summary>
    public sealed class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Agrega un nuevo pago
        /// </summary>
        public async Task<Payment> AddAsync(Payment payment, CancellationToken cancellationToken)
        {
            await _context.Payments.AddAsync(payment, cancellationToken);
            return payment;
        }
    }

}
