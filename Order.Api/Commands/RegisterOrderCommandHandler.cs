using MediatR;
using Order.Api.Events;
using Order.Domain.AgregatesModel;

namespace Order.Api.Commands
{
    public class RegisterOrderCommandHandler : IRequestHandler<RegisterOrderCommand, bool>
    {
        private readonly IPublisher _publisher;
        public RegisterOrderCommandHandler(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task<bool> Handle(RegisterOrderCommand request, CancellationToken cancellationToken)
        {
            var orderPlacedEvent = new OrderPlacedEvent()
            {
                Id = request.Id,
                Status = "created",
                TimeStamp = DateTime.Now,
                Version = 0,
                Products = request.ProductOrders.Select(t => new ProductOrderModel()
                {
                    ProductId = t.Id,
                    Quantity = t.Quantity
                }).ToArray()
            };

            await _publisher.Publish(orderPlacedEvent);

            return true;
        }
    }
}
