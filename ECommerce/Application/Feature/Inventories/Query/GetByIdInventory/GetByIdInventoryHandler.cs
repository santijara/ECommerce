using ECommerce.Application.Common;
using ECommerce.Application.Dtos.InventoriesDtos;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Feature.Inventories.Query.GetByIdInventory
{
    /// <summary>
    /// Maneja la consulta del inventario asociado a un producto.
    /// Solo realiza lectura de datos (Query) sin modificar el estado del sistema.
    /// </summary>
    public class GetByIdInventoryHandler: IRequestHandler<GetByIdInventoryQuery, Result<InventoryResponse>>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductRepository _productRepository;
        public GetByIdInventoryHandler(IInventoryRepository inventoryRepository, IProductRepository productRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
        }

        public async Task<Result<InventoryResponse>>Handle(GetByIdInventoryQuery query, CancellationToken token)
        {
            //Validar que el producto exista
            var product = await _productRepository.GetByIdAsync(query.Id, token);
            if(product == null) return  Result<InventoryResponse>.Failure("No se encontro informacion del producto");

            //Obtener el inventario asociado al producto
            var inventory = await _inventoryRepository.GetByProductId(query.Id, token);
            if (inventory == null) return Result<InventoryResponse>.Failure("No se encontro informacion del inventario");

            //Construcción del DTO combinando información de producto e inventario
            var response = new InventoryResponse
            {
                Id = inventory.Id,
                ProductId =inventory.ProductId,
                Name = product.Name,
                Description = product.Description,
                Amount = product.Price.Amount,
                Currency = product.Price.Currency,
                TotalStock  = inventory.TotalStock,
                CategoryId = product.CategoryId,
                IsActive = product.IsActive
            };

            return Result<InventoryResponse>.Success(response);
        }
    }
}
