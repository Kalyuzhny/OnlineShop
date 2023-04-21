using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Order.Domain.AgregatesModel;
using Order.Infrastructure.Configuration;

namespace Order.Infrastructure
{
    public class OrderDBContext 
    {
        public readonly IMongoCollection<OrderEventsModel> OrderEventsCollection;

        public OrderDBContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            OrderEventsCollection = database.GetCollection<OrderEventsModel>(mongoDBSettings.Value.CollectionName);
        }
    }
}