using ECommerce.Application.Common;
using ECommerce.Application.Dtos.UserDtos;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.Feature.User.Query.GetByIdUser
{
    /// <summary>
    /// Maneja la consulta para obtener un usuario en específico por su identificador.
    /// </summary>

    public class GetByIdUserHandler: IRequestHandler<GetByIdUserQuery, Result<UserResponse>>
    {
        private readonly IUserService _userService;

        public GetByIdUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result<UserResponse>>Handle(GetByIdUserQuery get, CancellationToken cancellation)
        {
            //Obtener id usuario
            var user = await _userService.GetByIdUser(get.id);

            //Validacion
            if (user == null) return Result<UserResponse>.Failure("No se encontro informacion");


            //Proyección a DTO
            return Result<UserResponse>.Success(new UserResponse() { Id = user.Id, Name = user.Name, LastName = user.LastName, 
                Document = user.Document, NumberPhone = user.NumberPhone });
        }
    }
}
