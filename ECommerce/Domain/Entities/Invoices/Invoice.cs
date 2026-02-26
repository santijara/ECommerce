namespace ECommerce.Domain.Entities.Invoices
{
    /// <summary>
    /// Entidad de dominio "Factura" (Invoice).
    /// Representa la factura de un pedido.
    /// Controla estados de envío y validación de la factura.
    /// Hereda de AgregateRoot para soportar eventos de dominio.
    /// </summary>
    public class Invoice : AgregateRoot
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public string InvoiceNumber { get; private set; } = default!;
        public DateTime IssuedAtUtc { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal Tax { get; private set; }
        public decimal Total { get; private set; }
        public InvoiceStatus Status { get; private set; }
        public string? Message { get; private set; }

        //EF
        private Invoice() { } 

        private Invoice(Guid orderId,decimal subTotal,decimal tax,decimal total,string invoiceNumber)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            SubTotal = subTotal;
            Tax = tax;
            Total = total;
            InvoiceNumber = invoiceNumber;
            IssuedAtUtc = DateTime.UtcNow;
            Status = InvoiceStatus.Pending;
        }

        //Factory Method
        public static Invoice Create(Guid orderId,decimal subTotal,decimal tax,decimal total)
        {
            var invoiceNumber = GenerateInvoiceNumber();

            return new Invoice(
                orderId,
                subTotal,
                tax,
                total,
                invoiceNumber);
        }


        /// Marca la factura como enviada exitosamente.
        public void MarkAsSent(string message)
        {
            Status = InvoiceStatus.Sent;       
            Message = message;
        }

        /// Marca la factura como enviada exitosamente.
        public void MarkAsFailed(string error)
        {
            Status = InvoiceStatus.Failed;
            Message = error;
        }

        /// Genera un número de factura único usando ticks de UTC.
        private static string GenerateInvoiceNumber() => $"INV-{DateTime.UtcNow.Ticks}";
    }

}
