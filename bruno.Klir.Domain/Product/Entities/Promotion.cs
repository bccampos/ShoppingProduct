using bruno.Klir.Domain.Common;
using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Product.ValueObjects;

namespace bruno.Klir.Domain.Product.Entities
{
    public class Promotion : Entity<PromotionId>
    {
        //public PromotionId PromotionId { get; set; }
        public virtual ICollection<Aggregate.Product> Products { get; set; }
        public string Name { get; set; }
        public int ApplyAtEach { get; set; }
        public DiscountType DiscountType { get; set; } = DiscountType.None;
        public int Quantity { get; set; }
        public bool IsPromotionActive { get; set; }
        public decimal DiscountValue { get; set; }

        private Promotion(PromotionId id, string name, int applyAtEach, DiscountType discountType, int quantity,
             decimal discountValue) : base(id)
        {
            Activate();

            Name = name;
            ApplyAtEach = applyAtEach;
            DiscountType = discountType;
            Quantity = quantity;
            DiscountValue = discountValue;

            Products = new List<Aggregate.Product>();
        }
        public Promotion()
        {            
        }

        public Promotion Create(Guid promotionId, string name, int applyAtEach, DiscountType discountType, int quantity, decimal discountValue)
        { 
            return new Promotion(PromotionId.Create(promotionId), name, applyAtEach, discountType, quantity, discountValue);
        }

        public void Update(string name, int applyAtEach, DiscountType discountType, int quantity, decimal discountValue)
        {
            Name = name;
            ApplyAtEach = applyAtEach;
            DiscountType = discountType;
            Quantity = quantity;
            DiscountValue = discountValue;
        }

        public bool Activate() => IsPromotionActive = true;
        public bool Deactivate() => IsPromotionActive = false;
    }
}
