
using bruno.Klir.Domain.Product.Entities;

namespace bruno.Klir.Domain.Common.Interfaces
{
    public interface IPromotionApplicator
    {
        ShoppingItem Apply(ShoppingItem item);
    }
}
