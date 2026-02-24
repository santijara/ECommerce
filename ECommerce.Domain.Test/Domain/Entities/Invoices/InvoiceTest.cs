using ECommerce.Domain.Entities.Invoices;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Test.Domain.Entities.Invoices
{
    public class InvoiceTest
    {
        [Fact]
        public void Create_Should_Create_Invoice_With_Pending_Status()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            // Act
            var invoice = Invoice.Create(orderId, 100, 19, 119);

            // Assert
            invoice.OrderId.Should().Be(orderId);
            invoice.SubTotal.Should().Be(100);
            invoice.Tax.Should().Be(19);
            invoice.Total.Should().Be(119);
            invoice.Status.Should().Be(InvoiceStatus.Pending);
            invoice.InvoiceNumber.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void MarkAsFailed_Should_Set_Status_And_Message()
        {
            // Arrange
            var invoice = Invoice.Create(Guid.NewGuid(), 100, 19, 119);

            // Act
            invoice.MarkAsFailed("SMTP Error");

            // Assert
            invoice.Status.Should().Be(InvoiceStatus.Failed);
            invoice.Message.Should().Be("SMTP Error");
        }
    }
}
