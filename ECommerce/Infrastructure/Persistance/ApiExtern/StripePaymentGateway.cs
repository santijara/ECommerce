using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Persistance.ApiExtern
{
    /// <summary>
    /// Implementación de un Gateway de Pagos usando Stripe (simulado)
    /// Encapsula la integración con un proveedor externo (Stripe) para procesar pagos
    /// Permite desacoplar la lógica de dominio de la integración real con el proveedor
    /// </summary>
    public sealed class StripePaymentGateway : IPaymentGateway
    {
        public async Task<PaymentResult> ProcessAsync(
            Guid orderId,
            decimal amount,
            string currency,
            CancellationToken cancellationToken)
        {
            // Simulación llamada externa
            await Task.Delay(500, cancellationToken);

            // Aqui iria integracion real con Stripe SDK

            return PaymentResult.Success(Guid.NewGuid().ToString());
        }
    }

}
