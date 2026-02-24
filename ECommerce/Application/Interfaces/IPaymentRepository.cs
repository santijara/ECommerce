using ECommerce.Domain.Entities.Payments;

namespace ECommerce.Application.Interfaces
{
    public interface IPaymentRepository
    {
        /// <summary>
        /// Repositorio para manejar la persistencia y proceso de un pago agregando el registro a la BD
        /// </summary>
        Task<Payment> AddAsync(Payment payment, CancellationToken cancellationToken);
    }

}
