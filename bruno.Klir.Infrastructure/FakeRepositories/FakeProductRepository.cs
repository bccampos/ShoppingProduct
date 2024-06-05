using bruno.Klir.Domain.Aggregate;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Product.ValueObjects;
using System.Data.Common;
using System.Net.Http.Headers;

namespace bruno.Klir.Infrastructure.Repositories
{
    public class FakeProductRepository 
    {
        private readonly static List<Product> _existing = GetFakeProducts();

        public int CommitCalledCount { get; set; }
        public int DeleteCalledCount { get; set; }

        public FakeProductRepository()
        {
        }

        public Task<List<Product>> GetAllAsync()
        {
            return Task.FromResult(_existing);
        }

        public Task<Product> GetByIdAsync(ProductId productId)
        {
            return Task.FromResult(_existing.Find(e => e.Id.Value == productId.Value));
        }

        public void Add(Product product)
        {
            _existing.Add(product);
        }

        public void Update(Product product)
        {
            Product result = _existing.Single(e => e.Id.Value == product.Id.Value);

            result.Update(product.Name, product.Price);

            if (product.PromotionId is not null)
            {
                result.SetPromotion(product.PromotionId.Value);
            }
            else
            {
                result.ClearPromotion();
            }
        }

        public void Delete(Product product)
        {
            _existing.Remove(product);
        }

        public Task CommitAsync()
        {
            CommitCalledCount++;
            return Task.CompletedTask;
        }

        private static List<Product> GetFakeProducts()
        {
            var promotion = new List<Promotion>()
            {
                new Promotion().Create(new Guid("ba35b678-9289-45ce-a601-80f59cec908e"), "Buy 1 Get 1 Free", 0, Domain.Common.DiscountType.BuyOneGetOneFree, 2, 0),
                new Promotion().Create(new Guid("47133cb6-1135-4d05-b44a-bdfe41eb1b45"), "3 for 10 Euro", 1, Domain.Common.DiscountType.ThreeForTen, 3, 10)
            };

            return new List<Product>()
            {
                new Product().FullCreate(ProductId.Parse(new Guid("9a8ee6ff-7ed7-473d-9a82-f1e93f141f26")), "Product A", 20, 2, PromotionId.Parse(new Guid("ba35b678-9289-45ce-a601-80f59cec908e")), promotion.First()),
                new Product().FullCreate(ProductId.Parse(new Guid("faf6e8df-3c31-4cb4-b4ab-652ec39673fc")),"Product B", 4, 3, PromotionId.Parse(new Guid("47133cb6-1135-4d05-b44a-bdfe41eb1b45")), promotion.Last()),
                new Product().FullCreate(ProductId.Parse(new Guid("7ec7e402-eae7-442f-9a39-226724497905")),"Product C", 2, 5, null, null),
                new Product().FullCreate(ProductId.Parse(new Guid("6cf5aaee-cd8c-4b3a-8d79-cb3dccebdd8f")),"Product D", 4, 2, null, null)
            };
        }

        public DbConnection GetConnection()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
