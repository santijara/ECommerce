using ECommerce.Application.Common;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Invoices;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;

namespace ECommerce.Infrastructure.Persistance.Emails
{
    /// <summary>
    /// Implementacion de IInvoiceSender usando SMTP para enviar facturas por correo electrónico.
    ///  Maneja la lógica de envio de emails y captura errores devolviendo un Result<string>
    /// </summary>
    public sealed class SmtpInvoiceSender : IInvoiceSender
    {
        private readonly IConfiguration _configuration;

        public SmtpInvoiceSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Envia la factura al correo electrónico especificado.
        /// Devuelve Result<string> indicando éxito o fallo con mensaje.
        /// </summary>
        public async Task<Result<string>> SendAsync(Invoice invoice,string email,string pdfPath,CancellationToken cancellationToken)
        {
            try
            {
                if (!File.Exists(pdfPath))
                    return Result<string>.Failure("El archivo PDF no existe.");

                using var client = new HttpClient();

                var baseAddress = _configuration["MailSettings:BaseAddress"];
                var endpoint = _configuration["MailSettings:SendMail"];
                var apiKey = _configuration["MailSettings:ApiKey"];
                var fromEmail = _configuration["MailSettings:FromEmail"];

                client.BaseAddress = new Uri(baseAddress);

                var base64ApiKey = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"api:{apiKey}")
                );

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", base64ApiKey);

                var htmlBody = BuildBody(invoice);

                // Leer archivo PDF
                var fileBytes = await File.ReadAllBytesAsync(pdfPath, cancellationToken);
                var fileContent = new ByteArrayContent(fileBytes);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

                var content = new MultipartFormDataContent
        {
            { new StringContent(fromEmail), "from" },
            { new StringContent(email), "to" },
            { new StringContent($"Factura {invoice.InvoiceNumber}"), "subject" },
            { new StringContent(htmlBody), "html" }
        };

               
                content.Add(fileContent, "attachment", $"Factura_{invoice.InvoiceNumber}.pdf");

                var response = await client.PostAsync(endpoint, content, cancellationToken);
                var resultContent = await response.Content.ReadAsStringAsync(cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return Result<string>.Failure(
                        $"Error Mailgun: {response.StatusCode} - {resultContent}"
                    );
                }

                return Result<string>.Success("Factura enviada exitosamente con PDF adjunto");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"Error enviando la factura: {ex.Message}");
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


        /// <summary>
        /// Genera el PDF de la factura que se enviara al correo electronico y lo guarda en una carpeta Invoices
        /// </summary>
        public string SavePdf(Invoice invoice)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Invoices");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, $"Factura_{invoice.InvoiceNumber}.pdf");

             Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text($"FACTURA #{invoice.InvoiceNumber}")
                        .SemiBold().FontSize(20).AlignCenter();

                    page.Content()
                        .PaddingVertical(20)
                        .Column(column =>
                        {
                            column.Spacing(10);

                            column.Item().Text($"Fecha: {invoice.IssuedAtUtc:dd/MM/yyyy}");
                            column.Item().Text($"Subtotal: {invoice.SubTotal:C}");
                            column.Item().Text($"Impuestos: {invoice.Tax:C}");
                            column.Item().Text($"Total: {invoice.Total:C}")
                                         .Bold()
                                         .FontSize(14);

                            column.Item().PaddingTop(20)
                                .Text("Gracias por su compra.")
                                .Italic();
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generado el ");
                            x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        });
                });
            })
            .GeneratePdf(filePath);

            return filePath;
        }


    }
}
