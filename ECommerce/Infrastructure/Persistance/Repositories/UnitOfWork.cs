using ECommerce.Application.Interfaces;

namespace ECommerce.Infrastructure.Persistance.Repositories
{
    /// <summary>
    /// Implementación del patrón Unit of Work.
    /// Responsable de coordinar la persistencia de los cambios realizados en los repositorios.
    /// Centraliza las operaciones de guardado en la base de datos para garantizar consistencia.
    /// Se utiliza junto con repositorios que agregan o actualizan entidades sin hacer commit inmediato.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context; //Contexto EF Core inyectado
        }

        /// <summary>
        /// Persiste todos los cambios pendientes en el contexto hacia la base de datos.
        /// Patrón: Unit of Work
        /// Garantiza que todas las operaciones de los repositorios relacionados se guarden como una transacción.
        /// Retorna el número de registros afectados.
        /// </summary>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
