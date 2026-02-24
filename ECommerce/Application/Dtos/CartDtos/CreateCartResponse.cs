namespace ECommerce.Application.Dtos.CartDtos
{
    /// <summary>
    /// DTO que representa la respuesta al crear un carrito (Cart) en la aplicación.
    /// Contiene los datos necesarios para identificar el carrito creado y los items agregados.
    /// </summary>
    public class CreateCartResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
