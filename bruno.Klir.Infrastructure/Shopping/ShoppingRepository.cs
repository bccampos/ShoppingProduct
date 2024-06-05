using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Shopping;
using bruno.Klir.Domain.Shopping.ValueObjects;
using bruno.Klir.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace bruno.Klir.Infrastructure.Shopping
{
    public class ShoppingRepository : IShoppingGroupRepository
    {
        private readonly KlirBrunoContext _context;
        public ShoppingRepository(KlirBrunoContext context) 
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

        public void Add(ShoppingGroup item)
        {
            _context.Groups.AddAsync(item);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<List<ShoppingGroup>> GetAllAsync()
        {
            return await _context.Groups.Include(i => i.Items).ThenInclude(p => p.Product).ThenInclude(p => p.Promotion).ToListAsync();
        }

        public async Task<ShoppingGroup> GetByIdAsync(ShoppingGroupId id)
        {
            return await _context.Groups.Include(i => i.Items).ThenInclude(p => p.Product).ThenInclude(p => p.Promotion).FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public void Update(ShoppingGroup item)
        {
            _context.Groups.Update(item);
        }
    }
}
