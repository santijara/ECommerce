using ECommerce.Application.Common;
using MediatR;

namespace ECommerce.Application.Feature.User.Command.CreateUser
{
    /// <summary>
    /// Comando que representa la intención explícita de crear un nuevo usuario.
    /// Forma parte del modelo de escritura (CQRS).
    /// </summary>
    public record CreateUserCommand(
        string Name,
        string LastName,
        string NumberPhone,
        string Email,
        string Adress,
        string Document
        ):IRequest<Result>;
    
}
