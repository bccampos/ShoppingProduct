
using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Product.Entities;
using System;

namespace bruno.Klir.Domain.Factories.Promotion.Type
{
    public class Percentual : IPromotionApplicator
    {
        public ShoppingItem Apply(ShoppingItem item)
        {
            //TODO: Add the rules               
            return item;
        }
    }
}
