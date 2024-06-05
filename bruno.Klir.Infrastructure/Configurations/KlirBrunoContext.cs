using bruno.Klir.Domain.Aggregate;
using bruno.Klir.Domain.Models;
using bruno.Klir.Domain.Product.Entities;
using bruno.Klir.Domain.Shopping;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace bruno.Klir.Infrastructure.Configurations
{
    public class KlirBrunoContext : DbContext, IUnitOfWork
    {
        public KlirBrunoContext(DbContextOptions<KlirBrunoContext> options)
         : base(options) 
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<ShoppingItem> Items { get; set; }
        public DbSet<ShoppingGroup> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
