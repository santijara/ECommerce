using ECommerce.Application.Common;
using ECommerce.Application.Dtos.InventoriesDtos;
using MediatR;

namespace ECommerce.Application.Feature.Inventories.Query.GetByIdInventory
{
    /// <summary>
    /// Query que representa la solicitud para obtener
    /// la información de inventario asociada a un producto.
    /// Forma parte del modelo de lectura en CQRS.
    /// </summary>
    public record GetByIdInventoryQuery(
        Guid Id
        ): IRequest<Result<InventoryResponse>>;
    
}
