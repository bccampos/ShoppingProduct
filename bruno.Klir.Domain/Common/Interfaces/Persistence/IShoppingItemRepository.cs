using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Shopping.ValueObjects;

namespace bruno.Klir.Domain.Common.Interfaces.Persistence
{
    public interface IShoppingItemRepository
    {
        Task<ShoppingItem> GetByIdAsync(ShoppingItemId id);
        Task Add(ShoppingItem item);
        void Update(ShoppingItem item);
    }
}
