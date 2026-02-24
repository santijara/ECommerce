using ECommerce.Domain.Entities.Invoices;

namespace ECommerce.Application.Interfaces
{
    /// <summary>
    /// Repositorio encargado de la persistencia y consulta de facturas.
    /// Maneja operaciones básicas para crear y consultar facturas generadas tras un pago.
    /// </summary>
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice, CancellationToken cancellationToken);

        Task<Invoice?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Invoice?> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
