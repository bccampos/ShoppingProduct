using bruno.Klir.Domain.Factories.Promotion.Type;
using bruno.Klir.Domain.Product.Entities;

namespace bruno.Klir.Domain.Tests.Factory
{
    public class ThreeForTenTests
    {
        [Fact]
        public void Apply_ShouldNotApplyPromotion_WhenQuantityIsLessThanThree()
        {
            // Arrange
            var item = new ShoppingItem { Price = 10m, Quantity = 2 };
            var promotionApplicator = new ThreeForTen();

            // Act
            var result = promotionApplicator.Apply(item);

            // Assert
            Assert.Equal(10m, result.Price);
            Assert.Equal(0, result.Total); 
            Assert.False(result.PromotionApplied);
        }

        [Fact]
        public void Apply_ShouldApplyPromotionOnce_WhenQuantityIsThree()
        {
            // Arrange
            var item = new ShoppingItem { Price = 10m, Quantity = 3 };
            var promotionApplicator = new ThreeForTen();

            // Act
            var result = promotionApplicator.Apply(item);

            // Assert
            Assert.Equal(10m, result.Price);
            Assert.Equal(10m, result.Total); // Promotion applied: 3 for 10
            Assert.True(result.PromotionApplied);
        }

        [Fact]
        public void Apply_ShouldApplyPromotionOnce_WhenQuantityIsFour()
        {
            // Arrange
            var item = new ShoppingItem { Price = 10m, Quantity = 4 };
            var promotionApplicator = new ThreeForTen();

            // Act
            var result = promotionApplicator.Apply(item);

            // Assert
            Assert.Equal(10m, result.Price);
            Assert.Equal(20m, result.Total); // 10 + 10 (1 promotion + 1 item at full price)
            Assert.True(result.PromotionApplied);
        }

        [Fact]
        public void Apply_ShouldApplyPromotionTwice_WhenQuantityIsSix()
        {
            // Arrange
            var item = new ShoppingItem { Price = 10m, Quantity = 6 };
            var promotionApplicator = new ThreeForTen();

            // Act
            var result = promotionApplicator.Apply(item);

            // Assert
            Assert.Equal(10m, result.Price);
            Assert.Equal(20m, result.Total); // 2 promotions applied: 3 for 10 each
            Assert.True(result.PromotionApplied);
        }

        [Fact]
        public void Apply_ShouldApplyPromotionCorrectly_ForVariousQuantities()
        {
            // Arrange
            var item1 = new ShoppingItem { Price = 10m, Quantity = 5 };
            var promotionApplicator = new ThreeForTen();
            var result1 = promotionApplicator.Apply(item1);

            var item2 = new ShoppingItem { Price = 10m, Quantity = 7 };
            var result2 = promotionApplicator.Apply(item2);

            // Assert
            Assert.Equal(10m, result1.Price);
            Assert.Equal(30m, result1.Total); // 10 + 10 + 10 (1 promotion + 2 items at full price)
            Assert.True(result1.PromotionApplied);

            Assert.Equal(10m, result2.Price);
            Assert.Equal(30m, result2.Total); // 10 + 10 + 10 (2 promotions + 1 item at full price)
            Assert.True(result2.PromotionApplied);
        }
    }
}
