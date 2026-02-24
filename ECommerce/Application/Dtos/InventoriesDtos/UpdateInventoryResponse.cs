namespace ECommerce.Application.Dtos.InventoriesDtos
{
    /// <summary>
    /// DTO que representa la respuesta al actualizar el inventario.  
    /// </summary>
    public class UpdateInventoryResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int TotalStock { get; set; }
        public int ReservedStock { get; set; }
    }
}
