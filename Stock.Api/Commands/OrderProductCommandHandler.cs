using MediatR;
using Stock.Domain;
using Stock.Domain.AgregatesModel;

namespace Stock.Api.Commands
{
    public class OrderProductCommandHandler : IRequestHandler<OrderProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public OrderProductCommandHandler(IProductRepository productRepository) {
            _repository = productRepository;
        }
        public async Task<bool> Handle(OrderProductCommand request, CancellationToken cancellationToken)
        {
            var productModel = new ProductModel(request.Id, string.Empty, request.Quantity);

            var status = await _repository.OrderProductAsync(productModel);

            return status;
        }
    }
}
