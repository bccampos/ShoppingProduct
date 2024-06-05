using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Services;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;
using Moq;
using System.Reflection.Metadata;

namespace bruno.Klir.Domain.Tests.Service
{
    public class ShoppingServiceTests
    {
        private readonly Mock<IShoppingGroupRepository> _mockRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ShoppingService _shoppingService;

        public ShoppingServiceTests()
        {
            _mockRepository = new Mock<IShoppingGroupRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _shoppingService = new ShoppingService(_mockRepository.Object, _mockProductRepository.Object);
        }

        [Fact]
        public async Task RecalculatePrice_ShouldReturnNull_WhenGroupIsNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<ShoppingGroupId>()))
                .ReturnsAsync((ShoppingGroup)null);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _shoppingService.RecalculatePrice(new ShoppingGroupId()));
            Assert.Equal("Database error", exception.Message);
        }
    }
}
