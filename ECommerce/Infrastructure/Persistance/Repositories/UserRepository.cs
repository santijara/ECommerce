using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistance.Repositories
{
    /// <summary>
    /// Repositorio de los usuarios que implementa IUserService.
    /// Patrón: Repository
    /// Responsable de la persistencia y recuperación de entidades EUsers desde la base de datos.
    /// Usa EF Core como ORM.
    /// </summary>
    public class UserRepository: IUserService
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Agrega un nuevo usuario
        /// </summary>
        public async Task CreateUser(EUsers users)
        {
           await _context.Users.AddAsync(users);
           await _context.SaveChangesAsync();
        }

        /// <summary>
        /// consulta un nuevo usuario por id
        /// </summary>
        public async Task<EUsers>GetByIdUser(Guid id)
        {
          var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
    }
}
