using bruno.Klir.Domain.Aggregate;
using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Product.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace bruno.Klir.Infrastructure.Configurations
{
    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        protected readonly KlirBrunoContext _context;
        protected readonly DbSet<T> _entity;

        public IUnitOfWork UnitOfWork { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Repository(KlirBrunoContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public DbConnection GetConnection()
        {
            return _context.Database.GetDbConnection();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
