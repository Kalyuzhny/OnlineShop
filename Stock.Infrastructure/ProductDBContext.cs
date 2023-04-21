using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stock.Domain.AgregatesModel;
using Stock.Infrastructure.Configuration;

namespace Stock.Infrastructure
{
    public class ProductDBContext 
    {
        public readonly IMongoCollection<ProductModel> ProductCollection;

        public ProductDBContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            ProductCollection = database.GetCollection<ProductModel>(mongoDBSettings.Value.CollectionName);
        }
    }
}