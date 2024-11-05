using MediatR;

namespace CustomerService.Application.Messages
{
    public abstract class Event : Message, INotification
    {
        public Event() => Timestamp = DateTime.Now;
        public DateTime Timestamp { get; }
    }
}