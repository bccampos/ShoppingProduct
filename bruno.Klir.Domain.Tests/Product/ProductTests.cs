using bruno.Klir.Domain.Common;
using bruno.Klir.Domain.Product.ValueObjects;

namespace bruno.Klir.Domain.Tests.Product
{
    public class ProductTests
    {
        private readonly string _name;
        private readonly decimal _price;
        private readonly int _quantity;
        private readonly PromotionId _promotionId;

        public ProductTests() 
        {
            _name = "product_test";
            _price = 10;
            _quantity = 5;
            _promotionId = PromotionId.Parse(Guid.NewGuid());
        }

        [Fact]
        public void CreateProduct_WithValidData_ShouldCreateSuccess()
        {
            var product = new Aggregate.Product().Create(_name, _price, _quantity, _promotionId);

            Assert.Equal(_name, product.Name);
            Assert.Equal(_price, product.Price);
            Assert.Equal(_quantity, product.Quantity);
            Assert.Equal(_promotionId, product.PromotionId);
            Assert.Equal(true, product.IsActive);
        }

        [Fact]
        public void UpdateProduct_WithValidData_ShouldUpdateSuccess()
        {
            var nameUpdate = "product_test_update";
            var priceUpdate = 5;

            var product = new Aggregate.Product().Create(_name, _price, _quantity, _promotionId);

            product.Update(nameUpdate, priceUpdate);

            Assert.Equal(nameUpdate, product.Name);
            Assert.Equal(priceUpdate, product.Price);
        }

        [Fact]
        public void SetProductPromotion_ShouldSetWithSuccess()
        {
            var promotionId = Guid.NewGuid();
            var product = new Aggregate.Product().Create(_name, _price, _quantity, null);

            product.SetPromotion(promotionId);

            Assert.Equal(promotionId, product.PromotionId?.Value);
        }

        [Fact]
        public void RemoveProductPromotion_ShouldRemoveWithSuccess()
        {
            var product = new Aggregate.Product().Create(_name, _price, _quantity, _promotionId);

            product.ClearPromotion();

            Assert.Null(product.PromotionId);
        }

        [Fact]
        public void ActivateProduct_ShouldActivateWithSuccess()
        {
            var product = new Aggregate.Product().Create(_name, _price, _quantity, _promotionId);

            product.Activate();

            Assert.Equal(true, product.IsActive);
        }

        [Fact]
        public void InactivateProduct_ShouldInactivateWithSuccess()
        {
            var product = new Aggregate.Product().Create(_name, _price, _quantity, _promotionId);

            product.Inactivate();

            Assert.Equal(false, product.IsActive);
        }


    }
}
