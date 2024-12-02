using CustomerService.Domain.Repositories;
using EA.CommonLib.MessageBus;
using EA.CommonLib.MessageBus.Integration.DeleteCustomer;
using EA.CommonLib.Messages;
using FluentValidation.Results;
using MediatR;

namespace CustomerService.Application.Commands.DeleteCustomer
{
    public class DeleteCustomerHandler(ICustomerRepository customerRepository, IMessageBus bus)
               : CommandHandler, IRequestHandler<DeleteCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IMessageBus _bus = bus;
        public async Task<ValidationResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            request.ValidationResult = ValidationResult;

            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer is null)
            {
                AddError("Customer not found");
                return request.ValidationResult;
            }
            customer.SetAsDeleted();

            await _customerRepository.UpdateAsync(customer);

            return request.ValidationResult;
        }
    }
}
