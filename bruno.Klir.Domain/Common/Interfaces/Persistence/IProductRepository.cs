
using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Product.ValueObjects;

namespace bruno.Klir.Domain.Common.Interfaces.Persistence
{
    public interface IProductRepository : IRepository<Domain.Aggregate.Product>
    {
        Task<List<Aggregate.Product>> GetAllAsync();

        Task<Aggregate.Product> GetByIdAsync(ProductId productId);

        void Add(Aggregate.Product product);

        void Update(Aggregate.Product product);

        void Delete(Aggregate.Product product);
    }
}
