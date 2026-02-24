using ECommerce.Application.Common;
using ECommerce.Application.Dtos.ProductsDtos;
using ECommerce.Application.Feature.Product.Command.CreateProduct;
using ECommerce.Application.Feature.Product.Command.DeleteProduct;
using ECommerce.Application.Feature.Product.Command.UpdateProduct;
using ECommerce.Application.Feature.Product.Query.GetAllProduct;
using ECommerce.Application.Feature.Product.Query.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Endpoint para crear un nuevo producto.
        /// </summary>

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            if(response.IsFailure) return BadRequest(ApiResponse<string>.Fail(response.Error));
            return Ok(ApiResponse<Guid>.Ok(response.Value));
        }

        /// <summary>
        /// Endpoint para buscar por id un producto.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id),cancellationToken);
            if(result.IsFailure) return NotFound(ApiResponse<string>.Fail(result.Error));
            return Ok(ApiResponse<ProductDto>.Ok(result.Value));
        }


        /// <summary>
        /// Endpoint para buscar todos los productos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProductsQuery(),cancellationToken);
            if (result.IsFailure) return NotFound(ApiResponse<string>.Fail(result.Error));
            return Ok(ApiResponse<IEnumerable<ProductDto>>.Ok(result.Value));
        }

        /// <summary>
        /// Endpoint para actualizar los productos.
        /// </summary>

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductsRequest command,CancellationToken cancellationToken)
        {    
          var response =  await _mediator.Send(new UpdateProductCommand(id, command.Name, command.Price, command.Currency), cancellationToken);
            if (response.IsFailure) return NotFound(ApiResponse<string>.Fail(response.Error));
            return Ok(ApiResponse<ProductDto>.Ok(response.Value));
        }

        /// <summary>
        /// Endpoint para eliminar los productos.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
           var response =  await _mediator.Send(new DeleteProductCommand(id),cancellationToken);
            if (response.IsFailure) return NotFound(ApiResponse<string>.Fail(response.Error));
            return Ok(ApiResponse<bool>.Ok(response.IsSuccess));
           
        }
    }

}
