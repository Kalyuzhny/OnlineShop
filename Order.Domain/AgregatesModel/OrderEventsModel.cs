namespace Order.Domain.AgregatesModel
{
    public class OrderEventsModel
    {
        public int Id { get; set; }
        public string Status { get; init; }

        public DateTime TimeStamp { get; init; }

        //Version is not handled by code
        public int Version { get; init; } = 0;

        public ProductOrderModel[] Products { get; init; }

    }
}
