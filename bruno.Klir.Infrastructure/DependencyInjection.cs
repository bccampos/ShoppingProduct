using bruno.Klir.Domain.Common.Interfaces.Persistence;
using bruno.Klir.Infrastructure.Configurations;
using bruno.Klir.Infrastructure.Products;
using bruno.Klir.Infrastructure.Repositories;
using bruno.Klir.Infrastructure.Shopping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bruno.Klir.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IPromotionRepository, FakePromotionRepository>();

            services.AddScoped<IShoppingGroupRepository, ShoppingRepository>();

            services.AddDbContext<KlirBrunoContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("KlirBrunoDatabase"));
            });

            return services;
        }

    }
}
