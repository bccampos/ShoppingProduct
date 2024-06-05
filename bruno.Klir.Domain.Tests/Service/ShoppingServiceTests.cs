using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Services;
using bruno.Klir.Domain.Shopping.ValueObjects;
using bruno.Klir.Domain.Shopping;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Common;
using bruno.Klir.Domain.Factories.Promotion;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Factories.Promotion.Type;

namespace bruno.Klir.Domain.Tests.Service
{
    public class ShoppingServiceTests
    {
        private readonly Mock<IShoppingGroupRepository> _mockRepository;
        private readonly ShoppingService _shoppingService;

        public ShoppingServiceTests()
        {
            _mockRepository = new Mock<IShoppingGroupRepository>();
            //_shoppingService = new ShoppingService(_mockRepository.Object);
        }

        [Fact]
        public async Task RecalculatePrice_ShouldReturnNull_WhenGroupIsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                .ReturnsAsync((ShoppingGroup)null);

            // Act
            var result = await _shoppingService.RecalculatePrice(new ShoppingGroupId());

            // Assert
            Assert.Null(result);
        }
    }
}
