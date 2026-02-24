using ECommerce.Application.Common;

namespace ECommerce.Domain.Entities.Products
{
    /// <summary>
    /// Entidad de dominio "Productos" (Products)
    /// Representa los productos que se pueden crear en el sistema
    /// </summary>
    public class Eproducts
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public Guid CategoryId { get; private set; }
        public bool IsActive { get; private set; }

        //EF
        private Eproducts() { } 

        private Eproducts(Guid id, string name, string description, Money price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CategoryId = Guid.NewGuid();
            IsActive = true;
        }

        //Factory method
        public static Eproducts Create(string name,string description,Money price)
        {
            return new Eproducts( Guid.NewGuid(),name,description,price);
        }

        //Este metodo se utiliza para actualizar informacion del producto
        public void Update(string name,Money Amount)
        {
            Name = name;
            Price = Amount;
        }

    }

    /// <summary>
    /// Objeto de valor "Money"
    /// Representa un valor monetario con cantidad y moneda.
    /// Incluye validación de negocio en la creación.
    /// </summary>
    public sealed record Money
    {
        public decimal Amount { get; private set; }
        public string Currency {  get; private set; }

        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        //Factory Method
        public static Result<Money>Create(decimal amount, string currency)
        {
            if (amount <= 0)
                return Result<Money>.Failure("El precio debe ser mayor a cero");

            if (string.IsNullOrWhiteSpace(currency))
                return Result<Money>.Failure("Moneda requerida");

            return Result<Money>.Success(new Money(amount, currency.ToUpper()));

        }
    }
}
