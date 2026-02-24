using ECommerce.Application.Common;
using ECommerce.Application.Dtos.InventoriesDtos;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Inventory;
using MediatR;

namespace ECommerce.Application.Feature.Inventories.Command.UpdateInventory
{
    /// <summary>
    /// Maneja la actualización del inventario asociado a un producto.
    /// Orquesta la obtención del agregado y delega la lógica de modificación
    /// al dominio para proteger las invariantes.
    /// </summary>
    public class UpdateInventoryHandler:IRequestHandler<UpdateInventoryCommand, Result<UpdateInventoryResponse>>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInventoryHandler(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UpdateInventoryResponse>>Handle(UpdateInventoryCommand update, CancellationToken cancellation)
        {
            //Obtener el inventario asociado al producto
            var id = await _inventoryRepository.GetByProductId(update.Id, cancellation);
            if (id == null) return Result<UpdateInventoryResponse>.Failure("No se encontro informacion");

            //Delegar la actualización al agregado
            id.Update(update.TotalStock);

            //Persistir cambios de manera transaccional
            await _inventoryRepository.Update(id);
            await _unitOfWork.SaveChangesAsync(cancellation);


            //Construcción del DTO de respuesta
            var response = new UpdateInventoryResponse
            {
                Id = id.Id,
                ProductId =id.ProductId,             
                TotalStock  = id.TotalStock,
                ReservedStock = id.ReservedStock,                      
            };

            return Result<UpdateInventoryResponse>.Success(response);
        }
    }
}
