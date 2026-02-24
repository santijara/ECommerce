using FluentValidation;

namespace ECommerce.Application.Feature.User.Command.CreateUser
{
    /// <summary>
    /// Validador del comando CreateUserCommand.
    /// Define reglas estructurales básicas antes de que el comando
    /// llegue al handler
    /// </summary>
    public class CreateUserValidator:AbstractValidator<CreateUserCommand>
    {

       public CreateUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Nombre invalido");
            RuleFor(x =>x.LastName).NotEmpty().MaximumLength(100).WithMessage("Apellido invalido");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email invalido");
            RuleFor(x => x.Document).NotEmpty().MaximumLength(100).WithMessage("Documento invalido");
            RuleFor(x => x.NumberPhone).NotEmpty().MaximumLength(100).WithMessage("Numero invalido");
            RuleFor(x => x.Adress).NotEmpty().MaximumLength(100).WithMessage("Direccion invalido");

        }
    }
}
