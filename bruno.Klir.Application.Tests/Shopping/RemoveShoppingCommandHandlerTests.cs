using bruno.Klir.Application.Shopping.Commands;
using bruno.Klir.Application.Shopping.Commands.Handlers;
using bruno.Klir.Application.Tests.Shopping.Mock;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;
using Moq;

namespace bruno.Klir.Application.Tests.Shopping
{
    public class RemoveShoppingCommandHandlerTests
    {
        private readonly Mock<IShoppingGroupRepository> _shoppingRepositoryMock;
        private readonly RemoveShoppingCommandHandler _handler;

        public RemoveShoppingCommandHandlerTests()
        {
            _shoppingRepositoryMock = new Mock<IShoppingGroupRepository>();
            _handler = new RemoveShoppingCommandHandler(_shoppingRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Valid_ShouldReturnSuccess()
        {
            // Arrange
            var shoppingGroupRepositoryMock = new Mock<IShoppingGroupRepository>();
            var handler = new RemoveShoppingCommandHandler(shoppingGroupRepositoryMock.Object);

            var shoppingGroup = MockShopping.GetInstance().ShoppingGroup.First();

            shoppingGroupRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                                       .ReturnsAsync(shoppingGroup);

            var command = new RemoveShoppingCommand(new Guid("d000c53e-0713-47f2-8d48-5308258f5fa5"));
            
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            shoppingGroupRepositoryMock.Verify(repo => repo.Update(It.IsAny<ShoppingGroup>()), Times.Once);
            //shoppingGroupRepositoryMock.Verify(repo => repo.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShoppingGroupNotFound_ShouldReturnFailure()
        {
            // Arrange
            var shoppingGroupRepositoryMock = new Mock<IShoppingGroupRepository>();
            var handler = new RemoveShoppingCommandHandler(shoppingGroupRepositoryMock.Object);

            var shoppingGroup = MockShopping.GetInstance().ShoppingGroup.First();

            shoppingGroupRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                                       .ReturnsAsync((ShoppingGroup)null);

            var command = new RemoveShoppingCommand(new Guid("d000c53e-0713-47f2-8d48-5308258f5fa5"));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Shopping is not found", result.Errors.First().Message);
            shoppingGroupRepositoryMock.Verify(repo => repo.Update(It.IsAny<ShoppingGroup>()), Times.Never);
            //shoppingGroupRepositoryMock.Verify(repo => repo.CommitAsync(), Times.Never);
        }
    }
}
