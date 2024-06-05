using bruno.Klir.Application.Shopping.Commands.Handlers;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Common.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bruno.Klir.Application.Shopping.Commands;
using bruno.Klir.Domain.Shopping.ValueObjects;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Product.ValueObjects;
using bruno.Klir.Application.Tests.Shopping.Mock;

namespace bruno.Klir.Application.Tests.Shopping
{
    public class UpdateShoppingCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IShoppingGroupRepository> _shoppingRepositoryMock;
        private readonly Mock<IShoppingService> _shoppingServiceMock;
        private readonly UpdateShoppingCommandHandler _handler;

        public UpdateShoppingCommandHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _shoppingRepositoryMock = new Mock<IShoppingGroupRepository>();
            _shoppingServiceMock = new Mock<IShoppingService>();
            _handler = new UpdateShoppingCommandHandler(_shoppingRepositoryMock.Object, _shoppingServiceMock.Object, _productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShoppingGroupNotFound_ReturnsFailureResult()
        {
            // Arrange
            var command = new UpdateShoppingCommand { Id = new Guid(), Items = new List<ShoppingItem>() };

            _shoppingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                                   .ReturnsAsync((ShoppingGroup)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Shopping is not found", result.Errors.First().Message);
        }

        [Fact]
        public async Task Handle_ProductNotValid_ReturnsFailureResult()
        {
            // Arrange
            var command = new UpdateShoppingCommand
            {
                Id = Guid.NewGuid(),
                Items = new List<ShoppingItem>
            {
                new ShoppingItem
                {
                    ProductId = ProductId.Parse(Guid.NewGuid()),
                    Price = 10,
                    Quantity = 1
                }
            }
            };

            var shoppingGroup = new ShoppingGroup { Id = ShoppingGroupId.Parse(command.Id) };
            var product = MockProduct.GetInstance().Products.First();

            _shoppingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                                   .ReturnsAsync(shoppingGroup);

            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ProductId>()))
                                  .ReturnsAsync(product);

            _productRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<ProductId>())).ReturnsAsync(new Domain.Aggregate.Product());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Product is not valid", result.Errors.First().Message);
        }

        [Fact]
        public async Task Handle_ItemNotFound_AddsNewItem()
        {
            // Arrange
            var command = new UpdateShoppingCommand
            {
                Id = Guid.NewGuid(),
                Items = new List<ShoppingItem>
            {
                new ShoppingItem
                {
                    ProductId = ProductId.Parse(new Guid("aa967a5d-5edd-418d-804d-7ea68e2b65fe")),
                    Price = 10,
                    Quantity = 1
                }
            }
            };

            var shoppingGroup = MockShopping.GetInstance().ShoppingGroup.First();
            var product = MockProduct.GetInstance().Products.First();

            _shoppingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                                   .ReturnsAsync(shoppingGroup);
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ProductId>()))
                                  .ReturnsAsync(product);
            _productRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<ProductId>())).ReturnsAsync(product);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _shoppingRepositoryMock.Verify(repo => repo.Update(It.IsAny<ShoppingGroup>()), Times.Once);
            //_shoppingRepositoryMock.Verify(repo => repo.CommitAsync(), Times.Once);
            Assert.NotNull(shoppingGroup.Items);
        }

        [Fact]
        public async Task Handle_SuccessfulUpdate_ReturnsSuccessResult()
        {
            // Arrange
            // Arrange
            var command = new UpdateShoppingCommand
            {
                Id = Guid.NewGuid(),
                Items = new List<ShoppingItem>
            {
                new ShoppingItem
                {
                    ProductId = ProductId.Parse(new Guid("aa967a5d-5edd-418d-804d-7ea68e2b65fe")),
                    Price = 10,
                    Quantity = 1
                }
            }
            };

            var shoppingGroup = MockShopping.GetInstance().ShoppingGroup.First();
            var product = MockProduct.GetInstance().Products.First();

            _shoppingRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                                   .ReturnsAsync(shoppingGroup);
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<ProductId>()))
                                  .ReturnsAsync(product);
            _productRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<ProductId>())).ReturnsAsync(product);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            _shoppingRepositoryMock.Verify(repo => repo.Update(It.IsAny<ShoppingGroup>()), Times.Once);
            //_shoppingRepositoryMock.Verify(repo => repo.CommitAsync(), Times.Once);
        }
    }
}
