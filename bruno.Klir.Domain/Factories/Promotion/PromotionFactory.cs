
using bruno.Klir.Domain.Common;
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Factories.Promotion.Type;
using bruno.Klir.Domain.Product.Entities;
using System.Data;

namespace bruno.Klir.Domain.Factories.Promotion
{
    public class PromotionFactory
    {
        public static ShoppingItem ApplyPromotion(ShoppingItem item, DiscountType type)
        {
            var applicator = GetPromotionApplicator(type);

            applicator.Apply(item);
            
            return item;
        }

        public static Func<DiscountType, IPromotionApplicator> GetPromotionApplicator = type =>
        {
            return type switch
            {
                DiscountType.BuyOneGetOneFree => new BuyOneGetOneFree(),
                DiscountType.ThreeForTen => new ThreeForTen(),
                DiscountType.FixedPrice => new FixedPrice(),
                DiscountType.Percentual => new Percentual()
            };
        };


    }
}
