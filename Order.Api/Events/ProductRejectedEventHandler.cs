using MediatR;
using Order.Api.Integration;

namespace Order.Api.Events
{
    public class ProductRejectedEventHandler : INotificationHandler<OrderRejectedEvent>
    {
        private readonly IStockApiClient _stockApiClient;
        public ProductRejectedEventHandler(IStockApiClient stockApiClient)
        {
            _stockApiClient = stockApiClient;
        }

        public async Task Handle(OrderRejectedEvent notification, CancellationToken cancellationToken)
        {
            foreach(var product in notification.Products)
            {
                await _stockApiClient.AddProductAsync(product.ProductId, product.Quantity);
            }
        }
    }
}
