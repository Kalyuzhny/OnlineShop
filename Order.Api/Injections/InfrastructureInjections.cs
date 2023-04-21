using Order.Domain;
using Order.Infrastructure;
using Order.Infrastructure.Configuration;
using Order.Infrastructure.Repository;

namespace Order.Api.Injections
{
    public static class InfrastructureInjections
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
            services.Configure<IntegrationSettings>(builder.Configuration.GetSection("Integtation"));
            services.AddSingleton<OrderDBContext>();

            services.AddTransient<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
