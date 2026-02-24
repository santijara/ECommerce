using ECommerce.Application.Common;
using MediatR;

namespace ECommerce.Application.Feature.Checkout
{
    /// <summary>
    /// Command que inicia el proceso de Checkout.
    /// Contiene la información mínima necesaria para convertir
    /// un carrito en una orden y procesar su pago.
    /// Se procesa a través de MediatR y retorna un Result con la respuesta.
    /// Implementa CQRS pattern separando escritura de lectura.
    /// </summary>

    public sealed record CheckoutCommand(
      Guid UserId,
      Guid CartId
  ) : IRequest<Result<Guid>>;


}
