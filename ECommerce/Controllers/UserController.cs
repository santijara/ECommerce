using ECommerce.Application.Common;
using ECommerce.Application.Dtos.UserDtos;
using ECommerce.Application.Feature.User.Command.CreateUser;
using ECommerce.Application.Feature.User.Query.GetByIdUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult>CreateUser(CreateUserCommand createUserCommand)
        {
            var result = await _mediator.Send(createUserCommand);
            return Ok(ApiResponse<bool>.Ok(result.IsSuccess));
        }

        [HttpGet("id")]
        public async Task<IActionResult>GetByIdUser(Guid id, CancellationToken cancellation)
        {
            var response = await _mediator.Send(new GetByIdUserQuery(id),cancellation);
            if (response.IsFailure) return NotFound(ApiResponse<string>.Fail(response.Error));
            return Ok(ApiResponse<UserResponse>.Ok(response.Value));
        }
    }
}
