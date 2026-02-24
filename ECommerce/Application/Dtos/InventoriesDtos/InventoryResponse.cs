namespace ECommerce.Application.Dtos.InventoriesDtos
{
    /// <summary>
    /// DTO que representa la respuesta al registrar un nuevo inventario o buscarlo por Id
    /// </summary>

    public class InventoryResponse
    {
        public Guid Id { get;  set; }
        public Guid ProductId { get;  set; }
        public int TotalStock { get;  set; }
        public int ReservedStock { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public decimal Amount { get;  set; }
        public string Currency { get;  set; }
        public Guid CategoryId { get;  set; }
        public bool IsActive { get;  set; }
    }
}
