using Grpc.Core;

namespace Order.Api.Services
{
    public interface IOrderService
    {
        Task<PlaceOrderReply> PlaceOrder(PlaceOrderRequest request, ServerCallContext context);
        Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context);
    }
}