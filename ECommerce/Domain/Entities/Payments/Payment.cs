using ECommerce.Application.Common;

using ECommerce.Domain.Entities.Products;

namespace ECommerce.Domain.Entities.Payments
{
    /// <summary>
    /// Entidad de dominio "Pagos" (Payment)
    /// Representa el pago realizado de una orden generada
    /// </summary>
    public sealed class Payment
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }

        public Money Amount { get; private set; }
        public PaymentStatus Status { get; private set; }

        public string? TransactionId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        //EF
        private Payment() { } 

        private Payment(Guid orderId, Money amount)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            Amount = amount;
            Status = PaymentStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        //Factory method
        public static Result<Payment> Create(Guid orderId, Money amount)
        {
            if (orderId == Guid.Empty)
                return Result<Payment>.Failure("Order inválida");

            if (amount.Amount <= 0)
                return Result<Payment>.Failure("Monto inválido");

            return Result<Payment>.Success(
                new Payment(orderId, amount)
            );
        }

        //Cambia el estado de un pago realizado a completado
        public Result MarkAsCompleted(string transactionId)
        {
            if (Status != PaymentStatus.Pending)
                return Result.Failure("Pago ya procesado");

            if (string.IsNullOrWhiteSpace(transactionId))
                return Result.Failure("TransactionId inválido");

            Status = PaymentStatus.Completed;
            TransactionId = transactionId;
      
            return Result.Success();
        }

        //Cambia el estado si un pago no se realizo de manera correcta
        // a fallido
        public Result MarkAsFailed()
        {
            if (Status != PaymentStatus.Pending)
                return Result.Failure("No puede marcarse como fallido");

            Status = PaymentStatus.Failed;
            return Result.Success();
        }

       
    }

}
