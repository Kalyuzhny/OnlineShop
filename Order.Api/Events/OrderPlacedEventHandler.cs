using MediatR;
using Order.Domain;
using Order.Domain.AgregatesModel;

namespace Order.Api.Events
{
    public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly IOrderRepository _repository;
        private readonly IPublisher _publisher;
        private readonly ILogger<OrderPlacedEventHandler> _logger;

        public OrderPlacedEventHandler(IOrderRepository repository, IPublisher publisher, ILogger<OrderPlacedEventHandler> logger)
        {
            _repository = repository;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            var model = new OrderEventsModel()
            {
                Id = notification.Id,
                Status = "created",
                TimeStamp = DateTime.Now,
                Version = notification.Version,
                Products = notification.Products
            };

            try
            {

                await _repository.AddOrderEventAsync(model);
            }
            catch (Exception)
            {
                var rejectedEvent = new OrderRejectedEvent()
                {
                    Id = notification.Id,
                    Status = "rejected",
                    TimeStamp = DateTime.Now,
                    Version = notification.Version,
                    Products = notification.Products
                };

                await _publisher.Publish(rejectedEvent,
                                         cancellationToken);

                throw;
            }
        }
    }
}
