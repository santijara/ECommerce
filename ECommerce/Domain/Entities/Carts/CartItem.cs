using ECommerce.Domain.Entities.Products;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities.Carts
{
    /// <summary>
    /// Representa un item dentro del carrito de compras.
    /// </summary>
    public sealed class CartItem
    {
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; private set; }

        private CartItem() { } // EF

        internal CartItem(Guid productId, int quantity, Money unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        /// <summary>
        /// Incrementa la cantidad del item.
        /// </summary>
       
        internal void IncreaseQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainExceptions("Cantidad inválida");

            Quantity += quantity;
        }

       
    }
}
