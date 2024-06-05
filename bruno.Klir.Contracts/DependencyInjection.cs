using Microsoft.Extensions.DependencyInjection;

namespace bruno.Klir.Contracts
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContracts(this IServiceCollection services)
        {
            return services;
        }
    }
}
