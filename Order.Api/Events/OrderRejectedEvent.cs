using MediatR;
using Order.Domain.AgregatesModel;

namespace Order.Api.Events
{
    public class OrderRejectedEvent : OrderEventsModel, INotification
    {
    }
}
