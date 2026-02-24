using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Carts;
using ECommerce.Domain.Entities.Orders.Events;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Domain.Entities.Orders
{
    /// <summary>
    /// Entidad de dominio "Orden" (Order)
    /// Representa la orden de compra de un usuario.
    /// Contiene lógica de negocio para agregar items, calcular totales y manejar estados.
    /// Hereda de AgregateRoot para soportar eventos de dominio.
    /// </summary>

    public sealed class Order: AgregateRoot
    {
        private readonly List<OrderItem> _items = new();

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public string Currency { get; private set; }

        //EF
        private Order() { } 

        private Order(Guid id, Guid userId, string currency)
        {
            Id = id;
            UserId = userId;
            Currency = currency;
            Status = OrderStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        // Factory Method para crear una orden a partir de items de carrito
        public static Result<Order> Create(Guid userId, IEnumerable<CartItem> cartItems)
        {
            if (!cartItems.Any())
                return Result<Order>.Failure("La orden debe tener items");

            var order = new Order(Guid.NewGuid(), userId,"COP");

            foreach (var item in cartItems)
            {
                order.AddItem(item.ProductId, item.Quantity, item.UnitPrice);
            }

            return Result<Order>.Success(order);
        }

        private void AddItem(Guid productId, int quantity, Money unitPrice)
        {
            _items.Add(new OrderItem(Id, productId, quantity, unitPrice));
        }

        // Calcula el total de la orden sumando todos los items
        public decimal GetTotal()=> _items.Sum(x => x.GetTotal());

        /// <summary>
        /// Marca la orden como pagada
        /// Solo se puede pagar si está pendiente
        /// Genera un evento de dominio OrderPaidDomainEvent
        /// </summary>
        public Result MarkAsPaid()
        {
            if (Status != OrderStatus.Pending)
                return Result.Failure("Solo órdenes pendientes pueden pagarse");

            Status = OrderStatus.Paid;
            AddDomainEvent(new OrderPaidDomainEvent(Id, _items.Sum(x => x.GetTotal())));
            return Result.Success();
        }

        // Subtotal de la orden (sin impuestos)
        public decimal SubTotal  => _items.Sum(x => x.UnitPrice.Amount * x.Quantity);

        // Impuesto aplicado (19%)
        public decimal Tax => SubTotal * 0.19m;

        //Total de la orden (subtotal + impuestos)
        public decimal Total => SubTotal + Tax;
    }

}
