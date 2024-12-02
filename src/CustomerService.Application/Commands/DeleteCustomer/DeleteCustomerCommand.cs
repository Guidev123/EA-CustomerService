using EA.CommonLib.Messages;

namespace CustomerService.Application.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : Command
    {
        public DeleteCustomerCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; private set; }
    }
}
