using MongoDB.Bson;
using MongoDB.Driver;
using Order.Domain;
using Order.Domain.AgregatesModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace Order.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDBContext _dbContext;
        public OrderRepository(OrderDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(OrderRepository));
        }

        public async Task<bool> AddOrderEventAsync(OrderEventsModel orderEvent)
        {
            var nextId = getNextSequence(orderEvent.Id);

            orderEvent.Id = nextId;
            await _dbContext.OrderEventsCollection.InsertOneAsync(orderEvent);

            return true;
        }

        public int getNextSequence(int id)
        {
            var filter = Builders<OrderEventsModel>.Filter.And(
            Builders<OrderEventsModel>.Filter.Eq("_id", id));
            var updates = Builders<OrderEventsModel>.Update.Inc("_id", 1);

            var ret = _dbContext.OrderEventsCollection.FindOneAndUpdateAsync(filter, updates);

            return ret.Id;
        }
    }
}
