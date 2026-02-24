using FluentValidation;

namespace ECommerce.Application.Feature.Product.Command.CreateProduct
{
    /// <summary>
    /// Valida las reglas de entrada (input validation) para el comando CreateProductCommand.
    /// Se ejecuta antes de que el handler procese la lógica de aplicación.
    /// No contiene reglas de negocio del dominio, solo validación de formato y consistencia básica.
    /// </summary>
    public sealed class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        
    public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio")
                .MaximumLength(150).WithMessage("El nombre del producto debe contener maximo 150 caracteres");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripcion es requerida")
                .MaximumLength(500).WithMessage("la descripcion debe contener maximo 500 caracteres");

            RuleFor(x => x.Amount)
                  .GreaterThan(0)
                  .WithMessage("El valor debe ser mayor a cero.");

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
