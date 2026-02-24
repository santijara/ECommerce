using ECommerce.Application.Common;
using ECommerce.Application.Dtos.ProductsDtos;
using MediatR;

namespace ECommerce.Application.Feature.Product.Query.GetAllProduct
{
    /// <summary>
    /// Consulta que representa la intención de obtener
    /// todos los productos registrados.
    /// 
    /// Forma parte del modelo de lectura (CQRS).
    /// No contiene estado porque no requiere parámetros.
    /// </summary>
    public sealed record GetAllProductsQuery()
    : IRequest<Result<IEnumerable<ProductDto>>>;

}
