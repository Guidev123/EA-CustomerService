using CustomerService.Application.Extensions;
using CustomerService.Domain.Repositories;
using EA.CommonLib.Helpers;
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
                AddError(request.ValidationResult!, ErrorMessages.CUSTOMER_NOT_FOUND.GetDescription());
                return new Response<DeleteCustomerCommand>(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult!));
            }
            customer.SetAsDeleted();
            _customerRepository.UpdateAsync(customer);

            var persistData = await _customerRepository.UnitOfWork.CommitAsync();
            if (!persistData)
            {
                AddError(request.ValidationResult!, ErrorMessages.FAIL_PERSIST_DATA.GetDescription());
                return new Response<DeleteCustomerCommand>(request, 400, ErrorMessages.ERROR.GetDescription(), GetAllErrors(request.ValidationResult!));
            }

            return new Response<DeleteCustomerCommand>(request, 204, ErrorMessages.SUCCESS.GetDescription());
        }
    }
}
