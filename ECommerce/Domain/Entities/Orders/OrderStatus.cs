namespace ECommerce.Domain.Entities.Orders
{
    /// <summary>
    /// Representa el estado de las ordenes
    /// </summary>
    public enum OrderStatus
    {
        Pending,
        Paid,
        Cancelled,
        Shipped
    }

}
