using MediatR;
using Stock.Domain;
using Stock.Domain.AgregatesModel;

namespace Stock.Api.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public AddProductCommandHandler(IProductRepository productRepository) {
            _repository = productRepository;
        }
        public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productModel = new ProductModel(request.Id, request.Name, request.Quantity);

            var status = await _repository.AddProductAsync(productModel);

            return status;
        }
    }
}
