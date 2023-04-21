using MediatR;

namespace Stock.Api.Commands
{
    public record AddProductCommand : IRequest<bool>
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public long Quantity { get; private set; }

        public AddProductCommand(string id, string name, long quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }        
    }
}
