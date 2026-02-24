using ECommerce.Application.Common;
using MediatR;

namespace ECommerce.Application.Feature.Product.Command.CreateProduct
{
    /// <summary>
    /// Comando que representa la intención de crear un nuevo producto.
    /// Contiene los datos necesarios para construir el agregado Product,
    /// incluyendo la información requerida para crear el Value Object Money.
    /// </summary>
    public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Amount,
    string Currency
) : IRequest<Result<Guid>>;


}
