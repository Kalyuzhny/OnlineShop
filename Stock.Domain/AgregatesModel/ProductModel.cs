using Stock.Domain.Exceptions;

namespace Stock.Domain.AgregatesModel
{
    public class ProductModel
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public long Quantity { get; private set; }

        public ProductModel(string id, string name, long quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }

        public void AddProduct(long orderQuantity)
        {
            Quantity += orderQuantity;
        }

        public void WithdrawProduct(long orderQuantity)
        {
            var newQuantity = Quantity - orderQuantity;

            if (newQuantity < 0)
                throw new NotEnoughQuantityException("There are not enough products in stock");

            Quantity = newQuantity;
        }
    }
}
