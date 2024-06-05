using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;

namespace bruno.Klir.Domain.Common.Interfaces.Persistence
{
    public interface IShoppingGroupRepository : IRepository<ShoppingGroup>
    {
        Task<List<ShoppingGroup>> GetAllAsync();
        Task<ShoppingGroup> GetByIdAsync(ShoppingGroupId id);
        void Add(ShoppingGroup item);
        void Update(ShoppingGroup item);
    }
}
