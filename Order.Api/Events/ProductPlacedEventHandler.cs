using MediatR;
using Order.Api.Integration;
using Order.Domain.AgregatesModel;

namespace Order.Api.Events
{
    public class ProductPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly IStockApiClient _stockApiClient;
        private readonly IPublisher _publisher;
        public ProductPlacedEventHandler(IStockApiClient stockApiClient, IPublisher publisher)
        {
            _stockApiClient = stockApiClient;
            _publisher = publisher;
        }

        public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            var acceptedProducts = new List<ProductOrderModel>();
            var commonStatus = true;

            foreach(var product in notification.Products)
            {
                var status = await _stockApiClient.OrderProductAsync(product.ProductId, product.Quantity);

                if (!status)
                {
                    commonStatus = false;
                    break;
                }                    
                
                acceptedProducts.Add(product);
            }

            if (!commonStatus)
                await _publisher.Publish(new OrderRejectedEvent()
                {
                    Id = notification.Id,
                    Status = "rejected",
                    TimeStamp = DateTime.Now,
                    Version = notification.Version,
                    Products = acceptedProducts.ToArray()
                });
        }
    }
}
