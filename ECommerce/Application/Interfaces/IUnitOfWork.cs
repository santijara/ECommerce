namespace ECommerce.Application.Interfaces
{
    /// <summary>
    /// Patrón Unit of Work.
    /// Encapsula la transacción de la base de datos.
    /// Permite que múltiples operaciones en diferentes repositorios se persistan de forma atómica.
    /// Facilita el manejo de transacciones y asegura consistencia en el dominio.
    /// </summary>
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
