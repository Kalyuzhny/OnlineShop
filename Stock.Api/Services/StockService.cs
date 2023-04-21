using Grpc.Core;
using MediatR;
using Stock.Api.Commands.Extensions;
using Stock.Domain.Exceptions;

namespace Stock.Api.Services
{
    public class StockService : Stock.StockBase
    {
        private readonly ILogger<StockService> _logger;
        private readonly IMediator _mediatr;
        public StockService(ILogger<StockService> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediatr = mediatr;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<ProductReply> GetProduct(ProductRequest request, ServerCallContext context)
        {
            //TODO
            return Task.FromResult(new ProductReply
            {
                Id = "someId",
                Quantity = 1
            });
        }

        public override async Task<AddProductReply> AddProduct(AddProductRequest request, ServerCallContext context)
        {
            try
            {
                var status = await _mediatr.Send(request.ToAddProductCommand());

                return new AddProductReply()
                {
                    Id = request.Id,
                    Status = status
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new AddProductReply()
                {
                    Id = request.Id,
                    Status = false,
                    Message = ex.Message
                };
            }
        }

        public override async Task<OrderProductReply> OrderProduct(OrderProductRequest request, ServerCallContext context)
        {
            try
            {
                var status = await _mediatr.Send(request.ToOrderProductCommand());

                return new OrderProductReply()
                {
                    Id = request.Id,
                    Status = status
                };
            }
            catch (NotEnoughQuantityException ex)
            {
                _logger.LogError(ex.Message);
                return new OrderProductReply()
                {
                    Id = request.Id,
                    Status = false,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new OrderProductReply()
                {
                    Id = request.Id,
                    Status = false,
                    Message = ex.Message
                };
            }
        }
    }
}