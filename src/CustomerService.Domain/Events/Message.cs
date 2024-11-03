namespace CustomerService.Domain.Events
{
    public abstract class Message
    {
        protected Message() => MessageType = GetType().Name;
        public string MessageType { get; }
    }
}
