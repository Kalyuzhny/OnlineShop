namespace Order.Api.Integration
{
    public interface IStockApiClient
    {
        Task<bool> OrderProductAsync(string productId, int quantity);

        Task<bool> AddProductAsync(string productId, int quantity);
    }
}