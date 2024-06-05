using bruno.Klir.Domain.Common.Interfaces;
using bruno.Klir.Domain.Factories.Promotion.Type;
using bruno.Klir.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace bruno.Klir.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<IPromotionApplicator, BuyOneGetOneFree>();
            services.AddSingleton<IPromotionApplicator, ThreeForTen>();
            services.AddSingleton<IPromotionApplicator, FixedPrice>();
            services.AddSingleton<IPromotionApplicator, Percentual>();

            services.AddScoped<IShoppingService, ShoppingService>();

            return services;
        }

    }
}
