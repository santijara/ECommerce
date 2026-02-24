using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Invoices;
using System.Net;
using System.Net.Mail;

namespace ECommerce.Infrastructure.Persistance.Emails
{
    /// <summary>
    /// Implementacion de IInvoiceSender usando SMTP para enviar facturas por correo electrónico.
    ///  Maneja la lógica de envio de emails y captura errores devolviendo un Result<string>
    /// </summary>
    public sealed class SmtpInvoiceSender : IInvoiceSender
    {
        /// <summary>
        /// Envia la factura al correo electrónico especificado.
        /// Devuelve Result<string> indicando éxito o fallo con mensaje.
        /// </summary>
        public async Task<Result<string>> SendAsync(Invoice invoice,string email,CancellationToken cancellationToken)
        {
            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress("no-reply@tuecommerce.com"),
                    Subject = $"Factura {invoice.InvoiceNumber}",
                    Body = BuildBody(invoice),
                    IsBodyHtml = true
                };

                message.To.Add(email);

                using var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(
                        "tu-correo@gmail.com",
                        "tu-app-password"),
                    EnableSsl = true
                };

                await client.SendMailAsync(message, cancellationToken);

                return Result<string>.Success("Exitoso");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Construye el cuerpo HTML del correo con los datos de la factura.
        /// Encapsula la lógica de formato de mensaje
        /// </summary>
        private string BuildBody(Invoice invoice)
        {
            return $@"
            <h2>Factura {invoice.InvoiceNumber}</h2>
            <p>Subtotal: {invoice.SubTotal:C}</p>
            <p>Impuestos: {invoice.Tax:C}</p>
            <p>Total: {invoice.Total:C}</p>
            <p>Fecha: {invoice.IssuedAtUtc}</p>
        ";
        }
    }
}
