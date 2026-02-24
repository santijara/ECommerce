using ECommerce.Application.Feature.User.Command.CreateUser;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Entities.Users;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Test.Application.Feature.User.Command.CreateUser
{
    public class CreateUserHandlerTests
    {
        private readonly Mock<IUserService> _userService = new();

        private CreateUserHandler CreateHandler()
            => new(_userService.Object);

        [Fact]
        public async Task Handle_Should_Create_User_And_Return_Success()
        {
            // Arrange
            var command = new CreateUserCommand(
                "Santiago",
                "Jaramillo",
                "3103877884",
                "jara1497@gmail.com",
                "Cra 64 # 104 A 18",
                "1020478327");

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();

            _userService.Verify(x =>
                x.CreateUser(It.IsAny<EUsers>()),
                Times.Once);
        }
    }
}
