using FluentValidation;

namespace ECommerce.Application.Feature.Product.Command.UpdateProduct
{
    /// <summary>
    /// Valida las reglas de entrada (input validation) para el comando UpdateProductValidator.
    /// Se ejecuta antes de que el handler procese la lógica de aplicación.
    /// No contiene reglas de negocio del dominio, solo validación de formato y consistencia básica.
    /// </summary>
    public sealed class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("El identificador del producto es obligatorio.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(150)
                .WithMessage("El nombre del producto no puede superar los 150 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("El precio debe ser mayor a cero.");

            RuleFor(x => x.Currency)
                .NotEmpty()
                .WithMessage("La moneda es obligatoria.")
                .Length(3)
                .WithMessage("La moneda debe tener exactamente 3 caracteres.")
                .Matches("^[A-Z]{3}$")
                .WithMessage("La moneda debe estar en mayúsculas (por ejemplo: USD, COP, EUR).");
        }
    }
}
