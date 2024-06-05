using bruno.Klir.Domain.Common;
using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Product.ValueObjects;

namespace bruno.Klir.Domain.Aggregate
{
    public class Product : AggregateRoot<ProductId>, IEquatable<Product>, IAggregateRoot
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }

        public PromotionId? PromotionId { get; set; }

        public virtual Promotion? Promotion { get; set; }

        public virtual ICollection<ShoppingItem> Items { get; set; }

        public Product()
        {
            Inactivate();
        }

        private Product(ProductId id, string name, decimal price, int quantity, PromotionId? promotionId) : base(id)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            PromotionId = promotionId;
        }

        public Product Create(string name, decimal price, int quantity, PromotionId? promotionId)
        {
            Activate();
            //Validate() 
            //TODO: 1. Validate Name 2. Validate Price  

            return new Product(ProductId.Create(), name, price, quantity, promotionId);
        }

        public Product FullCreate(ProductId id, string name, decimal price, int quantity, PromotionId? promotionId, Promotion? promotion)
        {
            var current = new Product(id, name, price, quantity, promotionId);
            current.Activate();
            current.Promotion = promotion;

            return current;
        }

        public void Update(string name, decimal price)
        {
            Name = name;
            Price = price;

            //Validate() 
        }

        public void SetPromotion(Guid promotionId)
        {
            PromotionId = new PromotionId();
            PromotionId.Set(promotionId);
        }

        public void ClearPromotion()
        {
            PromotionId = null;
        }

        public bool IsProductValid()
        {
            return IsActive && IsPromotionValid();
        }


        public void Activate() => IsActive = true;

        public void Inactivate() => IsActive = false;

        public bool Equals(Product other)
        {
            return other != null && other.Id == this.Id;
        }
        private bool IsPromotionValid() => Promotion is null || Promotion.IsPromotionActive;
    }
}
