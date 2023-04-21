using MediatR;

namespace Order.Api.Commands
{
    public class RegisterOrderCommand : IRequest<bool>
    {
        public int Id { get; private set; }
        public ProductOrder[] ProductOrders { get; private set; }

        public RegisterOrderCommand(int id, ProductOrder[] productOrders) { 
            Id = id;
            ProductOrders = productOrders;
        }
    }
}
