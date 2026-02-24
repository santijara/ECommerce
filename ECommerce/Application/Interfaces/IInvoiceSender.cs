using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Invoices;

namespace ECommerce.Application.Interfaces
{
    /// <summary>
    /// Servicio encargado de enviar facturas electrónicas a los usuarios.
    /// Encapsula la lógica de comunicación con un proveedor externo o envío por correo.
    /// Devuelve un resultado que indica éxito o falla y un mensaje o Id de transacción.
    /// </summary>
    public interface IInvoiceSender
    {
        Task<Result<string>> SendAsync(Invoice invoice,string email,CancellationToken cancellationToken);
    }
}
