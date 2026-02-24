using ECommerce.Application.Common;

namespace ECommerce.Application.Interfaces
{
    public interface IPaymentGateway
    {
        /// <summary>
        /// Repositorio para manejar la persistencia y proceso de pago ante una api externa.
        /// Donde se obtiene una respuesta la cual se procesa segun su estado.
        /// </summary>
        Task<PaymentResult> ProcessAsync(Guid orderId,decimal amount,string currency,CancellationToken cancellationToken);
    }
}
