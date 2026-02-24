namespace ECommerce.Domain.Entities.Payments
{
    /// <summary>
    /// Representa el estado de los pagos realizados
    /// </summary>
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }

}
