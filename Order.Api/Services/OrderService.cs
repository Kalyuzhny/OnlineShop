using Grpc.Core;
using MediatR;
using Order.Api;
using Order.Api.Commands;

namespace Order.Api.Services
{
    public class OrderService : Order.OrderBase
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IMediator _mediatr;
        public OrderService(ILogger<OrderService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediatr = mediator;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public async override Task<PlaceOrderReply> PlaceOrder(PlaceOrderRequest request, ServerCallContext context)
        {
            try
            {
                var registerOrderCommand = new RegisterOrderCommand(request.Orderid,
                    request.Products.Select(t => new ProductOrder(t.ProductId, t.Quantity)).ToArray());

                await _mediatr.Send(registerOrderCommand);

                return new PlaceOrderReply
                {
                    Id = request.Orderid.ToString(),
                    Status = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return new PlaceOrderReply
                {
                    Id = request.Orderid.ToString(),
                    Message = ex.Message,
                    Status = false
                };
            }            
        }
    }
}