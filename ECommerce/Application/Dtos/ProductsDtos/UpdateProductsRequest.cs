namespace ECommerce.Application.Dtos.ProductsDtos
{
    /// <summary>
    /// DTO que representa la información necesaria para actualizar un producto en el sistema.
    /// Se utiliza para recibir los datos desde el cliente o la API.
    /// </summary>
    public class UpdateProductsRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Currency {  get; set; }

    }
}
