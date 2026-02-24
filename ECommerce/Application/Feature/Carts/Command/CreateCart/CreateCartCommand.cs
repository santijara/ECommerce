using ECommerce.Application.Common;
using ECommerce.Application.Dtos.CartDtos;
using MediatR;

namespace ECommerce.Application.Feature.Carts.Command.CreateCart
{
    /// <summary>
    /// Command que representa la intención de crear un carrito
    /// para un usuario agregando un producto con una cantidad específica.
    /// Se procesa a través de MediatR y retorna un Result con la respuesta.
    /// /// Implementa CQRS pattern separando escritura de lectura.
    /// </summary>
    public record CreateCartCommand(    
        Guid UserId,
        Guid ProductId,
        int Quantity

        ):IRequest<Result<CreateCartResponse>>;
    
}
