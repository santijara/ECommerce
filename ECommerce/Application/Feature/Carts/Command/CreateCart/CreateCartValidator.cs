using FluentValidation;

namespace ECommerce.Application.Feature.Carts.Command.CreateCart
{
    /// <summary>
    /// Valida el request CreateCartCommand antes de ejecutar la lógica de aplicación.
    /// </summary>

    public class CreateCartValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("El identificador del usuario es obligatorio.");

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("El identificador del producto es obligatorio.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("La cantidad debe ser mayor a cero.")
                .LessThanOrEqualTo(100)
                .WithMessage("La cantidad no puede ser mayor a 100 unidades.");
        }
    }

}
