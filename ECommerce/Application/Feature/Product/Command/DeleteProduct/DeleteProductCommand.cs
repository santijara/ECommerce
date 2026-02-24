using ECommerce.Application.Common;
using MediatR;

namespace ECommerce.Application.Feature.Product.Command.DeleteProduct
{
    /// <summary>
    /// Comando que representa la intención de eliminar un producto.
    /// Forma parte del modelo de escritura en CQRS y provoca un cambio de estado.
    /// </summary>
    public sealed record DeleteProductCommand(Guid Id) : IRequest<Result>;

}
