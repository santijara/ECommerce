namespace ECommerce.Application.Common
{
    /// <summary>
    /// Representa una respuesta estándar para payment.
    /// Encapsula el estado de la operación, un mensaje descriptivo
    /// y los datos resultantes.
    /// </summary>

    public sealed class PaymentResult
    {
        public bool IsSuccess { get; init; }
        public string? TransactionId { get; init; }
        public string? ErrorMessage { get; init; }

        public static PaymentResult Success(string transactionId)
            => new() { IsSuccess = true, TransactionId = transactionId };

        public static PaymentResult Failure(string error)
            => new() { IsSuccess = false, ErrorMessage = error };
    }

}
