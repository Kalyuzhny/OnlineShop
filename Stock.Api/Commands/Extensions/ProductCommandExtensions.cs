namespace Stock.Api.Commands.Extensions
{
    public static class ProductCommandExtensions
    {
        public static AddProductCommand ToAddProductCommand(this AddProductRequest productRequest)
        {
            return new AddProductCommand(productRequest.Id, productRequest.Name, productRequest.Quantity);
        }

        public static OrderProductCommand ToOrderProductCommand(this OrderProductRequest productRequest)
        {
            return new OrderProductCommand(productRequest.Id, productRequest.Quantity);
        }
    }
}
