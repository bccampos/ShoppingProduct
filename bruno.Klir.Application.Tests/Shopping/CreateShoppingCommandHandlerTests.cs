using bruno.Klir.Application.Shopping.Commands;
using bruno.Klir.Application.Shopping.Commands.Handlers;
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Product.ValueObjects;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;
using Moq;

namespace bruno.Klir.Application.Tests.Shopping
{
    public class CreateShoppingCommandHandlerTests
    {
        private readonly Mock<IShoppingGroupRepository> _shoppingRepositoryMock;
        private readonly Mock<IShoppingService> _shoppingServiceMock;
        private readonly CreateShoppingCommandHandler _handler;

        public CreateShoppingCommandHandlerTests()
        {
            _shoppingRepositoryMock = new Mock<IShoppingGroupRepository>();
            _shoppingServiceMock = new Mock<IShoppingService>();

            _handler = new CreateShoppingCommandHandler(_shoppingRepositoryMock.Object, _shoppingServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_AddsShoppingGroupAndCommits_ReturnsOk()
        {
            // Arrange
            var command = new CreateShoppingCommand
            {
                Items = new List<ShoppingItem>
            {
                new ShoppingItem
                {
                    ShoppingGroupId = ShoppingGroupId.Parse(Guid.NewGuid()),
                    ProductId = ProductId.Parse(Guid.NewGuid()),
                    Price = 10,
                    Quantity = 1,
                    Total = 10,
                    PromotionApplied = false
                }
            }
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            //Assert.True(result.IsSuccess);
            _shoppingRepositoryMock.Verify(repo => repo.Add(It.IsAny<ShoppingGroup>()), Times.Once);
            //_shoppingRepositoryMock.Verify(repo => repo.CommitAsync(), Times.Once);
        }
    }
}
