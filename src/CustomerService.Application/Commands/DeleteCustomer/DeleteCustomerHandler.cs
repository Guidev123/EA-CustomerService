using CustomerService.Domain.Repositories;
using EA.CommonLib.Messages;
using EA.CommonLib.Responses;
using MediatR;

namespace CustomerService.Application.Commands.DeleteCustomer
{
    public class DeleteCustomerHandler(ICustomerRepository customerRepository)
               : CommandHandler, IRequestHandler<DeleteCustomerCommand, Response<DeleteCustomerCommand>>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        public async Task<Response<DeleteCustomerCommand>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer is null)
            {
                AddError(request.ValidationResult!, "Customer not found");
                return new Response<DeleteCustomerCommand>(request, 400, GetAllErrors(request.ValidationResult!));
            }
            customer.SetAsDeleted();

            await _customerRepository.UpdateAsync(customer);

            return new Response<DeleteCustomerCommand>(request, 204, "Success");
        }
    }
}
