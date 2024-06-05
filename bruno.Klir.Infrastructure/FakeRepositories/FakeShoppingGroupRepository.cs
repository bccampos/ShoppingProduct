using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;

namespace bruno.Klir.Infrastructure.Repositories
{
    public class FakeShoppingGroupRepository 
    {
        private readonly static List<ShoppingGroup> _existing = GetFakeShoppingGroup();

        public int CommitCalledCount { get; set; }
        public int DeleteCalledCount { get; set; }

        public FakeShoppingGroupRepository()
        {
        }
        public Task<ShoppingGroup> GetByIdAsync(ShoppingGroupId id)
        {
            return Task.FromResult(_existing.Single(e => e.Id.Value == id.Value));
        }

        public Task<List<ShoppingGroup>> GetAllAsync()
        {
            return Task.FromResult(_existing);
        }

        public void Add(ShoppingGroup shoppingGroup)
        {
            _existing.Add(shoppingGroup);
        }

        public void Update(ShoppingGroup shoppingGroup)
        {
            ShoppingGroup result = _existing.Single(e => e.Id.Value == shoppingGroup.Id.Value);

            result.Clear();

            _existing.Add(shoppingGroup);
        }

        public Task CommitAsync()
        {
            CommitCalledCount++;
            return Task.CompletedTask;
        }

        private static List<ShoppingGroup> GetFakeShoppingGroup()
        {
            return new List<ShoppingGroup>();
        }

    }
}
