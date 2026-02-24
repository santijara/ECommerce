using ECommerce.Application.Common;
using ECommerce.Application.Dtos.InventoriesDtos;
using MediatR;

namespace ECommerce.Application.Feature.Inventories.Command.UpdateInventory
{

    /// <summary>
    /// Command que inicia el proceso de actualizacion del inventario.
    /// Se procesa a través de MediatR y retorna un Result con la respuesta.
    /// Implementa CQRS pattern separando escritura de lectura.
    /// </summary>
    public record UpdateInventoryCommand(
        Guid Id,
        int TotalStock
        ):IRequest<Result<UpdateInventoryResponse>>;
   
}
