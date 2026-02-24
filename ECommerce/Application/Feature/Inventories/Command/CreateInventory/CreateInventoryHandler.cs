using ECommerce.Application.Common;
using ECommerce.Application.Dtos.InventoriesDtos;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Inventory;
using MediatR;

namespace ECommerce.Application.Feature.Inventories.Command.CreateInventory
{
    /// <summary>
    /// Maneja la creación del inventario asociado a un producto.
    /// delega las reglas de negocio al dominio (Inventory).
    /// </summary>
    public class CreateInventoryHandler:IRequestHandler<CreateInventoryCommand, Result<InventoryResponse>>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateInventoryHandler(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<Result<InventoryResponse>>Handle(CreateInventoryCommand command, CancellationToken cancellation)
        {

            //verificar que el producto exista
            var product = await _productRepository.GetByIdAsync(command.ProductId, cancellation);
            if (product == null) return Result<InventoryResponse>.Failure("No se encontro ningun producto relacionado");

            /// Factory Method del agregado Inventory.
            /// Garantiza la creación válida del inventario
            /// Implementa el patrón Result para manejo explícito de errores.
            var inventory = Inventory.Create(product.Id, command.TotalStock);
            if (inventory.IsFailure) return Result<InventoryResponse>.Failure(inventory.Error);


            //Persistencia transaccional
            await _inventoryRepository.Add(inventory.Value, cancellation);
            await _unitOfWork.SaveChangesAsync(cancellation);


            //Construcción del DTO de respuesta
            // Se combinan datos del producto y del inventario
            var response = new InventoryResponse {Id = inventory.Value.Id,ProductId =inventory.Value.ProductId, 
                Name = product.Name, Description = product.Description, Amount = product.Price.Amount,
                Currency = product.Price.Currency, TotalStock  = inventory.Value.TotalStock, CategoryId = product.CategoryId, IsActive = product.IsActive
            };

            return Result<InventoryResponse>.Success(response);
                
        }
    }
}
