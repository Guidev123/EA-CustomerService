using CustomerService.Application.Commands.CreateCustomer;
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
                return new Response<DeleteCustomerCommand>(request, 400, "Error", GetAllErrors(request.ValidationResult!));
            }
            customer.SetAsDeleted();
            _customerRepository.UpdateAsync(customer);

            var persistData = await _customerRepository.UnitOfWork.CommitAsync();
            if (!persistData)
            {
                AddError(request.ValidationResult!, "Fail to persist data");
                return new Response<DeleteCustomerCommand>(request, 400, "Error", GetAllErrors(request.ValidationResult!));
            }

            return new Response<DeleteCustomerCommand>(request, 204, "Success");
        }
    }
}
