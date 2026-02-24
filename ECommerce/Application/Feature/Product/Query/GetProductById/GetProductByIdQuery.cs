using ECommerce.Application.Common;
using ECommerce.Application.Dtos.ProductsDtos;
using MediatR;

namespace ECommerce.Application.Feature.Product.Query.GetProductById
{
    /// <summary>
    /// Consulta que representa la intención de obtener
    /// por medio de un id un producto registrado
    /// 
    /// Forma parte del modelo de lectura (CQRS)
    /// No contiene estado porque no requiere parámetros
    /// </summary>
    public sealed record GetProductByIdQuery(Guid Id)
     : IRequest<Result<ProductDto>>;

}
