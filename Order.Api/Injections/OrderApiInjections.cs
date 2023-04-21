using Order.Api.Integration;

namespace Order.Api.Injections
{
    public static class OrderApiInjections
    {
        public static IServiceCollection AddApiDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStockApiClient, StockApiClient>();

            return services;
        }
    }
}
