using ECommerce.Application.Feature.Product.Command.CreateProduct;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Products;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Test.Application.Feature.Product.Command.CreateProduct
{
    public class CreateProductHandlerTests
    {
        private readonly Mock<IProductRepository> _repository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        private CreateProductHandler CreateHandler()
            => new(_repository.Object, _unitOfWork.Object);

        [Fact]
        public async Task Handle_Should_Create_Product_And_Return_Id()
        {
            // Arrange
            var command = new CreateProductCommand(
                "Pizza",
                "Pizza grande",
                20000,
                "COP");

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBe(Guid.Empty);

            _repository.Verify(x =>
                x.AddAsync(It.IsAny<Eproducts>(), It.IsAny<CancellationToken>()),
                Times.Once);

            _unitOfWork.Verify(x =>
                x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
