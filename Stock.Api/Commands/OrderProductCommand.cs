using MediatR;

namespace Stock.Api.Commands
{
    public record OrderProductCommand : IRequest<bool>
    {
        public string Id { get; private set; }
        public long Quantity { get; private set; }

        public OrderProductCommand(string id, long quantity)
        {
            Id = id;
            Quantity = quantity;
        }        
    }
}
