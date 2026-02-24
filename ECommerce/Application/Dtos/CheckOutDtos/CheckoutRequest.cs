namespace ECommerce.Application.Dtos.CheckOutDtos
{
    /// <summary>
    /// DTO que representa la información necesaria para procesar un checkout (finalizar compra) en el sistema.
    /// Se utiliza para recibir los datos desde el cliente o la API.
    /// </summary>
    public sealed class CheckoutRequest
    {
        public Guid UserId { get; set; }
        public Guid CartId { get; set; }
        public string PaymentMethod { get; set; } = default!;
    }

}
