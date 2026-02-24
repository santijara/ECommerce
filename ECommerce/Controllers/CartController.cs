using ECommerce.Application.Common;
using ECommerce.Application.Dtos.CartDtos;
using ECommerce.Application.Feature.Carts.Command.CreateCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor que inyecta MediatR para enviar comandos y consultas.
        /// </summary>
        /// <param name="mediator">Instancia de MediatR.</param>
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crea un carrito de compras con los datos recibidos.
        /// </summary>
        /// <param name="cartCommand">Comando con la información del carrito y del usuario.</param>
        /// <param name="token">Token de cancelación para operaciones asincrónicas.</param>
        /// <returns>Retorna la información del carrito creado o un error si falla.</returns>
        [HttpPost]

        [HttpPost]
        public async Task<IActionResult>CreateCart(CreateCartCommand cartCommand, CancellationToken token)
        {
            //Envía el comando al handler correspondiente usando MediatR
            var response = await _mediator.Send(cartCommand, token);

            //Envía el comando al handler correspondiente usando MediatR
            if (response.IsFailure) return NotFound(ApiResponse<string>.Fail(response.Error));

            //Si es exitoso, retorna 200 OK con la información del carrito
            return Ok(ApiResponse<CreateCartResponse>.Ok(response.Value));
        }
    }
}
