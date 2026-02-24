using ECommerce.Application.Common;
using ECommerce.Application.Dtos;
using ECommerce.Application.Feature.Checkout;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
   
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor que inyecta MediatR para enviar comandos de checkout.
        /// </summary>
        public CheckoutController(IMediator checkoutService)
        {
            _mediator = checkoutService;
        }

        /// <summary>
        /// Endpoint para procesar el pago y finalizar el checkout de un carrito.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutCommand command)
        {        
            var result = await _mediator.Send(command);

            if (!result.IsSuccess) return BadRequest(ApiResponse<string>.Fail(result.Error));
            return Ok(ApiResponse<Guid>.Ok(result.Value));
        }
    }
}
