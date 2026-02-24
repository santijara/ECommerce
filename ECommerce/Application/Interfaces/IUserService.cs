using ECommerce.Domain.Entities.Users;

namespace ECommerce.Application.Interfaces
{
    /// <summary>
    /// Repositorio para manejar la persistencia y consulta de los usuarios.
    /// Define las operaciones que cualquier implementación de repositorio debe cumplir.
    /// </summary>
    public interface IUserService
    {
        Task CreateUser(EUsers eUsers);

        Task<EUsers>GetByIdUser(Guid id);
    }
}
