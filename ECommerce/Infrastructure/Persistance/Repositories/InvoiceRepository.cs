using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Invoices;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistance.Repositories
{
    /// <summary>
    /// Repositorio de la factura que implementa InvoiceRepository.
    /// Patrón: Repository
    /// Responsable de la persistencia y recuperación de entidades Invoice desde la base de datos.
    /// Usa EF Core como ORM.
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Agrega una nueva factura
        /// </summary>
        public async Task AddAsync(
            Invoice invoice,
            CancellationToken cancellationToken)
        {
            await _context.Invoices.AddAsync(invoice, cancellationToken);
        }

        /// <summary>
        /// consulta una factura por id
        /// </summary>
        public async Task<Invoice?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _context.Invoices.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Invoice?> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            return await _context.Invoices.FirstOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
        }
    }

}
