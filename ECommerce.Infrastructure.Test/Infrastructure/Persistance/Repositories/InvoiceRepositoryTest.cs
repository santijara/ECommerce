using ECommerce.Domain.Entities.Invoices;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Persistance.Repositories;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Test.Infrastructure.Persistance.Repositories
{
    public class InvoiceRepositoryTest
    {
        [Fact]
        public async Task AddAsync_Should_Save_Invoice()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options, mediatorMock.Object);
            var repository = new InvoiceRepository(context);

            var invoice = Invoice.Create(Guid.NewGuid(), 100, 19, 119);

            // Act
            await repository.AddAsync(invoice, CancellationToken.None);
            await context.SaveChangesAsync();

            // Assert
            var saved = await context.Invoices.FirstOrDefaultAsync();

            saved.Should().NotBeNull();
            saved!.Total.Should().Be(119);
        }
    }
}
