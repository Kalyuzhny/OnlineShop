using MediatR;
using Order.Domain;
using Order.Domain.AgregatesModel;

namespace Order.Api.Events
{
    public class OrderRejectedEventHandler : INotificationHandler<OrderRejectedEvent>
    {
        private readonly IOrderRepository _repository;

        public OrderRejectedEventHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(OrderRejectedEvent notification, CancellationToken cancellationToken)
        {
            var model = new OrderEventsModel()
            {
                Id = notification.Id,
                Status = "rejected",
                TimeStamp = DateTime.Now,
                Version = notification.Version,
                Products = notification.Products
            };

            await _repository.AddOrderEventAsync(model);
        }
    }
}
