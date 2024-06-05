
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Product.Entities;
using System;

namespace bruno.Klir.Domain.Factories.Promotion.Type
{
    public class ThreeForTen : IPromotionApplicator
    {
        public ShoppingItem Apply(ShoppingItem item)
        {
            if (item.Quantity < 3)
                return item;

            var timesToApplyPromotion = Math.Truncate(item.Quantity / 3m);

            var timesToChargeFullPrice = item.Quantity % 3;

            item.SetTotal((timesToApplyPromotion * 10m) + (timesToChargeFullPrice * item.Price));

            item.SetPromotion();

            return item;
        }
    }
}
