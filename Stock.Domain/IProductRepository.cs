using Stock.Domain.AgregatesModel;

namespace Stock.Domain
{
    public interface IProductRepository
    {
        Task<ProductModel> GetProductAsync(string id);

        Task<bool> AddProductAsync(ProductModel product);

        Task<bool> OrderProductAsync(ProductModel product);
    }
}