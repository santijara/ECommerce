using ECommerce.Domain.Entities.Products;

namespace ECommerce.Domain.Entities.Orders
{
    /// <summary>
    /// Entidad de dominio items de la orden (OrderItem)
    /// Representa los items asociados a una orden
    /// </summary>
    public sealed class OrderItem
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public Money UnitPrice { get; private set; }

        private OrderItem() { } 

        internal OrderItem(Guid orderId, Guid productId, int quantity, Money unitPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal GetTotal() => UnitPrice.Amount * Quantity;
    }

}
