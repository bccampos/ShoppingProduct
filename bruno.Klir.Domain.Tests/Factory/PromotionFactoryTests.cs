using bruno.Klir.Domain.Common;
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Factories.Promotion;
using bruno.Klir.Domain.Factories.Promotion.Type;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Services;
using Moq;

namespace bruno.Klir.Domain.Tests.Factory
{
    public class PromotionFactoryTests
    {

        private readonly Mock<IPromotionApplicator> _mockPromotionApplicator;

        public PromotionFactoryTests()
        {
            _mockPromotionApplicator = new Mock<IPromotionApplicator>();
        }

        [Fact]
        public void ApplyPromotion_ShouldApplyBuyOneGetOneFree_WhenDiscountTypeIsBuyOneGetOneFree()
        {
            // Arrange
            var item = new ShoppingItem { /* initialize properties */ };
            PromotionFactory.GetPromotionApplicator = type => type == DiscountType.BuyOneGetOneFree ? _mockPromotionApplicator.Object : null;

            // Act
            var result = PromotionFactory.ApplyPromotion(item, DiscountType.BuyOneGetOneFree);

            // Assert
            _mockPromotionApplicator.Verify(x => x.Apply(It.IsAny<ShoppingItem>()), Times.Once);
            Assert.Equal(item, result);
        }

        [Fact]
        public void ApplyPromotion_ShouldApplyThreeForTen_WhenDiscountTypeIsThreeForTen()
        {
            // Arrange
            var item = new ShoppingItem { /* initialize properties */ };
            PromotionFactory.GetPromotionApplicator = type => type == DiscountType.ThreeForTen ? _mockPromotionApplicator.Object : null;

            // Act
            var result = PromotionFactory.ApplyPromotion(item, DiscountType.ThreeForTen);

            // Assert
            _mockPromotionApplicator.Verify(x => x.Apply(It.IsAny<ShoppingItem>()), Times.Once);
            Assert.Equal(item, result);
        }
    }
}
