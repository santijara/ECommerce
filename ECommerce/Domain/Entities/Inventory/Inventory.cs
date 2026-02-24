using ECommerce.Application.Common;

namespace ECommerce.Domain.Entities.Inventory
{
    /// <summary>
    /// Entidad de dominio "Inventario".
    /// 🔹 Gestiona el stock de un producto.
    /// 🔹 Permite reservas y control de inventario disponible.
    /// 🔹 Encapsula las reglas de negocio del stock.
    /// </summary>
    public sealed class Inventory
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }

        public int TotalStock { get; private set; }
        public int ReservedStock { get; private set; }

        public int AvailableStock => TotalStock - ReservedStock;

        //Constructor privado requerido por EF Core
        private Inventory() { } 

        private Inventory(Guid productId, int initialStock)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            TotalStock = initialStock;
            ReservedStock = 0;
        }

        /// <summary>
        /// Factory Method para crear un inventario con validación de dominio.
        /// Devuelve Result<Inventory> indicando éxito o error.
        /// Evita creación de inventarios inválidos.
        /// </summary>
        public static Result<Inventory> Create(Guid productId, int initialStock)
        {
            if (productId == Guid.Empty)
                return Result<Inventory>.Failure("Producto inválido");

            if (initialStock < 0)
                return Result<Inventory>.Failure("Stock inicial inválido");

            return Result<Inventory>.Success(new Inventory(productId, initialStock));
        }

        /// <summary>
        /// Actualiza el stock total del inventario.
        /// Puede ser usado para ajustes de inventario o reposición.
        /// </summary>
        public void Update(int initialStock)
        {
            TotalStock = initialStock;
        }

        /// <summary>
        /// Reserva una cantidad de stock para un pedido.
        /// Devuelve Result indicando éxito o error.
        /// Impide reservar más de lo disponible.
        /// </summary>
        public Result Reserve(int quantity)
        {
            if (quantity <= 0)
                return Result.Failure("Cantidad inválida");

            if (AvailableStock < quantity)
                return Result.Failure("Stock insuficiente");

            ReservedStock += quantity;
            return Result.Success();
        }

  

    }

}
