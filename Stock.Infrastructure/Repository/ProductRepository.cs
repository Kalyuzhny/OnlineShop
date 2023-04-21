using MongoDB.Driver;
using Stock.Domain;
using Stock.Domain.AgregatesModel;
using Stock.Domain.Exceptions;

namespace Stock.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDBContext _dbContext;
        public ProductRepository(ProductDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(ProductRepository));
        }

        public async Task<bool> AddProductAsync(ProductModel product)
        {
            var filter = Builders<ProductModel>.Filter.Eq("Id", product.Id);

            var count = await _dbContext.ProductCollection.CountAsync(filter);
            if (count == 0)
            {
                await _dbContext.ProductCollection.InsertOneAsync(product);
                return true;
            }
            else
            {
                var productFromDb = await _dbContext.ProductCollection.Find(t => t.Id == product.Id).FirstAsync();
                productFromDb.AddProduct(product.Quantity);
                                
                var update = Builders<ProductModel>.Update.Set("Quantity", productFromDb.Quantity);

                await _dbContext.ProductCollection.FindOneAndUpdateAsync(filter, update);
            }

            return true;
        }

        public async Task<bool> OrderProductAsync(ProductModel product)
        {
            var filter = Builders<ProductModel>.Filter.Eq("Id", product.Id);

            var count = await _dbContext.ProductCollection.CountAsync(filter);
            if (count == 0)
            {
                throw new NotEnoughQuantityException("Product is not fount in DB");
            }
            else
            {
                var productFromDb = await _dbContext.ProductCollection.Find(t => t.Id == product.Id).FirstAsync();
                productFromDb.WithdrawProduct(product.Quantity);

                var update = Builders<ProductModel>.Update.Set("Quantity", productFromDb.Quantity);

                await _dbContext.ProductCollection.UpdateOneAsync(filter, update);
            }

            return true;
        }

        public async Task<ProductModel> GetProductAsync(string id)
        {
            return await _dbContext.ProductCollection.Find(t => t.Id == id).FirstAsync();
        }
    }
}
