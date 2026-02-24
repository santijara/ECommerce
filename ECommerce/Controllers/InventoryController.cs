using ECommerce.Application.Common;
using ECommerce.Application.Dtos.InventoriesDtos;
using ECommerce.Application.Feature.Inventories.Command.CreateInventory;
using ECommerce.Application.Feature.Inventories.Command.UpdateInventory;
using ECommerce.Application.Feature.Inventories.Query.GetByIdInventory;
using ECommerce.Application.Feature.Product.Query.GetProductById;
using ECommerce.Infrastructure.Persistance.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint para crear un nuevo inventario de un producto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult>CreateInventory(CreateInventoryCommand request, CancellationToken token)
        {
            var result = await _mediator.Send(request, token);
            if (result.IsFailure) return Ok(ApiResponse<string>.Fail(result.Error));
            return Ok(ApiResponse<InventoryResponse>.Ok(result.Value));
        }


        /// <summary>
        /// Endpoint para buscar por id un inventario.
        /// </summary>
        [HttpGet("id")]
        public async Task<IActionResult>GetByIdInventory(Guid id, CancellationToken token)
        {
            var result = await _mediator.Send(new GetByIdInventoryQuery(id),token);
            if (result.IsFailure) return NotFound(ApiResponse<string>.Fail(result.Error));
            return Ok(ApiResponse<InventoryResponse>.Ok(result.Value));
        }

        /// <summary>
        /// Endpoint para actualizar un inventario.
        /// </summary>
        [HttpPut("id")]
        public async Task<IActionResult>UpdateInventory(Guid id, UpdateInventoryRequest request, CancellationToken token)
        {
            var response = await _mediator.Send(new UpdateInventoryCommand(id, request.TotalStock),token);
            if (response.IsFailure) return NotFound(ApiResponse<string>.Fail(response.Error));
            return Ok(ApiResponse<UpdateInventoryResponse>.Ok(response.Value));
        }
    }
}
