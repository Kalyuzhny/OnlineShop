namespace Order.Api.Commands
{
    public class ProductOrder
    {
        public string Id { get; private set; }
        public int Quantity { get; private set; }

        public ProductOrder(string id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}
