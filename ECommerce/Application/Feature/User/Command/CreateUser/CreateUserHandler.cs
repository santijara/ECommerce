using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Users;
using MediatR;

namespace ECommerce.Application.Feature.User.Command.CreateUser
{

    /// <summary>
    /// Maneja la creación de un usuario.
    /// delega la creación al dominio y persiste el agregado.
    /// </summary>
    public class CreateUserHandler: IRequestHandler<CreateUserCommand, Result>
    {
    
        
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result>Handle(CreateUserCommand command, CancellationToken cancellation)
        {
            //Crear el Value Object Email.
            //Se transforma el string en un objeto validado del dominio.
            var emailResult = Email.Create(command.Email);

            // Validación defensiva para mantener consistencia
            // con el patrón Result y evitar excepciones.
            if (emailResult.IsFailure)
                return Result.Failure(emailResult.Error);

            var user = new EUsers(
                command.Name,
                command.LastName,
                command.NumberPhone,
                emailResult.Value,
                command.Adress,
                command.Document);

            //Delegar la persistencia al servicio.
            //El servicio puede contener validaciones adicionales
            //como verificación de duplicados o reglas transversales.
            await _userService.CreateUser(user);

            //Retornar resultado exitoso sin exponer entidad de dominio.
            return Result.Success();
        }
    }
}
