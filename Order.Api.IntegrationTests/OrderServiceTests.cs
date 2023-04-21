using Order.Api.Services;
using ProtoBuf.Grpc.Client;

namespace Order.Api.IntegrationTests
{
    public class OrderServiceTests
    {
        private IOrderService _orderService;

        public OrderServiceTests()
        {
            var testServerFixture = new TestServerFixture();
            var channel = testServerFixture.GrpcChannel;

            _orderService = channel.CreateGrpcService<IOrderService>();
        }

        [Test]
        public async Task OrderShouldFailWhenDBIsNotInitialized()
        {
            //Arrange
            var placeOrderRequest = new PlaceOrderRequest()
            {
                Orderid = 1
            };
            placeOrderRequest.Products.Add(new Product() 
            { 
                ProductId= "2",
                Quantity = 2
            });

            //Act
            var result = await _orderService.PlaceOrder(placeOrderRequest, null);

            //Assert
            Assert.False(result.Status);
        }
    }
}