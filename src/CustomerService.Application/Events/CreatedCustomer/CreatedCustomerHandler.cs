using MediatR;

namespace CustomerService.Application.Events.CreatedCustomer
{
    public class CreatedCustomerHandler : INotificationHandler<CreatedCustomerEvent>
    {
        public Task Handle(CreatedCustomerEvent notification, CancellationToken cancellationToken)
        {
            // SEND A WELCOME EMAIL
            return Task.CompletedTask;
        }
    }
}
