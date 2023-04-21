using Stock.Domain;
using Stock.Infrastructure;
using Stock.Infrastructure.Configuration;
using Stock.Infrastructure.Repository;

namespace Stock.Api.Injections
{
    public static class InfrastructureInjections
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
            services.AddSingleton<ProductDBContext>();

            services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
