using Order.Domain.AgregatesModel;

namespace Order.Domain
{
    public interface IOrderRepository
    {
        Task<bool> AddOrderEventAsync(OrderEventsModel orderEvent);
    }
}
