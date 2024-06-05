using bruno.Klir.Domain.Aggregate;
using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Product.ValueObjects;
using bruno.Klir.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace bruno.Klir.Infrastructure.Products
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly KlirBrunoContext _context;

        public ProductRepository(KlirBrunoContext context) 
        {
            _context = context; 
        }
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
            set => throw new NotImplementedException();
        }
        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<Product> GetByIdAsync(ProductId productId)
        {
            return await _context.Products.Include(x => x.Promotion).FirstOrDefaultAsync(x => x.Id.Equals(productId.Value));
        }
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(x => x.Promotion).ToListAsync();
        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
