using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;

namespace bruno.Klir.Domain.Common.Interfaces
{
    public interface IShoppingService
    {
        Task<ShoppingGroup> RecalculatePrice(ShoppingGroupId item);
    }
}
