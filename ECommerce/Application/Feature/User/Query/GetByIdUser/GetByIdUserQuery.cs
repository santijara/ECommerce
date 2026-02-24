using ECommerce.Application.Common;
using ECommerce.Application.Dtos.UserDtos;
using MediatR;

namespace ECommerce.Application.Feature.User.Query.GetByIdUser
{
    /// <summary>
    /// Consulta que representa la intención de obtener
    /// por medio de un id un usuario registrado
    /// 
    /// Forma parte del modelo de lectura (CQRS)
    /// No contiene estado porque no requiere parámetros
    /// </summary>
    public record GetByIdUserQuery(Guid id):IRequest<Result<UserResponse>>;
   
}
