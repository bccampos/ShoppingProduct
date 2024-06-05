using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Product.ValueObjects;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;

namespace bruno.Klir.Domain.Product.Entities
{
    public sealed class ShoppingItem : Entity<ShoppingItemId>
    {
        //public ShoppingItemId ShoppingItemId { get; private set; }
        public ShoppingGroupId ShoppingGroupId { get; set; }
        public ShoppingGroup ShoppingGroup { get; set; }
        public ProductId ProductId { get; set; }
        public Aggregate.Product Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public bool PromotionApplied { get; set; }

        public ShoppingItem()
        {
            
        }

        public ShoppingItem(ShoppingItemId shoppingItemId, ShoppingGroupId shoppingGroup, ProductId productId, decimal price,
                   int quantity, decimal total, bool promotionApplied)
        {
            Id = shoppingItemId;
            ShoppingGroupId = shoppingGroup;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
            Total = total;
            PromotionApplied = promotionApplied;
        }

        public ShoppingItem Create(ShoppingGroupId shoppingGroup, ProductId productId, decimal price,
                   int quantity, decimal total, bool promotionApplied)
        { 
            return new ShoppingItem(ShoppingItemId.Create(), shoppingGroup, productId, price, quantity, total, promotionApplied);
        }

        public ShoppingItem FullCreate(ShoppingItemId shoppingItemId, ShoppingGroupId shoppingGroup, ProductId productId, decimal price,
             int quantity, decimal total, bool promotionApplied, Aggregate.Product product)
        {
            var current = new ShoppingItem(shoppingItemId, shoppingGroup, productId, price, quantity, total, promotionApplied);
            current.Product = product;

            return current;
        }

        public void Update(ShoppingGroupId shoppingGroupId, ProductId productId, decimal price,
                   int quantity, decimal total, bool promotionApplied)
        {
            ShoppingGroupId = shoppingGroupId;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
            Total = total;
            PromotionApplied = promotionApplied;
        }
        public void SetPromotion()
        {
            PromotionApplied = true;
        }

        public void SetTotal(decimal total)
        {
            Total = total;
        }

        public void SetQuantity(int quantity)
        {
            Quantity += quantity;
        }
        public void SetPrice(decimal unitPrice)
        {
            Price = Price;
        }

        public void ApplyPromotion() => PromotionApplied = true;
        public void RemovePromotion() => PromotionApplied = false;

    
    }
}
