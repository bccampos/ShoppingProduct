using bruno.Klir.Application.Shopping.Commands;
using bruno.Klir.Application.Shopping.Commands.Handlers;
using bruno.Klir.Application.Tests.Shopping.Mock;
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;
using Moq;

namespace bruno.Klir.Application.Tests.Shopping
{
    public class CalculateShoppingCommandHandlerTests
    {
        private readonly Mock<IShoppingGroupRepository> _shoppingRepositoryMock;
        private readonly Mock<IShoppingService> _shoppingServiceMock;
        private readonly Mock<IProductRepository> _productRepository; 
        private readonly CalculateShoppingCommandHandler _handler;

        public CalculateShoppingCommandHandlerTests()
        {
            _shoppingRepositoryMock = new Mock<IShoppingGroupRepository>();
            _shoppingServiceMock = new Mock<IShoppingService>();
            _productRepository = new Mock<IProductRepository>();
            _handler = new CalculateShoppingCommandHandler(_shoppingRepositoryMock.Object, _shoppingServiceMock.Object, _productRepository.Object);
        }

        [Fact]
        public async Task Handle_ShoppingGroupNotFound_ReturnsFailureResult()
        {
            // Arrange
            var command = new CalculateShoppingCommand(Guid.NewGuid());  

            _shoppingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                                   .ReturnsAsync((ShoppingGroup)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Shopping is not found", result.Errors.First().Message);
        }

        [Fact]
        public async Task Handle_SuccessfulPriceRecalculation_ReturnsSuccessResult()
        {
            // Arrange         
            var shoppingGroup = MockShopping.GetInstance().ShoppingGroup.First();
            var command = new CalculateShoppingCommand(shoppingGroup.Id.Value);

            _shoppingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                                   .ReturnsAsync(shoppingGroup);

            _shoppingServiceMock.Setup(service => service.RecalculatePrice(It.IsAny<ShoppingGroupId>()))
                                .ReturnsAsync(shoppingGroup);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task Handle_ServiceThrowsException_ReturnsFailureResult()
        {
            // Arrange
            var command = new CalculateShoppingCommand(Guid.NewGuid());
            var shoppingGroup = MockShopping.GetInstance().ShoppingGroup.First();

            _shoppingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                                   .ReturnsAsync(shoppingGroup);

            _shoppingServiceMock.Setup(service => service.RecalculatePrice(It.IsAny<ShoppingGroupId>()))
                                .ThrowsAsync(new Exception("Service error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Service error", exception.Message);
        }

        [Fact]
        public async Task Handle_ServiceThrowsDatabaseErrorException_ThrowsException()
        {
            // Arrange
            var command = new CalculateShoppingCommand(Guid.NewGuid());
            var shoppingGroup = MockShopping.GetInstance().ShoppingGroup.First();


            _shoppingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                                   .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("Database error", exception.Message);
        }
    }
}
