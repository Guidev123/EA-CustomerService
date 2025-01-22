using SharedLib.Domain.Messages;

namespace CustomerService.Application.Commands.Delete
{
    public class DeleteCustomerCommand : Command<DeleteCustomerCommand>
    {
        public DeleteCustomerCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; private set; }
    }
}
