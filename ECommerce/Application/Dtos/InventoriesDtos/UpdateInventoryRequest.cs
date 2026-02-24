namespace ECommerce.Application.Dtos.InventoriesDtos
{
    /// <summary>
    /// DTO que representa la información necesaria para actualizar el inventario almacenado
    /// Se utiliza para recibir los datos desde el cliente o la API.
    /// </summary>
    public class UpdateInventoryRequest
    {
        public int TotalStock { get; set; }
    }
}
