using ECommerce.Application.Common;
using ECommerce.Application.Dtos.ProductsDtos;
using MediatR;

namespace ECommerce.Application.Feature.Product.Command.UpdateProduct
{
    /// <summary>
    /// Command que representa la intención de actualizar
    /// los datos básicos de un producto existente.
    /// Forma parte del modelo CQRS y retorna un Result<ProductDto>
    /// </summary>
    public sealed record UpdateProductCommand(
     Guid Id,
     string Name,
     decimal Price,
     string Currency
    
 ) : IRequest<Result<ProductDto>>;

}
