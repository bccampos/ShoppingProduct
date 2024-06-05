using bruno.Klir.Domain.Factories.Promotion.Type;
using bruno.Klir.Domain.Product.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bruno.Klir.Domain.Tests.Factory
{
    public class BuyOneGetOneFreeTests
    {
        [Fact]
        public void Apply_ShouldNotApplyPromotion_WhenQuantityIsOne()
        {
            // Arrange
            var item = new ShoppingItem { Price = 10m, Quantity = 1 };
            var promotionApplicator = new BuyOneGetOneFree();

            // Act
            var result = promotionApplicator.Apply(item);

            // Assert
            Assert.Equal(10m, result.Price);
            Assert.Equal(0, result.Total);
            Assert.False(result.PromotionApplied);
        }

        [Fact]
        public void Apply_ShouldApplyPromotionOnce_WhenQuantityIsTwo()
        {
            // Arrange
            var item = new ShoppingItem { Price = 10m, Quantity = 2 };
            var promotionApplicator = new BuyOneGetOneFree();

            // Act
            var result = promotionApplicator.Apply(item);

            // Assert
            Assert.Equal(10m, result.Price);
            Assert.Equal(10m, result.Total); 
            Assert.True(result.PromotionApplied);
        }

        [Fact]
        public void Apply_ShouldApplyPromotionOnce_WhenQuantityIsThree()
        {
            // Arrange
            var item = new ShoppingItem { Price = 10m, Quantity = 3 };
            var promotionApplicator = new BuyOneGetOneFree();

            // Act
            var result = promotionApplicator.Apply(item);

            // Assert
            Assert.Equal(10m, result.Price);
            Assert.Equal(20m, result.Total); 
            Assert.True(result.PromotionApplied);
        }

        [Fact]
        public void Apply_ShouldApplyPromotionTwice_WhenQuantityIsFour()
        {
            // Arrange
            var item = new ShoppingItem { Price = 10m, Quantity = 4 };
            var promotionApplicator = new BuyOneGetOneFree();

            // Act
            var result = promotionApplicator.Apply(item);

            // Assert
            Assert.Equal(10m, result.Price);
            Assert.Equal(20m, result.Total); 
            Assert.True(result.PromotionApplied);
        }

        [Fact]
        public void Apply_ShouldApplyPromotionCorrectly_ForVariousQuantities()
        {
            // Arrange & Act
            var item1 = new ShoppingItem { Price = 10m, Quantity = 5 };
            var promotionApplicator = new BuyOneGetOneFree();
            var result1 = promotionApplicator.Apply(item1);

            var item2 = new ShoppingItem { Price = 10m, Quantity = 6 };
            var result2 = promotionApplicator.Apply(item2);

            // Assert
            Assert.Equal(10m, result1.Price);
            Assert.Equal(30m, result1.Total); 
            Assert.True(result1.PromotionApplied);

            Assert.Equal(10m, result2.Price);
            Assert.Equal(30m, result2.Total); 
            Assert.True(result2.PromotionApplied);
        }
    }
}
