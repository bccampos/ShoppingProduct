
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Product.Entities;
using System;

namespace bruno.Klir.Domain.Factories.Promotion.Type
{
    public class BuyOneGetOneFree : IPromotionApplicator
    {
        public ShoppingItem Apply(ShoppingItem item)
        {
            if (item.Quantity == 1)
                return item;

            var timesToChargeByPromotion = Math.Truncate(item.Quantity / 2m);

            var timesToChargeFullPrice = item.Quantity % 2;

            if (timesToChargeByPromotion > 0)
            {
                item.SetPromotion();
                item.SetTotal(item.Price * (timesToChargeByPromotion + timesToChargeFullPrice));
            }
            else
            {
                item.SetTotal(item.Price * timesToChargeFullPrice);
            }

            return item;
        }
    }
}
