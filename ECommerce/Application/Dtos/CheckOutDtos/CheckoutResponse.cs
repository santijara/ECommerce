namespace ECommerce.Application.Dtos.CheckOutDtos
{
    /// <summary>
    /// DTO que representa la información necesaria para procesar la respuesta al Checkout..
    /// </summary>
    public sealed class CheckoutResponse
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; } = default!;
    }

}
