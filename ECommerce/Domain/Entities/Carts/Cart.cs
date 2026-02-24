using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Products;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities.Carts
{
    public sealed class Cart
    {
        //Lista interna de items, encapsulada para proteger invariantes
        private readonly List<CartItem> _items = new();

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public bool IsActive { get; private set; }

        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        //Constructor privado y vacio requerido por EF
        private Cart() { }

        private Cart(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
            IsActive = true;
        }

        public void Close()
        {
            IsActive = false;
        }

        ///Patrón Factory Method para mantener encapsulamiento y consistencia
        public static Cart create(Guid userid)
        {
            return new Cart(Guid.NewGuid(), userid);
          
        }


        /// <summary>
        /// Agrega un producto al carrito.
        /// Si el producto ya existe, incrementa la cantidad.
        /// Lanza excepción si la cantidad es inválida (cantidad <= 0).
        /// </summary>
        public void AddItem(Guid productId, int quantity, Money price)
        {
            if (quantity <= 0)throw new DomainExceptions("Cantidad inválida");

            var existingItem = _items
                .FirstOrDefault(x => x.ProductId == productId);

            if (existingItem is not null)
            {
                existingItem.IncreaseQuantity(quantity);
                return;
            }

            _items.Add(new CartItem(productId, quantity, price));
        }

        /// <summary>
        /// Limpia todos los items del carrito.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

    }
}
